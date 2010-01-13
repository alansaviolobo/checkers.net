/* BEGIN CREATE DATABASE */
IF EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE name = 'Checkers')
	BEGIN
		USE [Master]
		DROP DATABASE [Checkers]
	END
ELSE
	PRINT 'Creating Database Checkers'

CREATE DATABASE [Checkers]
GO
/* END CREATE DATABASE */

/* BEGIN DATABASE SCHEMA */
USE [Checkers]
GO
/****** Object:  Table [dbo].[Token]    Script Date: 01/13/2010 11:05:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Token](
	[Token_Id] [int] IDENTITY(1,1) NOT NULL,
	[Token_Type] [varchar](50) NULL,
	[Token_Menu] [int] NULL,
	[Token_Quantity] [decimal](6, 2) NULL,
	[Token_Source] [int] NULL,
	[Token_Status] [int] NULL,
	[Token_TimeStamp] [datetime] NULL,
 CONSTRAINT [PK_Token] PRIMARY KEY CLUSTERED 
(
	[Token_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Source]    Script Date: 01/13/2010 11:05:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Source](
	[Source_Id] [int] IDENTITY(1,1) NOT NULL,
	[Source_Type] [varchar](50) NULL,
	[Source_Number] [int] NULL,
	[Source_AmountPayable] [decimal](6, 2) NULL,
	[Source_Status] [int] NULL,
	[Source_TimeStamp] [datetime] NULL,
 CONSTRAINT [PK_Source] PRIMARY KEY CLUSTERED 
(
	[Source_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Package]    Script Date: 01/13/2010 11:05:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Package](
	[Package_Id] [int] IDENTITY(1,1) NOT NULL,
	[Package_Name] [varchar](100) NULL,
	[Package_Type] [varchar](50) NULL,
	[Package_Comments] [varchar](200) NULL,
	[Package_Status] [int] NULL,
	[Package_TimeStamp] [datetime] NULL,
 CONSTRAINT [PK_Package] PRIMARY KEY CLUSTERED 
(
	[Package_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PettyCash]    Script Date: 01/13/2010 11:05:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PettyCash](
	[PettyCash_Id] [int] IDENTITY(1,1) NOT NULL,
	[PettyCash_Amount] [decimal](6, 2) NULL,
	[PettyCash_Balance] [decimal](6, 2) NULL,
	[PettyCash_GivenBy] [int] NULL,
	[PettyCash_ReceivedBy] [int] NULL,
	[PettyCash_TimeStamp] [datetime] NULL,
 CONSTRAINT [PK_PettyCash] PRIMARY KEY CLUSTERED 
(
	[PettyCash_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Catalog]    Script Date: 01/13/2010 11:04:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Catalog](
	[Catalog_Id] [int] IDENTITY(1,1) NOT NULL,
	[Catalog_Name] [varchar](100) NULL,
	[Catalog_Type] [varchar](50) NULL,
	[Catalog_Status] [int] NULL,
	[Catalog_TimeStamp] [datetime] NULL,
 CONSTRAINT [PK_Catalog] PRIMARY KEY CLUSTERED 
(
	[Catalog_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Contact]    Script Date: 01/13/2010 11:04:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Contact](
	[Contact_Id] [int] IDENTITY(1,1) NOT NULL,
	[Contact_Name] [varchar](100) NULL,
	[Contact_UserName] [varchar](100) NULL,
	[Contact_Password] [varchar](100) NULL,
	[Contact_Type] [varchar](100) NULL,
	[Contact_Phone] [varchar](100) NULL,
	[Contact_Address] [varchar](200) NULL,
	[Contact_Email] [varchar](100) NULL,
	[Contact_OrganizationName] [varchar](100) NULL,
	[Contact_OrganizationPhone] [varchar](100) NULL,
	[Contact_OrganizationAddress] [varchar](200) NULL,
	[Contact_Credit] [decimal](6, 2) NULL,
	[Contact_Status] [int] NULL,
	[Contact_TimeStamp] [datetime] NULL,
 CONSTRAINT [PK_Contact] PRIMARY KEY CLUSTERED 
(
	[Contact_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Invoice]    Script Date: 01/13/2010 11:05:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Invoice](
	[Invoice_Id] [int] IDENTITY(1,1) NOT NULL,
	[Invoice_Amount] [decimal](6, 2) NULL,
	[Invoice_Discount] [decimal](6, 2) NULL,
	[Invoice_Tax] [decimal](6, 2) NULL,
	[Invoice_Type] [varchar](50) NULL,
	[Invoice_Source] [int] NULL,
	[Invoice_PaymentMode] [varchar](100) NULL,
	[Invoice_Reason] [varchar](200) NULL,
	[Invoice_Client] [int] NULL,
	[Invoice_Status] [int] NULL,
	[Invoice_TimeStamp] [datetime] NULL,
 CONSTRAINT [PK_Invoice] PRIMARY KEY CLUSTERED 
(
	[Invoice_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Bill, Reciept' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Invoice', @level2type=N'COLUMN',@level2name=N'Invoice_Type'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Credit, Credit 
Card, Cash, Cheque, Zero Billing' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Invoice', @level2type=N'COLUMN',@level2name=N'Invoice_PaymentMode'
GO
/****** Object:  Table [dbo].[Activity]    Script Date: 01/13/2010 11:04:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Activity](
	[Activity_Id] [int] IDENTITY(1,1) NOT NULL,
	[Activity_Action] [varchar](1000) NULL,
	[Activity_User] [int] NULL,
	[Activity_TimeStamp] [datetime] NULL,
 CONSTRAINT [PK_Action] PRIMARY KEY CLUSTERED 
(
	[Activity_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PettyExpense]    Script Date: 01/13/2010 11:05:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PettyExpense](
	[PettyExpense_Id] [int] IDENTITY(1,1) NOT NULL,
	[PettyExpense_Amount] [decimal](6, 2) NULL,
	[PettyExpense_Merchandise] [varchar](200) NULL,
	[PettyExpense_Quantity] [decimal](6, 2) NULL,
	[PettyExpense_ReceivedBy] [int] NULL,
	[PettyExpense_TimeStamp] [datetime] NULL,
 CONSTRAINT [PK_PettyExpense] PRIMARY KEY CLUSTERED 
(
	[PettyExpense_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Converter]    Script Date: 01/13/2010 11:04:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Converter](
	[Converter_Id] [int] IDENTITY(1,1) NOT NULL,
	[Converter_Menu] [int] NULL,
	[Converter_Inventory] [int] NULL,
	[Converter_InventoryQuantity] [decimal](6, 2) NULL,
	[Converter_Status] [int] NULL,
	[Converter_TimeStamp] [datetime] NULL,
 CONSTRAINT [PK_Component] PRIMARY KEY CLUSTERED 
(
	[Converter_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Event]    Script Date: 01/13/2010 11:04:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Event](
	[Event_Id] [int] IDENTITY(1,1) NOT NULL,
	[Event_Name] [varchar](100) NULL,
	[Event_FromTimeStamp] [datetime] NULL,
	[Event_ToTimeStamp] [datetime] NULL,
	[Event_Organizer] [int] NULL,
	[Event_Venue] [varchar](100) NULL,
	[Event_Status] [int] NULL,
	[Event_TimeStamp] [datetime] NULL,
 CONSTRAINT [PK_Event] PRIMARY KEY CLUSTERED 
(
	[Event_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Sales]    Script Date: 01/13/2010 11:05:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sales](
	[Sales_Id] [int] IDENTITY(1,1) NOT NULL,
	[Sales_Menu] [int] NULL,
	[Sales_Quantity] [decimal](6, 2) NULL,
	[Sales_Cost] [decimal](6, 2) NULL,
	[Sales_Source] [int] NULL,
	[Sales_Package] [int] NULL,
	[Sales_Status] [int] NULL,
	[Sales_TimeStamp] [datetime] NULL,
 CONSTRAINT [PK_Sales] PRIMARY KEY CLUSTERED 
(
	[Sales_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Purchase]    Script Date: 01/13/2010 11:05:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Purchase](
	[Purchase_Id] [int] IDENTITY(1,1) NOT NULL,
	[Purchase_Inventory] [int] NULL,
	[Purchase_Quantity] [decimal](6, 2) NULL,
	[Purchase_Status] [int] NULL,
	[Purchase_TimeStamp] [datetime] NOT NULL,
 CONSTRAINT [PK_Purchase] PRIMARY KEY CLUSTERED 
(
	[Purchase_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Menu]    Script Date: 01/13/2010 11:05:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Menu](
	[Menu_Id] [int] IDENTITY(1,1) NOT NULL,
	[Menu_Name] [varchar](100) NULL,
	[Menu_Category] [varchar](100) NULL,
	[Menu_SellingPrice] [decimal](6, 2) NULL,
	[Menu_Status] [int] NULL,
	[Menu_TimeStamp] [datetime] NULL,
 CONSTRAINT [PK_Material] PRIMARY KEY CLUSTERED 
(
	[Menu_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  StoredProcedure [dbo].[InventoryDelete]    Script Date: 01/13/2010 11:04:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InventoryDelete]
	@Id INT
AS
	BEGIN
		UPDATE Inventory SET Inventory_Status = 0
		WHERE Inventory_Id = @Id AND Inventory_Status = 1
		RETURN 1
	END
GO
/****** Object:  Table [dbo].[Content]    Script Date: 01/13/2010 11:04:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Content](
	[Content_Id] [int] IDENTITY(1,1) NOT NULL,
	[Content_Material] [int] NULL,
	[Content_Quantity] [decimal](6, 2) NULL,
	[Content_UnitPrice] [decimal](6, 2) NULL,
	[Content_Discount] [decimal](6, 2) NULL,
	[Content_Cost] [decimal](6, 2) NULL,
	[Content_Package] [int] NULL,
	[Content_Status] [int] NULL,
	[Content_TimeStamp] [datetime] NULL,
 CONSTRAINT [PK_Content] PRIMARY KEY CLUSTERED 
(
	[Content_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Inventory]    Script Date: 01/13/2010 11:04:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Inventory](
	[Inventory_Id] [int] IDENTITY(1,1) NOT NULL,
	[Inventory_Name] [varchar](100) NULL,
	[Inventory_BuyingPrice] [decimal](6, 2) NULL,
	[Inventory_Threshold] [decimal](6, 2) NULL,
	[Inventory_Quantity] [decimal](6, 2) NULL,
	[Inventory_PurchaseUnit] [varchar](100) NULL,
	[Inventory_ConversionUnit] [decimal](6, 2) NULL,
	[Inventory_Status] [int] NULL,
	[Inventory_TimeStamp] [datetime] NULL,
 CONSTRAINT [PK_Stock] PRIMARY KEY CLUSTERED 
(
	[Inventory_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  StoredProcedure [dbo].[SalesDelete]    Script Date: 01/13/2010 11:04:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SalesDelete]
	@Id INT,
	@Menu INT,
	@Quantity DECIMAL(6, 2),
	@Source INT,
	@SourceType VARCHAR(100)
AS
	DECLARE @Cost DECIMAL(6, 2)
	SET @Cost = (SELECT Menu_SellingPrice FROM Menu WHERE Menu_Id = @Menu AND 
Menu_Status = 1)
	DECLARE @OldQuantity DECIMAL(6, 2)
	DECLARE @SourceId INT
	DECLARE @Amount DECIMAL(6, 2)
	
	IF EXISTS(SELECT * FROM Source WHERE Source_Number = @Source AND 
Source_Type = @SourceType AND Source_Status = 1)
		BEGIN
			SET @SourceId = (SELECT Source_Id FROM Source  WHERE 
Source_Number = @Source AND Source_Type = @SourceType AND Source_Status = 1)		
			
		END
		
	BEGIN
		SET @Cost = (SELECT Sales_Cost FROM Sales WHERE Sales_Status = 1 
AND Sales_Id = @Id)			
		SET @Amount = (SELECT Source_AmountPayable FROM Source WHERE 
Source_Status = 1 AND Source_Id = @SourceId)
		
		UPDATE Sales SET Sales_Status = 0 
		WHERE Sales_Id = @Id AND Sales_Status = 1
		UPDATE Source SET Source_AmountPayable = (@Amount - @Cost)
		WHERE Source_Id = @SourceId	
		UPDATE Token SET Token_Status = 0 
		WHERE Token_Menu = @Menu AND Token_Source = @SourceId AND 
Token_Status = 1
		RETURN 1
	END
GO
/****** Object:  StoredProcedure [dbo].[TokenNew]    Script Date: 01/13/2010 11:04:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[TokenNew]
	@Type VARCHAR(50),
	@Menu INT,
	@Quantity DECIMAL(6, 2),
	@Source INT
AS
	INSERT INTO Token(Token_Type, Token_Menu, Token_Quantity, Token_Source, 
Token_Status, Token_TimeStamp) 
	VALUES (@Type, @Menu, @Quantity, @Source, 1, CURRENT_TIMESTAMP)
	RETURN 1
GO
/****** Object:  StoredProcedure [dbo].[InvoiceDelete]    Script Date: 01/13/2010 11:04:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InvoiceDelete]
	@Id INT,	
	@Source INT,
	@Reason VARCHAR(200)
AS
	BEGIN
		UPDATE Invoice SET Invoice_Status = 2, Invoice_Reason = @Reason
		WHERE Invoice_Id = @Id AND Invoice_Status = 1
		
		UPDATE Source SET Source_Status = 0 
		WHERE Source_Id = @Source AND Source_Status = 2
		
		UPDATE Sales SET Sales_Status = 0 
		WHERE Sales_Source = @Source AND Sales_Status = 1
		
		UPDATE Token SET Token_Status = 0
		WHERE Token_Source = @Source AND Token_Status = 1
		
		RETURN 1
	END
GO
/****** Object:  StoredProcedure [dbo].[InvoiceCloseCredit]    Script Date: 01/13/2010 11:04:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InvoiceCloseCredit]
	@Id INT,
	@ClientId INT,
	@Source INT
AS
	DECLARE @OldCredit DECIMAL(6, 2)
	DECLARE @Amount DECIMAL(6, 2)
	DECLARE @Tax DECIMAL(6, 2)
	DECLARE @Discount DECIMAL(6, 2)
	
	SET @OldCredit = (SELECT Contact_Credit FROM Contact WHERE Contact_Id = 
@ClientId)
	SET @Amount = (SELECT Invoice_Amount FROM Invoice WHERE Invoice_Id = @Id)
	SET @Tax = (SELECT Invoice_Tax FROM Invoice WHERE Invoice_Id = @Id)
	SET @Discount = (SELECT Invoice_Discount FROM Invoice WHERE Invoice_Id = 
@Id)
	
	
	UPDATE Contact SET Contact_Credit = @OldCredit + ( (@Amount - @Discount) + 
@Tax)
	WHERE Contact_Id = @ClientId
	
	UPDATE Invoice SET Invoice_PaymentMode = 'Credit', Invoice_Status = 0, 
Invoice_Client = @ClientId
	WHERE Invoice_Id = @Id
	
	UPDATE Source SET Source_Status = 0 
	WHERE Source_Id = @Source AND Source_Status = 2
	
	UPDATE Sales SET Sales_Status = 0 
	WHERE Sales_Source = @Source AND Sales_Status = 1
	
	UPDATE Token SET Token_Status = 0
	WHERE Token_Source = @Source AND Token_Status = 1
	
	RETURN 1
GO
/****** Object:  StoredProcedure [dbo].[InvoiceCloseZeroBilling]    Script Date: 01/13/2010 11:04:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InvoiceCloseZeroBilling]
	@Id INT,
	@Source INT
AS
	UPDATE Invoice SET Invoice_Discount = 100, Invoice_Status = 0, Invoice_PaymentMode = 'Zero Billing' 
	WHERE Invoice_Status = 1 AND Invoice_Id = @Id
	
	UPDATE Source SET Source_Status = 0 
	WHERE Source_Id = @Source AND Source_Status = 2
	
	UPDATE Sales SET Sales_Status = 0 
	WHERE Sales_Source = @Source AND Sales_Status = 1
	
	UPDATE Token SET Token_Status = 0
	WHERE Token_Source = @Source AND Token_Status = 1
	
	RETURN 1
GO
/****** Object:  StoredProcedure [dbo].[InvoiceClose]    Script Date: 01/13/2010 11:04:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InvoiceClose]
	@Id INT,
	@Source INT,
	@PaymentMode VARCHAR(100)
AS
	
	UPDATE Invoice SET Invoice_PaymentMode = @PaymentMode, Invoice_Status = 0
	WHERE Invoice_Id = @Id
	
	UPDATE Source SET Source_Status = 0 
	WHERE Source_Id = @Source AND Source_Status = 2
	
	UPDATE Sales SET Sales_Status = 0 
	WHERE Sales_Source = @Source AND Sales_Status = 1
	
	UPDATE Token SET Token_Status = 0
	WHERE Token_Source = @Source AND Token_Status = 1
		
	RETURN 1
GO
/****** Object:  StoredProcedure [dbo].[SalesNew]    Script Date: 01/13/2010 11:04:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SalesNew]
	@Id INT,
	@Menu INT,
	@Quantity DECIMAL(6, 2),
	@Source INT,
	@SourceType VARCHAR(100),
	@Package INT
AS
	DECLARE @Result INT
	DECLARE @Cost DECIMAL(6, 2)
	SET @Cost = (SELECT Menu_SellingPrice FROM Menu WHERE Menu_Id = @Menu AND 
Menu_Status = 1)
	DECLARE @OldQuantity DECIMAL(6, 2)
	DECLARE @SourceId INT
	DECLARE @Amount DECIMAL(6, 2)
	DECLARE @ConverterQuantity DECIMAL(6, 2)
	
	IF EXISTS(SELECT * FROM Source WHERE Source_Number = @Source AND Source_Type = @SourceType AND Source_Status = 1)
		BEGIN
			SET @SourceId = (SELECT Source_Id FROM Source  WHERE Source_Number = @Source AND Source_Type = @SourceType AND Source_Status = 1)					
		END
	ELSE
		BEGIN
			INSERT INTO Source(Source_Type, Source_Number, Source_AmountPayable, Source_Status, Source_TimeStamp) /* SourceType : Event / Table */
			VALUES(@SourceType, @Source, 0, 1, CURRENT_TIMESTAMP)
			SET @SourceId = SCOPE_IDENTITY()
		END
	
	BEGIN				
		IF EXISTS(SELECT * FROM Sales WHERE Sales_Menu = @Menu AND Sales_Source = @SourceId AND Sales_Status = 1)
			BEGIN
				SET @OldQuantity = (SELECT Sales_Quantity FROM Sales WHERE Sales_Menu = @Menu AND Sales_Source = @SourceId AND Sales_Status = 1)
				SET @Quantity = (@Quantity + @OldQuantity)
				
				UPDATE Sales SET Sales_Quantity = @Quantity, Sales_Cost = (@Cost * @Quantity), Sales_TimeStamp = CURRENT_TIMESTAMP
				WHERE Sales_Menu = @Menu AND Sales_Source = @SourceId AND Sales_Status = 1
			END
		ELSE
			BEGIN
				INSERT INTO Sales(Sales_Menu, Sales_Quantity, Sales_Cost, Sales_Source, Sales_Package, Sales_Status, Sales_TimeStamp)
				VALUES(@Menu, @Quantity, (@Cost * @Quantity), @SourceId, @Package, 1, CURRENT_TIMESTAMP)
			END
		
		SET @Amount = (SELECT SUM(Sales_Cost) FROM Sales WHERE Sales_Status = 1 AND Sales_Source = @SourceId)
		
		UPDATE Source SET Source_AmountPayable = @Amount
		WHERE Source_Id = @SourceId	
				
		RETURN 1
	END
