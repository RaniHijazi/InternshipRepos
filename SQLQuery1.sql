USE [master]
GO
/****** Object:  Database [LibrarieInternDB]    Script Date: 3/27/2024 4:27:24 PM ******/
CREATE DATABASE [LibrarieInternDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'LibrarieInternDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\LibrarieInternDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'LibrarieInternDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\LibrarieInternDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [LibrarieInternDB] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [LibrarieInternDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [LibrarieInternDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [LibrarieInternDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [LibrarieInternDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [LibrarieInternDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [LibrarieInternDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [LibrarieInternDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [LibrarieInternDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [LibrarieInternDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [LibrarieInternDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [LibrarieInternDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [LibrarieInternDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [LibrarieInternDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [LibrarieInternDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [LibrarieInternDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [LibrarieInternDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [LibrarieInternDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [LibrarieInternDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [LibrarieInternDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [LibrarieInternDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [LibrarieInternDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [LibrarieInternDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [LibrarieInternDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [LibrarieInternDB] SET RECOVERY FULL 
GO
ALTER DATABASE [LibrarieInternDB] SET  MULTI_USER 
GO
ALTER DATABASE [LibrarieInternDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [LibrarieInternDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [LibrarieInternDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [LibrarieInternDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [LibrarieInternDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [LibrarieInternDB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'LibrarieInternDB', N'ON'
GO
ALTER DATABASE [LibrarieInternDB] SET QUERY_STORE = ON
GO
ALTER DATABASE [LibrarieInternDB] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [LibrarieInternDB]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 3/27/2024 4:27:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[bills]    Script Date: 3/27/2024 4:27:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[bills](
	[BillsId] [int] IDENTITY(1,1) NOT NULL,
	[borrow_id] [int] NOT NULL,
	[BorrowedBookid] [int] NOT NULL,
 CONSTRAINT [PK_bills] PRIMARY KEY CLUSTERED 
(
	[BillsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[books]    Script Date: 3/27/2024 4:27:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[books](
	[BooksId] [int] IDENTITY(1,1) NOT NULL,
	[title] [nvarchar](max) NOT NULL,
	[author] [nvarchar](max) NOT NULL,
	[genre] [nvarchar](max) NOT NULL,
	[total_copies] [int] NOT NULL,
 CONSTRAINT [PK_books] PRIMARY KEY CLUSTERED 
(
	[BooksId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[borrowedbooks]    Script Date: 3/27/2024 4:27:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[borrowedbooks](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[user_id] [int] NOT NULL,
	[book_id] [int] NOT NULL,
	[overdue_fees] [int] NOT NULL,
	[borrow_date] [datetime2](7) NOT NULL,
	[return_date] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_borrowedbooks] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[subscriptions]    Script Date: 3/27/2024 4:27:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[subscriptions](
	[SubscriptionsId] [int] IDENTITY(1,1) NOT NULL,
	[subscription_name] [nvarchar](max) NOT NULL,
	[subscription_expiry_date] [datetime2](7) NOT NULL,
	[user_id] [int] NOT NULL,
 CONSTRAINT [PK_subscriptions] PRIMARY KEY CLUSTERED 
(
	[SubscriptionsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[users]    Script Date: 3/27/2024 4:27:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[users](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[username] [nvarchar](max) NOT NULL,
	[password] [nvarchar](max) NOT NULL,
	[total_fees_due] [decimal](18, 2) NOT NULL,
	[total_books_borrowed] [decimal](18, 2) NOT NULL,
	[Email] [nvarchar](max) NOT NULL,
	[is_admin] [bit] NOT NULL,
 CONSTRAINT [PK_users] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Index [IX_bills_BorrowedBookid]    Script Date: 3/27/2024 4:27:24 PM ******/
CREATE NONCLUSTERED INDEX [IX_bills_BorrowedBookid] ON [dbo].[bills]
(
	[BorrowedBookid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_borrowedbooks_book_id]    Script Date: 3/27/2024 4:27:24 PM ******/
CREATE NONCLUSTERED INDEX [IX_borrowedbooks_book_id] ON [dbo].[borrowedbooks]
(
	[book_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_borrowedbooks_user_id]    Script Date: 3/27/2024 4:27:24 PM ******/
CREATE NONCLUSTERED INDEX [IX_borrowedbooks_user_id] ON [dbo].[borrowedbooks]
(
	[user_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_subscriptions_user_id]    Script Date: 3/27/2024 4:27:24 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_subscriptions_user_id] ON [dbo].[subscriptions]
(
	[user_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[bills]  WITH CHECK ADD  CONSTRAINT [FK_bills_borrowedbooks_BorrowedBookid] FOREIGN KEY([BorrowedBookid])
REFERENCES [dbo].[borrowedbooks] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[bills] CHECK CONSTRAINT [FK_bills_borrowedbooks_BorrowedBookid]
GO
ALTER TABLE [dbo].[borrowedbooks]  WITH CHECK ADD  CONSTRAINT [FK_borrowedbooks_books_book_id] FOREIGN KEY([book_id])
REFERENCES [dbo].[books] ([BooksId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[borrowedbooks] CHECK CONSTRAINT [FK_borrowedbooks_books_book_id]
GO
ALTER TABLE [dbo].[borrowedbooks]  WITH CHECK ADD  CONSTRAINT [FK_borrowedbooks_users_user_id] FOREIGN KEY([user_id])
REFERENCES [dbo].[users] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[borrowedbooks] CHECK CONSTRAINT [FK_borrowedbooks_users_user_id]
GO
ALTER TABLE [dbo].[subscriptions]  WITH CHECK ADD  CONSTRAINT [FK_subscriptions_users_user_id] FOREIGN KEY([user_id])
REFERENCES [dbo].[users] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[subscriptions] CHECK CONSTRAINT [FK_subscriptions_users_user_id]
GO
USE [master]
GO
ALTER DATABASE [LibrarieInternDB] SET  READ_WRITE 
GO
