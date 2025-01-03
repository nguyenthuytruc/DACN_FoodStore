USE [DACS_QuanLyCuaHangAnUong]
 
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 6/12/2024 1:32:45 PM ******/
SET ANSI_NULLS ON
 
SET QUOTED_IDENTIFIER ON
 
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
 
/****** Object:  Table [dbo].[AspNetRoleClaims]    Script Date: 6/12/2024 1:32:45 PM ******/
SET ANSI_NULLS ON
 
SET QUOTED_IDENTIFIER ON
 
CREATE TABLE [dbo].[AspNetRoleClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
 
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 6/12/2024 1:32:45 PM ******/
SET ANSI_NULLS ON
 
SET QUOTED_IDENTIFIER ON
 
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](256) NULL,
	[NormalizedName] [nvarchar](256) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
 
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 6/12/2024 1:32:45 PM ******/
SET ANSI_NULLS ON
 
SET QUOTED_IDENTIFIER ON
 
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
 
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 6/12/2024 1:32:45 PM ******/
SET ANSI_NULLS ON
 
SET QUOTED_IDENTIFIER ON
 
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](450) NOT NULL,
	[ProviderKey] [nvarchar](450) NOT NULL,
	[ProviderDisplayName] [nvarchar](max) NULL,
	[UserId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
 
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 6/12/2024 1:32:45 PM ******/
SET ANSI_NULLS ON
 
SET QUOTED_IDENTIFIER ON
 
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](450) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
 
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 6/12/2024 1:32:45 PM ******/
SET ANSI_NULLS ON
 
SET QUOTED_IDENTIFIER ON
 
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](450) NOT NULL,
	[UserName] [nvarchar](256) NULL,
	[NormalizedUserName] [nvarchar](256) NULL,
	[Email] [nvarchar](256) NULL,
	[NormalizedEmail] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[Address] [nvarchar](max) NULL,
	[Age] [nvarchar](max) NULL,
	[FullName] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
 
/****** Object:  Table [dbo].[AspNetUserTokens]    Script Date: 6/12/2024 1:32:45 PM ******/
SET ANSI_NULLS ON
 
SET QUOTED_IDENTIFIER ON
 
CREATE TABLE [dbo].[AspNetUserTokens](
	[UserId] [nvarchar](450) NOT NULL,
	[LoginProvider] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](450) NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[LoginProvider] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
 
/****** Object:  Table [dbo].[FoodCategories]    Script Date: 6/12/2024 1:32:45 PM ******/
SET ANSI_NULLS ON
 
SET QUOTED_IDENTIFIER ON
 
CREATE TABLE [dbo].[FoodCategories](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_FoodCategories] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
 
/****** Object:  Table [dbo].[Foods]    Script Date: 6/12/2024 1:32:45 PM ******/
SET ANSI_NULLS ON
 
SET QUOTED_IDENTIFIER ON
 
CREATE TABLE [dbo].[Foods](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Image] [nvarchar](max) NOT NULL,
	[Type] [nvarchar](max) NOT NULL,
	[Price] [decimal](18, 2) NOT NULL,
	[Status] [int] NOT NULL,
	[FoodCategoryId] [int] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Foods] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
 
/****** Object:  Table [dbo].[Invoices]    Script Date: 6/12/2024 1:32:45 PM ******/
SET ANSI_NULLS ON
 
SET QUOTED_IDENTIFIER ON
 
CREATE TABLE [dbo].[Invoices](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OrderId] [int] NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[FinishedAt] [datetime2](7) NOT NULL,
	[Price] [float] NOT NULL,
	[PaymentId] [int] NOT NULL,
	[Charge] [float] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[Status] [bit] NOT NULL,
 CONSTRAINT [PK_Invoices] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
 
/****** Object:  Table [dbo].[OrderDetails]    Script Date: 6/12/2024 1:32:45 PM ******/
SET ANSI_NULLS ON
 
SET QUOTED_IDENTIFIER ON
 
