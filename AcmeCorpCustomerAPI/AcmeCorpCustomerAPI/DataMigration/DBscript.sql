

/****** Object:  Table [dbo].[Customer]    Script Date: 11/9/2020 1:56:38 AM ******/


SET ANSI_NULLS ON

GO


SET QUOTED_IDENTIFIER ON

GO


CREATE TABLE [dbo].[Customer](


	[Id] [int] IDENTITY(1,1) NOT NULL,


	[Username] [nvarchar](255) NOT NULL,


	[Password] [nvarchar](255) NOT NULL,


	[FirstName] [nvarchar](255) NOT NULL,


	[LastName] [nvarchar](255) NOT NULL,

   
    [Role] [nvarchar](50) NOT NULL,


	[Email] [nvarchar](255) NOT NULL,


	[TS] [smalldatetime] NOT NULL,


	[Active] [bit] NOT NULL,



 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 

(


	[Id] ASC


)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]


) ON [PRIMARY]

GO

/****** Object:  Table [dbo].[Order]    Script Date: 11/9/2020 1:56:38 AM ******/


SET ANSI_NULLS ON

GO


SET QUOTED_IDENTIFIER ON

GO


CREATE TABLE [dbo].[Order](


	[Id] [int] IDENTITY(1,1) NOT NULL,


	[Quantity] [int] NOT NULL,


	[Total] [decimal](19, 4) NOT NULL,


	[TS] [smalldatetime] NOT NULL,


	[CustomerId] [int] NOT NULL,


 CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED 

(


	[Id] ASC


)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]


) ON [PRIMARY]

GO


SET IDENTITY_INSERT [dbo].[Customer] ON 

GO


INSERT [dbo].[Customer] ([Id], [Username], [Password], [Role], [FirstName], [LastName], [Email], [TS], [Active]) VALUES (1, N'user1', N'abc', N'Admin', N'Andy', N'Thomas', N'user1@acmecorp.com', CAST(N'2020-10-30T00:00:00' AS SmallDateTime), 1)

GO


INSERT [dbo].[Customer] ([Id], [Username], [Password], [Role], [FirstName], [LastName], [Email], [TS], [Active]) VALUES (2, N'user2', N'cdf', N'User', N'Wendy', N'Hi', N'user2@acmecorp.com', CAST(N'2020-10-30T00:00:00' AS SmallDateTime), 1)

GO


SET IDENTITY_INSERT [dbo].[Customer] OFF

GO


SET IDENTITY_INSERT [dbo].[Order] ON 

GO


INSERT [dbo].[Order] ([Id],[Quantity], [Total], [TS], [CustomerId]) VALUES (1, 5, CAST(120.0000 AS Decimal(19, 4)), CAST(N'2020-10-25T00:00:00' AS SmallDateTime), 1)

GO


INSERT [dbo].[Order] ([Id],[Quantity], [Total], [TS], [CustomerId]) VALUES (2, 2, CAST(750.0000 AS Decimal(19, 4)), CAST(N'2020-10-25T00:00:00' AS SmallDateTime), 1)

GO


SET IDENTITY_INSERT [dbo].[Order] OFF

GO


ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_Customer] FOREIGN KEY([CustomerId])


REFERENCES [dbo].[Customer] ([Id])

GO


ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_Customer]

GO

