namespace JiraAdapter 
{
    /// <summary>
    /// The query passed to the service.
    /// 
    /// For full documentation, please refer to the Jira Api documentation: https://docs.atlassian.com/jira/REST/cloud/#api/2/search-searchUsingSearchRequest
    /// </summary>
    public class JiraQuery : PagedQuery
    {
        public string Jql { get; set; }

        public string[] Fields { get; set; }
    }
}