GO
/****** Object:  StoredProcedure [dbo].[InvoiceNew]    Script Date: 01/13/2010 11:04:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InvoiceNew]
	@Id INT,
	@Amount DECIMAL(6, 2),
	@Discount DECIMAL(6, 2),
	@Tax DECIMAL(6, 2),
	@Type VARCHAR(50),
	@Source INT,
	@PaymentMode VARCHAR(100)
AS

	INSERT INTO Invoice(Invoice_Amount, Invoice_Discount, Invoice_Tax, Invoice_Type, Invoice_Source, Invoice_PaymentMode, Invoice_Status, Invoice_TimeStamp) 
	VALUES(@Amount, @Discount, @Tax, @Type, @Source, @PaymentMode, 1, 
CURRENT_TIMESTAMP)
	
	UPDATE Source SET Source_Status = 2
	WHERE Source_Id = @Source AND Source_Status = 1
	
	RETURN 1
GO
/****** Object:  StoredProcedure [dbo].[SalesEdit]    Script Date: 01/13/2010 11:04:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SalesEdit]
	@Id INT,
	@Menu INT,
	@Quantity DECIMAL(6, 2),
	@Source INT,
	@SourceType VARCHAR(100),
	@Package INT
AS
	DECLARE @Result INT
	DECLARE @Cost DECIMAL(6, 2)
	SET @Cost = (SELECT Menu_SellingPrice FROM Menu WHERE Menu_Id = @Menu AND Menu_Status = 1)
	DECLARE @OldQuantity DECIMAL(6, 2)
	DECLARE @SourceId INT
	DECLARE @Amount DECIMAL(6, 2)
	
	IF EXISTS(SELECT * FROM Source WHERE Source_Number = @Source AND Source_Type = @SourceType AND Source_Status = 1)
		BEGIN
			SET @SourceId = (SELECT Source_Id FROM Source  WHERE Source_Number = @Source AND Source_Type = @SourceType AND Source_Status = 1)				
		END
	ELSE
		BEGIN
			INSERT INTO Source(Source_Type, Source_Number, Source_AmountPayable, Source_Status, Source_TimeStamp) /* SourceType : Event / Table */			VALUES(@SourceType, @Source, 0, 1, CURRENT_TIMESTAMP)
			SET @SourceId = SCOPE_IDENTITY()
		END
		
	BEGIN			
		UPDATE Sales SET Sales_Menu = @Menu, Sales_Quantity = @Quantity, Sales_Cost = (@Cost * @Quantity), Sales_Source = @SourceId, Sales_Package = @Package 
		WHERE Sales_Id = @Id AND Sales_Status = 1
		
		SET @Amount = (SELECT SUM(Sales_Cost) FROM Sales WHERE Sales_Status = 1 AND Sales_Status = @SourceId)
		
		UPDATE Source SET Source_AmountPayable = @Amount
		WHERE Source_Id = @SourceId	
		RETURN 1
	END
