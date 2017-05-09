namespace JiraAdapter 
{
    public class PagedQuery 
    {
        public int StartAt { get; set; } = 0;

        public int MaxResults { get; set; } = 100;
    }
}