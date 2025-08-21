using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Books
    {
        [Key]
        public int BookId { get; set; }
        [Required]
        [DisplayName("Tên sách")]
        [MaxLength(30)]
        public string Title { get; set; }
        [Required]
        [DisplayName("Tên tác giả")]
        [MaxLength(30)]
        public string Author { get; set; }
        public string ISBN { get; set;}
        [DisplayName("Năm xuất bản")]
        public int PublishedYear { get; set; }

        [DisplayName("Thể loại")]
        public int CategoryID { get; set; }   // FK

        // Navigation property
        public virtual Categories Category { get; set; }


    }
}
