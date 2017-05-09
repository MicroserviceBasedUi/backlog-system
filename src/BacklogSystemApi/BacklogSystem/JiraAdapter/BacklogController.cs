using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace JiraAdapter {

    [Route("api/backlog")]
    public class BacklogController : Controller {

        private const string ProjectName = "AP";

        private const string StoryIssueType = "story";

        private static readonly string[] ExcludedStates = new[] { "done" };

        private readonly JiraConfiguration configuration;

        public BacklogController(JiraConfiguration configuration){
            this.configuration = configuration;
        }

        /// <summary>
        /// Returns the configuration that is used to access Jira.
        /// </summary>
        [HttpGet("config")]
        public IActionResult GetConfig() {
            return this.Ok(new
            {
                User = this.configuration.JiraUser,
                Password = this.configuration.JiraPassword,
                BaseUrl = this.configuration.BaseUrl
            });
        }

        /// <summary>
        /// Provides the details of the requested issue.
        /// </summary>
        /// <remarks>
        /// Note that the issue id is a string and not an integer or a GUID.
        ///
        ///     e. g. AP-1
        ///
        /// </remarks>
        /// <param name="issueId">The ID of the issue.</param>
        [HttpGet("issue/{issueId}")]
        public async Task<IActionResult> Get(string issueId)
        {
            this.Response.ContentType = "application/json";

            return this.Ok(await this.SendGetRequest($"issue/{issueId}"));
        }

        /// <summary>
        /// Provides a list of remaining stories in the backlog.
        /// </summary>
        [HttpGet("remaining")]
        public async Task<IActionResult> Remaining([FromQuery] PagedQuery pagedQuery = null)
        {
            pagedQuery = pagedQuery ?? new PagedQuery();

            this.Response.ContentType = "application/json";

            var query = new {
                jql = $"project = {ProjectName} AND issuetype = {StoryIssueType} AND status NOT IN ({string.Join(", ", ExcludedStates)})",
                startAt = pagedQuery.StartAt,
                maxResults = pagedQuery.MaxResults,
                fields = new [] 
                {
                    "summary",
                    "status",
                    "customfield_10006", // Sprint
                    "customfield_10004" // Story Points
                }
            };

            return this.Ok(await this.SendPostRequest("search", query));
        }

		        /// <summary>
        /// Provides a list of remaining stories in the backlog.
        /// </summary>
        [HttpGet("closed")]
        public async Task<IActionResult> Closed([FromQuery] PagedQuery pagedQuery = null)
        {
            pagedQuery = pagedQuery ?? new PagedQuery();

            this.Response.ContentType = "application/json";

            var query = new {
                jql = $"project = {ProjectName} AND issuetype = {StoryIssueType} AND status IN ({string.Join(", ", ExcludedStates)})",
                startAt = pagedQuery.StartAt,
                maxResults = pagedQuery.MaxResults,
                fields = new [] 
                {
                    "summary",
                    "status",
                    "customfield_10006", // Sprint
                    "customfield_10004" // Story Points
                }
            };

            return this.Ok(await this.SendPostRequest("search", query));
        }

        /// <summary>
        /// Provides a powerful and flexible way to query Jira.
        /// The given query will directly be passed onto Jira.
        /// </summary>
        [HttpPost("search")]
        public async Task<IActionResult> Search([FromBody] JiraQuery query) {
            this.Response.ContentType = "application/json";

            return this.Ok(await this.SendPostRequest("search", query));
        }

        /// <summary>
        /// Queries the versions of the project.
        /// </summary>
        [HttpGet("releases")]
		public async Task<IActionResult> Releases([FromQuery] PagedQuery query) {
			return this.Ok(this.SendGetRequest($"project/{ProjectName}/versions?expand"));
		}

        private async Task<string> SendPostRequest(string url, object payload)
        {

            var stringContent = JsonConvert.SerializeObject(payload, Formatting.Indented, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });

            var content = new StringContent(stringContent);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = this.SendRequest(client => client.PostAsync($"{this.configuration.BaseUrl}/{url}", content));

            return await response.Content.ReadAsStringAsync();
        }

        private async Task<string> SendGetRequest(string url)
        {
            return await this.SendRequest(client => client.GetAsync($"{this.configuration.BaseUrl}/{url}")).Content.ReadAsStringAsync();
        }

        private HttpResponseMessage SendRequest(Func<HttpClient, Task<HttpResponseMessage>> request)
        {
            using (var client = new HttpClient())
            {
                var basicAuthString = Encoding.UTF8.GetBytes($"{this.configuration.JiraUser}:{this.configuration.JiraPassword}");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(basicAuthString));

                return request(client).Result;
            }
        }
    }
}
