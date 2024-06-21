CREATE TABLE [dbo].[t_wallet] (
    [f_memberId] INT NOT NULL,
    [f_amount]   INT CONSTRAINT [DF_t_wallet_f_amount] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_t_wallet] PRIMARY KEY CLUSTERED ([f_memberId] ASC)
);

