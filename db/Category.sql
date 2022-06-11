use spares_warehouse;

/****** Object:  Table [dbo].[Basic_information]    Script Date: 2022/5/30 �W�� 08:45:51 ******/
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


INSERT INTO [dbo].[Category]([Name],[Description]) VALUES(N'�@��ӧ�',N'�ΥH���@�Ͳ��]�ƥH�~���@��ӧ�,�p�����B��ޡB�q�u...(��ƪo�B�K�t�s���a�ݩ�@��ӧ�)');
INSERT INTO [dbo].[Category]([Name],[Description]) VALUES(N'�]�ƹs��',N'�Ͳ��v��H�~���u��,�]�ƱM�ݹs��B����B�l�L�B�]�Ƥ��i�ίʤ��t��');
INSERT INTO [dbo].[Category]([Name],[Description]) VALUES(N'�v��',N'���U�Ͳ����u��,�i�H���ؤ����ܧ�C������ݤJ�w�޲z�O�s��');
INSERT INTO [dbo].[Category]([Name],[Description]) VALUES(N'�u�O��',N'�����I�O�u�O����');

GO