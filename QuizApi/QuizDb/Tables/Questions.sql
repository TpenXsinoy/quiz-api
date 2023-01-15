CREATE TABLE [dbo].[Questions]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [TopicId] INT NOT NULL, 
    [Question] NVARCHAR(MAX) NOT NULL, 
    [CorrectAnswer] NVARCHAR(50) NOT NULL
    CONSTRAINT [FK_TopicQuestion] FOREIGN KEY ([TopicId]) REFERENCES [dbo].[Topics] ([Id])
);
