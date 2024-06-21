CREATE TABLE [dbo].[t_totalSpent] (
    [f_memberId]   INT NOT NULL,
    [f_totalSpent] INT CONSTRAINT [DF_t_totalSpent_f_totalSpent] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_t_totalSpent] PRIMARY KEY CLUSTERED ([f_memberId] ASC)
);

