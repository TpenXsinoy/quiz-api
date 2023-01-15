CREATE TABLE [dbo].[Topics]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [QuizId] INT NOT NULL, 
    [Name] NVARCHAR(50) NOT NULL
     CONSTRAINT [FK_QuizTopic] FOREIGN KEY ([QuizId]) REFERENCES [dbo].[Quizzes] ([Id])
);
