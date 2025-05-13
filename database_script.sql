USE master;
GO

-- Drop the database if it exists
IF EXISTS (SELECT name FROM sys.databases WHERE name = N'WebBanTranhDanGian')
BEGIN
    ALTER DATABASE [WebBanTranhDanGian] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE [WebBanTranhDanGian];
END
GO

-- Create database
CREATE DATABASE [WebBanTranhDanGian];
GO

USE [WebBanTranhDanGian];
GO

-- Create tables
-- Categories table
CREATE TABLE [Categories] (
    [CategoryId] INT IDENTITY(1,1) NOT NULL,
    [Name] NVARCHAR(100) NOT NULL,
    [Description] NVARCHAR(MAX) NULL,
    [ImageUrl] NVARCHAR(MAX) NULL,
    CONSTRAINT [PK_Categories] PRIMARY KEY ([CategoryId])
);
GO

-- Users table
CREATE TABLE [Users] (
    [UserId] INT IDENTITY(1,1) NOT NULL,
    [FullName] NVARCHAR(100) NOT NULL,
    [Email] NVARCHAR(100) NOT NULL,
    [Password] NVARCHAR(100) NOT NULL,
    [Phone] NVARCHAR(20) NULL,
    [Address] NVARCHAR(MAX) NULL,
    [RegisterDate] DATETIME2 NOT NULL,
    [Role] NVARCHAR(20) NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY ([UserId])
);
GO

