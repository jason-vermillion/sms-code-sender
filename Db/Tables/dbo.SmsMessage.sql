
GO

IF OBJECT_ID(N'dbo.SmsMessage', N'U') IS NULL
BEGIN 
    CREATE TABLE dbo.SmsMessage (
        SmsMessageId INT NOT NULL IDENTITY(1,1),
        ToPhone nvarchar(255),
        FromPhone nvarchar(255),
        MessageBody nvarchar(800),
        MessageSid nvarchar(255),
        CreatedOn datetime DEFAULT getdate(),
        CONSTRAINT PK_SmsMessage_SmsMessageId
                    PRIMARY KEY CLUSTERED (SmsMessageId)
                    WITH (IGNORE_DUP_KEY = OFF)
        );
END
GO
