using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFirstToNewDatabase
{
    public class Blog
    {
        public int BlogId { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        public string Name { get; set; }

        public virtual List<Post> Posts
        {
            get; set;
        }
    }

}