-- Products table
CREATE TABLE [Products] (
    [ProductId] INT IDENTITY(1,1) NOT NULL,
    [Name] NVARCHAR(200) NOT NULL,
    [Description] NVARCHAR(MAX) NOT NULL,
    [Price] DECIMAL(18,2) NOT NULL,
    [DiscountPrice] DECIMAL(18,2) NULL,
    [ImageUrl] NVARCHAR(MAX) NULL,
    [Size] NVARCHAR(50) NULL,
    [Artist] NVARCHAR(100) NULL,
    [Origin] NVARCHAR(100) NULL,
    [StockQuantity] INT NOT NULL,
    [CreatedDate] DATETIME2 NOT NULL,
    [IsActive] BIT NOT NULL,
    [IsFeatured] BIT NOT NULL,
    [CategoryId] INT NOT NULL,
    CONSTRAINT [PK_Products] PRIMARY KEY ([ProductId]),
    CONSTRAINT [FK_Products_Categories_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [Categories] ([CategoryId]) ON DELETE CASCADE
);
GO

-- Orders table
CREATE TABLE [Orders] (
    [OrderId] INT IDENTITY(1,1) NOT NULL,
    [OrderNumber] NVARCHAR(50) NULL,
    [OrderDate] DATETIME2 NOT NULL,
    [ShipName] NVARCHAR(100) NOT NULL,
    [ShipAddress] NVARCHAR(MAX) NOT NULL,
    [ShipPhone] NVARCHAR(20) NOT NULL,
    [ShipEmail] NVARCHAR(100) NULL,
    [Note] NVARCHAR(MAX) NULL,
    [TotalAmount] DECIMAL(18,2) NOT NULL,
    [Status] NVARCHAR(20) NOT NULL,
    [PaymentMethod] NVARCHAR(20) NOT NULL,
    [IsPaid] BIT NOT NULL,
    [UserId] INT NOT NULL,
    CONSTRAINT [PK_Orders] PRIMARY KEY ([OrderId]),
    CONSTRAINT [FK_Orders_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([UserId]) ON DELETE CASCADE
);
GO

-- OrderDetails table
CREATE TABLE [OrderDetails] (
    [OrderId] INT NOT NULL,
    [ProductId] INT NOT NULL,
    [Quantity] INT NOT NULL,
    [UnitPrice] DECIMAL(18,2) NOT NULL,
    CONSTRAINT [PK_OrderDetails] PRIMARY KEY ([OrderId], [ProductId]),
    CONSTRAINT [FK_OrderDetails_Orders_OrderId] FOREIGN KEY ([OrderId]) REFERENCES [Orders] ([OrderId]) ON DELETE CASCADE,
    CONSTRAINT [FK_OrderDetails_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([ProductId]) ON DELETE CASCADE
);
GO

-- ShoppingCarts table
CREATE TABLE [ShoppingCarts] (
    [UserId] INT NOT NULL,
    [ProductId] INT NOT NULL,
    [Quantity] INT NOT NULL,
    [DateCreated] DATETIME2 NOT NULL,
    CONSTRAINT [PK_ShoppingCarts] PRIMARY KEY ([UserId], [ProductId]),
    CONSTRAINT [FK_ShoppingCarts_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([UserId]) ON DELETE CASCADE,
    CONSTRAINT [FK_ShoppingCarts_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([ProductId]) ON DELETE CASCADE
);
GO

-- Create indexes
CREATE INDEX [IX_OrderDetails_ProductId] ON [OrderDetails] ([ProductId]);
CREATE INDEX [IX_Orders_UserId] ON [Orders] ([UserId]);
CREATE INDEX [IX_Products_CategoryId] ON [Products] ([CategoryId]);
CREATE INDEX [IX_ShoppingCarts_ProductId] ON [ShoppingCarts] ([ProductId]);
GO

-- Insert sample data
-- Add Categories
INSERT INTO [Categories] ([Name], [Description], [ImageUrl])
VALUES 
    (N'Tranh Đông Hồ', N'Tranh dân gian Đông Hồ là một loại tranh dân gian truyền thống của Việt Nam, được sản xuất tại làng Đông Hồ, xã Song Hồ, huyện Thuận Thành, tỉnh Bắc Ninh.', N'/images/categories/donghopaintings.jpg'),
    (N'Tranh Hàng Trống', N'Tranh Hàng Trống là loại tranh dân gian được sản xuất tại phố Hàng Trống, Hà Nội, Việt Nam.', N'/images/categories/hangtrongpaintings.jpg'),
    (N'Tranh Kim Hoàng', N'Tranh Kim Hoàng là một dòng tranh dân gian có nguồn gốc từ làng Kim Hoàng, xã Vân Canh, huyện Hoài Đức, thành phố Hà Nội.', N'/images/categories/kimhoangpaintings.jpg'),
    (N'Tranh Làng Sình', N'Tranh làng Sình là một loại tranh dân gian truyền thống, có nguồn gốc từ làng Sình, xã Phú Mậu, thành phố Huế, tỉnh Thừa Thiên Huế.', N'/images/categories/langsinhpaintings.jpg');
GO

-- Add Admin User
DECLARE @AdminPassword NVARCHAR(100);
-- This is SHA256 hash of "Admin123"
SET @AdminPassword = '240be518fabd2724ddb6f04eeb1da5967448d7e831c08c8fa822809f74c720a9';

INSERT INTO [Users] ([FullName], [Email], [Password], [Phone], [Address], [RegisterDate], [Role])
VALUES (N'Admin', N'admin@webbantranhdangian.com', @AdminPassword, N'0123456789', N'Hà Nội', GETDATE(), N'Admin');
GO

-- Add Products
INSERT INTO [Products] (
    [Name], [Description], [Price], [DiscountPrice], [ImageUrl], 
    [Size], [Artist], [Origin], [StockQuantity], 
    [CreatedDate], [IsActive], [IsFeatured], [CategoryId]
)
VALUES 
    -- Tranh Đông Hồ
    (N'Đám Cưới Chuột', 
     N'Tranh Đám Cưới Chuột là một tác phẩm nổi tiếng trong dòng tranh dân gian Đông Hồ, miêu tả đám cưới của loài chuột với đầy đủ nghi lễ như trong đám cưới của con người.',
     500000, NULL, N'/images/products/damcuoichuot.jpg',
     N'40x60cm', N'Nghệ nhân Nguyễn Văn A', N'Làng Đông Hồ, Bắc Ninh', 15,
     GETDATE(), 1, 1, 1),
     
    (N'Vinh Hoa', 
     N'Tranh Vinh Hoa thể hiện mong ước về sự sung túc, phú quý. Hình ảnh em bé ôm gà, cá chép với hoa sen là biểu tượng cho sự no đủ và hạnh phúc.',
     450000, NULL, N'/images/products/vinhhoa.jpg',
     N'40x60cm', N'Nghệ nhân Nguyễn Văn B', N'Làng Đông Hồ, Bắc Ninh', 10,
     GETDATE(), 1, 1, 1),
     
    -- Tranh Hàng Trống
    (N'Tứ Bình', 
     N'Tranh Tứ Bình là bộ tranh bốn mùa, thể hiện vẻ đẹp và đặc trưng của bốn mùa trong năm qua hình ảnh các loài hoa tượng trưng.',
     600000, NULL, N'/images/products/tubinh.jpg',
     N'40x60cm', N'Nghệ nhân Trần Văn C', N'Phố Hàng Trống, Hà Nội', 8,
     GETDATE(), 1, 1, 2),
     
    (N'Ngũ Hổ', 
     N'Tranh Ngũ Hổ thể hiện năm con hổ với các tư thế khác nhau, tượng trưng cho sức mạnh và quyền uy.',
     550000, NULL, N'/images/products/nguho.jpg',
     N'50x70cm', N'Nghệ nhân Trần Văn D', N'Phố Hàng Trống, Hà Nội', 12,
     GETDATE(), 1, 0, 2),
     
    -- Tranh Kim Hoàng
    (N'Cá Chép Hóa Rồng', 
     N'Tranh Cá Chép Hóa Rồng là biểu tượng cho sự nỗ lực vượt qua thử thách, đạt đến thành công và vinh quang.',
     700000, NULL, N'/images/products/cachephoarong.jpg',
     N'45x65cm', N'Nghệ nhân Lê Văn E', N'Làng Kim Hoàng, Hà Nội', 5,
     GETDATE(), 1, 1, 3),
     
    -- Tranh Làng Sình
    (N'Ngư Tiều Canh Mục', 
     N'Tranh Ngư Tiều Canh Mục thể hiện bốn nghề nghiệp truyền thống: đánh cá, đốn củi, cày ruộng và chăn trâu.',
     650000, NULL, N'/images/products/ngutieucanhuc.jpg',
     N'50x70cm', N'Nghệ nhân Phạm Văn F', N'Làng Sình, Huế', 7,
     GETDATE(), 1, 1, 4);
GO

-- Query to verify sample data
SELECT * FROM Categories;
SELECT * FROM Users;
SELECT * FROM Products;
GO 