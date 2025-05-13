using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using WebBanTranhDanGian.Models;

namespace WebBanTranhDanGian.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CartController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Cart
        public async Task<IActionResult> Index()
        {
            var cartItems = GetCartItems();
            if (cartItems == null || cartItems.Count == 0)
            {
                ViewBag.Message = "Giỏ hàng của bạn đang trống.";
                return View(new List<CartItem>());
            }

            var productIds = cartItems.Select(i => i.ProductId).ToList();
            var products = await _context.Products
                .Where(p => productIds.Contains(p.ProductId))
                .ToListAsync();

            foreach (var item in cartItems)
            {
                var product = products.FirstOrDefault(p => p.ProductId == item.ProductId);
                if (product != null)
                {
                    item.Product = product;
                    item.Total = item.Quantity * product.Price;
                }
            }

            return View(cartItems);
        }

        // POST: Cart/AddToCart
        [HttpPost]
        public async Task<IActionResult> AddToCart(int productId, int quantity = 1)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null)
            {
                return NotFound();
            }

            var cartItems = GetCartItems();
            var existingItem = cartItems.FirstOrDefault(i => i.ProductId == productId);

            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                cartItems.Add(new CartItem
                {
                    ProductId = productId,
                    Quantity = quantity
                });
            }

            SaveCartItems(cartItems);

            return RedirectToAction(nameof(Index));
        }

        // POST: Cart/UpdateQuantity
        [HttpPost]
        public IActionResult UpdateQuantity(int productId, int quantity)
        {
            if (quantity <= 0)
            {
                return RedirectToAction(nameof(RemoveItem), new { productId });
            }

            var cartItems = GetCartItems();
            var item = cartItems.FirstOrDefault(i => i.ProductId == productId);

            if (item != null)
            {
                item.Quantity = quantity;
                SaveCartItems(cartItems);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Cart/RemoveItem/5
        public IActionResult RemoveItem(int productId)
        {
            var cartItems = GetCartItems();
            var item = cartItems.FirstOrDefault(i => i.ProductId == productId);

            if (item != null)
            {
                cartItems.Remove(item);
                SaveCartItems(cartItems);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Cart/ClearCart
        public IActionResult ClearCart()
        {
            HttpContext.Session.Remove("Cart");
            return RedirectToAction(nameof(Index));
        }

        // Helper methods
        private List<CartItem> GetCartItems()
        {
            var cartJson = HttpContext.Session.GetString("Cart");
            return string.IsNullOrEmpty(cartJson)
                ? new List<CartItem>()
                : JsonSerializer.Deserialize<List<CartItem>>(cartJson) ?? new List<CartItem>();
        }

        private void SaveCartItems(List<CartItem> cartItems)
        {
            var cartJson = JsonSerializer.Serialize(cartItems);
            HttpContext.Session.SetString("Cart", cartJson);
        }
    }

    // Helper class for cart
    public class CartItem
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Total { get; set; }
        public Product Product { get; set; }
    }
} 