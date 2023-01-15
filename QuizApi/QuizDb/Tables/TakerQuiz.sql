CREATE TABLE [dbo].[TakerQuiz]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [TakerId] INT NOT NULL, 
    [QuizId] INT NOT NULL, 
    CONSTRAINT [FK_TakerQuiz_ToTable] FOREIGN KEY ([TakerId]) REFERENCES [Takers]([Id]), 
    CONSTRAINT [FK_TakerQuiz_ToTable_1] FOREIGN KEY ([QuizId]) REFERENCES [Quizzes]([Id])
)