CREATE TABLE [dbo].[OrderDetails](
	[OrderId] [int] NOT NULL,
	[FoodId] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
	[Status] [int] NOT NULL,
 CONSTRAINT [PK_OrderDetails] PRIMARY KEY CLUSTERED 
(
	[FoodId] ASC,
	[OrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
 
/****** Object:  Table [dbo].[Orders]    Script Date: 6/12/2024 1:32:45 PM ******/
SET ANSI_NULLS ON
 
SET QUOTED_IDENTIFIER ON
 
CREATE TABLE [dbo].[Orders](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TableId] [int] NOT NULL,
	[Status] [bit] NOT NULL,
	[TotalPrice] [float] NOT NULL,
	[StatusPay] [bit] NOT NULL,
	[Created] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
 
/****** Object:  Table [dbo].[Payments]    Script Date: 6/12/2024 1:32:45 PM ******/
SET ANSI_NULLS ON
 
SET QUOTED_IDENTIFIER ON
 
CREATE TABLE [dbo].[Payments](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Payments] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
 
/****** Object:  Table [dbo].[Tables]    Script Date: 6/12/2024 1:32:45 PM ******/
SET ANSI_NULLS ON
 
SET QUOTED_IDENTIFIER ON
 
CREATE TABLE [dbo].[Tables](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Status] [int] NOT NULL,
 CONSTRAINT [PK_Tables] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
 
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240421143301_Init', N'8.0.4')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240421144322_AddIdentity', N'8.0.4')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240421150611_ExtendIdentityUser', N'8.0.4')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240520144119_update-model', N'8.0.4')
 
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'6af3201d-8467-4301-b578-c7d7a5897ed0', N'Admin', N'ADMIN', NULL)
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'e9c63e2f-9dfa-44d1-b8b3-462c391ac62c', N'Employee', N'EMPLOYEE', NULL)
 
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'479de8df-66c6-4423-832f-0b8e7c368861', N'6af3201d-8467-4301-b578-c7d7a5897ed0')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'917307e1-6437-486b-ba6e-4da9987ab920', N'e9c63e2f-9dfa-44d1-b8b3-462c391ac62c')
 
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Address], [Age], [FullName]) VALUES (N'479de8df-66c6-4423-832f-0b8e7c368861', N'admin@gmail.com', N'ADMIN@GMAIL.COM', N'admin@gmail.com', N'ADMIN@GMAIL.COM', 0, N'AQAAAAIAAYagAAAAEK6q+U7fvClDuZZKuup0XJ7LjxkOtkxuHqYKZ4aD0I8nd3y9ptOujFMbidGVY16rEA==', N'WPJQNXGC4PCEV3A6HMM4VJEHJQJ7QTVE', N'e2550e4c-56db-4a85-bbff-919995ff42ed', NULL, 0, 0, NULL, 1, 0, NULL, NULL, N'admin')
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Address], [Age], [FullName]) VALUES (N'917307e1-6437-486b-ba6e-4da9987ab920', N'nv@gmail.com', N'NV@GMAIL.COM', N'nv@gmail.com', N'NV@GMAIL.COM', 0, N'AQAAAAIAAYagAAAAELlPwvNULs3Sni4pilA+Sl47Eztnl4kXK8hyHTV1gM9udatxRMerHi/giseKw3cpoQ==', N'O45ZOSFD6I4AM2WSQGXGFPLEIX75F6AY', N'b2e1fbfb-0322-4d5d-a803-4171797b536e', NULL, 0, 0, NULL, 1, 0, NULL, NULL, N'NHAN VIEN ')
 
SET IDENTITY_INSERT [dbo].[FoodCategories] ON 

INSERT [dbo].[FoodCategories] ([Id], [Name], [IsDeleted]) VALUES (2, N'Bia', 0)
INSERT [dbo].[FoodCategories] ([Id], [Name], [IsDeleted]) VALUES (3, N'Thức ăn', 0)
INSERT [dbo].[FoodCategories] ([Id], [Name], [IsDeleted]) VALUES (5, N'Đồ ăn', 0)
INSERT [dbo].[FoodCategories] ([Id], [Name], [IsDeleted]) VALUES (6, N'Đồ nướng', 0)
INSERT [dbo].[FoodCategories] ([Id], [Name], [IsDeleted]) VALUES (7, N'Đồ ăn', 0)
SET IDENTITY_INSERT [dbo].[FoodCategories] OFF
 
