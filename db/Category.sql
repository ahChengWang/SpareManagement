use spares_warehouse;

/****** Object:  Table [dbo].[Basic_information]    Script Date: 2022/5/30 上午 08:45:51 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Category](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](200) NOT NULL,
 CONSTRAINT [PK_category] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


INSERT INTO [dbo].[Category]([Name],[Description]) VALUES(N'一般耗材',N'用以維護生產設備以外的一般耗材,如螺絲、氣管、電線...(潤滑油、鐵氟龍膠帶屬於一般耗材)');
INSERT INTO [dbo].[Category]([Name],[Description]) VALUES(N'設備零件',N'生產治具以外之工具,設備專屬零件、金具、吸嘴、設備不可或缺之配件');
INSERT INTO [dbo].[Category]([Name],[Description]) VALUES(N'治具',N'輔助生產之工具,可隨機種切換變更。切換後需入庫管理保存者');
INSERT INTO [dbo].[Category]([Name],[Description]) VALUES(N'線板材',N'測試點燈線板材類');

GO