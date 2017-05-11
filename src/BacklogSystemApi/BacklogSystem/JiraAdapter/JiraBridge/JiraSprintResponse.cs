using System.Collections.Generic;

namespace BacklogSystem.JiraAdapter.JiraBridge
{
	public class JiraSprintResponse
	{
		public int StartAt { get; set; }

		public int MaxResults { get; set; }

		public int Total { get; set; }

		public IEnumerable<JiraSprint> Values { get; set; }
	}

}