GO
/****** Object:  StoredProcedure [dbo].[PackageNew]    Script Date: 01/13/2010 11:04:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[PackageNew]
	@Name VARCHAR(100),
	@Type VARCHAR(50),
	@Comments VARCHAR(200)
AS
	IF EXISTS(SELECT * FROM Package WHERE Package_Name = @Name AND 
Package_Type = @Type)
		RETURN 0
		
	ELSE
		BEGIN
			INSERT INTO Package(Package_Name, Package_Type, 
Package_Comments, Package_Status, Package_TimeStamp)
			VALUES(@Name, @Type, @Comments, 1, CURRENT_TIMESTAMP)
			RETURN 1
		END
GO
/****** Object:  StoredProcedure [dbo].[PackageEdit]    Script Date: 01/13/2010 11:04:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[PackageEdit]
	@Id INT,
	@Name VARCHAR(100),
	@Type VARCHAR(50),
	@Comments VARCHAR(200)
AS
	IF EXISTS(SELECT * FROM Package WHERE Package_Name = @Name AND Package_Id 
!= @Id)
		RETURN 0
	ELSE		
		BEGIN
			UPDATE Package SET Package_Name = @Name, Package_Type = 
@Type, Package_Comments = @Comments
			WHERE Package_Id = @Id AND Package_Status = 1
			RETURN 1
		END
GO
/****** Object:  StoredProcedure [dbo].[PackageDelete]    Script Date: 01/13/2010 11:04:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[PackageDelete]
	@Id INT
AS
	UPDATE Package SET Package_Status = 0
	WHERE Package_Id = @Id AND Package_Status = 1
	RETURN 1
GO
/****** Object:  StoredProcedure [dbo].[PettyCashNew]    Script Date: 01/13/2010 11:04:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[PettyCashNew]
	@Amount DECIMAL(6, 2),
	@GivenBy INT,
	@ReceivedBy INT
AS	
	DECLARE @Balance DECIMAL(6, 2)
	IF EXISTS(SELECT * FROM PettyCash)
		SET @Balance = (SELECT TOP 1 PettyCash_Balance FROM PettyCash 
ORDER BY PettyCash_Id DESC)
	ELSE
		SET @Balance = 0
		
	INSERT INTO PettyCash(PettyCash_Amount, PettyCash_Balance, 
PettyCash_GivenBy, PettyCash_ReceivedBy, PettyCash_TimeStamp) 
	VALUES(@Amount, (@Amount + @Balance), @GivenBy, @ReceivedBy, 
CURRENT_TIMESTAMP)
	
	RETURN 1
GO
/****** Object:  StoredProcedure [dbo].[PettyExpenseNew]    Script Date: 01/13/2010 11:04:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[PettyExpenseNew]
	@Amount DECIMAL(6, 2),
	@Merchandise VARCHAR(200),
	@Quantity DECIMAL(6, 2),
	@ReceivedBy INT
AS
	DECLARE @Balance DECIMAL(6, 2)
	DECLARE @Id INT
	SET @Balance = (SELECT TOP 1 PettyCash_Balance FROM PettyCash ORDER BY 
PettyCash_Id DESC)
	SET @Id = (SELECT TOP 1 PettyCash_Id FROM PettyCash ORDER BY PettyCash_Id 
DESC)
	
	INSERT INTO PettyExpense(PettyExpense_Amount, PettyExpense_Merchandise, 
PettyExpense_Quantity, PettyExpense_ReceivedBy, PettyExpense_TimeStamp) 
	VALUES(@Amount, @Merchandise, @Quantity, @ReceivedBy, CURRENT_TIMESTAMP)
	
	
	UPDATE PettyCash SET PettyCash_Balance = @Balance - @Amount
	WHERE PettyCash_Id = @Id
	
	RETURN 1
GO
/****** Object:  StoredProcedure [dbo].[CatalogEdit]    Script Date: 01/13/2010 11:04:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CatalogEdit]
	@Id INT,
	@Name VARCHAR(100),
	@Type VARCHAR(50)
AS	
	IF EXISTS(SELECT * FROM Catalog WHERE Catalog_Name = @Name AND 
Catalog_Type = @Type AND Catalog_Status = 1)
		RETURN 0
	ELSE
		BEGIN
			INSERT INTO Catalog(Catalog_Name, Catalog_Type, 
Catalog_Status, Catalog_TimeStamp) 
			VALUES(@Name, @Type, 1, CURRENT_TIMESTAMP)
			RETURN 1
		END
GO
/****** Object:  StoredProcedure [dbo].[CatalogNew]    Script Date: 01/13/2010 11:04:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CatalogNew]
	@Id INT,
	@Name VARCHAR(100),
	@Type VARCHAR(50)
AS
	BEGIN
		UPDATE Catalog SET Catalog_Name = @Name, Catalog_Type = @Type
		WHERE Catalog_Id = @Id AND Catalog_Status = 1
		Return 1
	END
GO
/****** Object:  StoredProcedure [dbo].[CatalogDelete]    Script Date: 01/13/2010 11:04:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CatalogDelete]
	@Id INT
AS
	BEGIN
			UPDATE Catalog SET Catalog_Status = 0
			WHERE Catalog_Id = @Id AND Catalog_Status = 1
			Return 1
	END
GO
/****** Object:  StoredProcedure [dbo].[ContactDelete]    Script Date: 01/13/2010 11:04:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ContactDelete]
	@Id INT
AS
	
	BEGIN
		UPDATE Contact SET Contact_Status = 0 
		WHERE Contact_Id = @Id AND Contact_Status = 1
		RETURN 1
	END
GO
/****** Object:  StoredProcedure [dbo].[ContactEdit]    Script Date: 01/13/2010 11:04:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ContactEdit]
	@Id INT,
	@Name VARCHAR(100),
	@UserName VARCHAR(100),
	@Password VARCHAR(100),
	@Type VARCHAR(100),
	@Phone VARCHAR(100),
	@Address VARCHAR(200),
	@Email VARCHAR(100),
	@OrganizationName VARCHAR(100),
	@OrganizationAddress VARCHAR(200),
	@OrganizationPhone VARCHAR(100)
AS
	BEGIN
		IF EXISTS(SELECT * FROM Contact WHERE (Contact_UserName = 
@UserName OR Contact_Phone = @Phone) AND Contact_Id != @Id)
			RETURN 0
		ELSE
			BEGIN
				UPDATE Contact SET Contact_Name = @Name, 
Contact_UserName = @UserName, Contact_Type = @Type, Contact_Phone = @Phone, 
Contact_Address = @Address, Contact_Email = @Email, Contact_OrganizationName = 
@OrganizationName, Contact_OrganizationAddress = @OrganizationAddress, 
Contact_OrganizationPhone = @OrganizationPhone
				WHERE Contact_Id = @Id AND Contact_Status = 1
				RETURN 1
			END
	END
GO
/****** Object:  StoredProcedure [dbo].[ContactNew]    Script Date: 01/13/2010 11:04:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ContactNew]
	@Id INT,
	@Name VARCHAR(100),
	@UserName VARCHAR(100),
	@Password VARCHAR(100),
	@Type VARCHAR(100),
	@Phone VARCHAR(100),
	@Address VARCHAR(200),
	@Email VARCHAR(100),
	@OrganizationName VARCHAR(100),
	@OrganizationAddress VARCHAR(200),
	@OrganizationPhone VARCHAR(100)
AS
	IF EXISTS(SELECT * FROM Contact WHERE Contact_UserName = @UserName OR 
Contact_Phone = @Phone)
		RETURN 0
	ELSE
		BEGIN
			INSERT INTO Contact(Contact_Name, Contact_UserName, 
Contact_Password, Contact_Type, Contact_Phone, Contact_Address, Contact_Email, 
Contact_OrganizationName, Contact_OrganizationAddress, Contact_OrganizationPhone, 
Contact_Credit, Contact_Status, Contact_TimeStamp) 
			VALUES(@Name, @UserName, @Password, @Type, @Phone, 
@Address, @Email, @OrganizationName, @OrganizationAddress, @OrganizationPhone, 0, 
1, CURRENT_TIMESTAMP)
			RETURN 1
		END
GO
/****** Object:  StoredProcedure [dbo].[ReceiptNew]    Script Date: 01/13/2010 11:04:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ReceiptNew] /*default type=Receipt*/
	@Amount DECIMAL(6, 2),
	@PaymentMode VARCHAR(100),
	@ClientId INT