SET IDENTITY_INSERT [dbo].[Foods] ON 

INSERT [dbo].[Foods] ([Id], [Name], [Image], [Type], [Price], [Status], [FoodCategoryId], [IsDeleted]) VALUES (10, N'iu', N'/images/suatuoi.jpg', N'Ly', CAST(321321.00 AS Decimal(18, 2)), 0, 2, 0)
INSERT [dbo].[Foods] ([Id], [Name], [Image], [Type], [Price], [Status], [FoodCategoryId], [IsDeleted]) VALUES (11, N'iu', N'/images/btt.jpg', N'Ly', CAST(34231.00 AS Decimal(18, 2)), 1, 2, 0)
INSERT [dbo].[Foods] ([Id], [Name], [Image], [Type], [Price], [Status], [FoodCategoryId], [IsDeleted]) VALUES (12, N'Đồ ăn', N'/images/pic2.jpg', N'Ly', CAST(1000.00 AS Decimal(18, 2)), 1, 2, 0)
SET IDENTITY_INSERT [dbo].[Foods] OFF
 
SET IDENTITY_INSERT [dbo].[Invoices] ON 

INSERT [dbo].[Invoices] ([Id], [OrderId], [CreatedAt], [FinishedAt], [Price], [PaymentId], [Charge], [IsDeleted], [Status]) VALUES (2, 9, CAST(N'2024-05-31T16:02:57.2953350' AS DateTime2), CAST(N'2024-05-31T16:03:06.1685137' AS DateTime2), 356552, 1, 0, 0, 1)
INSERT [dbo].[Invoices] ([Id], [OrderId], [CreatedAt], [FinishedAt], [Price], [PaymentId], [Charge], [IsDeleted], [Status]) VALUES (9, 18, CAST(N'2024-06-12T05:48:14.4887376' AS DateTime2), CAST(N'2024-06-12T05:48:14.4887904' AS DateTime2), 356552, 2, 0, 0, 1)
INSERT [dbo].[Invoices] ([Id], [OrderId], [CreatedAt], [FinishedAt], [Price], [PaymentId], [Charge], [IsDeleted], [Status]) VALUES (10, 19, CAST(N'2024-06-12T05:48:34.1299269' AS DateTime2), CAST(N'2024-06-12T05:48:34.1299270' AS DateTime2), 356552, 1, 0, 0, 1)
INSERT [dbo].[Invoices] ([Id], [OrderId], [CreatedAt], [FinishedAt], [Price], [PaymentId], [Charge], [IsDeleted], [Status]) VALUES (11, 20, CAST(N'2024-06-12T05:49:16.5036604' AS DateTime2), CAST(N'2024-06-12T05:49:16.5037024' AS DateTime2), 356552, 2, 0, 0, 1)
INSERT [dbo].[Invoices] ([Id], [OrderId], [CreatedAt], [FinishedAt], [Price], [PaymentId], [Charge], [IsDeleted], [Status]) VALUES (12, 21, CAST(N'2024-06-12T05:57:16.5662566' AS DateTime2), CAST(N'2024-06-12T05:57:16.5663326' AS DateTime2), 356552, 1, 0, 0, 1)
SET IDENTITY_INSERT [dbo].[Invoices] OFF
 
