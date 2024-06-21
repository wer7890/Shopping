CREATE TABLE [dbo].[t_walletChanges] (
    [f_id]             INT      IDENTITY (1, 1) NOT NULL,
    [f_memberId]       INT      NOT NULL,
    [f_previousAmount] INT      NOT NULL,
    [f_finalAmount]    INT      NOT NULL,
    [f_changeAmount]   INT      NOT NULL,
    [f_createdTime]    DATETIME CONSTRAINT [DF_t_walletChanges_f_createdTime] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_t_walletChanges] PRIMARY KEY CLUSTERED ([f_id] ASC)
);

