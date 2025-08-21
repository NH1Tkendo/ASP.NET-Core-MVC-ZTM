using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Categories
    {
        [Key]
        public int CategoryId { get; set; }
        [Required]
        [DisplayName("Tên catogory")]
        [MaxLength(30)]
        public string CategoryName { get; set; }
        [DisplayName("Chú thích")]
        [MaxLength(100)]
        public string Description { get; set; }

        public virtual ICollection<Books> Books { get; set; }
    }
}