INSERT [dbo].[OrderDetails] ([OrderId], [FoodId], [Quantity], [Status]) VALUES (9, 10, 1, 0)
INSERT [dbo].[OrderDetails] ([OrderId], [FoodId], [Quantity], [Status]) VALUES (18, 10, 1, 0)
INSERT [dbo].[OrderDetails] ([OrderId], [FoodId], [Quantity], [Status]) VALUES (19, 10, 1, 0)
INSERT [dbo].[OrderDetails] ([OrderId], [FoodId], [Quantity], [Status]) VALUES (20, 10, 1, 0)
INSERT [dbo].[OrderDetails] ([OrderId], [FoodId], [Quantity], [Status]) VALUES (21, 10, 1, 0)
INSERT [dbo].[OrderDetails] ([OrderId], [FoodId], [Quantity], [Status]) VALUES (22, 10, 1, 0)
INSERT [dbo].[OrderDetails] ([OrderId], [FoodId], [Quantity], [Status]) VALUES (9, 11, 1, 0)
INSERT [dbo].[OrderDetails] ([OrderId], [FoodId], [Quantity], [Status]) VALUES (18, 11, 1, 0)
INSERT [dbo].[OrderDetails] ([OrderId], [FoodId], [Quantity], [Status]) VALUES (19, 11, 1, 0)
INSERT [dbo].[OrderDetails] ([OrderId], [FoodId], [Quantity], [Status]) VALUES (20, 11, 1, 0)
INSERT [dbo].[OrderDetails] ([OrderId], [FoodId], [Quantity], [Status]) VALUES (21, 11, 1, 0)
INSERT [dbo].[OrderDetails] ([OrderId], [FoodId], [Quantity], [Status]) VALUES (22, 11, 1, 0)
INSERT [dbo].[OrderDetails] ([OrderId], [FoodId], [Quantity], [Status]) VALUES (9, 12, 1, 0)
INSERT [dbo].[OrderDetails] ([OrderId], [FoodId], [Quantity], [Status]) VALUES (18, 12, 1, 0)
INSERT [dbo].[OrderDetails] ([OrderId], [FoodId], [Quantity], [Status]) VALUES (19, 12, 1, 0)
INSERT [dbo].[OrderDetails] ([OrderId], [FoodId], [Quantity], [Status]) VALUES (20, 12, 1, 0)
INSERT [dbo].[OrderDetails] ([OrderId], [FoodId], [Quantity], [Status]) VALUES (21, 12, 1, 0)
INSERT [dbo].[OrderDetails] ([OrderId], [FoodId], [Quantity], [Status]) VALUES (22, 12, 1, 0)
 
SET IDENTITY_INSERT [dbo].[Orders] ON 

INSERT [dbo].[Orders] ([Id], [TableId], [Status], [TotalPrice], [StatusPay], [Created]) VALUES (9, 7, 1, 356552, 1, CAST(N'2024-05-31T22:43:42.6234499' AS DateTime2))
INSERT [dbo].[Orders] ([Id], [TableId], [Status], [TotalPrice], [StatusPay], [Created]) VALUES (18, 7, 1, 356552, 1, CAST(N'2024-06-12T12:06:47.3237826' AS DateTime2))
INSERT [dbo].[Orders] ([Id], [TableId], [Status], [TotalPrice], [StatusPay], [Created]) VALUES (19, 7, 1, 356552, 1, CAST(N'2024-06-12T12:15:30.1796299' AS DateTime2))
INSERT [dbo].[Orders] ([Id], [TableId], [Status], [TotalPrice], [StatusPay], [Created]) VALUES (20, 7, 1, 356552, 1, CAST(N'2024-06-12T12:21:01.8222902' AS DateTime2))
INSERT [dbo].[Orders] ([Id], [TableId], [Status], [TotalPrice], [StatusPay], [Created]) VALUES (21, 7, 1, 356552, 1, CAST(N'2024-06-12T12:56:28.9121600' AS DateTime2))
INSERT [dbo].[Orders] ([Id], [TableId], [Status], [TotalPrice], [StatusPay], [Created]) VALUES (22, 7, 1, 356552, 0, CAST(N'2024-06-12T12:57:48.6462822' AS DateTime2))
SET IDENTITY_INSERT [dbo].[Orders] OFF
 
SET IDENTITY_INSERT [dbo].[Payments] ON 

