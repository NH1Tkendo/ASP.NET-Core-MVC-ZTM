using System;
using System.ComponentModel.DataAnnotations;

namespace EntityModel
{
    public class BlogMetadata
    {
        [Required(ErrorMessage = "Tên không được để trống")]
        [StringLength(50, ErrorMessage = "Tên không vượt quá 50 ký tự")]
        [Display(Name = "Tên")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Mô tả không được để trống")]
        [StringLength(500, ErrorMessage = "Mô tả không vượt quá 500 ký tự")]
        [Display(Name = "Mô tả")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Người sở hữu không được để trống")]
        [StringLength(50, ErrorMessage = "Người sở hữu không vượt quá 50 ký tự")]
        [Display(Name = "Người sở hữu")]
        public string Owner { get; set; }

        [Required(ErrorMessage = "Thứ hạng không được để trống")]
        [Range(1, 100, ErrorMessage = "Thứ hạng phải từ 1 đến 100")]
        [Display(Name = "Thứ hạng")]
        public int Rank { get; set; }
    }

    public class PostMetadata
    {
        [StringLength(100)]
        [Display(Name = "Tiêu đề")]
        public string Title { get; set; }

        [Display(Name = "Nội dung")]
        public string Content { get; set; }

        [Display(Name = "Ngày tạo")]
        [DataType(DataType.DateTime)]
        public DateTime? CreatedDate { get; set; }
    }
}
