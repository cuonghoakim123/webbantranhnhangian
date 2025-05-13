using System;
using System.ComponentModel.DataAnnotations;

namespace WebBanTranhDanGian.Models
{
    public class ShoppingCart
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }

        [Required]
        [Display(Name = "Số lượng")]
        public int Quantity { get; set; }

        [Display(Name = "Ngày thêm vào giỏ")]
        public DateTime DateCreated { get; set; } = DateTime.Now;

        // Navigation properties
        public virtual User User { get; set; }
        public virtual Product Product { get; set; }
    }
} 