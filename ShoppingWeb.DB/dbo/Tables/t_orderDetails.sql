CREATE TABLE [dbo].[t_orderDetails] (
    [f_id]              INT            IDENTITY (1, 1) NOT NULL,
    [f_orderId]         INT            NOT NULL,
    [f_productId]       INT            NOT NULL,
    [f_productNameTW]   NVARCHAR (100) NOT NULL,
    [f_productNameEN]   NVARCHAR (100) NOT NULL,
    [f_productPrice]    INT            NOT NULL,
    [f_productCategory] INT            NOT NULL,
    [f_quantity]        INT            NOT NULL,
    [f_subtotal]        INT            NOT NULL,
    CONSTRAINT [PK_t_orderDetails] PRIMARY KEY CLUSTERED ([f_id] ASC)
);

