USE [master]
GO
/****** Object:  Database [ABLE_TEMPLATE_DB]    Script Date: 02/12/2019 3:35:16 PM ******/
IF EXISTS (SELECT name FROM master.sys.sysdatabases WHERE name = 'ABLE_TEMPLATE_DB')
DROP DATABASE [ABLE_TEMPLATE_DB]
GO
/****** Object:  Database [ABLE_TEMPLATE_DB]    Script Date: 02/12/2019 4:58:28 PM ******/
CREATE DATABASE [ABLE_TEMPLATE_DB]
GO
ALTER DATABASE [ABLE_TEMPLATE_DB] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ABLE_TEMPLATE_DB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ABLE_TEMPLATE_DB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ABLE_TEMPLATE_DB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ABLE_TEMPLATE_DB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ABLE_TEMPLATE_DB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ABLE_TEMPLATE_DB] SET ARITHABORT OFF 
GO
ALTER DATABASE [ABLE_TEMPLATE_DB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ABLE_TEMPLATE_DB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ABLE_TEMPLATE_DB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ABLE_TEMPLATE_DB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ABLE_TEMPLATE_DB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ABLE_TEMPLATE_DB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ABLE_TEMPLATE_DB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ABLE_TEMPLATE_DB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ABLE_TEMPLATE_DB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ABLE_TEMPLATE_DB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [ABLE_TEMPLATE_DB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ABLE_TEMPLATE_DB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ABLE_TEMPLATE_DB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ABLE_TEMPLATE_DB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ABLE_TEMPLATE_DB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ABLE_TEMPLATE_DB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ABLE_TEMPLATE_DB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ABLE_TEMPLATE_DB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [ABLE_TEMPLATE_DB] SET  MULTI_USER 
GO
ALTER DATABASE [ABLE_TEMPLATE_DB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ABLE_TEMPLATE_DB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ABLE_TEMPLATE_DB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ABLE_TEMPLATE_DB] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [ABLE_TEMPLATE_DB] SET DELAYED_DURABILITY = DISABLED 
GO
USE [ABLE_TEMPLATE_DB]
GO
/****** Object:  User [l35732]    Script Date: 26/02/2020 1:03:49 PM ******/
CREATE USER [l35732] WITHOUT LOGIN WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  Table [dbo].[AccountClassification]    Script Date: 26/02/2020 1:03:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Recurring]    Script Date: 20/04/2020 12:13:02 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Recurring](
        [EntityID] [int] NULL,
        [TranType] [varchar](10) NULL,
        [StartDate] [datetime] NULL,
        [EndDate] [datetime] NULL,
        [Frequency] [varchar](50) NULL,
        [LastPosted] [datetime] NULL,
        [NotifyUserID] [int] NULL,
        [NotifyDate] [datetime] NULL,
        [AutomaticRecord] [bit] NOT NULL DEFAULT ((0))
) ON [PRIMARY]
GO
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Ingredient]    Script Date: 1/13/2021 10:27:42 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Ingredient](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ItemID] [int] NULL,
    [SalesID] [int] NULL,
	[Qty] [float] NULL,
	[PartNumber] [varchar](50) NULL,
	[Amount] [float] NULL,
	[TaxRate] [float] NULL,
	[TaxCode] [varchar](50) NULL,
	[TaxCollectedAccountID]  [varchar](50) NULL,
	[Cost] [float] NULL,
	[TotalCost] [float] NULL,
	[Description] [varchar](max) NULL,
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[AuditTrail]    Script Date: 26/02/2020 1:03:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[AuditTrail](
        [AuditTrailID] [int] NOT NULL,
        [AuditTypeID] [char](1) NULL,
        [TransactionNumber] [char](10) NULL,
        [ChangeDate] [date] NULL,
        [OriginalDate] [date] NULL,
        [WasThirteenthPeriod] [char](1) NULL,
        [IsReconciled] [char](1) NULL,
        [UserID] [int] NOT NULL,
        [Description] [varchar](500) NULL,
PRIMARY KEY CLUSTERED 
(
        [AuditTrailID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[BankDepositLines]    Script Date: 26/02/2020 1:03:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BankReconciliation](
        [BankReconID] [int] IDENTITY(1,1) NOT NULL,
        [BankAccountID] [int] NOT NULL,
        [StatementDate] [date] NULL,
        [LastStatementBalance] [float] NULL,
        [NewStatementBalance] [float] NULL,
        [IsReconciled] [bit] NULL,
        [UserID] [int] NULL,
        [EntryDate] [datetime] NULL,
        [ReconciledDate] [datetime] NULL,
        [LocationID] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BankReconciliationLines]    Script Date: 26/02/2020 1:03:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BankReconciliationLines](
        [BankReconLineID] [int] IDENTITY(1,1) NOT NULL,
        [BankReconID] [int] NULL,
        [JournalNumberID] [int] NULL,
        [IsCleared] [bit] NULL
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Budget]    Script Date: 06/05/2020 11:52AM Maricel ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Budget](
        [AccountID] [int] NULL,
        [FY] [int] NULL,
        [mo1] [float] NULL,
        [mo2] [float] NULL,
        [mo3] [float] NULL,
        [mo4] [float] NULL,
        [mo5] [float] NULL,
        [mo6] [float] NULL,
        [mo7] [float] NULL,
        [mo8] [float] NULL,
        [mo9] [float] NULL,
        [mo10] [float] NULL,
        [mo11] [float] NULL,
        [mo12] [float] NULL,
        [LocationID] [int] NOT NULL DEFAULT ((1))
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Contacts]    Script Date: 26/02/2020 1:03:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Contacts](
        [ContactID] [int] IDENTITY(1,1) NOT NULL,
        [Location] [int] NULL,
        [Street] [nvarchar](250) NULL,
        [City] [nvarchar](100) NULL,
        [State] [nvarchar](100) NULL,
        [Postcode] [nvarchar](50) NULL,
        [Country] [nvarchar](100) NULL,
        [Phone] [nvarchar](100) NULL,
        [Fax] [nvarchar](100) NULL,
        [Email] [nvarchar](150) NULL,
        [Website] [nvarchar](200) NULL,
        [ContactPerson] [nvarchar](200) NULL,
        [ProfileID] [int] NULL,
        [Comments] [nvarchar](500) NULL,
        [TypeOfContact] [varchar](255) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CostCentres]    Script Date: 26/02/2020 1:03:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CostCentres](
        [CostCentreID] [int] NOT NULL,
        [CostCentreName] [char](30) NULL,
        [CostCentreIdentification] [char](15) NULL,
        [CostCentreDescription] [varchar](500) NULL,
        [IsInactive] [char](1) NULL,
PRIMARY KEY CLUSTERED 
(
        [CostCentreID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Currency]    Script Date: 26/02/2020 1:03:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Currency](
        [CurrencyID] [int] IDENTITY(1,1) NOT NULL,
        [CurrencyCode] [char](5) NULL,
        [CurrencyName] [varchar](30) NULL,
        [ExchangeRate] [float] NULL,
        [CurrencySymbol] [char](5) NULL,
        [WholeNumberWord] [varchar](100) NULL,
        [DecimalWord] [varchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
        [CurrencyID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CustomList1]    Script Date: 26/02/2020 3:04:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CustomList1](
        [ID] [int] IDENTITY(1,1) NOT NULL,
        [RecordType] [varchar](20) NOT NULL,
        [List1Name] [varchar](100) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CustomList2]    Script Date: 26/02/2020 3:04:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CustomList2](
        [ID] [int] IDENTITY(1,1) NOT NULL,
        [RecordType] [varchar](20) NOT NULL,
        [List2Name] [varchar](100) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CustomList3]    Script Date: 26/02/2020 3:04:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CustomList3](
        [ID] [int] IDENTITY(1,1) NOT NULL,
        [RecordType] [varchar](20) NOT NULL,
        [List3Name] [varchar](100) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CustomNames]    Script Date: 26/02/2020 3:04:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CustomNames](
        [RecordType] [varchar](20) NOT NULL,
        [CList1Name] [varchar](50) NULL,
        [CList2Name] [varchar](50) NULL,
        [CList3Name] [varchar](50) NULL,
        [CField1Name] [varchar](50) NULL,
        [CField2Name] [varchar](50) NULL,
        [CField3Name] [varchar](50) NULL,
 CONSTRAINT [PK_CustomNames] PRIMARY KEY CLUSTERED 
(
        [RecordType] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[DataFileInformation]    Script Date: 26/02/2020 3:04:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[DataFileInformation](
        [CompanyName] [varchar](255) NULL,
        [Address] [varchar](255) NULL,
        [Phone] [varchar](20) NULL,
        [FaxNumber] [varchar](20) NULL,
        [Email] [varchar](255) NULL,
        [ABN] [varchar](14) NULL,
        [ABNBranch] [varchar](11) NULL,
        [ACN] [varchar](19) NULL,
        [SalesTaxNumber] [varchar](19) NULL,
        [GSTRegistrationNumber] [varchar](19) NULL,
        [PayeeNumber] [char](8) NULL,
        [CompanyRegistrationNumber] [varchar](150) NULL,
        [CurrentFinancialYear] [int] NULL,
        [LastMonthInFinancialYear] [varchar](10) NULL,
        [ConversionDate] [varchar](10) NULL,
        [PeriodsPerYear] [int] NULL,
        [BankCode] [char](3) NULL,
        [BankID] [char](3) NULL,
        [BSBCode] [char](3) NULL,
        [BankAccountNumber] [varchar](30) NULL,
        [BankAccountName] [varchar](150) NULL,
        [IsSelfBalancing] [char](3) NULL,
        [StatementParticulars] [char](3) NULL,
        [StatementCode] [char](3) NULL,
        [StatementReference] [char](3) NULL,
        [LastPurgeDate] [date] NULL,
        [LastBackupDate] [date] NULL,
        [DatabaseVersion] [char](9) NULL,
        [DataFileCountry] [char](3) NULL,
        [DriverBuildNumber] [int] NULL,
        [SerialNumber] [varchar](150) NULL,
        [CompanyFileNumber] [int] IDENTITY(1,1) NOT NULL,
        [UseRetailManagerLink] [char](1) NULL,
        [UseMultipleCurrencies] [char](1) NULL,
        [UseCostCentres] [char](1) NULL,
        [CostCentresRequired] [char](1) NULL,
        [UseSimplifiedTaxSystem] [char](1) NULL,
        [SimplifiedTaxSystemDate] [date] NULL,
        [UseSmallBusinessEntityConcession] [char](1) NULL,
        [SmallBusinessEntityConcessionDate] [date] NULL,
        [UseDailyAgeing] [char](1) NULL,
        [FirstAgeingPeriod] [int] NULL,
        [SecondAgeingPeriod] [int] NULL,
        [ThirdAgeingPeriod] [int] NULL,
        [IdentifyAgeByName] [char](1) NULL,
        [LockPeriodIsActive] [bit] NULL,
        [LockPeriodDate] [date] NULL,
        [LockThirteenthPeriod] [char](1) NULL,
        [DefaultCustomerTermsID] [int] NULL,
        [DefaultCustomerPriceLevelID] [char](3) NULL,
        [DefaultCustomerTaxCodeID] [int] NULL,
        [DefaultUseCustomerTaxCode] [char](1) NULL,
        [DefaultCustomerFreightTaxCodeID] [int] NULL,
        [DefaultCustomerCreditLimit] [float] NULL,
        [DefaultSupplierTermsID] [int] NULL,
        [DefaultSupplierTaxCodeID] [int] NULL,
        [DefaultUseSupplierTaxCode] [char](1) NULL,
        [DefaultSupplierFreightTaxCodeID] [int] NULL,
        [DefaultSupplierCreditLimit] [float] NULL,
        [InvoiceSubject] [varchar](255) NULL,
        [InvoiceMessage] [varchar](255) NULL,
        [IncludeInvoiceNumber] [char](1) NULL,
        [InvoiceQuoteSubject] [varchar](255) NULL,
        [InvoiceQuoteMessage] [varchar](255) NULL,
        [IncludeInvoiceQuoteNumber] [char](1) NULL,
        [InvoiceOrderSubject] [varchar](255) NULL,
        [InvoiceOrderMessage] [varchar](255) NULL,
        [IncludeInvoiceOrderNumber] [char](1) NULL,
        [PurchaseSubject] [varchar](255) NULL,
        [PurchaseMessage] [varchar](255) NULL,
        [IncludePurchaseNumber] [char](1) NULL,
        [PurchaseQuoteSubject] [varchar](255) NULL,
        [PurchaseQuoteMessage] [varchar](255) NULL,
        [IncludePurchaseQuoteNumber] [char](1) NULL,
        [PurchaseOrderSubject] [varchar](255) NULL,
        [PurchaseOrderMessage] [varchar](255) NULL,
        [IncludePurchaseOrderNumber] [char](1) NULL,
        [StatementSubject] [varchar](255) NULL,
        [StatementMessage] [varchar](255) NULL,
        [PaymentSubject] [varchar](255) NULL,
        [PaymentMessage] [varchar](255) NULL,
        [UseAuditTracking] [char](1) NULL,
        [UseCreditLimitWarning] [char](1) NULL,
        [LimitTypeID] [char](1) NULL,
        [ChangeControl] [varchar](15) NULL,
        [UseStandardCost] [char](1) NULL,
        [UseReceivablesFreight] [char](1) NULL,
        [UseReceivablesDeposits] [char](1) NULL,
        [UseReceivablesDiscounts] [char](1) NULL,
        [UseReceivablesLateFees] [char](1) NULL,
        [UsePayablesInventory] [char](1) NULL,
        [UsePayablesFreight] [char](1) NULL,
        [UsePayablesDeposits] [char](1) NULL,
        [UsePayablesDiscounts] [char](1) NULL,
        [UsePayablesLateFees] [char](1) NULL,
        [BegMonthInFinancialYear] [varchar](10) NULL,
        [CreationDate] [datetime] NULL DEFAULT (getutcdate()),
        [IsActive] [bit] NULL DEFAULT ((0)),
        [ActivationDate] [datetime] NULL,
		[Add1] [varchar](255) NULL,
		[Add2] [varchar](100) NULL,
		[Street] [varchar](100) NULL,
		[City] [varchar](100) NULL,
		[State] [varchar](100) NULL,
		[Country] [varchar](100) NULL,
		[POBox] [varchar](100) NULL,
		[ContactPerson] [varchar](100) NULL,
		[Reactivation] [varchar](255) NULL,
		[MaxLocation] [int]  NOT NULL CONSTRAINT [DF_DataFileInformation_MaxLocation]  DEFAULT ((1)),
		[MaxTerminal] [int]  NOT NULL CONSTRAINT [DF_DataFileInformation_MaxTerminal]  DEFAULT ((1)),
		[CompanyLogo] [varchar](max) NULL,
		[ImageType] [varchar](50) NULL,
 CONSTRAINT [PK__DataFile__4A9B611E1D8C0514] PRIMARY KEY CLUSTERED 
(
        [CompanyFileNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Forms]    Script Date: 26/02/2020 1:03:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Forms](
        [form_code] [nvarchar](20) NULL,
        [form_name] [nvarchar](100) NULL,
        [category] [nvarchar](50) NULL,
        [IsField] [bit] NULL CONSTRAINT [DF_Forms_IsUsed]  DEFAULT ((0)),
		[field_name] [nvarchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ContractPricing]    Script Date: 10/07/2020 1:08:23 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ContractPricing](
	[ContractID] [int] IDENTITY(1,1) NOT NULL,
	[ItemID] [int] NULL,
	[CustomerID] [int] NULL,
	[ContractPrice] [float] NULL,
	[ExpiryDate] [datetime] NULL,
	[IsExpiry] [bit] NULL,
 CONSTRAINT [PK_ContractTable] PRIMARY KEY CLUSTERED 
(
	[ContractID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[GiftCertificate]    Script Date: 30/06/2020 3:58:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[GiftCertificate](
        [ID] [int] IDENTITY(1,1) NOT NULL,
        [ItemID] [int] NOT NULL,
        [GCAmount] [float] NOT NULL CONSTRAINT [DF_GiftCertificate_GCAmount]  DEFAULT ((0)),
        [GCNumber] [varchar](150) NOT NULL,
        [StartDate] [datetime] NOT NULL,
        [EndDate] [datetime] NOT NULL,
        [IsUsed] [bit] NOT NULL CONSTRAINT [DF_GiftCertificate_IsUsed]  DEFAULT ((0)),
        [issuedSalesID] [int] NULL,
        [usedSalesID] [int] NULL,
        [promoid] [int] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Identifiers]    Script Date: 30/06/2020 3:58:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Identifiers](
        [IdentifierID] [char](3) NOT NULL,
        [Description] [char](250) NOT NULL,
        [ChangeControl] [char](5) NOT NULL,
 CONSTRAINT [PK_Identifiers] PRIMARY KEY CLUSTERED 
(
        [IdentifierID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Items]    Script Date: 26/02/2020 3:04:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Items](
        [ID] [int] IDENTITY(1,1) NOT NULL,
        [PartNumber] [varchar](100) NOT NULL,
        [ItemNumber] [varchar](100) NOT NULL,
        [SupplierItemNumber] [varchar](100) NULL,
        [ItemName] [varchar](100) NULL,
        [ItemDescription] [text] NULL CONSTRAINT [DF_Items_ItemDescription]  DEFAULT ('<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN"><HTML><HEAD><META content="text/html; charset=unicode" http-equiv=Content-Type><META name=GENERATOR content="MSHTML 11.00.10570.1001"></HEAD><BODY></BODY></HTML>'),
        [IsBought] [bit] NULL DEFAULT ((0)),
        [IsSold] [bit] NULL DEFAULT ((0)),
        [IsCounted] [bit] NULL DEFAULT ((0)),
        [COSAccountID] [varchar](100) NULL,
        [IncomeAccountID] [varchar](100) NULL,
        [AssetAccountID] [varchar](100) NULL,
        [CList1] [int] NULL,
        [CList2] [int] NULL,
        [CList3] [int] NULL,
        [CField1] [varchar](100) NULL,
        [CField2] [varchar](100) NULL,
        [CField3] [varchar](100) NULL,
        [BuyingUOM] [varchar](20) NULL,
        [QtyPerBuyingUOM] [float] NULL,
        [SupplierID] [int] NULL,
        [PurchaseTaxCode] [varchar](20) NULL,
        [SellingUOM] [varchar](20) NULL,
        [QtyPerSellingUnit] [float] NULL,
        [SalesTaxCode] [varchar](20) NULL,
        [IsAutoBuild] [bit] NULL,
        [AddedBy] [int] NULL,
        [DateCreated] [datetime] NULL,
        [IsInactive] [bit] NOT NULL CONSTRAINT [DF_Items_IsInactive]  DEFAULT ((0)),
        [CategoryID] [int] NULL,
        [isMain] [bit] NOT NULL CONSTRAINT [DF_Items_isMain]  DEFAULT ((0)),
        [BrandName] [varchar](255) NULL,
        [ItemDescriptionSimple] [varchar](255) NULL,
        [BundleType] [varchar](50) NULL,
 CONSTRAINT [PK_Items] PRIMARY KEY CLUSTERED 
(
        [PartNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ItemsAdjustment]    Script Date: 26/02/2020 3:04:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ItemsAdjustment](
        [ItemAdjID] [int] IDENTITY(1,1) NOT NULL,
        [LocationID] [int] NOT NULL,
        [ItemAdjNumber] [varchar](20) NULL,
        [TransactionDate] [datetime] NULL,
        [EntryDate] [datetime] NULL,
        [UserID] [int] NULL,
        [Memo] [varchar](255) NULL,
        [Type] [nchar](10) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ItemsAdjustmentLines]    Script Date: 26/02/2020 3:04:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ItemsAdjustmentLines](
        [ItemAdjLineID] [int] IDENTITY(1,1) NOT NULL,
        [ItemAdjID] [int] NULL,
        [ItemID] [int] NULL,
        [Qty] [float] NULL,
        [UnitCostEx] [float] NULL,
        [AmountEx] [float] NULL,
        [AccountID] [varchar](100) NULL,
        [JobID] [int] NULL,
        [LineMemo] [varchar](100) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ItemsAutoBuild]    Script Date: 26/02/2020 3:04:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ItemsAutoBuild](
        [ItemID] [int] NULL,
        [PartItemID] [int] NULL,
        [PartItemQty] [float] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ItemsCostPrice]    Script Date: 26/02/2020 1:03:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ItemsCostPrice](
        [ItemID] [int] NULL,
        [LocationID] [int] NOT NULL,
        [LastCostEx] [float] NULL,
        [StandardCostEx] [float] NULL,
        [AverageCostEx] [float] NULL,
        [PrevAverageCostEx] [float] NULL  
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ItemsCount]    Script Date: 26/02/2020 1:03:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ItemsCount](
        [ItemCountID] [int] IDENTITY(1,1) NOT NULL,
        [ItemCountNumber] [varchar](20) NULL,
        [LocationID] [int] NOT NULL,
        [ExpenseAccountID] [varchar](100) NULL,
        [UserID] [int] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ItemsCountLines]    Script Date: 26/02/2020 3:04:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ItemsCountLines](
        [ItemID] [int] NULL,
        [ExpectedQty] [float] NULL,
        [CountedQty] [float] NULL,
        [VarianceQty] [float] NULL,
        [UntiCostEx] [float] NULL,
        [CountedValue] [float] NULL,
        [VarianceValue] [float] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ItemsQty]    Script Date: 26/02/2020 1:03:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ItemsQty](
        [ItemID] [int] NULL,
        [LocationID] [int] NOT NULL,
        [BegQty] [float] NULL,
        [OnHandQty] [float] NULL,
        [MinQty] [float] NULL,
        [ReOrderQty] [float] NULL,
        [CommitedQty] [float] NULL,
        [MaxQty] [float] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ItemsSellingPrice]    Script Date: 26/02/2020 1:03:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ItemsSellingPrice](
        [ItemID] [int] NOT NULL,
        [LocationID] [int] NOT NULL,
        [Level0] [float] NULL,
        [Level1] [float] NULL,
        [Level2] [float] NULL,
        [Level3] [float] NULL,
        [Level4] [float] NULL,
        [Level5] [float] NULL,
        [Level6] [float] NULL,
        [Level7] [float] NULL,
        [Level8] [float] NULL,
        [Level9] [float] NULL,
        [Level10] [float] NULL,
        [Level11] [float] NULL,
        [Level12] [float] NULL,
        [Level0QtyDiscount] [float] NULL,
        [Level1QtyDiscount] [float] NULL,
        [Level2QtyDiscount] [float] NULL,
        [Level3QtyDiscount] [float] NULL,
        [Level4QtyDiscount] [float] NULL,
        [Level5QtyDiscount] [float] NULL,
        [Level6QtyDiscount] [float] NULL,
        [Level7QtyDiscount] [float] NULL,
        [Level8QtyDiscount] [float] NULL,
        [Level9QtyDiscount] [float] NULL,
        [Level10QtyDiscount] [float] NULL,
        [Level11QtyDiscount] [float] NULL,
        [Level12QtyDiscount] [float] NULL,
        [SalesPrice0] [float] NULL CONSTRAINT [DF_ItemsSellingPrice_SalesPrice0]  DEFAULT ((0)),
        [SalesPrice1] [float] NULL CONSTRAINT [DF_ItemsSellingPrice_SalesPrice1]  DEFAULT ((0)),
        [SalesPrice2] [float] NULL CONSTRAINT [DF_ItemsSellingPrice_SalesPrice2]  DEFAULT ((0)),
        [SalesPrice3] [float] NULL CONSTRAINT [DF_ItemsSellingPrice_SalesPrice3]  DEFAULT ((0)),
        [SalesPrice4] [float] NULL CONSTRAINT [DF_ItemsSellingPrice_SalesPrice4]  DEFAULT ((0)),
        [SalesPrice5] [float] NULL CONSTRAINT [DF_ItemsSellingPrice_SalesPrice5]  DEFAULT ((0)),
        [SalesPrice6] [float] NULL CONSTRAINT [DF_ItemsSellingPrice_SalesPrice6]  DEFAULT ((0)),
        [SalesPrice7] [float] NULL CONSTRAINT [DF_ItemsSellingPrice_SalesPrice7]  DEFAULT ((0)),
        [SalesPrice8] [float] NULL CONSTRAINT [DF_ItemsSellingPrice_SalesPrice8]  DEFAULT ((0)),
        [SalesPrice9] [float] NULL CONSTRAINT [DF_ItemsSellingPrice_SalesPrice9]  DEFAULT ((0)),
        [SalesPrice10] [float] NULL CONSTRAINT [DF_ItemsSellingPrice_SalesPrice10]  DEFAULT ((0)),
        [SalesPrice11] [float] NULL CONSTRAINT [DF_ItemsSellingPrice_SalesPrice11]  DEFAULT ((0)),
        [SalesPrice12] [float] NULL CONSTRAINT [DF_ItemsSellingPrice_SalesPrice12]  DEFAULT ((0)),
        [StartSaleDate] [datetime] NULL,
        [EndSalesDate] [datetime] NULL,
        [CalculationBasis] [varchar](50) NULL,
        [CostBasis] [float] NULL,
        [RoundingMethod] [varchar](100) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ItemTransaction]    Script Date: 18/07/2020 11:56:58 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ItemTransaction](
        [TransactionDate] [datetime] NULL,
        [EntryDate] [datetime] NULL,
        [ItemID] [int] NULL,
        [TransactionQty] [float] NULL,
        [QtyAdjustment] [float] NULL,
        [CostEx] [float] NULL,
        [TotalCostEx] [float] NULL,
        [TranType] [nchar](10) NULL,
        [SourceTranID] [int] NULL,
        [UserID] [int] NULL,
		[LocationID] [int] NOT NULL DEFAULT ((1)),
        [isStockTake] [bit] NULL CONSTRAINT [DF_ItemTransaction_isStockTake]  DEFAULT ((0))
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[JobBudget]    Script Date: 26/02/2020 1:03:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[JobBudget](
        [JobID] [int] NULL,
        [AccountID] [int] NULL,
        [Budget] [float] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[JobOpeningBalance]    Script Date: 26/02/2020 1:03:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[JobOpeningBalance](
        [JobID] [int] NULL,
        [AccountID] [int] NULL,
        [OpeningJobBalance] [float] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Jobs]    Script Date: 26/02/2020 1:03:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Jobs](
        [JobID] [int] IDENTITY(1,1) NOT NULL,
        [JobCode] [varchar](255) NOT NULL,
        [JobName] [varchar](255) NOT NULL,
        [JobDescription] [varchar](500) NULL,
        [IsInactive] [bit] NULL,
        [ContactName] [varchar](255) NULL,
        [Manager] [varchar](255) NULL,
        [PercentCompleted] [float] NULL,
        [StartDate] [date] NULL,
        [FinishDate] [date] NULL,
        [CustomerID] [int] NULL,
        [Income] [float] NULL,
        [Cost] [float] NULL,
        [Expense] [float] NULL,
        [IsHeader] [char](1) NULL,
        [ParentJobID] [int] NULL,
        [LocationID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
        [JobID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Journal]    Script Date: 26/02/2020 3:04:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Journal](
        [JournalNumberID] [int] IDENTITY(1,1) NOT NULL,
        [JournalNumber] [int] NULL,
        [TransactionDate] [datetime] NULL,
        [EntryDate] [datetime] NULL CONSTRAINT [DF__Journal__EntryDa__59FA5E80]  DEFAULT (getutcdate()),
        [Memo] [varchar](500) NULL,
        [AccountID] [varchar](100) NULL,
        [DebitAmount] [float] NULL,
        [CreditAmount] [float] NULL,
        [JobID] [varchar](255) NULL,
        [AllocationMemo] [varchar](500) NULL,
        [Category] [varchar](255) NULL,
        [TransactionNumber] [varchar](30) NULL,
        [Type] [nchar](10) NULL,
        [IsCleared] [bit] NULL CONSTRAINT [DF__Journal__IsClear__5AEE82B9]  DEFAULT ((0)),
        [IsDeposited] [bit] NULL CONSTRAINT [DF__Journal__IsDepos__5BE2A6F2]  DEFAULT ((0)),
        [LocationID] [int] NOT NULL CONSTRAINT [DF_Journal_LocationID]  DEFAULT ((1)),  
        [EntityID] [int] NULL,
 CONSTRAINT [PK_Journal] PRIMARY KEY CLUSTERED 
(
        [JournalNumberID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[LoyaltyMember]    Script Date: 30/06/2020 3:58:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[LoyaltyMember](
	[ID] [int] IDENTITY(1,1) NOT NULL,
        [ProfileID] [int] NOT NULL,
        [Number] [varchar](100) NOT NULL,
        [Name] [varchar](100) NULL,
        [Street] [varchar](100) NULL,
        [City] [varchar](100) NULL,
        [State] [varchar](100) NULL,
        [PostCode] [varchar](20) NULL,
        [Country] [varchar](100) NULL,
        [Phone] [varchar](50) NULL,
        [Fax] [varchar](50) NULL,
        [Email] [varchar](100) NULL,
        [StartDate] [datetime] NULL,
        [EndDate] [datetime] NULL,
        [IsActive] [bit] NOT NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Payment]    Script Date: 18/07/2020 11:37:24 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Payment](
        [PaymentID] [int] IDENTITY(1,1) NOT NULL,
        [ProfileID] [int] NULL,
        [TotalAmount] [float] NULL,
        [Memo] [nvarchar](500) NULL,
        [PaymentFor] [nvarchar](30) NULL,
        [AccountID] [varchar](100) NULL,
        [UserID] [int] NULL,
        [PaymentMethodID] [int] NULL,
        [TransactionDate] [datetime] NULL,
        [PaymentNumber] [nvarchar](50) NULL,
        [LocationID] [int] NOT NULL CONSTRAINT [DF_Payment_LocationID]  DEFAULT ((1)),
        [PaymentAuthorisationNumber] [varchar](255) NULL,
        [PaymentCardNumber] [varchar](255) NULL,
        [PaymentNameOnCard] [varchar](255) NULL,
        [PaymentExpirationDate] [varchar](20) NULL,
        [PaymentCardNotes] [varchar](255) NULL,
        [PaymentBSB] [varchar](255) NULL,
        [PaymentBankAccountNumber] [varchar](255) NULL,
        [PaymentBankAccountName] [varchar](255) NULL,
        [PaymentChequeNumber] [varchar](25) NULL,
        [PaymentBankNotes] [varchar](255) NULL,
        [PaymentNotes] [varchar](255) NULL,
        [SessionID] [int] NULL,
		[Source] [nvarchar](10) NULL,
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PaymentDetails]    Script Date: 18/07/2020 11:56:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PaymentDetails](
        [PaymentDetailsID] [int] IDENTITY(1,1) NOT NULL,
        [PaymentMethod] [varchar](255) NULL,
        [PaymentCardNumber] [varchar](255) NULL,
        [PaymentNameOnCard] [varchar](255) NULL,
        [PaymentExpirationDate] [varchar](20) NULL,
        [CardNotes] [varchar](255) NULL,
        [PaymentBSB] [varchar](255) NULL,
        [PaymentBankAccountNumber] [varchar](255) NULL,
        [PaymentBankAccountName] [varchar](255) NULL,
        [PaymentChequeNumber] [varchar](25) NULL,
        [BankNotes] [varchar](500) NULL,
        [PaymentAuthorisationNumber] [varchar](255) NULL,
        [PaymentNotes] [varchar](500) NULL,
        [PaymentID] [int] NULL,
        [PaymentMethodID] [int] NULL,
        [PaymentGCNo] [varchar](150) NULL,
        [PaymentGCNotes] [varchar](500) NULL
PRIMARY KEY CLUSTERED 
(
        [PaymentDetailsID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PaymentLines]    Script Date: 26/02/2020 3:04:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PaymentLines](
        [PaymentLineID] [int] IDENTITY(1,1) NOT NULL,
        [PaymentID] [int] NOT NULL,
        [EntityID] [int] NULL,
        [Amount] [float] NULL,
        [EntryDate] [datetime] NULL DEFAULT (getutcdate())
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PaymentMethods]    Script Date: 26/02/2020 3:04:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PaymentMethods](
        [PaymentMethod] [varchar](255) NOT NULL,
        [GLAccountCode] [varchar](100) NULL,
        [id] [int] IDENTITY(1,1) NOT NULL,
PRIMARY KEY CLUSTERED 
(
        [PaymentMethod] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PaymentTypes]    Script Date: 26/02/2020 3:04:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PaymentTypes](
        [PaymentTypeID] [char](1) NOT NULL,
        [Description] [char](30) NOT NULL,
 CONSTRAINT [PK_PaymentTypes] PRIMARY KEY CLUSTERED 
(
        [PaymentTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PointsRedemption]    Script Date: 25/06/2020 1:54:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PointsRedemption](
        [id] [int] IDENTITY(1,1) NOT NULL,
        [itemid] [int] NOT NULL,
        [customerid] [int] NOT NULL,
        [redeemedpoints] [float] NULL,
        [type] [varchar](50) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Terminal]    Script Date: 11/4/2020 2:24:14 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Terminal](
	[TerminalID] [int] IDENTITY(1,1) NOT NULL,
	[TerminalName] [varchar](max) NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

/****** Object:  Table [dbo].[Preference]    Script Date: 26/02/2020 3:04:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Preference](
        [PreferenceID] [int] IDENTITY(1,1) NOT NULL,
        [LocalCurrency] [varchar](5) NULL,
        [IsTranEditable] [bit] NOT NULL CONSTRAINT [DF_Preference_IsTranEditable]  DEFAULT ((1)),
        [GroupUndepFund] [bit] NOT NULL CONSTRAINT [DF_Preference_GroupUndepFund]  DEFAULT ((0)),
        [IsItemPriceInclusive] [bit] NOT NULL CONSTRAINT [DF_Preference_IsItemPriceInclusive]  DEFAULT ((0)),
        [KeepQuotations] [bit] NOT NULL CONSTRAINT [DF_Preference_KeepQuotations]  DEFAULT ((0)),
        [CustomerMandatory] [bit]  NOT NULL CONSTRAINT [DF_Preference_CustomerMandatory]  DEFAULT ((1)),
        [DefaultCustomerID] [int] NULL,
        [TradeDebtorGLCode] [varchar](50) NULL,
        [TradeCreditorGLCode] [varchar](50) NULL,
        [IsTaxInclusive] [bit] NOT NULL,
        [PurchaseFreightGLCode] [varchar](50) NOT NULL CONSTRAINT [DF_Preference_PurchaseFreightGLCode]  DEFAULT (('0')),
        [SalesFreightGLCode] [varchar](50) NOT NULL CONSTRAINT [DF_Preference_SalesFreightGLCode]  DEFAULT (('0')),
        [SalesDepositGLCode] [varchar](50) NOT NULL CONSTRAINT [DF_Preference_SalesDepositGLCode]  DEFAULT (('0')),
        [PurchasePaymentGLCode] [varchar](50) NOT NULL CONSTRAINT [DF_Preference_PurchasePaymentGLCode]  DEFAULT (('0')),
        [StandardShift] [varchar](10)NOT NULL CONSTRAINT [DF_Preference_StandardShift]  DEFAULT (('08:00')),
		[AutoEndSession] [bit] NULL CONSTRAINT [DF_Preference_AutoEndSession]  DEFAULT ((1)),
        [RestoDineInShipping] [int] NULL DEFAULT ((0)),
	    [RestoDeliveryShipping] [int] NULL DEFAULT ((0)),
	    [RestoTakeAwayShipping] [int] NULL DEFAULT ((0)),
 CONSTRAINT [PK_Preference] PRIMARY KEY CLUSTERED 
(
        [PreferenceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Profile]    Script Date: 26/02/2020 3:04:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Profile](
        [ID] [int] IDENTITY(1,1) NOT NULL,
        [ProfileIDNumber] [varchar](255) NULL,
        [Name] [varchar](255) NULL,
        [Designation] [varchar](255) NULL,
        [IsInactive] [char](1) NULL CONSTRAINT [DF_Profile_IsInactive]  DEFAULT ((0)),
        [UseProfileTaxCode] [char](1) NULL DEFAULT ((0)),
        [CurrentBalance] [float] NULL CONSTRAINT [DF_Profile_CurrentBalance]  DEFAULT ((0)),
        [TotalDeposits] [float] NULL CONSTRAINT [DF_Profile_TotalDeposits]  DEFAULT ((0)),
        [LastPaymentDate] [date] NULL,
        [MethodOfPaymentID] [varchar](255) NULL,
        [PaymentCardNumber] [varchar](255) NULL,
        [PaymentNameOnCard] [varchar](255) NULL,
        [PaymentExpirationDate] [date] NULL,
        [PaymentBankBranch] [varchar](255) NULL,
        [PaymentNotes] [varchar](500) NULL,
        [HourlyBillingRate] [float] NULL CONSTRAINT [DF_Profile_HourlyBillingRate]  DEFAULT ((0)),
        [ShippingMethodID] [varchar](255) NULL,
        [ProfileSince] [date] NULL DEFAULT (getutcdate()),
        [Type] [varchar](255) NOT NULL,
        [ABN] [varchar](255) NULL,
        [ABNBranch] [varchar](255) NULL,
        [TaxIDNumber] [varchar](255) NULL,
        [GSTIDNumber] [varchar](255) NULL,
        [PaymentBSB] [char](7) NULL,
        [PaymentBankAccountName] [varchar](255) NULL,
        [PaymentBankAccountNumber] [int] NULL,
        [FreightTaxCode] [varchar](10) NULL,
        [TaxCode] [varchar](10) NULL,
        [IncomeAccountID] [varchar](255) NULL,
        [LastSaleDate] [date] NULL,
        [TotalReceivableDays] [int] NULL,
        [TotalPaidInvoices] [int] NULL,
        [HighestInvoiceAmount] [float] NULL DEFAULT ((0)),
        [HighestReceivableAmount] [float] NULL DEFAULT ((0)),
        [SellingNotes] [varchar](500) NULL,
        [ExpenseAccountID] [varchar](255) NULL,
        [LastPuchaseDate] [date] NULL,
        [TotalPayableDays] [int] NULL DEFAULT ((0)),
        [TotalPaidPurchases] [int] NULL DEFAULT ((0)),
        [HighestPurchaseAmount] [float] NULL DEFAULT ((0)),
        [HighestPayableAmount] [float] NULL DEFAULT ((0)),
        [SupplierNotes] [varchar](500) NULL,
        [TermsOfPayment] [varchar](10) NULL CONSTRAINT [DF_Profile_TermsOfPayment]  DEFAULT ('CASH'),
        [CreditLimit] [float] NULL CONSTRAINT [DF__Profile__CreditL__2838E5BA]  DEFAULT ((0)),
        [BalanceDueDays] [int] NULL CONSTRAINT [DF_Profile_BalanceDueDays]  DEFAULT ((0)),
        [BalanceDueDate] [int] NULL,
        [EarlyPaymentDiscountPercent] [float] NULL CONSTRAINT [DF_Profile_EarlyPaymentDiscountPercent]  DEFAULT ((0)),
        [LatePaymentChargePercent] [float] NULL CONSTRAINT [DF_Profile_LatePaymentChargePercent]  DEFAULT ((0)),
        [DiscountDays] [int] NULL,
        [DiscountDate] [int] NULL,
        [VolumeDiscount] [float] NULL CONSTRAINT [DF_Profile_VolumeDiscount]  DEFAULT ((0)),
        [ContactID] [int] NULL,
        [LocationID] [int] NULL CONSTRAINT [DF_Profile_LocationID]  DEFAULT ((1)),
        [ItemPriceLevel] [int]  NULL CONSTRAINT [DF_Profile_ItemPriceLevel]  DEFAULT ((0)),
 CONSTRAINT [PK_Profile] PRIMARY KEY CLUSTERED 
(
        [ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PromotionItems]    Script Date: 25/06/2020 1:54:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PromotionItems](
        [id] [int] IDENTITY(1,1) NOT NULL,
        [itemid] [int] NOT NULL,
        [requiredpoints] [float] NULL,
        [promotype] [varchar](40) NULL,
        [quantity] [int] NULL,
        [promoid] [int] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PurchaseLines]    Script Date: 26/02/2020 3:04:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[PurchaseLines](
        [PurchaseLineID] [int] IDENTITY(1,1) NOT NULL,
        [PurchaseID] [int] NULL,
        [JobID] [int] NULL,
        [EntityID] [int] NULL,
        [EntryDate] [datetime] NULL,
        [TransactionDate] [datetime] NULL,
        [OrderQty] [int] NULL,
        [ReceiveQty] [int] NULL,
        [UnitPrice] [float] NULL,
        [ActualUnitPrice] [float] NULL,
        [DiscountPercent] [float] NULL,
        [SubTotal] [float] NULL,
        [TotalAmount] [float] NULL,
        [Description] [nvarchar](1000) NULL,
        [TaxCode] [nvarchar](10) NULL,
        [LineMemo] [nvarchar](1000) NULL,
        [TaxAmount] [float] NULL,
        [TaxPaidAccountID] [varchar](100) NULL,
        [TaxRate] [float] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO

/****** Object:  Table [dbo].[Purchases]    Script Date: 18/07/2020 11:56:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Purchases](
        [PurchaseID] [int] IDENTITY(1,1) NOT NULL,
        [SupplierID] [int] NULL,
        [UserID] [int] NULL,
        [TermsReferenceID] [int] NULL,
        [ShippingContactID] [int] NULL,
        [BillingContactID] [int] NULL,
        [PurchaseType] [nvarchar](50) NULL,
        [LayoutType] [nvarchar](50) NULL,
        [PurchaseNumber] [nvarchar](50) NULL,
        [EntryDate] [datetime] NULL,
        [TransactionDate] [datetime] NULL,
        [SubTotal] [float] NULL,
        [FreightSubTotal] [float] NULL,
        [GrandTotal] [float] NULL,
        [TotalPaid] [float] NULL,
        [TotalDue] [float] NULL,
        [SupplierINVNumber] [nvarchar](50) NULL,
        [PurchaseReference] [nvarchar](50) NULL,
        [ShippingMethodID] [int] NULL,
        [PromiseDate] [datetime] NULL,
        [Memo] [nvarchar](1000) NULL,
        [Comments] [nvarchar](500) NULL,
        [POStatus] [nvarchar](50) NULL,
        [ClosedDate] [datetime] NULL,
        [TaxTotal] [float] NULL,
        [FreightTax] [float] NULL,
        [IsTaxInclusive] [nchar](1) NULL,
        [LocationID] [int] NOT NULL,
        [FreightTaxCode] [nchar](10) NULL,
        [FreightTaxRate] [float] NULL,
		[ApprovedBy] [int] NULL,
		[ApprovedDate] [datetime]  NULL DEFAULT (getutcdate())
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ReceiveItems]    Script Date: 21/07/2020 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[ReceiveItems](
	[ReceiveItemID] [int] IDENTITY(1,1) NOT NULL,
	[SupplierID] [int] NULL,
	[UserID] [int] NULL,
	[TermsReferenceID] [int] NULL,
	[ShippingContactID] [int] NULL,
	[BillingContactID] [int] NULL,
	[PurchaseID] [int] NULL,
	[ReceiveItemNumber] [nvarchar](50) NULL,
	[EntryDate] [datetime] NULL DEFAULT (getutcdate()),
	[TransactionDate] [datetime] NULL,
	[SubTotal] [float] NULL DEFAULT ((0.0)),
	[FreightSubTotal] [float] NULL DEFAULT ((0.0)),
	[GrandTotal] [float] NULL DEFAULT ((0.0)),
	[TotalPaid] [float] NULL,
	[TotalDue] [float] NULL,
	[SupplierINVNumber] [nvarchar](50) NULL,
	[ReceiveItemReference] [nvarchar](50) NULL,
	[ShippingMethodID] [int] NULL,
	[PromiseDate] [datetime] NULL,
	[Memo] [nvarchar](1000) NULL,
	[Comments] [nvarchar](500) NULL,
	[POStatus] [nvarchar](50) NULL,
	[ClosedDate] [datetime] NULL,
	[TaxTotal] [float] NULL DEFAULT ((0.0)),
	[FreightTax] [float] NULL DEFAULT ((0.0)),
	[IsTaxInclusive] [nchar](1) NULL,
	[LocationID] [int] NOT NULL DEFAULT ((1)),
	[FreightTaxCode] [nchar](10) NULL,
	[FreightTaxRate] [float] NULL DEFAULT ((0.0))
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO

/****** Object:  Table [dbo].[ReceiveItemsLines]    Script Date: 21/07/2020 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[ReceiveItemsLines](
	[ReceiveItemLineID] [int] IDENTITY(1,1) NOT NULL,
	[ReceiveItemID] [int] NULL,
	[JobID] [int] NULL,
	[EntityID] [int] NULL,
	[EntryDate] [datetime] NULL DEFAULT (getutcdate()),
	[TransactionDate] [datetime] NULL,
	[OrderQty] [int] NULL DEFAULT ((0)),
	[ReceiveQty] [int] NULL DEFAULT ((0)),
	[UnitPrice] [float] NULL DEFAULT ((0)),
	[ActualUnitPrice] [float] NULL DEFAULT ((0)),
	[DiscountPercent] [float] NULL DEFAULT ((0)),
	[SubTotal] [float] NULL DEFAULT ((0)),
	[TotalAmount] [float] NULL DEFAULT ((0)),
	[Description] [nvarchar](1000) NULL,
	[TaxCode] [nvarchar](10) NULL,
	[LineMemo] [nvarchar](1000) NULL,
	[TaxAmount] [float] NULL DEFAULT ((0)),
	[TaxPaidAccountID] [varchar](100) NULL,
	[TaxRate] [float] NULL DEFAULT ((0))
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[RecordJournal]    Script Date: 26/02/2020 1:03:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[RecordJournal](
        [RecordJournalID] [int] IDENTITY(1,1) NOT NULL,
        [RecordJournalNumber] [varchar](20) NOT NULL,
        [TotalDebit] [float] NULL,
        [TotalCredit] [float] NULL,
        [TransactionDate] [datetime] NULL,
        [EntryDate] [datetime] NULL,
        [PayfromAccount] [char](1) NULL,
        [ElectronicPayment] [char](1) NULL,
        [Memo] [varchar](500) NULL,
        [IsTaxInclusive] [char](1) NULL,
        [CurrencyID] [int] NULL,
        [TransactionExchangeRate] [float] NULL,
        [CostCentreID] [int] NULL,
        [Type] [nchar](10) NULL,
        [UserID] [int] NULL,
        [LocationID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
        [RecordJournalID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO

/****** Object:  Table [dbo].[RecordJournalLine]    Script Date: 26/02/2020 3:04:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[RecordJournalLine](
        [RecordJournalLineID] [int] IDENTITY(1,1) NOT NULL,
        [RecordJournalID] [int] NOT NULL,
        [TransactionDate] [datetime] NULL,
        [EntryDate] [datetime] NULL,
        [AccountID] [varchar](20) NOT NULL,
        [Debit] [float] NULL,
        [Credit] [float] NULL,
        [JobID] [int] NOT NULL,
        [TaxCode] [varchar](20) NULL,
        [LineMemo] [varchar](500) NULL,
        [TaxExclusiveAmount] [float] NULL,
        [TaxInclusiveAmount] [float] NULL,
        [TransactionExchangeRate] [float] NULL,
        [TaxAccountID] [varchar](20) NULL,
        [CurrencyID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
        [RecordJournalLineID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Sales]    Script Date: 26/02/2020 3:04:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sales](
        [SalesID] [int] IDENTITY(1,1) NOT NULL,
        [CustomerID] [int] NULL,
        [UserID] [int] NULL,
        [TermsReferenceID] [int] NULL,
        [ShippingContactID] [int] NULL,
        [BillingContactID] [int] NULL,
        [SalesNumber] [nvarchar](50) NULL,
        [SalesType] [nvarchar](50) NULL,
        [InvoiceType] [nvarchar](50) NULL,
        [LayoutType] [nvarchar](50) NULL,
        [TransactionDate] [datetime] NULL,
        [EntryDate] [datetime] NULL DEFAULT (getutcdate()),
        [PromiseDate] [datetime] NULL,
        [ShippingMethodID] [int] NULL,
        [InvoiceStatus] [nvarchar](50) NULL,
        [SubTotal] [float] NULL CONSTRAINT [DF_Sales_SubTotal]  DEFAULT ((0.0)),
        [TaxTotal] [float] NULL CONSTRAINT [DF_Sales_TaxTotal]  DEFAULT ((0.0)),
        [FreightSubTotal] [float] NULL CONSTRAINT [DF_Sales_FreightSubTotal]  DEFAULT ((0.0)),
        [FreightTax] [float] NULL CONSTRAINT [DF_Sales_FreightTax]  DEFAULT ((0.0)),
        [GrandTotal] [float] NULL CONSTRAINT [DF_Sales_GrandTotal]  DEFAULT ((0.0)),
        [TotalPaid] [float] NULL CONSTRAINT [DF_Sales_TotalPaid]  DEFAULT ((0.0)),
        [TotalDue] [float] NULL CONSTRAINT [DF_Sales_TotalDue]  DEFAULT ((0.0)),
        [IsTaxInclusive] [nchar](1) NULL,
        [Memo] [nvarchar](1000) NULL,
        [Comments] [nvarchar](500) NULL,
        [CustomerPONumber] [nvarchar](50) NULL,
        [SalesReference] [nvarchar](50) NULL,
        [ClosedDate] [datetime] NULL,
        [LocationID] [int] NOT NULL DEFAULT ((1)),
        [TaxRate] [float] NULL,
        [FreightTaxCode] [nchar](10) NULL,
        [FreightTaxRate] [float] NULL,
        [SalesPersonID] [int] NULL,
        [SessionID] [int] NULL,
        [TableNumber] [varchar](100) NULL,
        [OrderStatus] [varchar](100) NULL DEFAULT ('NA')
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SalesLines]    Script Date: 26/02/2020 1:03:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[SalesLines](
        [SalesLineID] [int] IDENTITY(1,1) NOT NULL,
        [SalesID] [int] NULL,
        [JobID] [int] NULL,
        [TransactionDate] [datetime] NULL,
        [EntryDate] [datetime] NULL DEFAULT (getutcdate()),
        [TaxCode] [nvarchar](10) NULL,
        [Description] [nvarchar](1000) NULL,
        [OrderQty] [int] NULL CONSTRAINT [DF_SalesLines_OrderQty]  DEFAULT ((0)),
        [ShipQty] [int] NULL CONSTRAINT [DF_SalesLines_ShipQty]  DEFAULT ((0)),
        [UnitPrice] [float] NULL CONSTRAINT [DF_SalesLines_UnitPrice]  DEFAULT ((0.0)),
        [ActualUnitPrice] [float] NULL CONSTRAINT [DF_SalesLines_ActualUnitPrice]  DEFAULT ((0.0)),
        [DiscountPercent] [float] NULL CONSTRAINT [DF_SalesLines_DiscountPercent]  DEFAULT ((0.0)),
        [SubTotal] [float] NULL CONSTRAINT [DF_SalesLines_SubTotal]  DEFAULT ((0.0)),
        [TaxAmount] [float] NULL CONSTRAINT [DF_SalesLines_TaxAmount]  DEFAULT ((0.0)),
        [TotalAmount] [float] NULL CONSTRAINT [DF_SalesLines_TotalAmount]  DEFAULT ((0.0)),
        [LineMemo] [nvarchar](1000) NULL,
        [EntityID] [int] NULL,
        [TaxCollectedAccountID] [varchar](100) NULL,
        [CostPrice] [float] NULL,
        [TotalCost] [float] NULL,
        [TaxRate] [float] NULL,
		[PromoID] [int] NULL  DEFAULT ((0)),
        [KitchenStatus] [varchar](100) NULL DEFAULT ('NA')
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ShippingMethods]    Script Date: 26/02/2020 1:03:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ShippingMethods](
        [ShippingMethod] [varchar](255) NOT NULL,
        [ShippingID] [int] IDENTITY(1,1) NOT NULL,
PRIMARY KEY CLUSTERED 
(
        [ShippingMethod] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SubAccountTypes]    Script Date: 26/02/2020 3:04:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SubAccountTypes](
        [SubAccountTypeID] [varchar](3) NOT NULL,
        [Description] [varchar](500) NOT NULL,
        [AccountClassificationID] [varchar](4) NOT NULL,
 CONSTRAINT [PK_SubAccountTypes] PRIMARY KEY CLUSTERED 
(
        [SubAccountTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SystemAuditTrail]    Script Date: 26/02/2020 3:04:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SystemAuditTrail](
        [id] [bigint] IDENTITY(1,1) NOT NULL,
        [UserID] [int] NOT NULL,
        [AuditDate] [datetime] NOT NULL CONSTRAINT [DF_SystemAuditTrail_AuditDate]  DEFAULT (getutcdate()),
        [FormCode] [varchar](50) NOT NULL,
        [AuditAction] [text] NULL,
        [AffectedRecordID] [nvarchar](50) NULL,
        [OldData] [text] NULL,
        [NewData] [text] NULL,
        [LocationID] [int] NOT NULL CONSTRAINT [DF_SystemAuditTrail_LocationID]  DEFAULT ((1))
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TaxCodes]    Script Date: 26/02/2020 3:04:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TaxCodes](
        [TaxCode] [varchar](50) NOT NULL,
        [TaxCodeDescription] [varchar](500) NULL,
        [TaxPercentageRate] [float] NULL,
        [TaxThreshold] [float] NULL,
        [TaxCodeTypeID] [varchar](3) NULL,
        [TaxCollectedAccountID] [varchar](30) NULL,
        [TaxPaidAccountID] [varchar](30) NULL,
        [LinkedCardID] [int] NULL,
 CONSTRAINT [PK_TaxCodes] PRIMARY KEY CLUSTERED 
(
        [TaxCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Terms]    Script Date: 26/02/2020 3:04:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Terms](
        [TermsID] [int] IDENTITY(1,1) NOT NULL,
        [LatePaymentChargePercent] [float] NULL,
        [EarlyPaymentDiscountPercent] [float] NULL,
        [TermsOfPaymentID] [varchar](20) NOT NULL,
        [DiscountDays] [int] NULL,
        [BalanceDueDays] [int] NULL,
        [ImportPaymentIsDue] [int] NULL,
        [DiscountDate] [int] NULL,
        [BalanceDueDate] [int] NULL,
        [SalesPurchaseID] [int] NULL,
        [TransactionSrc] [nvarchar](20) NULL,
        [VolumeDiscount] [float] NULL,
        [ActualDueDate] [datetime] NULL,
        [ActualDiscountDate] [datetime] NULL,
 CONSTRAINT [PK__Terms__C05EBE00646BAF87] PRIMARY KEY CLUSTERED 
(
        [TermsID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TermsOfPayment]    Script Date: 26/02/2020 3:04:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TermsOfPayment](
        [TermsOfPaymentID] [varchar](10) NOT NULL,
        [Description] [varchar](500) NOT NULL,
PRIMARY KEY CLUSTERED 
(
        [TermsOfPaymentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TransactionSeries]    Script Date: 26/02/2020 3:04:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TransactionSeries](
        [LocationID] [int] NOT NULL DEFAULT ((1)),
        [JournalEntrySeries] [varchar](50) NULL,
        [JournalEntryPrefix] [nchar](10) NULL,
        [SalesOrderSeries] [varchar](50) NULL,
        [SalesOrderPrefix] [nchar](10) NULL,
        [SalesQuoteSeries] [varchar](50) NULL,
        [SalesQuotePrefix] [nchar](10) NULL,
        [SalesInvoiceSeries] [varchar](50) NULL,
        [SalesInvoicePrefix] [nchar](10) NULL,
        [PurchaseOrderSeries] [varchar](50) NULL,
        [PurchaseOrderPrefix] [nchar](10) NULL,
        [PurchaseQuoteSeries] [varchar](50) NULL,
        [PurchaseQuotePrefix] [nchar](10) NULL,
        [PurchaseBillSeries] [varchar](50) NULL,
        [PurchaseBillPrefix] [nchar](10) NULL,
        [ReceivedItemsSeries] [varchar](50) NULL,
        [ReceivedItemsPrefix] [nchar](10) NULL,
        [BuildItemsSeries] [varchar](50) NULL,
        [BuildItemsPrefix] [nchar](10) NULL,
        [PaymentSeries] [varchar](50) NULL,
        [PaymentPrefix] [nchar](10) NULL,
        [BillsPaymentSeries] [varchar](50) NULL,
        [BillsPaymentPrefix] [nchar](10) NULL,
        [SaleLayBySeries] [varchar](50) NULL,
        [SaleLayByPrefix] [nchar](10) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[User_Access]    Script Date: 26/02/2020 3:04:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User_Access](
        [user_id] [bigint] NULL,
        [form_code] [nvarchar](20) NULL,
        [u_view] [bit] NULL,
        [u_add] [bit] NULL,
        [u_edit] [bit] NULL,
        [u_delete] [bit] NULL,
        [date_lastmodified] [datetime] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO

/****** Object:  Table [dbo].[Sessions]    Script Date: 16/07/2020 3:31:01 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Sessions](
	[SessionID] [int] IDENTITY(1,1) NOT NULL,
	[SessionKey] [varchar](max) NULL,
	[SessionStart] [datetime] NULL,
	[SessionEnd] [datetime] NULL,                                               
	[SessionStatus] [varchar](50) NULL,
	[OpeningFund] [float] NULL CONSTRAINT [DF_Sessions_OpeningFund]  DEFAULT ((0)),
	[FloatFund] [float] Null CONSTRAINT [DF_Sessions_FloatFund]  DEFAULT ((0)),
	[UserID] [int] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

/****** Object:  Table [dbo].[Users]    Script Date: 26/02/2020 1:03:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[user_id] [bigint] IDENTITY(1,1) NOT NULL,
	[user_name] [nvarchar](50) NULL,
	[user_pwd] [nvarchar](255) NULL,
	[user_fullname] [nvarchar](100) NULL,
	[user_department] [nvarchar](100) NULL,
	[user_inactive] [bit] NULL,
	[date_created] [datetime] NULL,
	[date_lastmodified] [datetime] NULL,
	[date_lastaccess] [datetime] NULL,
	[IsSalesperson] [bit] NULL,
	[IsSupervisor] [bit] NULL,
	[IsTechnician] [bit] NULL,
	[IsAdministrator] [bit] NULL
) ON [PRIMARY]
GO


/****** Object:  Table [dbo].[UserSpecialRights]    Script Date: 26/02/2020 1:03:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserSpecialRights](
	[user_id] [bigint] IDENTITY(1,1) NOT NULL,
	[Is_salesperson] [bit] DEFAULT ((1)) NULL,
	[Is_supervisor] [bit] DEFAULT ((1)) NULL,
	[S_edit_customer_cl] [bit] DEFAULT ((1)) NULL,
	[S_override_customer_cl] [bit] DEFAULT ((1)) NULL,
	[S_edit_price_level] [bit] DEFAULT ((1)) NULL,
	[S_allow_quote] [bit] DEFAULT ((1)) NULL,
	[S_allow_order] [bit] DEFAULT ((1)) NULL,
	[S_allow_ar_invoice] [bit] DEFAULT ((1)) NULL,
	[S_allow_cash_invoice] [bit] DEFAULT ((1)) NULL,
	[S_allow_layby] [bit] DEFAULT ((1)) NULL,
	[S_edit_completed_invoice] [bit] DEFAULT ((1)) NULL,
	[S_void_invoice] [bit] DEFAULT ((1)) NULL,
	[S_remove_invoice_line] [bit] DEFAULT ((1)) NULL,
	[S_edit_previous_day_invoice] [bit] DEFAULT ((1)) NULL,
	[S_allow_discount] [bit] DEFAULT ((1)) NULL,
	[S_allow_price_change] [bit] DEFAULT ((1)) NULL,
	[S_allow_returns] [bit] DEFAULT ((1)) NULL,
	[S_allow_overselling] [bit] DEFAULT ((1)) NULL,
	[S_allow_sale_below_cost] [bit] DEFAULT ((1)) NULL,
	[S_show_cost_profit] [bit] DEFAULT ((1)) NULL,
	[S_allow_open_session] [bit] DEFAULT ((1)) NULL,
	[S_allow_close_session] [bit] DEFAULT ((1)) NULL,
	[S_allow_float_in] [bit] DEFAULT ((1)) NULL,
	[S_allow_float_out] [bit] DEFAULT ((1)) NULL,
	[S_print_customer_statement] [bit] DEFAULT ((1)) NULL,
	[P_approve_po] [bit] DEFAULT ((1)) NULL,
	[P_receive_po] [bit] DEFAULT ((1)) NULL,
	[P_undo_receive_po] [bit] DEFAULT ((1)) NULL,
	[I_show_cost] [bit] DEFAULT ((1)) NULL,
	[I_edit_cost] [bit] DEFAULT ((1)) NULL,
	[I_show_price] [bit] DEFAULT ((1)) NULL,
	[I_edit_price] [bit] DEFAULT ((1)) NULL,
	[I_override_average_cost] [bit] DEFAULT ((1)) NULL
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Images]    Script Date: 03/06/2020 12:20:58 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Images](
    [ImageID] [int] IDENTITY(1,1) NOT NULL,
    [ProfileID] [int] NULL,
    [Specimen] [varbinary](max) NULL,
    [LocationID] [int] NULL,
    [Description] [nvarchar](100) NULL,
    [ItemID] [int] NULL,
	[ImageType] [varchar](100) NULL,
 CONSTRAINT [PK_Images] PRIMARY KEY CLUSTERED 
(
[ImageID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO

/****** Object:  Table [dbo].[Barcodes]    Script Date: 18/06/2020 1:04:42 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Barcodes](
        [BarcodeID] [int] IDENTITY(1,1) NOT NULL,
        [BarcodeData] [varchar](max) NULL,
        [ItemID] [int] NULL,
        [BarcodeType] [varchar](max) NULL,
 CONSTRAINT [PK_Barcodes] PRIMARY KEY CLUSTERED 
(
        [BarcodeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[PaymentTender](
        [ID] [int] IDENTITY(1,1) NOT NULL,
        [Amount] [float] NULL,
        [PaymentMethodID] [int] NULL,
        [PaymentID] [int] NULL,
 CONSTRAINT [PK_PaymentTender] PRIMARY KEY CLUSTERED 
(
        [ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

/****** Object:  Table [dbo].[PointsExchangeRate]    Script Date: 27/06/2020 9:50:48 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[PointsExchangeRate](
        [ID] [int]  IDENTITY(1,1) NOT NULL,
        [CustomerID] [int] NULL,
        [PointsValue] [float] NULL,
        [AmountValue] [float] NULL,
 CONSTRAINT [PK_PointsExchangeRate] PRIMARY KEY CLUSTERED 
(
        [ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


/****** Object:  Table [dbo].[AccumulatedPoints]    Script Date: 22/06/2020 10:59:48 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[AccumulatedPoints](
        [ID] [int] IDENTITY(1,1) NOT NULL,
        [PromoID] [int] NULL,
        [TransactionDate] [datetime] NULL,
        [PointsAccumulated] [float] NULL,
        [CustomerID] [int] NULL,
        [SalesLineID] [int] NULL,
        [ItemID] [int] NULL,
        [RedemptionType] [varchar](50) NULL,
        [RedeemID] [int] NULL,
 CONSTRAINT [PK_AccumulatedPoints] PRIMARY KEY CLUSTERED 
(
        [ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

/****** Object:  Table [dbo].[Promos]    Script Date: 22/06/2020 11:03:13 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Promos](
        [PromoID] [int] IDENTITY(1,1) NOT NULL,
        [PromoCode] [varchar](50) NULL,
        [isActive] [bit] NULL,
        [PointsValue] [float] NULL,
        [PointAccumulationCriteria] [varchar](100) NULL,
        [StartDate] [datetime] NULL,
        [EndDate] [datetime] NULL,
        [RuleCriteria] [varchar](max) NULL,
        [RuleCriteriaID] [varchar](max) NULL,
        [PromotionType] [varchar](max) NULL,
 CONSTRAINT [PK_Promos] PRIMARY KEY CLUSTERED 
(
        [PromoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF

/****** Object:  Table [dbo].[Category]    Script Date: 02/07/2020 10:32:52 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Category](
        [CategoryID] [int] IDENTITY(1,1) NOT NULL,
        [MainCategoryID] [int] NULL,
        [CategoryCode] [varchar](50) NULL,
        [Description] [varchar](250) NULL,
        [IncomeGLCode] [varchar](50) NULL,
        [COSGLCode] [varchar](50) NULL,
        [InventoryGLCode] [varchar](50) NULL,
        [ItemType] [varchar](50) NULL,
        [ShowInMenu] [bit] NULL DEFAULT ((0)),
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
        [CategoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

/****** Object:  Table [dbo].[CountPerSession]    Script Date: 24/07/2020 2:34:31 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CountPerSession](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SessionID] [int] NULL,
	[PaymentMethodID] [int] NULL,
	[TotalCount] [float] NULL,
 CONSTRAINT [PK_CountPerSession] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

/****** Object:  Table [dbo].[PriceChange]    Script Date: 08/07/2020 1:24:15 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[PriceChange](
	[ChangeID] [int] IDENTITY(1,1) NOT NULL,
	[ItemID] [int] NULL,
	[PriceBefore] [float] NULL,
	[PriceAfter] [float] NULL,
	[ChangeDate] [datetime] NULL,
	[UserID] [int] NULL,
	[PriceLevel] [varchar](50) NULL,
	[CalcMethod] [varchar](100) NULL,
	[PercentChange] [float] NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO
INSERT [dbo].[Terminal]([TerminalName])VALUES( N'Terminal1')
SET IDENTITY_INSERT [dbo].[Currency] ON 

INSERT [dbo].[Currency] ([CurrencyID], [CurrencyCode], [CurrencyName], [ExchangeRate], [CurrencySymbol], [WholeNumberWord], [DecimalWord]) VALUES (1, N'C-1  ', N'PNG Kina', 1, N'PGK  ', N'Kina', N'Toea')
SET IDENTITY_INSERT [dbo].[Currency] OFF

SET IDENTITY_INSERT [dbo].[PointsExchangeRate] ON 

INSERT [dbo].[PointsExchangeRate] ([ID], [CustomerID], [PointsValue], [AmountValue]) VALUES (1, -1, 1, 1)
SET IDENTITY_INSERT [dbo].[PointsExchangeRate] OFF

SET IDENTITY_INSERT [dbo].[DataFileInformation] ON 

INSERT [dbo].[DataFileInformation] ([CompanyName], [Address], [Phone], [FaxNumber], [Email], [ABN], [ABNBranch], [ACN], [SalesTaxNumber], [GSTRegistrationNumber], [PayeeNumber], [CompanyRegistrationNumber], [CurrentFinancialYear], [LastMonthInFinancialYear], [ConversionDate], [PeriodsPerYear], [BankCode], [BankID], [BSBCode], [BankAccountNumber], [BankAccountName], [IsSelfBalancing], [StatementParticulars], [StatementCode], [StatementReference], [LastPurgeDate], [LastBackupDate], [DatabaseVersion], [DataFileCountry], [DriverBuildNumber], [SerialNumber], [CompanyFileNumber], [UseRetailManagerLink], [UseMultipleCurrencies], [UseCostCentres], [CostCentresRequired], [UseSimplifiedTaxSystem], [SimplifiedTaxSystemDate], [UseSmallBusinessEntityConcession], [SmallBusinessEntityConcessionDate], [UseDailyAgeing], [FirstAgeingPeriod], [SecondAgeingPeriod], [ThirdAgeingPeriod], [IdentifyAgeByName], [LockPeriodIsActive], [LockPeriodDate], [LockThirteenthPeriod], [DefaultCustomerTermsID], [DefaultCustomerPriceLevelID], [DefaultCustomerTaxCodeID], [DefaultUseCustomerTaxCode], [DefaultCustomerFreightTaxCodeID], [DefaultCustomerCreditLimit], [DefaultSupplierTermsID], [DefaultSupplierTaxCodeID], [DefaultUseSupplierTaxCode], [DefaultSupplierFreightTaxCodeID], [DefaultSupplierCreditLimit], [InvoiceSubject], [InvoiceMessage], [IncludeInvoiceNumber], [InvoiceQuoteSubject], [InvoiceQuoteMessage], [IncludeInvoiceQuoteNumber], [InvoiceOrderSubject], [InvoiceOrderMessage], [IncludeInvoiceOrderNumber], [PurchaseSubject], [PurchaseMessage], [IncludePurchaseNumber], [PurchaseQuoteSubject], [PurchaseQuoteMessage], [IncludePurchaseQuoteNumber], [PurchaseOrderSubject], [PurchaseOrderMessage], [IncludePurchaseOrderNumber], [StatementSubject], [StatementMessage], [PaymentSubject], [PaymentMessage], [UseAuditTracking], [UseCreditLimitWarning], [LimitTypeID], [ChangeControl], [UseStandardCost], [UseReceivablesFreight], [UseReceivablesDeposits], [UseReceivablesDiscounts], [UseReceivablesLateFees], [UsePayablesInventory], [UsePayablesFreight], [UsePayablesDeposits], [UsePayablesDiscounts], [UsePayablesLateFees], [BegMonthInFinancialYear], [CreationDate], [IsActive], [ActivationDate]) VALUES (N'SEIKO                                   ', N'minikagi Japan', N'4334545', N'', N'sales@seiko.jp', N'', NULL, NULL, NULL, NULL, NULL, N'435345', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'98989', 4, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2020-07-18 03:29:30.880' AS DateTime), 0, NULL)
SET IDENTITY_INSERT [dbo].[DataFileInformation] OFF
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField]) VALUES (N'JOB', N'Jobs', N'References', 0)
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField]) VALUES (N'CURRENCY', N'Currency', N'References', 0)
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField]) VALUES (N'TAXCODE', N'Tax Codes', N'References', 0)
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField]) VALUES (N'CUSTOMNAME', N'Custom Fields And Lists Names', N'References', 0)
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField]) VALUES (N'CUSTOMLIST1', N'Custom List 1', N'References', 0)
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField]) VALUES (N'CUSTOMLIST2', N'Custom List 2', N'References', 0)
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField]) VALUES (N'CUSTOMLIST3', N'Custom List 3', N'References', 0)
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField]) VALUES (N'GIFTCERT', N'Gift Certificates', N'References', 0)
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField]) VALUES (N'PAYMENTMETHOD', N'Payment Methods', N'References', 0)
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField]) VALUES (N'SHIPPINGMETHOD', N'Shipping Methods', N'References', 0)
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField]) VALUES (N'CUSTOMERS', N'Customers', N'References', 0)
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField]) VALUES (N'SUPPLIERS', N'Suppliers', N'References', 0)
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField]) VALUES (N'REFNP', N'References Navigation Pane', N'References', 0)
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField]) VALUES (N'RPTPROFILES', N'Profile List Report', N'Reports - Profile', 0)
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField]) VALUES (N'RPTLOYAL', N'Report Customiser - Loyalty Members ', N'Reports - Profile', 0)
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField]) VALUES (N'REPORTSNP', N'Reports Navigation Pane', N'Reports', 0)
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField]) VALUES (N'RPTTAXTRANS', N'Tax Transactions Summary', N'Reports - Tax', 0)
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField]) VALUES (N'RPTTAXTRAND', N'Tax Transactions Detail', N'Reports - Tax', 0)
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField]) VALUES (N'RPTTAXLIST', N'Tax Code List', N'Reports - Tax', 0)
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField]) VALUES (N'JOBBALANCE', N'Job Balances', N'Setup', 0)
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField]) VALUES (N'PREFERENCES', N'Preferences', N'Setup', 0)
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField]) VALUES (N'SETUPWIZARD', N'Setup', N'Setup', 0)
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField]) VALUES (N'ARBEGBALANCE', N'Accounts Receivable Starting Balances', N'Setup', 0)
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField]) VALUES (N'ENTERPURCHASE', N'Enter Purchase', N'Purchase', 0)
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField]) VALUES (N'RPTITEMLSTSMRY', N'Report Customizer - Item Summary', N'Reports - Inventory', 0)
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField]) VALUES (N'RPTITEMLSTDTL', N'Report Customizer - Item Details', N'Reports - Inventory', 0)
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField]) VALUES (N'RPTANALYSEINVSMRY', N'Report Customizer - Analyse Inventory Summary', N'Reports - Inventory', 0)
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField]) VALUES (N'RPTANALYSEINVDTL', N'Report Customizer - Analyse Inventory Detail', N'Reports - Inventory', 0)
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField]) VALUES (N'RPTAUTOBUILDITEM', N'Report Customizer - Auto Build Items', N'Reports - Inventory', 0)
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField]) VALUES (N'RPTINVCNTSHEET', N'Report Customizer - Item Count', N'Reports - Inventory', 0)
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField]) VALUES (N'RPTCATEGORYLIST', N'Report Customizer - Category List', N'Reports - Inventory', 0)
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField]) VALUES (N'RPTPRICEDTL', N'Report Customizer - Price List', N'Reports - Inventory', 0)
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField]) VALUES (N'RPTPRICEANALYSIS', N'Report Customizer - Price Analysis', N'Reports - Inventory', 0)
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField]) VALUES (N'RPTITEMREGSMRY', N'Report Customizer - Item Register Summary', N'Reports - Inventory', 0)
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField]) VALUES (N'RPTITEMTRANSACTION', N'Report Customizer - Item Transactions', N'Reports - Inventory', 0)
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField]) VALUES (N'RPTBESTSELLING', N'Report Customizer - Best or Least Selling Item', N'Reports - Inventory', 0)
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField]) VALUES (N'RPTITEMSALESANALYSIS', N'Report Customizer - Item Sales Analysis', N'Reports - Inventory', 0)
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField]) VALUES (N'RPTCATEGORYSALES', N'Report Customizer - Category Sales Analysis', N'Reports - Inventory', 0)
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField]) VALUES (N'RPTSALESSMRY', N'Report Customizer - Sales Summary', N'Reports - Sales', 0)
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField]) VALUES (N'RPTSALESDTL', N'Report Customizer - Sales Detail', N'Reports - Sales', 0)
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField]) VALUES (N'RPTANALYSESALES', N'Report Customizer - Analyse Sales', N'Reports - Sales', 0)
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField]) VALUES (N'RPTANALYSESLSFY', N'Report Customizer - Analyse Sales Date Range Comparison', N'Reports - Sales', 0)
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField]) VALUES (N'RPTCUSTPMNT', N'Report Customizer - Payments [Closed Invioices]', N'Reports - Sales', 0)
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField]) VALUES (N'RPTCUSTLEDGER', N'Report Customizer - Customer Ledger', N'Reports - Sales', 0)
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField]) VALUES (N'RPTALLSALES', N'Report Customizer - All Sales', N'Reports - Sales', 0)
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField]) VALUES (N'RPTCLSEDINVOICE', N'Report Customizer - Closed Invoice', N'Reports - Sales', 0)
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField]) VALUES (N'RPTOPENINV', N'Report Customizer - Open Invoices ', N'Reports - Sales', 0)
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField]) VALUES (N'RPTQUOTES', N'Report Customizer - Quotes', N'Reports - Sales', 0)
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField]) VALUES (N'RPTINVCTRANS', N'Report Customiser - Invoice Transaction', N'Reports - Sales', 0)
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField]) VALUES (N'RPTRETCR', N'Report Customiser - Return & Credits', N'Reports - Sales', 0)
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField]) VALUES (N'RPTSLSITEMSMRY', N'Report Customizer - Sales Item Summary', N'Reports - Sales', 0)
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField]) VALUES (N'RPTORDER', N'Report Customizer - Sales Orders', N'Reports - Sales', 0)
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField]) VALUES (N'PRINTSALERECEIPT', N'Print Sales Receipts', N'Reports - Sales', 0)
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField]) VALUES (N'RPTSLSITEMDTLS', N'Report Customizer - Sales Item Details', N'Reports - Sales', 0)
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField]) VALUES (N'RPTAGERECSMRY', N'Report Customizer - Ageing Summary', N'Reports - Sales', 0)
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField]) VALUES (N'RPTAGERECDTL', N'Report Customizer - Ageing Detail', N'Reports - Sales', 0)
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField]) VALUES (N'RPTSLSRECONSUMMARY', N'RptReconciliationSummary', N'Reports - Sales', 0)
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField]) VALUES (N'RPTSLSRECONDETAILS', N'Report Customizer -Reconciliation Detail', N'Reports - Sales', 0)
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField]) VALUES (N'RPTSMRYWITHTAX', N'Sales Summary with Tax', N'Reports - Sales', 0)
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField]) VALUES (N'RPTCUSTSTATEMENT', N'Sales Statement', N'Reports - Sales', 0)
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField]) VALUES (N'RPTSALESRECJOURNAL', N'Report Customizer - Receivable Journal', N'Reports - Sales', 0)
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField]) VALUES (N'RPTLAYBY', N'Report Customizer - Lay By Report', N'Reports - Sales', 0)
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField]) VALUES (N'RPTPURCHASESMRY', N'Report Customizer - Purchase Summary', N'Reports - Purchase', 0)
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField]) VALUES (N'RPTPURCHASEDTL', N'Customizer - Purchase Report Details', N'Reports - Purchase', 0)
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField]) VALUES (N'RPTANALYSEPUR', N'Report Customiser - Analyse Purchase', N'Reports - Purchase', 0)
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField]) VALUES (N'RPTANALYSEPURFYA', N'Customiser - Analyse Puchase FY Comparison', N'Reports - Purchase', 0)
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField]) VALUES (N'RPTRECORDSUM', N'Report Customiser - Received Orders Summary', N'Reports - Purchase', 0)
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField]) VALUES (N'RPTALLPURCHASES', N'Report Customiser - All Purchase', N'Reports - Purchase', 0)
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField]) VALUES (N'RPTRECORDDET', N'Report Customiser - Received Orders Detail', N'Reports - Purchase', 0)
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField]) VALUES (N'RPTSUPITEMSMRY', N'Report Customiser - Supplier Item Summary', N'Reports - Purchase', 0)
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField]) VALUES (N'RPTSUPITEMDTLS', N'Report Customiser - Supplier Item Detail', N'Reports - Purchase', 0)
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField]) VALUES (N'SALESNAV', N'Sales Navigation', N'Sales', 0)
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField]) VALUES (N'SALESREGISTER', N'Sales Register', N'Sales', 0)
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField]) VALUES (N'RECVPMENT', N'Receive Sales Payment', N'Sales', 0)
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField]) VALUES (N'ENTERSALES', N'Enter Sales', N'Sales', 0)
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField]) VALUES (N'QUICKSALES', N'QuickSales', N'Sales', 0)
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField]) VALUES (N'RESTOPOS', N'Restaurant Point Of Sales', N'Sales', 0)
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField]) VALUES (N'EMAILSTMT', N'Email Statement', N'Sales', 0)
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField]) VALUES (N'PURCHASENAV', N'Purchase Navigation', N'Purchase', 0)
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField]) VALUES (N'PURCHASEREGSTR', N'Purchase Register', N'Purchase', 0)
GO
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField]) VALUES (N'INVENTORYNAV', N'Inventory Navigation Pane', N'Inventory', 0)
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField]) VALUES (N'ITEMS', N'Items', N'Inventory', 0)
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField]) VALUES (N'ITEMLIST', N'Item List', N'Inventory', 0)
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField]) VALUES (N'ITEMREGISTER', N'Item Register', N'Inventory', 0)
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField]) VALUES (N'BLDINVENTORY', N'Build Inventory', N'Inventory', 0)
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField]) VALUES (N'STCKADJMENT', N'Stock Adjustments', N'Inventory', 0)
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField]) VALUES (N'STCKREP', N'Stock Replenishment', N'Inventory', 0)
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField]) VALUES (N'STOCKTAKE', N'Stocktake', N'Inventory', 0)
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField]) VALUES (N'PRICEUPDATE', N'Price Update', N'Inventory', 0)
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField]) VALUES (N'AUTOBUILDITEM', N'Auto Build Items', N'Inventory', 0)
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField]) VALUES (N'PRICETAGS', N'Price Tags', N'Inventory', 0)
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField]) VALUES (N'PRICEHISTORY', N'Price History', N'Inventory', 0)
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField]) VALUES (N'CATEGORY', N'Categories', N'Inventory', 0)
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField]) VALUES (N'USERMAINTENANCE', N'User Maintenance', N'Setup', 0)
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField]) VALUES (N'DATAINFORMATION', N'Data Information', N'Setup', 0)
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField]) VALUES (N'SETUPGUIDE', N'Setup Guide', N'Setup', 0)
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField]) VALUES (N'EMAILSETUP', N'Email Setup', N'Setup', 0)
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField]) VALUES (N'REDEEM', N'Redeemable Items', N'Setup', 0)
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField]) VALUES (N'PROMO', N'Promotions Setup', N'Setup', 0)
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField]) VALUES (N'LOYALTYMEMBERS', N'Loyalty Members', N'Setup', 0)
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField]) VALUES (N'POINTSEXCHANGERATE', N'Points Exchange Rate', N'Setup', 0)
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField]) VALUES (N'TRANSACTIONJOURNAL', N'Transaction Journal', N'Setup', 0)
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField]) VALUES (N'BCKUPDATABASE', N'Backup Database', N'Utilities', 0)
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField]) VALUES (N'REMINDERS', N'Reminders', N'Utilities', 0)
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField]) VALUES (N'SYSAUDITTRAIL', N'System Audit Trail', N'Utilities', 0)
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField]) VALUES (N'TODOLIST', N'To Do List', N'Utilities', 0)
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField]) VALUES (N'TERMINALSETUP', N'Terminal Setup', N'Utilities', 0)
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField]) VALUES (N'SESMAN', N'Session Manager', N'Utilities', 0)
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField]) VALUES (N'IMPORTDATA', N'Import Data', N'Utilities', 0)
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField]) VALUES (N'RPTJOBLIST', N'Job List', N'Reports - Job', 0)
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField]) VALUES (N'RPTJOBACTIVITY', N'Job Activity', N'Reports - Job', 0)
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField]) VALUES (N'RPTJOBTRAN', N'Job Transactions', N'Reports - Job', 0)
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField]) VALUES (N'RPTJOBPROFLOSS', N'Job Profit And Loss', N'Reports - Job', 0)
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField]) VALUES (N'RPTDICSPRO', N'Report Customizer - Discount Promos', N'Reports - Promotions and Discounts', 0)
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField]) VALUES (N'RPTSALEPRO', N'Report Customizer - Sales By Promos', N'Reports - Promotions and Discounts', 0)
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField]) VALUES (N'RPTFREEPRO', N'Report Customizer - Free Products Report', N'Reports - Promotions and Discounts', 0)
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField]) VALUES (N'RPTSESSREPORT', N'POS Session Reports - Customizer', N'Reports - Session', 0)
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField]) VALUES (N'RPTTNDRDET', N'Report Customizer - Tender Details', N'Reports - Session', 0)
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField]) VALUES (N'RPTTNDRSUM', N'Report Customizer - Tender Summary', N'Reports - Session', 0)
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField], [field_name]) VALUES (N'INACTIVE', N'cbInactive', N'Items', 1, 'Inactive')
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField], [field_name]) VALUES (N'CATEGORIES', N'txtCategory', N'Items', 1, 'Category')
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField], [field_name]) VALUES (N'CATEGORIESPB', N'pbCat', N'Items', 1, 'Select Category')
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField], [field_name]) VALUES (N'PARTNUMBER', N'txtPartNumber', N'Items', 1, 'Part Number')
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField], [field_name]) VALUES (N'ITEMNUMBERS', N'txtItemNumber', N'Items', 1, 'Item Number')
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField], [field_name]) VALUES (N'SUPPLIERPARTNUMBER', N'txtSupplierPartNo', N'Items', 1, 'Supplier Number')
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField], [field_name]) VALUES (N'ITEMNAME', N'txtItemName', N'Items', 1, 'Item Name')
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField], [field_name]) VALUES (N'BRAND', N'txtBrand', N'Items', 1, 'Brand')
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField], [field_name]) VALUES (N'DESCRIPTION', N'itemDesc2', N'Items', 1, 'Item Description')
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField], [field_name]) VALUES (N'BTNIMAGEUPLOAD', N'btnBrowse', N'Items', 1, 'Image Upload')
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField], [field_name]) VALUES (N'ITEMDESCRIPTION', N'HTMLEditor', N'Items', 1, 'Item Description 2')
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField], [field_name]) VALUES (N'CUSTOMLISTS1', N'txtList1', N'Items', 1, 'Item Custom List 1')
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField], [field_name]) VALUES (N'CUSTOMLISTS2', N'txtList2', N'Items', 1, 'Item Custom List 2')
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField], [field_name]) VALUES (N'CUSTOMLISTS3', N'txtList3', N'Items', 1, 'Item Custom List 3')
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField], [field_name]) VALUES (N'CUSTOMLISTSPB1', N'pbList1', N'Items', 1, 'Select Custom List 1')
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField], [field_name]) VALUES (N'CUSTOMLISTSPB2', N'pbList2', N'Items', 1, 'Select Custom List 2')
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField], [field_name]) VALUES (N'CUSTOMLISTSPB3', N'pbList3', N'Items', 1, 'Select Custom List 3')
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField], [field_name]) VALUES (N'CUSTOMFIELD1', N'txtField1', N'Items', 1, 'Item Custom Field 1')
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField], [field_name]) VALUES (N'CUSTOMFIELD2', N'txtField2', N'Items', 1, 'Item Custom Field 2')
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField], [field_name]) VALUES (N'CUSTOMFIELD3', N'txtField3', N'Items', 1, 'Item Custom Field 3')
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField], [field_name]) VALUES (N'CUSTOMFIELDPB1', N'pbField1', N'Items', 1, 'Select Custom Field 1')
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField], [field_name]) VALUES (N'CUSTOMFIELDPB2', N'pbField2', N'Items', 1, 'Select Custom Field 2')
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField], [field_name]) VALUES (N'CUSTOMFIELDPB3', N'pbField3', N'Items', 1, 'Select Custom Field 3')
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField], [field_name]) VALUES (N'BUYINGUOM', N'txtBUOM', N'Items', 1, 'Buying Unit of Measure')
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField], [field_name]) VALUES (N'QTYPERBUYINGUOM', N'txtQtyBUOM', N'Items', 1, 'Qty Per Buying UOM')
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField], [field_name]) VALUES (N'MINQTY', N'txtMinQty', N'Items', 1, 'Minimum Qty')
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField], [field_name]) VALUES (N'REORDERQTY', N'txtReOrderQty', N'Items', 1, 'Re-Order Qty')
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField], [field_name]) VALUES (N'MAXIMUMQTY', N'txtMaxQty', N'Items', 1, 'Maximum Qty')
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField], [field_name]) VALUES (N'PURCHASETAXCODES', N'txtPTaxCode', N'Items', 1, 'Purchase Tax Code')
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField], [field_name]) VALUES (N'PURCHASETAXCODESPB', N'pbPTaxCode', N'Items', 1, 'Select Purchase Tax Code')
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField], [field_name]) VALUES (N'LASTCOSTPRICE', N'txtLastCost', N'Items', 1, 'Last Cost Price (Ex)')
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField], [field_name]) VALUES (N'AVERAGECOSTPRICE', N'txtAverageCost', N'Items', 1, 'Average Cost Price (Ex)')
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField], [field_name]) VALUES (N'STANDARCOSTPRICE', N'txtStandardCost', N'Items', 1, 'Standard Cost Price (Ex)')
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField], [field_name]) VALUES (N'SUPPLIERNAME', N'txtSupplier', N'Items', 1, 'Supplier Name')
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField], [field_name]) VALUES (N'SUPPLIERSPB', N'pbSupplier', N'Items', 1, 'Select Supplier')
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField], [field_name]) VALUES (N'SELLINGUOM', N'txtSUOM', N'Items', 1, 'Selling Unit of Measure')
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField], [field_name]) VALUES (N'QTYSELLINGUOM', N'txtQtySUOM', N'Items', 1, 'Qty Per Selling UOM')
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField], [field_name]) VALUES (N'SALESTAXCODE', N'txtSTaxCode', N'Items', 1, 'Sales Tax Code')
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField], [field_name]) VALUES (N'SALESPB', N'pbSTaxCode', N'Items', 1, 'Select Sales Tax Code')
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField], [field_name]) VALUES (N'BASEFROMLASTCOST', N'rdoLC', N'Items', 1, 'Base From Last Cost')
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField], [field_name]) VALUES (N'BASEFROMAVERAGECOST', N'rdoAC', N'Items', 1, 'Base From Average Cost')
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField], [field_name]) VALUES (N'NOROUNDINGS', N'chkNoRounding', N'Items', 1, 'No Rounding')
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField], [field_name]) VALUES (N'NOROUNDINGCENTS', N'chkRound5', N'Items', 1, 'Round to Nearest 5 Cents')
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField], [field_name]) VALUES (N'ALWAYSENDWITH99', N'chkRound99', N'Items', 1, 'Always End With 99')
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField], [field_name]) VALUES (N'SALESSTARTDATE', N'dateTimePicker2', N'Items', 1, 'Sales Start Date')
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField], [field_name]) VALUES (N'SALESENDDATE', N'dateTimePicker1', N'Items', 1, 'Sales End Date')
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField], [field_name]) VALUES (N'DGPRICELEVEL', N'dgPriceLvl', N'Items', 1, 'Price Levels')
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField], [field_name]) VALUES (N'ISAUTOBUILDITEM', N'chkAutoBuild', N'Items', 1, 'This is an Auto Build Item')
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField], [field_name]) VALUES (N'DGAUTOBUILDITE,', N'dgridParts', N'Items', 1, 'Auto Build Items')
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField], [field_name]) VALUES (N'BTNREMOVEPART', N'btnRemovePart', N'Items', 1, 'Remove Part')
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField], [field_name]) VALUES (N'BTNADDPART', N'btnAddPart', N'Items', 1, 'Add Part')
GO
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField], [field_name]) VALUES (N'DGBARCODES', N'dgBarCodes', N'Items', 1, 'Bar Codes')
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField], [field_name]) VALUES (N'BTNADDBARCODES', N'btnAddBarcode', N'Items', 1, 'Add Barcode')
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField], [field_name]) VALUES (N'BTNEDITBARCODES', N'btnEditBarcode', N'Items', 1, 'Edit Barcode')
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField], [field_name]) VALUES (N'BTNDLETEBARCODES', N'btnDelBarCode', N'Items', 1, 'Delete Barcode')
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField], [field_name]) VALUES (N'BTNSAVEBARCODES', N'btnSaveBarcode', N'Items', 1, 'Save Barcode')
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField], [field_name]) VALUES (N'TXTCOSACCT', N'txtCostAcct', N'Items', 1, 'Cost of Sale Account')
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField], [field_name]) VALUES (N'TXTINCACCT', N'txtIncomeAcct', N'Items', 1, 'Income Account')
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField], [field_name]) VALUES (N'TXTASSTACCT', N'txtAssetAcct', N'Items', 1, 'Asset Inventory Account')
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField], [field_name]) VALUES (N'CBOTERMSOFPAYMENT', N'cboTerms', N'Customer', 1, 'Terms of Payment')
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField], [field_name]) VALUES (N'VOLUMEDISCOUNT', N'txtVolumeDiscount', N'Customer', 1, 'Volume Discount %')
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField], [field_name]) VALUES (N'ITEMPRICELEVEL', N'cboPriceLevel', N'Customer', 1, 'Item Price Level')
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField], [field_name]) VALUES (N'BTNPRINTSATATEMENT', N'btnSalesStatement', N'Customer', 1, 'Sales Statement')
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField], [field_name]) VALUES (N'APPROVEPO', N'btnApprove', N'Purchases', 1, 'Approve PO')
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField], [field_name]) VALUES (N'RECIEVEITEM', N'btnReceive', N'Purchases', 1, 'Receive Items')
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField], [field_name]) VALUES (N'QUOTE', N'Save QUOTE', N'Sales', 1, 'Save Quote')
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField], [field_name]) VALUES (N'ORDER', N'Save ORDER', N'Sales', 1, 'Save Order')
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField], [field_name]) VALUES (N'INVOICE', N'Save INVOICE', N'Sales', 1, 'Save Invoice')
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField], [field_name]) VALUES (N'LAY-BY', N'Save LAY-BY', N'Sales', 1, 'Save Lay-by')
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField], [field_name]) VALUES (N'VQUOTE', N'Void QUOTE', N'Sales', 1, 'Void Quote')
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField], [field_name]) VALUES (N'VORDER', N'Void INVOICE', N'Sales', 1, 'Void Order')
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField], [field_name]) VALUES (N'VINVOICE', N'Void ORDER', N'Sales', 1, 'Void Invoice')
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField], [field_name]) VALUES (N'VLAY-BY', N'Void LAY-BY', N'Sales', 1, 'Void Lay-by')
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField], [field_name]) VALUES (N'BTNUNDOAPPROVE', N'btnUndoApprove', N'Purchases', 1, 'Undo Approve PO')
INSERT [dbo].[Forms] ([form_code], [form_name], [category], [IsField], [field_name]) VALUES (N'BTNDELETEPO', N'btnDeletePO', N'Purchases', 1, 'Delete PO')
SET IDENTITY_INSERT [dbo].[Items] ON 
INSERT [dbo].[Items] ([ID], [PartNumber], [ItemNumber], [SupplierItemNumber], [ItemName], [ItemDescription], [IsBought], [IsSold], [IsCounted], [COSAccountID], [IncomeAccountID], [AssetAccountID], [CList1], [CList2], [CList3], [CField1], [CField2], [CField3], [BuyingUOM], [QtyPerBuyingUOM], [SupplierID], [PurchaseTaxCode], [SellingUOM], [QtyPerSellingUnit], [SalesTaxCode], [IsAutoBuild], [AddedBy], [DateCreated], [IsInactive], [CategoryID], [isMain], [BrandName], [ItemDescriptionSimple], [BundleType]) VALUES (1, N'PDCODE', N'PDCODE', N'PDCODE', N'PRICEDISCOUNT', N'<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML><HEAD>
<META content="text/html; charset=unicode" http-equiv=Content-Type>
<META name=GENERATOR content="MSHTML 11.00.10570.1001"></HEAD>
<BODY></BODY></HTML>
', 0, 1, 0, N'0', N'0', N'0', 0, 0, 0, N'', N'', N'', N'Each', 0, 0, N'', N'Each', 0, N'', 0, 1, CAST(N'2020-06-25 09:11:55.220' AS DateTime), 0, 2, 1, N'', NULL, NULL)
SET IDENTITY_INSERT [dbo].[Items] OFF
INSERT [dbo].[ItemsCostPrice] ([ItemID], [LocationID], [LastCostEx], [StandardCostEx], [AverageCostEx], [PrevAverageCostEx]) VALUES (1, 1, 0, 0, 0, 0)
INSERT [dbo].[ItemsQty] ([ItemID], [LocationID], [BegQty], [OnHandQty], [MinQty], [ReOrderQty], [CommitedQty], [MaxQty]) VALUES (1, 1, 0, 0, 0, 0, 0, 0)
INSERT [dbo].[ItemsSellingPrice] ([ItemID], [LocationID], [Level0], [Level1], [Level2], [Level3], [Level4], [Level5], [Level6], [Level7], [Level8], [Level9], [Level10], [Level11], [Level12], [Level0QtyDiscount], [Level1QtyDiscount], [Level2QtyDiscount], [Level3QtyDiscount], [Level4QtyDiscount], [Level5QtyDiscount], [Level6QtyDiscount], [Level7QtyDiscount], [Level8QtyDiscount], [Level9QtyDiscount], [Level10QtyDiscount], [Level11QtyDiscount], [Level12QtyDiscount], [SalesPrice0], [SalesPrice1], [SalesPrice2], [SalesPrice3], [SalesPrice4], [SalesPrice5], [SalesPrice6], [SalesPrice7], [SalesPrice8], [SalesPrice9], [SalesPrice10], [SalesPrice11], [SalesPrice12], [StartSaleDate], [EndSalesDate], [CalculationBasis], [CostBasis], [RoundingMethod]) VALUES (1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, NULL, NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Items] ON 
INSERT [dbo].[Items] ([ID], [PartNumber], [ItemNumber], [SupplierItemNumber], [ItemName], [ItemDescription], [IsBought], [IsSold], [IsCounted], [COSAccountID], [IncomeAccountID], [AssetAccountID], [CList1], [CList2], [CList3], [CField1], [CField2], [CField3], [BuyingUOM], [QtyPerBuyingUOM], [SupplierID], [PurchaseTaxCode], [SellingUOM], [QtyPerSellingUnit], [SalesTaxCode], [IsAutoBuild], [AddedBy], [DateCreated], [IsInactive], [CategoryID], [isMain], [BrandName], [ItemDescriptionSimple], [BundleType]) VALUES (2, N'SBAL', N'SBAL', N'SBAL', N'STARTING BALANCE', N'<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML><HEAD>
<META content="text/html; charset=unicode" http-equiv=Content-Type>
<META name=GENERATOR content="MSHTML 11.00.10570.1001"></HEAD>
<BODY></BODY></HTML>
', 1, 1, 0, N'0', N'0', N'0', 0, 0, 0, N'', N'', N'', N'Each', 0, 0, N'', N'Each', 0, N'', 0, 1, CAST(N'2020-06-25 09:11:55.220' AS DateTime), 0, 2, 1, N'', NULL, NULL)
SET IDENTITY_INSERT [dbo].[Items] OFF
INSERT [dbo].[ItemsCostPrice] ([ItemID], [LocationID], [LastCostEx], [StandardCostEx], [AverageCostEx], [PrevAverageCostEx]) VALUES (2, 1, 0, 0, 0, 0)
INSERT [dbo].[ItemsQty] ([ItemID], [LocationID], [BegQty], [OnHandQty], [MinQty], [ReOrderQty], [CommitedQty], [MaxQty]) VALUES (2, 1, 0, 0, 0, 0, 0, 0)
INSERT [dbo].[ItemsSellingPrice] ([ItemID], [LocationID], [Level0], [Level1], [Level2], [Level3], [Level4], [Level5], [Level6], [Level7], [Level8], [Level9], [Level10], [Level11], [Level12], [Level0QtyDiscount], [Level1QtyDiscount], [Level2QtyDiscount], [Level3QtyDiscount], [Level4QtyDiscount], [Level5QtyDiscount], [Level6QtyDiscount], [Level7QtyDiscount], [Level8QtyDiscount], [Level9QtyDiscount], [Level10QtyDiscount], [Level11QtyDiscount], [Level12QtyDiscount], [SalesPrice0], [SalesPrice1], [SalesPrice2], [SalesPrice3], [SalesPrice4], [SalesPrice5], [SalesPrice6], [SalesPrice7], [SalesPrice8], [SalesPrice9], [SalesPrice10], [SalesPrice11], [SalesPrice12], [StartSaleDate], [EndSalesDate], [CalculationBasis], [CostBasis], [RoundingMethod]) VALUES (2, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, NULL, NULL, NULL, NULL, NULL)

INSERT [dbo].[Category] ([MainCategoryID], [CategoryCode],[Description],[ItemType],[ShowInMenu]) VALUES (N'0', N'DMCategory', N'PDCode Item', N'DMCTG',0)
INSERT [dbo].[Category] ([MainCategoryID], [CategoryCode],[Description],[ItemType],[ShowInMenu]) VALUES (N'1', N'DSCategory', N'PDCode Item', N'DSCTG',0)

SET IDENTITY_INSERT [dbo].[PaymentMethods] ON 

INSERT [dbo].[PaymentMethods] ([PaymentMethod], [GLAccountCode], [id]) VALUES (N'Cash', NULL, 3)
INSERT [dbo].[PaymentMethods] ([PaymentMethod], [GLAccountCode], [id]) VALUES (N'Cheque', NULL, 4)
INSERT [dbo].[PaymentMethods] ([PaymentMethod], [GLAccountCode], [id]) VALUES (N'Complimentary', NULL, 12)
INSERT [dbo].[PaymentMethods] ([PaymentMethod], [GLAccountCode], [id]) VALUES (N'Credit Card', NULL, 5)
INSERT [dbo].[PaymentMethods] ([PaymentMethod], [GLAccountCode], [id]) VALUES (N'Debit Card', NULL, 6)
INSERT [dbo].[PaymentMethods] ([PaymentMethod], [GLAccountCode], [id]) VALUES (N'Direct Deposit', NULL, 9)
INSERT [dbo].[PaymentMethods] ([PaymentMethod], [GLAccountCode], [id]) VALUES (N'Eftpos', NULL, 10)
INSERT [dbo].[PaymentMethods] ([PaymentMethod], [GLAccountCode], [id]) VALUES (N'GC', NULL, 16)
INSERT [dbo].[PaymentMethods] ([PaymentMethod], [GLAccountCode], [id]) VALUES (N'Other', NULL, 7)
INSERT [dbo].[PaymentMethods] ([PaymentMethod], [GLAccountCode], [id]) VALUES (N'Paypal', NULL, 11)
INSERT [dbo].[PaymentMethods] ([PaymentMethod], [GLAccountCode], [id]) VALUES (N'Salary Sacrifice', NULL, 14)
INSERT [dbo].[PaymentMethods] ([PaymentMethod], [GLAccountCode], [id]) VALUES (N'Staff Deduction', NULL, 13)
INSERT [dbo].[PaymentMethods] ([PaymentMethod], [GLAccountCode], [id]) VALUES (N'Voucher', NULL, 15)
SET IDENTITY_INSERT [dbo].[PaymentMethods] OFF

SET IDENTITY_INSERT [dbo].[ShippingMethods] ON 

INSERT [dbo].[ShippingMethods] ([ShippingMethod], [ShippingID]) VALUES (N'Air', 1)
INSERT [dbo].[ShippingMethods] ([ShippingMethod], [ShippingID]) VALUES (N'Airfreight', 2)
INSERT [dbo].[ShippingMethods] ([ShippingMethod], [ShippingID]) VALUES (N'Australia Post', 3)
INSERT [dbo].[ShippingMethods] ([ShippingMethod], [ShippingID]) VALUES (N'Best Way', 4)
INSERT [dbo].[ShippingMethods] ([ShippingMethod], [ShippingID]) VALUES (N'Border Express', 5)
INSERT [dbo].[ShippingMethods] ([ShippingMethod], [ShippingID]) VALUES (N'C.O.D.', 6)
INSERT [dbo].[ShippingMethods] ([ShippingMethod], [ShippingID]) VALUES (N'Courier', 7)
INSERT [dbo].[ShippingMethods] ([ShippingMethod], [ShippingID]) VALUES (N'Couriers Please', 8)
INSERT [dbo].[ShippingMethods] ([ShippingMethod], [ShippingID]) VALUES (N'Direct Freighters', 9)
INSERT [dbo].[ShippingMethods] ([ShippingMethod], [ShippingID]) VALUES (N'Federal Express', 10)
INSERT [dbo].[ShippingMethods] ([ShippingMethod], [ShippingID]) VALUES (N'Freight', 11)
INSERT [dbo].[ShippingMethods] ([ShippingMethod], [ShippingID]) VALUES (N'International', 12)
INSERT [dbo].[ShippingMethods] ([ShippingMethod], [ShippingID]) VALUES (N'Pick Up', 13)
INSERT [dbo].[ShippingMethods] ([ShippingMethod], [ShippingID]) VALUES (N'Road Freight', 14)
INSERT [dbo].[ShippingMethods] ([ShippingMethod], [ShippingID]) VALUES (N'TNT', 15)
INSERT [dbo].[ShippingMethods] ([ShippingMethod], [ShippingID]) VALUES (N'UPS', 16)
INSERT [dbo].[ShippingMethods] ([ShippingMethod], [ShippingID]) VALUES (N'Wards Skyroad', 17)
INSERT [dbo].[ShippingMethods] ([ShippingMethod], [ShippingID]) VALUES (N'Take Out', 18)
INSERT [dbo].[ShippingMethods] ([ShippingMethod], [ShippingID]) VALUES (N'Dine In', 19)
SET IDENTITY_INSERT [dbo].[ShippingMethods] OFF

INSERT [dbo].[TaxCodes] ([TaxCode], [TaxCodeDescription], [TaxPercentageRate], [TaxThreshold], [TaxCodeTypeID], [TaxCollectedAccountID], [TaxPaidAccountID], [LinkedCardID]) VALUES (N'GST', N'Goods & Services Tax', 10, NULL, NULL, N'0', N'0', NULL)
INSERT [dbo].[TaxCodes] ([TaxCode], [TaxCodeDescription], [TaxPercentageRate], [TaxThreshold], [TaxCodeTypeID], [TaxCollectedAccountID], [TaxPaidAccountID], [LinkedCardID]) VALUES (N'N-T', N'Not Reportable', 0, NULL, NULL, N'0', N'0', NULL)
INSERT [dbo].[TaxCodes] ([TaxCode], [TaxCodeDescription], [TaxPercentageRate], [TaxThreshold], [TaxCodeTypeID], [TaxCollectedAccountID], [TaxPaidAccountID], [LinkedCardID]) VALUES (N'EXEMPT', N'Tax Exempted', 0, NULL, NULL, N'0', N'0', NULL)
SET IDENTITY_INSERT [dbo].[Terms] ON 

INSERT [dbo].[Terms] ([TermsID], [LatePaymentChargePercent], [EarlyPaymentDiscountPercent], [TermsOfPaymentID], [DiscountDays], [BalanceDueDays], [ImportPaymentIsDue], [DiscountDate], [BalanceDueDate], [SalesPurchaseID], [TransactionSrc], [VolumeDiscount], [ActualDueDate], [ActualDiscountDate]) VALUES (1, 33, 33, N'ADES', 3, 3, 3, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Terms] ([TermsID], [LatePaymentChargePercent], [EarlyPaymentDiscountPercent], [TermsOfPaymentID], [DiscountDays], [BalanceDueDays], [ImportPaymentIsDue], [DiscountDate], [BalanceDueDate], [SalesPurchaseID], [TransactionSrc], [VolumeDiscount], [ActualDueDate], [ActualDiscountDate]) VALUES (3, 5, 5, N'CFS', 5, 5, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Terms] ([TermsID], [LatePaymentChargePercent], [EarlyPaymentDiscountPercent], [TermsOfPaymentID], [DiscountDays], [BalanceDueDays], [ImportPaymentIsDue], [DiscountDate], [BalanceDueDate], [SalesPurchaseID], [TransactionSrc], [VolumeDiscount], [ActualDueDate], [ActualDiscountDate]) VALUES (4, 4, 4, N'WER', 6, 6, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Terms] ([TermsID], [LatePaymentChargePercent], [EarlyPaymentDiscountPercent], [TermsOfPaymentID], [DiscountDays], [BalanceDueDays], [ImportPaymentIsDue], [DiscountDate], [BalanceDueDate], [SalesPurchaseID], [TransactionSrc], [VolumeDiscount], [ActualDueDate], [ActualDiscountDate]) VALUES (5, 8, 8, N'QRT', 8, 8, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Terms] OFF
INSERT [dbo].[TermsOfPayment] ([TermsOfPaymentID], [Description]) VALUES (N'CASH', N'Cash Only')
INSERT [dbo].[TermsOfPayment] ([TermsOfPaymentID], [Description]) VALUES (N'SD', N'Specific # of Days')
INSERT [dbo].[Preference] ([DefaultCustomerID],[IsTaxInclusive]) VALUES (N'0',N'1')
INSERT [dbo].[TransactionSeries] ([LocationID], [JournalEntrySeries], [JournalEntryPrefix], [SalesOrderSeries], [SalesOrderPrefix], [SalesQuoteSeries], [SalesQuotePrefix], [SalesInvoiceSeries], [SalesInvoicePrefix], [PurchaseOrderSeries], [PurchaseOrderPrefix], [PurchaseQuoteSeries], [PurchaseQuotePrefix], [PurchaseBillSeries], [PurchaseBillPrefix], [ReceivedItemsSeries], [ReceivedItemsPrefix], [BuildItemsSeries], [BuildItemsPrefix], [PaymentSeries], [PaymentPrefix], [BillsPaymentSeries], [BillsPaymentPrefix], [SaleLayBySeries], [SaleLayByPrefix]) VALUES (1, N'00000000', N'J         ', N'00000000', N'SO        ', N'00000000', N'SQ        ', N'00000000', N'SI        ', N'00000000', N'PO        ', N'00000000', N'PQ        ', N'00000000', N'PB        ', N'00000000', N'RI        ', N'00000000', N'BI        ', N'00000000', N'SP        ', N'00000000', N'BP        ', N'00000000', N'SL        ')
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'JOB', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'CURRENCY', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'TAXCODE', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'ACCOUNTS', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'RPTPROFILES', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'PAYMENTMETHOD', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'SHIPPINGMETHOD', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'CUSTOMERS', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'SUPPLIERS', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'TRANSFERMONEY', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'JOURNALENTRY', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'TRANSACTIONJOURNAL', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'BANKREGISTER', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'RPTACCOUNTSLISTS', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'RPTACCOUNTSLISTD', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'GLBALANCE', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'GLNP', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'BANKNP', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'REFNP', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'REPORTSNP', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'BANKREC', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'BANKDEPOSIT', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'LINKAB', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'LINKP', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'LINKS', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'RPTGLTRANS', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'RPTINVCTRANS', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'RPTGLTRANSD', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'RPTTRIAL', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'RPTFREEPRO', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'RPTSALEPRO', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'RPTDICSPRO', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'RPTLINKEDACCTS', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'RPTBALANCESHEET', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'RPTBALANCESHEETMP', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'RPTPROFITLOSS', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'RPTLOYAL', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'RPTTAXTRANS', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'RPTTAXTRAND', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'RPTTAXLIST', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'RPTBANKDEPOSIT', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'RPTUNDEPOSITED', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'RTPBANKRECON', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'NEWFY', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'IMPORTDATA', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'RPTPROFITLOSSMP', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'JOBBALANCE', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'PREFERENCES', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'SETUPWIZARD', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'BUDGET', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'ARBEGBALANCE', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'APBEGBALANCE', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'CUSTOMNAME', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'CUSTOMLIST1', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'CUSTOMLIST2', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'CUSTOMLIST3', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'ENTERPURCHASE', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'ENTERSALES', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'RPTITEMLSTSMRY', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'RPTITEMLSTDTL', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'RPTANALYSEINVSMRY', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'RPTANALYSEINVDTL', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'RPTAUTOBUILDITEM', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'RPTINVCNTSHEET', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'RPTPRICESMRY', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'RPTPRICEDTL', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'RPTBESTSELLING', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'RPTPRICEANALYSIS', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'RPTITEMSALESANALYSIS', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'RPTCATEGORYSALES', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'RPTSALESSMRY', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'RPTSALESDTL', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'RPTANALYSESALES', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'RPTANALYSESLSFY',1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'RPTLAYBY',1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'RPTCUSTPMNT', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'RPTCUSTLEDGER', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'RPTALLSALES', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'RPTCLSEDINVOICE', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'RPTOPENINV',1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'RPTORDER',1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'RPTRETCR',1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'RPTQUOTES', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'RPTSLSITEMSMRY', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'RPTSLSITEMDTLS', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'RPTAGERECSMRY', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'RPTAGERECDTL', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'RPTPURCHASESMRY', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'RPTPURCHASEDTL', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'RPTANALYSEPUR', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'RPTCATEGORYLIST', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'RPTANALYSEPURFYA', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'RPTRECORDSUM', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'RPTALLPURCHASES', 1, 1, 1, 1,NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'RPTCLOSEDBILLS', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'RPTRECORDDET', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'RPTPURQUOTES', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'RPTRETDEBITS',1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'RPTOPENITEMRECPT', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'RPTSUPPMTHIST', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'RPTSUPPMTS', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'RPTSUPITEMSMRY', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'RPTSUPITEMDTLS', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'RPTAGEPAYSMRY', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'RPTAGEPAYDTL', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'RPTTNDRDET', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'RPTTNDRSUM', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'USERMAINTENANCE', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'DATAINFORMATION', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'BCKUPDATABASE', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'CLOSEACCTGBOOK', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'NEWFINYEAR', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'RPTJOBLIST', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'PRICEHISTORY', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'PRICETAGS', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'CATEGORY', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'PRINTSALERECEIPT', 1, 1, 1, 1, NULL)
GO
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'RPTJOBACTIVITY', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'RPTJOBTRAN', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'RPTJOBPROFLOSS', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'RPTACCTTRAN', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'RPTSESSREPORT', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'SALESNAV', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'SALESREGISTER', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'RECVPMENT', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'PURCHASENAV', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'PURCHASEREGSTR', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'PAYBILLS', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'INVENTORYNAV', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'ITEMS', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'ITEMLIST', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'ITEMREGISTER', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'BLDINVENTORY', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'STCKADJMENT', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'SETUPGUIDE', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'STOCKTAKE', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'AUTOBUILDITEM', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'PRICEUPDATE', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'EMAILSTMT', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'RPTCHEQUESMRY', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'RPTITEMREGSMRY', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'RPTITEMTRANSACTION', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'RPTINVVALUERECON', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'RPTPRCRECONSUMMARY', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'RPTPRCRECONDETAILS', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'RPTBILLTRANSACTION', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'RPTPAYABLESJOURNAL', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'RPTREMITTANCEADVICE', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'RPTSLSRECONSUMMARY', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'RPTSLSRECONDETAILS', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'RPTSMRYWITHTAX', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'RPTCUSTSTATEMENT', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'RPTSALESRECJOURNAL', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'RPTREMITADVICE', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'REMINDERS', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'SYSAUDITTRAIL', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'EMAILACCOUNT', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'TODOLIST', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'TERMINALSETUP', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'EMAILSETUP', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'CFLOWANALYSIS', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'CFLOWSTATEMENT', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'REDEEM', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'PROMO', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'POINTSEXCHANGERATE', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'QUICKSALES', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'RESTOPOS', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'GIFTCERT', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'LOYALTYMEMBERS', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'INACTIVE', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'CATEGORIES', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'CATEGORIESPB', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'PARTNUMBER', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'ITEMNUMBERS', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'SUPPLIERPARTNUMBER', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'ITEMNAME', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'BRAND', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'DESCRIPTION', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'BTNIMAGEUPLOAD', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'SESMAN', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'ITEMDESCRIPTION', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'CUSTOMLISTS1', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'CUSTOMLISTS2', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'CUSTOMLISTS3', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'CUSTOMLISTSPB1', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'CUSTOMLISTSPB2', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'CUSTOMLISTSPB3', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'CUSTOMFIELD1', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'CUSTOMFIELD2', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'CUSTOMFIELD3', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'CUSTOMFIELDPB1', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'CUSTOMFIELDPB2', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'CUSTOMFIELDPB3', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'BUYINGUOM', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'QTYPERBUYINGUOM', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'MINQTY', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'REORDERQTY', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'MAXIMUMQTY', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'PURCHASETAXCODES', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'PURCHASETAXCODESPB', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'LASTCOSTPRICE', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'AVERAGECOSTPRICE', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'STANDARCOSTPRICE', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'SUPPLIERNAME', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'SUPPLIERSPB', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'SELLINGUOM', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'QTYSELLINGUOM', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'SALESPB', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'BASEFROMLASTCOST', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'BASEFROMAVERAGECOST', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'NOROUNDINGS', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'NOROUNDINGCENTS', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'ALWAYSENDWITH99', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'SALESSTARTDATE', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'SALESENDDATE', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'DGPRICELEVEL', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'ISAUTOBUILDITEM', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'DGAUTOBUILDITE', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'BTNREMOVEPART', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'BTNADDPART', 1, 1, 1, 1, NULL)
GO
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'BTNADDBARCODES', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'BTNEDITBARCODES', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'BTNDLETEBARCODES', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'BTNSAVEBARCODES', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'TXTCOSACCT', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'TXTINCACCT', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'TXTASSTACCT', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'CBOTERMSOFPAYMENT', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'VOLUMEDISCOUNT', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'ITEMPRICELEVEL', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'BTNPRINTSATATEMENT', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'APPROVEPO', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'RECIEVEITEM', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'QUOTE', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'ORDER', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'INVOICE', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'LAY-BY', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'VQUOTE', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'VORDER', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'VINVOICE', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'VLAY-BY', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'BTNUNDOAPPROVE', 1, 1, 1, 1, NULL)
INSERT [dbo].[User_Access] ([user_id], [form_code], [u_view], [u_add], [u_edit], [u_delete], [date_lastmodified]) VALUES (1, N'BTNDELETEPO', 1, 1, 1, 1, NULL)
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([user_id], [user_name], [user_pwd], [user_fullname], [user_department], [user_inactive], [date_created], [date_lastmodified], [date_lastaccess],	[IsSalesperson] ,[IsSupervisor],[IsTechnician],	[IsAdministrator]) VALUES (1, N'administrator', N'D404559F602EAB6FD602AC7680DACBFAADD13630335E951F097AF3900E9DE176B6DB28512F2E000B9D04FBA5133E8B1C6E8DF59DB3A8AB9D60BE4B97CC9E81DB', N'Administrator', N'', 0, NULL, NULL, NULL, 0,0,0,1)
SET IDENTITY_INSERT [dbo].[Users] OFF

INSERT INTO [dbo].[UserSpecialRights] ([Is_salesperson], [Is_supervisor], [S_edit_customer_cl], [S_override_customer_cl], [S_edit_price_level], [S_allow_quote],[S_allow_order],[S_allow_ar_invoice],[S_allow_cash_invoice],[S_allow_layby],[S_edit_completed_invoice],[S_void_invoice],[S_remove_invoice_line],[S_edit_previous_day_invoice],[S_allow_discount],[S_allow_price_change],[S_allow_returns],[S_allow_overselling],[S_allow_sale_below_cost],[S_show_cost_profit],[S_allow_open_session],[S_allow_close_session],[S_allow_float_in],[S_allow_float_out],[S_print_customer_statement],[P_approve_po],[P_receive_po],[P_undo_receive_po],[I_show_cost],[I_edit_cost],[I_show_price],[I_edit_price],[I_override_average_cost]) VALUES (1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1)


ALTER TABLE [dbo].[Items] ADD  CONSTRAINT [DF_Items_COSAccountID]  DEFAULT ((0)) FOR [COSAccountID]
GO
ALTER TABLE [dbo].[Items] ADD  CONSTRAINT [DF_Items_IncomeAccountID]  DEFAULT ((0)) FOR [IncomeAccountID]
GO
ALTER TABLE [dbo].[Items] ADD  CONSTRAINT [DF_Items_AssetAccountID]  DEFAULT ((0)) FOR [AssetAccountID]
GO
ALTER TABLE [dbo].[Items] ADD  CONSTRAINT [DF_Items_CList1]  DEFAULT ((0)) FOR [CList1]
GO
ALTER TABLE [dbo].[Items] ADD  CONSTRAINT [DF_Items_CList2]  DEFAULT ((0)) FOR [CList2]
GO
ALTER TABLE [dbo].[Items] ADD  CONSTRAINT [DF_Items_CList3]  DEFAULT ((0)) FOR [CList3]
GO
ALTER TABLE [dbo].[Items] ADD  CONSTRAINT [DF_Items_DateCreated]  DEFAULT (getutcdate()) FOR [DateCreated]
GO
ALTER TABLE [dbo].[ItemsAdjustment] ADD  DEFAULT ((1)) FOR [LocationID]
GO
ALTER TABLE [dbo].[ItemsAdjustment] ADD  CONSTRAINT [DF_ItemsAdjustment_EntryDate]  DEFAULT (getutcdate()) FOR [EntryDate]
GO
ALTER TABLE [dbo].[ItemsCostPrice] ADD  DEFAULT ((1)) FOR [LocationID]
GO
ALTER TABLE [dbo].[ItemsCostPrice] ADD  CONSTRAINT [DF_ItemsCostPrice_PrevAverageCostEx]  DEFAULT ((0)) FOR [PrevAverageCostEx]
GO
ALTER TABLE [dbo].[ItemsCount] ADD  DEFAULT ((1)) FOR [LocationID]
GO
ALTER TABLE [dbo].[ItemsQty] ADD  DEFAULT ((1)) FOR [LocationID]
GO
ALTER TABLE [dbo].[ItemsSellingPrice] ADD  DEFAULT ((1)) FOR [LocationID]
GO
ALTER TABLE [dbo].[JobBudget] ADD  CONSTRAINT [DF_JobBudget_Budget]  DEFAULT ((0)) FOR [Budget]
GO
ALTER TABLE [dbo].[JobOpeningBalance] ADD  CONSTRAINT [DF_JobOpeningBalance_OpeningJobBalance]  DEFAULT ((0)) FOR [OpeningJobBalance]
GO
ALTER TABLE [dbo].[Jobs] ADD  CONSTRAINT [DF_Jobs_LocationID]  DEFAULT ((1)) FOR [LocationID]
GO
ALTER TABLE [dbo].[PurchaseLines] ADD  DEFAULT (getutcdate()) FOR [EntryDate]
GO
ALTER TABLE [dbo].[PurchaseLines] ADD  DEFAULT ((0)) FOR [OrderQty]
GO
ALTER TABLE [dbo].[PurchaseLines] ADD  DEFAULT ((0)) FOR [ReceiveQty]
GO
ALTER TABLE [dbo].[PurchaseLines] ADD  DEFAULT ((0.0)) FOR [UnitPrice]
GO
ALTER TABLE [dbo].[PurchaseLines] ADD  DEFAULT ((0.0)) FOR [ActualUnitPrice]
GO
ALTER TABLE [dbo].[PurchaseLines] ADD  DEFAULT ((0.0)) FOR [DiscountPercent]
GO
ALTER TABLE [dbo].[PurchaseLines] ADD  DEFAULT ((0.0)) FOR [SubTotal]
GO
ALTER TABLE [dbo].[PurchaseLines] ADD  DEFAULT ((0.0)) FOR [TotalAmount]
GO
ALTER TABLE [dbo].[PurchaseLines] ADD  DEFAULT ((0.0)) FOR [TaxAmount]
GO
ALTER TABLE [dbo].[PurchaseLines] ADD  CONSTRAINT [DF_PurchaseLines_TaxRate] DEFAULT ((0)) FOR [TaxRate]
GO
ALTER TABLE [dbo].[Purchases] ADD  DEFAULT (getutcdate()) FOR [EntryDate]
GO
ALTER TABLE [dbo].[Purchases] ADD  DEFAULT ((0.0)) FOR [SubTotal]
GO
ALTER TABLE [dbo].[Purchases] ADD  DEFAULT ((0.0)) FOR [FreightSubTotal]
GO
ALTER TABLE [dbo].[Purchases] ADD  DEFAULT ((0.0)) FOR [GrandTotal]
GO
ALTER TABLE [dbo].[Purchases] ADD  DEFAULT ((0.0)) FOR [TotalPaid]
GO
ALTER TABLE [dbo].[Purchases] ADD  DEFAULT ((0.0)) FOR [TotalDue]
GO
ALTER TABLE [dbo].[Purchases] ADD  DEFAULT ((0.0)) FOR [TaxTotal]
GO
ALTER TABLE [dbo].[Purchases] ADD  DEFAULT ((0.0)) FOR [FreightTax]
GO
ALTER TABLE [dbo].[Purchases] ADD  DEFAULT ((1)) FOR [LocationID]
GO
ALTER TABLE [dbo].[Purchases] ADD  CONSTRAINT [DF_Purchases_FreightTaxRate]  DEFAULT ((0)) FOR [FreightTaxRate]
GO
ALTER TABLE [dbo].[RecordJournal] ADD  CONSTRAINT [DF_RecordJournal_EntryDate]  DEFAULT (getutcdate()) FOR [EntryDate]
GO
ALTER TABLE [dbo].[RecordJournal] ADD  CONSTRAINT [DF_RecordJournal_TransactionExchangeRate]  DEFAULT ((1)) FOR [TransactionExchangeRate]
GO
ALTER TABLE [dbo].[RecordJournal] ADD  CONSTRAINT [DF_RecordJournal_LocationID]  DEFAULT ((1)) FOR [LocationID]
GO
ALTER TABLE [dbo].[RecordJournalLine] ADD  CONSTRAINT [DF_RecordJournalLine_EntryDate]  DEFAULT (getutcdate()) FOR [EntryDate]
GO
ALTER TABLE [dbo].[ItemTransaction] ADD  CONSTRAINT [DF_ItemTransaction_EntryDate]  DEFAULT (getutcdate()) FOR [EntryDate]
GO

ALTER TABLE [dbo].[ItemTransaction] ADD  CONSTRAINT [DF_ItemTransaction_TransactionQty]  DEFAULT ((0)) FOR [TransactionQty]
GO
ALTER TABLE [dbo].[ItemTransaction] ADD  CONSTRAINT [DF_ItemTransaction_QtyAdjustment]  DEFAULT ((0)) FOR [QtyAdjustment]
GO
ALTER TABLE [dbo].[ItemTransaction] ADD  CONSTRAINT [DF_ItemTransaction_CostEx]  DEFAULT ((0)) FOR [CostEx]
GO
ALTER TABLE [dbo].[ItemTransaction] ADD  CONSTRAINT [DF_ItemTransaction_TotalCostEx]  DEFAULT ((0)) FOR [TotalCostEx]
GO

USE [master]
GO
ALTER DATABASE [ABLE_TEMPLATE_DB] SET  READ_WRITE 
GO
