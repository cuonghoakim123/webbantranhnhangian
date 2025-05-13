using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebBanTranhDanGian.Models
{
    public class Category
    {
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Tên danh mục không được để trống")]
        [Display(Name = "Tên danh mục")]
        [StringLength(100)]
        public string Name { get; set; }

        [Display(Name = "Mô tả")]
        public string Description { get; set; }

        [Display(Name = "Hình ảnh")]
        public string ImageUrl { get; set; }

        // Navigation property
        public virtual ICollection<Product> Products { get; set; }
    }
} 