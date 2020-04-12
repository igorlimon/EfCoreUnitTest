using System.Linq;
using EntityFramework.UnitTests;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace XUnitTestProject1.InMemory
{
    public class BlogServiceTests
    {
        [Fact]
        public void Add_writes_to_database()
        {
            var options = new DbContextOptionsBuilder<BloggingContext>()
                .UseInMemoryDatabase(databaseName: "Add_writes_to_database")
                .Options;

            // Run the test against one instance of the context
            using (var context = new BloggingContext(options))
            {
                context.Authors.Add(new Author() { Id = 11, Name = "Ion" });
                var service = new BlogService(context);
                service.Add("https://example.com");
                context.SaveChanges();
            }

            // Use a separate instance of the context to verify correct data was saved to database
            using (var context = new BloggingContext(options))
            {
                Assert.Equal(1, context.Blogs.Count());
                Assert.Equal("https://example.com", context.Blogs.Single().Url);
                Assert.Equal(1, context.Authors.Count());
            }
        }

        [Fact]
        public void Find_searches_url()
        {
            var options = new DbContextOptionsBuilder<BloggingContext>()
                .UseInMemoryDatabase(databaseName: "Find_searches_url")
                .Options;

            // Insert seed data into the database using one instance of the context
            using (var context = new BloggingContext(options))
            {
                context.Blogs.Add(new Blog { Url = "https://example.com/cats" });
                context.Blogs.Add(new Blog { Url = "https://example.com/catfish" });
                context.Blogs.Add(new Blog { Url = "https://example.com/dogs" });
                context.SaveChanges();
            }

            // Use a clean instance of the context to run the test
            using (var context = new BloggingContext(options))
            {
                var service = new BlogService(context);
                var result = service.Find("cat");
                Assert.Equal(2, result.Count());
            }
        }
    }
}
