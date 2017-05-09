namespace JiraAdapter 
{
    public class JiraQuery : PagedQuery
    {
        public string Jql { get; set; }

        public string[] Fields { get; set; }
    }
}