AS
	INSERT INTO Invoice(Invoice_Amount, Invoice_Type, Invoice_PaymentMode, 
Invoice_Client, Invoice_Status, Invoice_TimeStamp)
	VALUES (@Amount, 'Receipt', @PaymentMode, @ClientId, 0, CURRENT_TIMESTAMP) 
	
	UPDATE Contact SET Contact_Credit  = (Contact_Credit - @Amount)
	WHERE Contact_Id = @ClientId
	
	RETURN 1
GO
/****** Object:  StoredProcedure [dbo].[ChangePassword]    Script Date: 01/13/2010 11:04:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ChangePassword]
	@Id INT,
	@OldPassword VARCHAR(100),
	@NewPassword VARCHAR(100)
AS
	
	IF EXISTS(SELECT * FROM Contact WHERE Contact_Id = @Id AND 
Contact_Password = @OldPassword)
		BEGIN
			UPDATE Contact SET Contact_Password = @NewPassword 
			WHERE Contact_Id = @Id
			RETURN 1
		END
	
	ELSE
		RETURN 0
GO
/****** Object:  StoredProcedure [dbo].[ReceiptDelete]    Script Date: 01/13/2010 11:04:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ReceiptDelete]
	@Id INT,
	@ClientId INT
AS
	DECLARE @Amount DECIMAL(6, 2)
	SET @Amount = (SELECT Invoice_Amount FROM Invoice WHERE Invoice_Id = @Id 
AND Invoice_Status = 0)
	
	UPDATE Contact SET Contact_Credit  = (Contact_Credit + @Amount)
	WHERE Contact_Id = @ClientId
	
	UPDATE Invoice SET Invoice_Status = 2
	WHERE Invoice_Id = @Id AND Invoice_Status = 0
	
	RETURN 1
GO
/****** Object:  StoredProcedure [dbo].[InvoiceEdit]    Script Date: 01/13/2010 11:04:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InvoiceEdit]
	@Id INT,
	@Amount DECIMAL(6, 2),
	@Discount DECIMAL(6, 2),
	@Tax DECIMAL(6, 2),
	@Type VARCHAR(100),
	@Source INT,
	@PaymentMode VARCHAR(100),
	@Status INT,
	@Reason VARCHAR(200)
AS
	BEGIN
		UPDATE Invoice SET Invoice_Amount = @Amount, Invoice_Discount = 
@Discount, Invoice_Tax = @Tax, Invoice_Type = @Type, Invoice_PaymentMode = 
@PaymentMode
		WHERE Invoice_Id = @Id AND Invoice_Status = 1
		RETURN 1
	END
GO
/****** Object:  StoredProcedure [dbo].[ActivityNew]    Script Date: 01/13/2010 11:04:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ActivityNew]
	@Action VARCHAR(1000),
	@User INT
AS	
	INSERT INTO Activity(Activity_Action, Activity_User, Activity_TimeStamp) 
	VALUES(@Action, @User, CURRENT_TIMESTAMP)
	Return 1
GO
/****** Object:  StoredProcedure [dbo].[ConverterNew]    Script Date: 01/13/2010 11:04:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ConverterNew]
	@Menu INT,
	@Inventory INT,
	@InventoryQuantity DECIMAL(6, 2)
AS
	IF EXISTS(SELECT * FROM Converter WHERE Converter_Menu = @Menu AND 
Converter_Inventory = @Inventory AND Converter_Status = 1)
		BEGIN
			UPDATE Converter SET Converter_InventoryQuantity = 
(@InventoryQuantity +  Converter_InventoryQuantity) WHERE
			Converter_Menu = @Menu AND Converter_Inventory = 
@Inventory AND Converter_Status = 1
		END
	ELSE
		BEGIN
			INSERT INTO Converter(Converter_Menu, Converter_Inventory, 
Converter_InventoryQuantity, Converter_Status, Converter_TimeStamp) 
			VALUES(@Menu, @Inventory, @InventoryQuantity, 1, 
CURRENT_TIMESTAMP)
			RETURN 1
		END
GO
/****** Object:  StoredProcedure [dbo].[ConverterEdit]    Script Date: 01/13/2010 11:04:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ConverterEdit]
	@Id INT,
	@Menu INT,
	@Inventory INT,
	@InventoryQuantity DECIMAL(6, 2)
AS
	IF EXISTS(SELECT * FROM Converter WHERE Converter_Menu = @Menu AND 
Converter_Inventory = @Inventory)
		RETURN 1
	ELSE
		BEGIN
			UPDATE Converter SET Converter_Menu = @Menu, 
Converter_Inventory = @Inventory, Converter_InventoryQuantity = @InventoryQuantity
			WHERE Converter_Id = @Id AND Converter_Status = 1
			RETURN 1
		END
GO
/****** Object:  StoredProcedure [dbo].[ConverterDelete]    Script Date: 01/13/2010 11:04:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ConverterDelete]
	@Id INT
AS
	BEGIN
		UPDATE Converter SET Converter_Status = 0
		WHERE Converter_Id = @Id AND Converter_Status = 1
		RETURN 1
	END
GO
/****** Object:  StoredProcedure [dbo].[EventNew]    Script Date: 01/13/2010 11:04:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[EventNew]
	@Name VARCHAR(100),
	@From DATETIME,
	@To DATETIME,
	@Organizer INT,
	@Venue VARCHAR(100)
AS
	IF EXISTS(SELECT * FROM Event WHERE Event_Name = @Name AND 
Event_FromTimeStamp = @From AND Event_ToTimeStamp = @To AND Event_Organizer = 
@Organizer)
		RETURN 0
	ELSE
		BEGIN
			INSERT INTO Event(Event_Name, Event_FromTimeStamp, 
Event_ToTimeStamp, Event_Organizer, Event_Venue, Event_Status, Event_TimeStamp) 
			VALUES(@Name, @From, @To, @Organizer, @Venue, 1, 
CURRENT_TIMESTAMP)
			RETURN 1
		END
GO
/****** Object:  StoredProcedure [dbo].[EventEdit]    Script Date: 01/13/2010 11:04:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[EventEdit]
	@Id INT,
	@Name VARCHAR(100),
	@From DATETIME,
	@To DATETIME,
	@Organizer INT,
	@Venue VARCHAR(100)
AS

	UPDATE Event SET Event_Name = @Name, Event_FromTimeStamp = @From, 
Event_ToTimeStamp = @To, Event_Organizer = @Organizer, Event_Venue = @Venue
	WHERE Event_Id = @Id AND Event_Status = 1
	RETURN 1
GO
/****** Object:  StoredProcedure [dbo].[EventDelete]    Script Date: 01/13/2010 11:04:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[EventDelete]
	@Id INT
AS
	UPDATE Event SET Event_Status = 0
	WHERE Event_Id = @Id AND Event_Status = 1
	RETURN 1
GO
/****** Object:  StoredProcedure [dbo].[PurchaseNew]    Script Date: 01/13/2010 11:04:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[PurchaseNew]
	@Inventory INT,
	@Quantity DECIMAL(6,2)
AS
	DECLARE @OldQuantity DECIMAL(6, 2)
	SET @OldQuantity = (SELECT Inventory_Quantity FROM Inventory WHERE Inventory_Id = @Inventory)
	DECLARE @ConversionUnit DECIMAL(6, 2)
	SET @ConversionUnit = (SELECT Inventory_ConversionUnit FROM Inventory WHERE Inventory_Id = @Inventory)
	
	INSERT INTO Purchase(Purchase_Inventory, Purchase_Quantity, Purchase_Status, Purchase_TimeStamp) 
	VALUES(@Inventory, @Quantity, 1, CURRENT_TIMESTAMP)
	
	UPDATE Inventory Set Inventory_Quantity = (@Quantity * @ConversionUnit) + @OldQuantity 
	WHERE Inventory_Id = @Inventory
	
	RETURN 1
GO
/****** Object:  StoredProcedure [dbo].[PurchaseEdit]    Script Date: 01/13/2010 11:04:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[PurchaseEdit]
	@Id INT,
	@Inventory INT,
	@Quantity DECIMAL(6,2)
AS
	DECLARE @OldQuantity DECIMAL(6, 2)
	SET @OldQuantity = (SELECT Inventory_Quantity FROM Inventory WHERE 
Inventory_Id = @Inventory)
	
	BEGIN
		UPDATE PURCHASE SET Purchase_Inventory = @Inventory, 
Purchase_Quantity = @Quantity
		WHERE Purchase_Id = @Id AND Purchase_Status = 1
		RETURN 1
	END
GO
/****** Object:  StoredProcedure [dbo].[PurchaseDelete]    Script Date: 01/13/2010 11:04:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[PurchaseDelete]
	@Id INT
AS
	BEGIN
		UPDATE PURCHASE SET Purchase_Status = 0
		WHERE Purchase_Id = @Id AND Purchase_Status = 1
		RETURN 1
	END
GO
/****** Object:  StoredProcedure [dbo].[MenuNew]    Script Date: 01/13/2010 11:04:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[MenuNew]
	@Id INT,
	@Name VARCHAR(100),
	@Category VARCHAR(100),
	@Price DECIMAL(6, 2)
AS
	BEGIN
		IF EXISTS(SELECT * FROM Menu WHERE Menu_Name = @Name)
			RETURN 0
		ELSE
			BEGIN
				INSERT INTO Menu(Menu_Name, Menu_Category, 
Menu_SellingPrice, Menu_Status, Menu_TimeStamp)
				VALUES(@Name, @Category, @Price, 1, 
CURRENT_TIMESTAMP)
				RETURN 1
			END
	END
GO
/****** Object:  StoredProcedure [dbo].[SelectItemByType]    Script Date: 01/13/2010 11:04:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SelectItemByType]
	@Type VARCHAR(100)	
AS
	
	IF(@Type = NULL)
		RETURN (SELECT COUNT(*) FROM Menu WHERE Menu_Status = 1)
	ELSE
		RETURN (SELECT COUNT(*) FROM Menu WHERE Menu_Category = @Type AND 
Menu_Status = 1)
GO
/****** Object:  StoredProcedure [dbo].[MenuEdit]    Script Date: 01/13/2010 11:04:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[MenuEdit]
	@Id INT,
	@Name VARCHAR(100),
	@Category VARCHAR(100),
	@Price DECIMAL(6, 2)
AS
	BEGIN
		IF EXISTS(SELECT * FROM Menu WHERE Menu_Name = @Name AND Menu_Id 
!= @Id)
				RETURN 0
		ELSE
			BEGIN
				UPDATE Menu SET Menu_Name = @Name, Menu_Category = 
@Category, Menu_SellingPrice = @Price
				WHERE Menu_Id = @Id AND Menu_Status = 1
				RETURN 1
			END
	END
GO
/****** Object:  StoredProcedure [dbo].[MenuDelete]    Script Date: 01/13/2010 11:04:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[MenuDelete]
	@Id INT
AS
	BEGIN
		UPDATE Menu SET Menu_Status = 0
		WHERE Menu_Id = @Id AND Menu_Status = 1
		RETURN 1
	END
GO
/****** Object:  StoredProcedure [dbo].[SpContent]    Script Date: 01/13/2010 11:04:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SpContent]
	@Id INT,
	@Material INT,
	@Quantity DECIMAL(6, 2),
	@UnitPrice DECIMAL(6, 2),
	@Discount DECIMAL(6, 2),
	@Cost DECIMAL(6, 2),
	@Package INT,	
	@Status INT,
	@Operation VARCHAR(50)
AS
	DECLARE @Result INT
	
	IF(@Operation = 'New')
		BEGIN
			IF EXISTS(SELECT * FROM Content WHERE Content_Material = 
@Material AND Content_Package = @Package)
				SET @Result = 0
			ELSE
				BEGIN
					INSERT INTO Content(Content_Material, 
Content_Quantity, Content_UnitPrice, Content_Discount, Content_Cost, 
Content_Package, Content_Status) 
					VALUES(@Material, @Quantity, @UnitPrice, 
@Discount, @Cost, @Package, 1)
					SET @Result = 1
				END
		END
	ELSE IF(@Operation = 'Edit')
		BEGIN
			UPDATE Content SET Content_Material = @Material, 
Content_Quantity = @Quantity, Content_UnitPrice = @UnitPrice, Content_Discount = 
@Discount, Content_Cost = @Cost, Content_Package = @Package
			WHERE Content_Id = @Id AND Content_Status = 1
			SET @Result = 1
		END
	ELSE IF(@Operation = 'Edit')
		BEGIN
			UPDATE Content SET Content_Status = 0
			WHERE Content_Id = @Id AND Content_Status = 1
			SET @Result = 1
		END
	
	RETURN @Result
GO
/****** Object:  StoredProcedure [dbo].[InventoryNew]    Script Date: 01/13/2010 11:04:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InventoryNew]
	@Name VARCHAR(100),
	@BuyingPrice DECIMAL(6, 2),
	@Threshold DECIMAL(6, 2),
	@PurchaseUnit VARCHAR(100),
	@ConversionUnit DECIMAL(6, 2)
AS
	IF EXISTS(SELECT * FROM Inventory WHERE Inventory_Name = @Name)
		RETURN 0
	ELSE
		BEGIN
			INSERT INTO Inventory(Inventory_Name, 
Inventory_BuyingPrice, Inventory_Threshold, Inventory_Quantity, 
Inventory_PurchaseUnit, Inventory_ConversionUnit, Inventory_Status, 
Inventory_TimeStamp)
			VALUES(@Name, @BuyingPrice, @Threshold,0, @PurchaseUnit, 
@ConversionUnit, 1, CURRENT_TIMESTAMP)
			
			RETURN 1
		END
GO
/****** Object:  StoredProcedure [dbo].[InventoryEdit]    Script Date: 01/13/2010 11:04:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InventoryEdit]
	@Id INT,
	@Name VARCHAR(100),
	@BuyingPrice DECIMAL(6, 2),
	@Threshold DECIMAL(6, 2),
	@PurchaseUnit VARCHAR(100),
	@ConversionUnit DECIMAL(6, 2)
AS
	IF EXISTS(SELECT * FROM Inventory WHERE Inventory_Name = @Name AND 
Inventory_Id != @Id)
		RETURN 0
	ELSE
		BEGIN
			UPDATE Inventory SET Inventory_Name = @Name, 
Inventory_BuyingPrice =  @BuyingPrice, Inventory_Threshold = @Threshold, 
Inventory_PurchaseUnit = @PurchaseUnit, Inventory_ConversionUnit = @ConversionUnit
			WHERE Inventory_Id = @Id AND Inventory_Status = 1
			RETURN 1
		END
GO
/****** Object:  StoredProcedure [dbo].[InventorySubtract]    Script Date: 01/13/2010 11:04:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InventorySubtract]
	@Id INT,
	@Quantity DECIMAL(6, 2)
AS
	UPDATE Inventory SET Inventory_Quantity = (Inventory_Quantity - @Quantity) 
	WHERE Inventory_Id = @Id
	RETURN 1
GO
/****** Object:  StoredProcedure [dbo].[InventoryAdd]    Script Date: 01/13/2010 11:04:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InventoryAdd]
	@Id INT,
	@Quantity DECIMAL(6, 2)
AS
	UPDATE Inventory SET Inventory_Quantity = (Inventory_Quantity + @Quantity) 
	WHERE Inventory_Id = @Id
	RETURN
GO
/* END DATABASE SCHEMA */

/* BEGIN CREATE USER  WITH PERMISSION */
IF NOT EXISTS (SELECT loginname FROM master.dbo.syslogins WHERE name = 'Checkers' 
AND dbname = 'Checkers')
BEGIN	
	CREATE LOGIN Checkers WITH PASSWORD = 'Checkers',DEFAULT_DATABASE=
[Checkers], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF
	EXEC SP_ADDSRVROLEMEMBER [Checkers], [sysadmin]
END

USE [Checkers]
CREATE USER Checkers FOR LOGIN Checkers
WITH DEFAULT_SCHEMA = Checkers
EXEC SP_ADDROLEMEMBER [db_owner], [Checkers]
/* END CREATE USER WITH PERMISSION */