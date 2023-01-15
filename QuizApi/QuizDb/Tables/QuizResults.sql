CREATE TABLE [dbo].[QuizResults]
(

	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [QuizId] INT NOT NULL, 
    [QuizName] NVARCHAR(50) NOT NULL,
    [TakerId] INT NOT NULL, 
    [TakerName] NVARCHAR(50) NOT NULL, 
    [Score] INT NOT NULL, 
    [Evaluation] VARCHAR(50) NOT NULL
    CONSTRAINT [FK_QuizResultTaker] FOREIGN KEY ([TakerId]) REFERENCES [dbo].[Takers] ([Id])
    CONSTRAINT [FK_QuizResultQuiz] FOREIGN KEY ([QuizId]) REFERENCES [dbo].[Quizzes] ([Id])
);
