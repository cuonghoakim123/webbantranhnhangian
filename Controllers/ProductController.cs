using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebBanTranhDanGian.Models;

namespace WebBanTranhDanGian.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Product
        public async Task<IActionResult> Index(int? categoryId, string searchString, int page = 1)
        {
            int pageSize = 12;
            var products = _context.Products
                .Where(p => p.IsActive)
                .Include(p => p.Category)
                .AsQueryable();

            ViewBag.Categories = await _context.Categories.ToListAsync();
            
            // Fix image paths for categories
            foreach (var category in ViewBag.Categories)
            {
                if (!string.IsNullOrEmpty(category.ImageUrl) && category.ImageUrl.EndsWith(".jpg"))
                {
                    category.ImageUrl = category.ImageUrl.Replace(".jpg", ".svg");
                }
            }
            
            ViewBag.CurrentCategory = categoryId;
            ViewBag.SearchString = searchString;

            if (categoryId.HasValue)
            {
                products = products.Where(p => p.CategoryId == categoryId);
            }

            if (!string.IsNullOrEmpty(searchString))
            {
                products = products.Where(p => p.Name.Contains(searchString) || 
                                             p.Description.Contains(searchString) ||
                                             p.Category.Name.Contains(searchString));
            }

            var totalItems = await products.CountAsync();
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = page;

            var productList = await products
                .OrderByDescending(p => p.CreatedDate)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
                
            // Fix image paths for products
            foreach (var product in productList)
            {
                if (!string.IsNullOrEmpty(product.ImageUrl) && product.ImageUrl.EndsWith(".jpg"))
                {
                    product.ImageUrl = product.ImageUrl.Replace(".jpg", ".svg");
                }
            }

            return View(productList);
        }

        // GET: Product/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.ProductId == id);

            if (product == null)
            {
                return NotFound();
            }
            
            // Fix image path for the product
            if (!string.IsNullOrEmpty(product.ImageUrl) && product.ImageUrl.EndsWith(".jpg"))
            {
                product.ImageUrl = product.ImageUrl.Replace(".jpg", ".svg");
            }

            // Get related products from the same category
            var relatedProducts = await _context.Products
                .Where(p => p.CategoryId == product.CategoryId && p.ProductId != product.ProductId && p.IsActive)
                .Take(4)
                .ToListAsync();
                
            // Fix image paths for related products
            foreach (var relatedProduct in relatedProducts)
            {
                if (!string.IsNullOrEmpty(relatedProduct.ImageUrl) && relatedProduct.ImageUrl.EndsWith(".jpg"))
                {
                    relatedProduct.ImageUrl = relatedProduct.ImageUrl.Replace(".jpg", ".svg");
                }
            }

            ViewBag.RelatedProducts = relatedProducts;

            return View(product);
        }
    }
} 