using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using WebBanTranhDanGian.Models;

namespace WebBanTranhDanGian
{
    public static class DbInitializer
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                // Create the database if it doesn't exist
                context.Database.EnsureCreated();

                // Check if the database has been seeded
                if (context.Categories.Any())
                {
                    return; // DB has been seeded
                }

                // Add categories
                var categories = new Category[]
                {
                    new Category
                    {
                        Name = "Tranh Đông Hồ",
                        Description = "Tranh dân gian Đông Hồ là một loại tranh dân gian truyền thống của Việt Nam, được sản xuất tại làng Đông Hồ, xã Song Hồ, huyện Thuận Thành, tỉnh Bắc Ninh.",
                        ImageUrl = "/images/categories/donghopaintings.jpg"
                    },
                    new Category
                    {
                        Name = "Tranh Hàng Trống",
                        Description = "Tranh Hàng Trống là loại tranh dân gian được sản xuất tại phố Hàng Trống, Hà Nội, Việt Nam.",
                        ImageUrl = "/images/categories/hangtrongpaintings.jpg"
                    },
                    new Category
                    {
                        Name = "Tranh Kim Hoàng",
                        Description = "Tranh Kim Hoàng là một dòng tranh dân gian có nguồn gốc từ làng Kim Hoàng, xã Vân Canh, huyện Hoài Đức, thành phố Hà Nội.",
                        ImageUrl = "/images/categories/kimhoangpaintings.jpg"
                    },
                    new Category
                    {
                        Name = "Tranh Làng Sình",
                        Description = "Tranh làng Sình là một loại tranh dân gian truyền thống, có nguồn gốc từ làng Sình, xã Phú Mậu, thành phố Huế, tỉnh Thừa Thiên Huế.",
                        ImageUrl = "/images/categories/langsinhpaintings.jpg"
                    }
                };

                context.Categories.AddRange(categories);
                context.SaveChanges();

                // Add products
                var products = new Product[]
                {
                    // Tranh Đông Hồ
                    new Product
                    {
                        Name = "Đám Cưới Chuột",
                        Description = "Tranh Đám Cưới Chuột là một tác phẩm nổi tiếng trong dòng tranh dân gian Đông Hồ, miêu tả đám cưới của loài chuột với đầy đủ nghi lễ như trong đám cưới của con người.",
                        Price = 500000,
                        StockQuantity = 15,
                        Size = "40x60cm",
                        Artist = "Nghệ nhân Nguyễn Văn A",
                        Origin = "Làng Đông Hồ, Bắc Ninh",
                        ImageUrl = "/images/products/damcuoichuot.jpg",
                        IsActive = true,
                        IsFeatured = true,
                        CategoryId = 1
                    },
                    new Product
                    {
                        Name = "Vinh Hoa",
                        Description = "Tranh Vinh Hoa thể hiện mong ước về sự sung túc, phú quý. Hình ảnh em bé ôm gà, cá chép với hoa sen là biểu tượng cho sự no đủ và hạnh phúc.",
                        Price = 450000,
                        StockQuantity = 10,
                        Size = "40x60cm",
                        Artist = "Nghệ nhân Nguyễn Văn B",
                        Origin = "Làng Đông Hồ, Bắc Ninh",
                        ImageUrl = "/images/products/vinhhoa.jpg",
                        IsActive = true,
                        IsFeatured = true,
                        CategoryId = 1
                    },
                    // Tranh Hàng Trống
                    new Product
                    {
                        Name = "Tứ Bình",
                        Description = "Tranh Tứ Bình là bộ tranh bốn mùa, thể hiện vẻ đẹp và đặc trưng của bốn mùa trong năm qua hình ảnh các loài hoa tượng trưng.",
                        Price = 600000,
                        StockQuantity = 8,
                        Size = "40x60cm",
                        Artist = "Nghệ nhân Trần Văn C",
                        Origin = "Phố Hàng Trống, Hà Nội",
                        ImageUrl = "/images/products/tubinh.jpg",
                        IsActive = true,
                        IsFeatured = true,
                        CategoryId = 2
                    },
                    new Product
                    {
                        Name = "Ngũ Hổ",
                        Description = "Tranh Ngũ Hổ thể hiện năm con hổ với các tư thế khác nhau, tượng trưng cho sức mạnh và quyền uy.",
                        Price = 550000,
                        StockQuantity = 12,
                        Size = "50x70cm",
                        Artist = "Nghệ nhân Trần Văn D",
                        Origin = "Phố Hàng Trống, Hà Nội",
                        ImageUrl = "/images/products/nguho.jpg",
                        IsActive = true,
                        IsFeatured = false,
                        CategoryId = 2
                    },
                    // Tranh Kim Hoàng
                    new Product
                    {
                        Name = "Cá Chép Hóa Rồng",
                        Description = "Tranh Cá Chép Hóa Rồng là biểu tượng cho sự nỗ lực vượt qua thử thách, đạt đến thành công và vinh quang.",
                        Price = 700000,
                        StockQuantity = 5,
                        Size = "45x65cm",
                        Artist = "Nghệ nhân Lê Văn E",
                        Origin = "Làng Kim Hoàng, Hà Nội",
                        ImageUrl = "/images/products/cachephoarong.jpg",
                        IsActive = true,
                        IsFeatured = true,
                        CategoryId = 3
                    },
                    // Tranh Làng Sình
                    new Product
                    {
                        Name = "Ngư Tiều Canh Mục",
                        Description = "Tranh Ngư Tiều Canh Mục thể hiện bốn nghề nghiệp truyền thống: đánh cá, đốn củi, cày ruộng và chăn trâu.",
                        Price = 650000,
                        StockQuantity = 7,
                        Size = "50x70cm",
                        Artist = "Nghệ nhân Phạm Văn F",
                        Origin = "Làng Sình, Huế",
                        ImageUrl = "/images/products/ngutieucanhuc.jpg",
                        IsActive = true,
                        IsFeatured = true,
                        CategoryId = 4
                    }
                };

                context.Products.AddRange(products);
                context.SaveChanges();

                // Add admin user
                var admin = new User
                {
                    FullName = "Admin",
                    Email = "admin@webbantranhdangian.com",
                    Password = HashPassword("Admin123"),
                    Phone = "0123456789",
                    Address = "Hà Nội",
                    RegisterDate = DateTime.Now,
                    Role = "Admin"
                };

                context.Users.Add(admin);
                context.SaveChanges();
            }
        }

        private static string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
} 