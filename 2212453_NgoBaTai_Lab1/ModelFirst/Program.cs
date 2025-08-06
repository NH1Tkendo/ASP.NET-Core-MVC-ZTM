using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelFirst
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var blog = new Blog { Name = ”abc” };
            db.Blogs.Add(blog);
            db.SaveChanges();
        }
    }
}
