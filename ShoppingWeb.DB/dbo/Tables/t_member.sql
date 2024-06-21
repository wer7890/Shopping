CREATE TABLE [dbo].[t_member] (
    [f_id]            INT           IDENTITY (1, 1) NOT NULL,
    [f_account]       VARCHAR (16)  NOT NULL,
    [f_pwd]           VARCHAR (16)  NOT NULL,
    [f_name]          NVARCHAR (15) NOT NULL,
    [f_birthday]      DATE          NOT NULL,
    [f_level]         TINYINT       CONSTRAINT [DF_t_member_f_level] DEFAULT ((0)) NOT NULL,
    [f_phoneNumber]   VARCHAR (10)  NOT NULL,
    [f_email]         VARCHAR (50)  NOT NULL,
    [f_address]       NVARCHAR (50) NOT NULL,
    [f_accountStatus] BIT           CONSTRAINT [DF_t_member_f_accountStatus] DEFAULT ((1)) NOT NULL,
    [f_createdTime]   DATETIME      CONSTRAINT [DF_t_member_f_createdTime] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_t_member] PRIMARY KEY CLUSTERED ([f_id] ASC)
);

