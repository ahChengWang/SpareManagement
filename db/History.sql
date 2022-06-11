use spares_warehouse;

/****** Object:  Table [dbo].[History]    Script Date: 2022/5/30 ¤W¤È 08:45:51 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[History](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CategoryId] [int] NOT NULL,
	[PartNo] [nvarchar](50) NOT NULL,
	[Status] [int] NULL,
	[Quantity] [int] NOT NULL,
	[EmpName] [nvarchar](50) NOT NULL,
	[UpdateTime] [datetime] NOT NULL,
	[Memo] [nvarchar](200) NOT NULL
) ON [PRIMARY]

GO

CREATE CLUSTERED INDEX idx_history_1 ON History (CategoryId,PartNo);