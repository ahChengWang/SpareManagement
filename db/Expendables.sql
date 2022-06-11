use spares_warehouse;

/****** Object:  Table [dbo].[Expendables]    Script Date: 2022/5/30 ¤W¤È 08:45:51 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Expendables](
	[PartNo] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[PurchaseId] [nvarchar](50) NOT NULL,
	[Spec] [nvarchar](50) NOT NULL,
	[IsKeySpare] [bit] NULL,
	[IsCommercial] [bit] NULL,
	[Placement] [nvarchar](50) NOT NULL,
	[Inventory] [int] NOT NULL,
	[SafetyCount] [int] NOT NULL,
	[Memo] [nvarchar](200) NULL,
 CONSTRAINT [PK_expendables] PRIMARY KEY CLUSTERED 
(
	[PartNo] ASC,[PurchaseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO