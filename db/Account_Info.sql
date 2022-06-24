use spares_warehouse;

/****** Object:  Table [dbo].[Account_Info]    Script Date: 2022/5/30 ¤W¤È 08:45:51 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Account_Info](
	[Account] [varchar](20) NOT NULL,
	[Password] [varchar](20) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Dept] [nvarchar](50) NULL,
 CONSTRAINT [PK_account_info] PRIMARY KEY CLUSTERED 
(
	[Account] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


INSERT INTO [Account_Info] ([Account],[Password],[Name],[Dept])
VALUES(N'admin',N'123',N'',null);