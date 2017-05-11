namespace JiraAdapter
{
	public class Story
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public int StoryPoints { get; set; }
		public string Status { get; set; }
		public int Priority { get; set; } // not used
	}
}