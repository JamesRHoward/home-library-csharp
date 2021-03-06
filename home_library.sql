USE [home_library]
GO
/****** Object:  Table [dbo].[all_books]    Script Date: 7/20/2016 9:55:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[all_books](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[title] [varchar](255) NULL,
	[author] [varchar](255) NULL,
	[read_bool] [bit] NOT NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[books_categories]    Script Date: 7/20/2016 9:55:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[books_categories](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[book_id] [int] NULL,
	[category_id] [int] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[books_to_sell]    Script Date: 7/20/2016 9:55:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[books_to_sell](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[book_id] [int] NULL,
	[sold_bool] [bit] NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[borrow_sources]    Script Date: 7/20/2016 9:55:53 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[borrow_sources](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](255) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[borrowed_books]    Script Date: 7/20/2016 9:55:53 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[borrowed_books](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[book_id] [int] NULL,
	[due_date] [datetime] NULL,
	[returned_bool] [bit] NOT NULL,
	[source_id] [int] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[categories]    Script Date: 7/20/2016 9:55:53 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[categories](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[genre] [varchar](255) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[lent_books]    Script Date: 7/20/2016 9:55:53 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[lent_books](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[owned_book_id] [int] NULL,
	[returned_bool] [bit] NOT NULL,
	[recipient] [varchar](255) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[owned_books]    Script Date: 7/20/2016 9:55:53 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[owned_books](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[book_id] [int] NULL,
	[physical_bool] [bit] NOT NULL,
	[storage_id] [int] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[reading_list]    Script Date: 7/20/2016 9:55:53 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[reading_list](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[book_id] [int] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[storage]    Script Date: 7/20/2016 9:55:53 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[storage](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[place_name] [varchar](255) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[wishlist]    Script Date: 7/20/2016 9:55:53 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[wishlist](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[book_id] [int] NULL
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[all_books] ADD  DEFAULT ((0)) FOR [read_bool]
GO
ALTER TABLE [dbo].[books_to_sell] ADD  DEFAULT ((0)) FOR [sold_bool]
GO
ALTER TABLE [dbo].[borrowed_books] ADD  DEFAULT ((0)) FOR [returned_bool]
GO
ALTER TABLE [dbo].[lent_books] ADD  DEFAULT ((0)) FOR [returned_bool]
GO
ALTER TABLE [dbo].[owned_books] ADD  DEFAULT ((1)) FOR [physical_bool]
GO
