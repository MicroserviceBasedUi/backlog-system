using System.ComponentModel.DataAnnotations;

namespace JiraAdapter
{
    public class UpdateRankRequest
    {
        [Required]
        public string RankBeforeIssue { get; set; }
    }
}