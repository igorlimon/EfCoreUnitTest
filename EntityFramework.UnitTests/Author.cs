using System.Collections;
using System.Collections.Generic;

namespace EntityFramework.UnitTests
{
    public class Author
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Blog> Blogs { get; set; }
    }
}