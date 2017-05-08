using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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

        [HttpGet]
        [Route("config")]
        public IActionResult GetConfig() {
            return this.Ok(new 
            { 
                User = this.configuration.JiraUser,
                Password = this.configuration.JiraPassword,
                BaseUrl = this.configuration.BaseUrl
            });
        }

        [HttpGet]
        [Route("issue/{issueId}")]
        public async Task<IActionResult> Get(string issueId)
        {
            this.Response.ContentType = "application/json";

            return this.Ok(await this.SendGetRequest($"issue/{issueId}"));
        }   

        [HttpGet]
        [Route("remaining")]
        public async Task<IActionResult> Remaining([FromQuery] PagingInfo pagingInfo = null)
        {
            pagingInfo = pagingInfo ?? new PagingInfo();

            this.Response.ContentType = "application/json";

            var query = new {
                jql = $"project = {ProjectName} AND issuetype = {StoryIssueType} AND status NOT IN ({string.Join(", ", ExcludedStates)})",
                startAt = pagingInfo.StartAt,
                maxResults = pagingInfo.MaxPageSize,
                fields = new [] 
                {
                    "summary",
                    "status"
                }
            };

            return this.Ok(await this.SendPostRequest($"search", query));
        }

        private async Task<string> SendPostRequest(string url, object payload)
        {

            var stringContent = JsonConvert.SerializeObject(payload);

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