namespace JiraAdapter
{
    public class ProductBacklogItem 
    {
        public string Id { get; set; }

        public string Summary { get; set; }

        public int? StoryPoints { get; set; }

        public Priority Priority { get; set; }
    }
}