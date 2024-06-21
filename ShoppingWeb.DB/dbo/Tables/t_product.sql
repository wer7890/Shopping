CREATE TABLE [dbo].[t_product] (
    [f_id]           INT            IDENTITY (1, 1) NOT NULL,
    [f_nameTW]       NVARCHAR (100) NOT NULL,
    [f_nameEN]       NVARCHAR (100) NOT NULL,
    [f_category]     INT            NOT NULL,
    [f_img]          NVARCHAR (40)  NOT NULL,
    [f_price]        INT            NOT NULL,
    [f_stock]        INT            NOT NULL,
    [f_isOpen]       BIT            NOT NULL,
    [f_introduceTW]  NVARCHAR (500) NOT NULL,
    [f_introduceEN]  NVARCHAR (500) NOT NULL,
    [f_warningValue] INT            NOT NULL,
    [f_createdTime]  DATETIME       CONSTRAINT [DF_t_product_f_createdTime] DEFAULT (getdate()) NOT NULL,
    [f_createdUser]  INT            NOT NULL,
    CONSTRAINT [PK_t_product] PRIMARY KEY CLUSTERED ([f_id] ASC)
);

