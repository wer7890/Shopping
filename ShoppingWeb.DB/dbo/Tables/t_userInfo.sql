CREATE TABLE [dbo].[t_userInfo] (
    [f_id]        INT          IDENTITY (1, 1) NOT NULL,
    [f_account]   VARCHAR (16) NOT NULL,
    [f_pwd]       VARCHAR (64) NOT NULL,
    [f_roles]     TINYINT      NOT NULL,
    [f_sessionId] VARCHAR (24) NULL,
    CONSTRAINT [PK_t_userInfo] PRIMARY KEY CLUSTERED ([f_id] ASC)
);

