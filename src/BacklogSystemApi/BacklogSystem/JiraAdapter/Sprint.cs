using System;
using System.Collections.Generic;

namespace JiraAdapter
{
	public class Sprint
	{
		public string Name { get; set; }
		public DateTime StartedAt { get; set; }
		public DateTime CompletedAt { get; set; }
		public IEnumerable<Story> Stories { get; set; }
	}
}