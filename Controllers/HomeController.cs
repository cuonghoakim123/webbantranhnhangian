using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WebBanTranhDanGian.Models;

namespace WebBanTranhDanGian.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var featuredProducts = await _context.Products
                .Where(p => p.IsActive && p.IsFeatured)
                .Include(p => p.Category)
                .Take(8)
                .ToListAsync();

            // Fix image paths to use SVG files instead of JPG
            foreach (var product in featuredProducts)
            {
                if (!string.IsNullOrEmpty(product.ImageUrl) && product.ImageUrl.EndsWith(".jpg"))
                {
                    product.ImageUrl = product.ImageUrl.Replace(".jpg", ".svg");
                }
            }

            var categories = await _context.Categories.ToListAsync();
            
            // Fix image paths to use SVG files instead of JPG
            foreach (var category in categories)
            {
                if (!string.IsNullOrEmpty(category.ImageUrl) && category.ImageUrl.EndsWith(".jpg"))
                {
                    category.ImageUrl = category.ImageUrl.Replace(".jpg", ".svg");
                }
            }

            ViewBag.Categories = categories;

            return View(featuredProducts);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

    public class ErrorViewModel
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
