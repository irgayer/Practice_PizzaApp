CREATE TABLE [dbo].[Users]
(
	[Id] INT identity(1,1) NOT NULL PRIMARY KEY,
	[Login] nvarchar(1024) not null,
	[Password] nvarchar(1024) not null,
	FullName nvarchar(512) not null,
	PhoneNumber nvarchar(20) not null
)