INSERT [dbo].[Payments] ([Id], [Name]) VALUES (1, N'Tiền mặt')
INSERT [dbo].[Payments] ([Id], [Name]) VALUES (2, N'Momo')
SET IDENTITY_INSERT [dbo].[Payments] OFF
 
SET IDENTITY_INSERT [dbo].[Tables] ON 

INSERT [dbo].[Tables] ([Id], [Status]) VALUES (7, 1)
INSERT [dbo].[Tables] ([Id], [Status]) VALUES (8, 0)
SET IDENTITY_INSERT [dbo].[Tables] OFF
 
ALTER TABLE [dbo].[AspNetUsers] ADD  DEFAULT (N'') FOR [FullName]
 
ALTER TABLE [dbo].[FoodCategories] ADD  DEFAULT (CONVERT([bit],(0))) FOR [IsDeleted]
 
ALTER TABLE [dbo].[Foods] ADD  DEFAULT (CONVERT([bit],(0))) FOR [IsDeleted]
 
ALTER TABLE [dbo].[Invoices] ADD  DEFAULT ((0.0000000000000000e+000)) FOR [Charge]
 
ALTER TABLE [dbo].[Invoices] ADD  DEFAULT (CONVERT([bit],(0))) FOR [IsDeleted]
 
ALTER TABLE [dbo].[Invoices] ADD  DEFAULT (CONVERT([bit],(0))) FOR [Status]
 
ALTER TABLE [dbo].[Orders] ADD  DEFAULT ('0001-01-01T00:00:00.0000000') FOR [Created]
 
ALTER TABLE [dbo].[AspNetRoleClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
 
ALTER TABLE [dbo].[AspNetRoleClaims] CHECK CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId]
 
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
 
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId]
 
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
 
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId]
 
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
 
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId]
 
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
 
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId]
 
ALTER TABLE [dbo].[AspNetUserTokens]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
 
ALTER TABLE [dbo].[AspNetUserTokens] CHECK CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId]
 
ALTER TABLE [dbo].[Foods]  WITH CHECK ADD  CONSTRAINT [FK_Foods_FoodCategories_FoodCategoryId] FOREIGN KEY([FoodCategoryId])
REFERENCES [dbo].[FoodCategories] ([Id])
ON DELETE CASCADE
 
ALTER TABLE [dbo].[Foods] CHECK CONSTRAINT [FK_Foods_FoodCategories_FoodCategoryId]
 
ALTER TABLE [dbo].[Invoices]  WITH CHECK ADD  CONSTRAINT [FK_Invoices_Orders_OrderId] FOREIGN KEY([OrderId])
REFERENCES [dbo].[Orders] ([Id])
ON DELETE CASCADE
 
ALTER TABLE [dbo].[Invoices] CHECK CONSTRAINT [FK_Invoices_Orders_OrderId]
 
ALTER TABLE [dbo].[Invoices]  WITH CHECK ADD  CONSTRAINT [FK_Invoices_Payments_PaymentId] FOREIGN KEY([PaymentId])
REFERENCES [dbo].[Payments] ([Id])
ON DELETE CASCADE
 
ALTER TABLE [dbo].[Invoices] CHECK CONSTRAINT [FK_Invoices_Payments_PaymentId]
 
ALTER TABLE [dbo].[OrderDetails]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetails_Foods_FoodId] FOREIGN KEY([FoodId])
REFERENCES [dbo].[Foods] ([Id])
ON DELETE CASCADE
 
ALTER TABLE [dbo].[OrderDetails] CHECK CONSTRAINT [FK_OrderDetails_Foods_FoodId]
 
ALTER TABLE [dbo].[OrderDetails]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetails_Orders_OrderId] FOREIGN KEY([OrderId])
REFERENCES [dbo].[Orders] ([Id])
ON DELETE CASCADE
 
ALTER TABLE [dbo].[OrderDetails] CHECK CONSTRAINT [FK_OrderDetails_Orders_OrderId]
 
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_Tables_TableId] FOREIGN KEY([TableId])
REFERENCES [dbo].[Tables] ([Id])
ON DELETE CASCADE
 
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_Tables_TableId]
 
