USE kh
GO

INSERT INTO [orders].[Orders] (CreatedAt)
VALUES (GETDATE())
     , (GETDATE())

go