USE
    master
go

CREATE
    LOGIN kitchen with password = 'OpenAI@2023';
go

CREATE
    USER kitchen FOR login kitchen;
go

exec master..sp_addsrvrolemember @loginame = N'kitchen', @rolename = N'sysadmin'
go

CREATE
    DATABASE [kh];
go

ALTER
    DATABASE [kh] SET COMPATIBILITY_LEVEL = 140
GO
