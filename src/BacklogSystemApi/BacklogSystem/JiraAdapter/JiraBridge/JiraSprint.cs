using System;

namespace BacklogSystem.JiraAdapter.JiraBridge
{
	public class JiraSprint
	{
		public int Id { get; set; }

		public Uri Self { get; set; }

		public string State { get; set; }

		public string Name { get; set; }

		public DateTime StartDate { get; set; }

		public DateTime EndDate { get; set; }

		public DateTime CompleteDate { get; set; }

		public int OriginBoardId { get; set; }

		public string Goal { get; set; }
	}

}
