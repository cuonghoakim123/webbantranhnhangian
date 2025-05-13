using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using WebBanTranhDanGian.Models;

namespace WebBanTranhDanGian.Controllers
{
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrderController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Order/Checkout
        public IActionResult Checkout()
        {
            var cartItems = GetCartItems();
            if (cartItems == null || cartItems.Count == 0)
            {
                TempData["ErrorMessage"] = "Giỏ hàng của bạn đang trống. Vui lòng thêm sản phẩm vào giỏ hàng trước khi thanh toán.";
                return RedirectToAction("Index", "Cart");
            }

            // Populate the checkout form with user information if logged in
            // This is a simplified version without authentication
            var order = new Order
            {
                OrderDate = DateTime.Now,
                Status = "Pending",
                PaymentMethod = "COD",
                IsPaid = false
            };

            return View(order);
        }

        // POST: Order/Checkout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Checkout(Order order)
        {
            if (ModelState.IsValid)
            {
                // Get cart items
                var cartItems = GetCartItems();
                if (cartItems == null || cartItems.Count == 0)
                {
                    TempData["ErrorMessage"] = "Giỏ hàng của bạn đang trống. Vui lòng thêm sản phẩm vào giỏ hàng trước khi thanh toán.";
                    return RedirectToAction("Index", "Cart");
                }

                // Get product details
                var productIds = cartItems.Select(i => i.ProductId).ToList();
                var products = await _context.Products
                    .Where(p => productIds.Contains(p.ProductId))
                    .ToListAsync();

                // Calculate total amount
                decimal totalAmount = 0;
                foreach (var item in cartItems)
                {
                    var product = products.FirstOrDefault(p => p.ProductId == item.ProductId);
                    if (product != null)
                    {
                        totalAmount += item.Quantity * product.Price;
                    }
                }

                // Complete order details
                order.OrderNumber = GenerateOrderNumber();
                order.OrderDate = DateTime.Now;
                order.TotalAmount = totalAmount;
                order.Status = "Pending";

                _context.Orders.Add(order);
                await _context.SaveChangesAsync();

                // Create order details
                foreach (var item in cartItems)
                {
                    var product = products.FirstOrDefault(p => p.ProductId == item.ProductId);
                    if (product != null)
                    {
                        var orderDetail = new OrderDetail
                        {
                            OrderId = order.OrderId,
                            ProductId = product.ProductId,
                            Quantity = item.Quantity,
                            UnitPrice = product.Price
                        };

                        _context.OrderDetails.Add(orderDetail);
                    }
                }

                await _context.SaveChangesAsync();

                // Clear the cart
                HttpContext.Session.Remove("Cart");

                return RedirectToAction(nameof(Confirmation), new { id = order.OrderId });
            }

            return View(order);
        }

        // GET: Order/Confirmation/5
        public async Task<IActionResult> Confirmation(int id)
        {
            var order = await _context.Orders
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Product)
                .FirstOrDefaultAsync(o => o.OrderId == id);

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // Helper methods
        private string GenerateOrderNumber()
        {
            return "DG" + DateTime.Now.ToString("yyyyMMddHHmmss");
        }

        // Helper method to get cart items
        private List<CartItem> GetCartItems()
        {
            var cartJson = HttpContext.Session.GetString("Cart");
            return string.IsNullOrEmpty(cartJson)
                ? new List<CartItem>()
                : System.Text.Json.JsonSerializer.Deserialize<List<CartItem>>(cartJson) ?? new List<CartItem>();
        }
    }

    // CartItem is already defined in CartController.cs
} 