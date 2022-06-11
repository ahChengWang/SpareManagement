use spares_warehouse;

/****** Object:  Table [dbo].[Basic_information]    Script Date: 2022/5/30 ¤W¤È 08:45:51 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Basic_information](
	[CategoryId] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[PartNo] [nvarchar](50) NOT NULL,
	[Spec] [nvarchar](50) NOT NULL,
	[PurchaseId] [nvarchar](50) NOT NULL,
	[IsKeySpare] [bit] NULL,
	[IsCommercial] [bit] NULL,
	[Equipment] [nvarchar](200) NULL,
	[UseLocation] [varchar](20) NOT NULL,
	[PurchaseDelivery] [int] NOT NULL,
	[SafetyCount] [int] NOT NULL,
	[IsSpecial] [bit] NOT NULL,
	[IsNeedInspect] [bit] NULL,
	[InspectCycle] [int] NULL,
	[Placement] [nvarchar](50) NOT NULL,
	[CreateUser] [nvarchar](50) NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[Manager] [nvarchar](50) NOT NULL,
	[Memo] [nvarchar](200) NULL,
	[LastSerialNo] [int] NOT NULL DEFAULT 0,
 CONSTRAINT [PK_basic_information] PRIMARY KEY CLUSTERED 
(
	[CategoryId] ASC,[PartNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


CREATE INDEX idx_basic_information_1
ON Basic_information(PartNo,Name,PurchaseId,Placement,CreateDate);
