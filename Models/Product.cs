using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebBanTranhDanGian.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Tên tranh không được để trống")]
        [Display(Name = "Tên tranh")]
        [StringLength(200)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Mô tả không được để trống")]
        [Display(Name = "Mô tả")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Giá bán không được để trống")]
        [Display(Name = "Giá bán")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Display(Name = "Giá khuyến mãi")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal? DiscountPrice { get; set; }

        [Display(Name = "Hình ảnh")]
        public string ImageUrl { get; set; }

        [Display(Name = "Kích thước")]
        public string Size { get; set; }

        [Display(Name = "Nghệ nhân")]
        public string Artist { get; set; }

        [Display(Name = "Xuất xứ")]
        public string Origin { get; set; }

        [Display(Name = "Số lượng trong kho")]
        public int StockQuantity { get; set; }

        [Display(Name = "Ngày tạo")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [Display(Name = "Có hiển thị")]
        public bool IsActive { get; set; } = true;

        [Display(Name = "Sản phẩm nổi bật")]
        public bool IsFeatured { get; set; } = false;

        // Foreign keys
        public int CategoryId { get; set; }

        // Navigation properties
        public virtual Category Category { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual ICollection<ShoppingCart> ShoppingCarts { get; set; }
    }
} 