using System.Collections.Generic;

namespace JiraAdapter.JiraBridge
{
    public class JiraResponse 
    {
        public int StartAt { get; set; }

        public int MaxResults { get; set; }

        public int Total { get; set; }

        public IEnumerable<JiraIssue> Issues { get; set; }
    }
}