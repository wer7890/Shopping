CREATE TABLE [dbo].[t_order] (
    [f_id]             INT           IDENTITY (1, 1) NOT NULL,
    [f_serialNumber]   VARCHAR (24)  NOT NULL,
    [f_memberId]       INT           NOT NULL,
    [f_total]          INT           NOT NULL,
    [f_orderStatus]    TINYINT       CONSTRAINT [DF_t_order_f_orderStatus] DEFAULT ((1)) NOT NULL,
    [f_deliveryStatus] TINYINT       CONSTRAINT [DF_t_order_f_deliveryStatus] DEFAULT ((1)) NOT NULL,
    [f_deliveryMethod] TINYINT       NOT NULL,
    [f_destination]    NVARCHAR (30) NOT NULL,
    [f_arrivalDate]    DATE          NOT NULL,
    [f_createdTime]    DATETIME      CONSTRAINT [DF_t_order_f_createdTime] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_t_order] PRIMARY KEY CLUSTERED ([f_id] ASC)
);

