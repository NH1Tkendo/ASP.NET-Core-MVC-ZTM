using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Bulky.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("Tên catogory")]
        [MaxLength(30)]
        public string Name { get; set; }
        [DisplayName("Thứ tự hiển thị")]
        [Range(1, 100)]
        public int DisplayOrder { get; set; }
    }
}
