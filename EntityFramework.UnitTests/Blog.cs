namespace EntityFramework.UnitTests
{
    public class Blog
    {
        public int BlogId { get; set; }
        public string Url { get; set; }

        public int AuthorId { get; set; }
        public Author Author { get; set; }
    }
}