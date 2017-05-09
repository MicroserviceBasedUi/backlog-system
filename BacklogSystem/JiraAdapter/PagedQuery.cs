using System.ComponentModel;

namespace JiraAdapter 
{
    public class PagedQuery 
    {
        [DefaultValue(0)]
        public int StartAt { get; set; } = 0;

        [DefaultValue(100)]
        public int MaxResults { get; set; } = 100;
    }
}