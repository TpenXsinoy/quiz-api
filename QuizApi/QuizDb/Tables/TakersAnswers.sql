CREATE TABLE [dbo].[TakersAnswers]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [TakerId] INT NOT NULL, 
    [QuizId] INT NOT NULL, 
    [TopicId] INT NOT NULL, 
    [QuestionId] INT NOT NULL, 
    [Answer] VARCHAR(50) NULL, 
    [Status] VARCHAR(50) NULL 
    CONSTRAINT [FK_TakersAnswer] FOREIGN KEY ([TakerId]) REFERENCES [dbo].[Takers] ([Id])
    CONSTRAINT [FK_QuizTopicQuestionTakerAnswers] FOREIGN KEY ([QuizId]) REFERENCES [dbo].[Quizzes] ([Id])
    CONSTRAINT [FK_TopicTakerAnswers] FOREIGN KEY ([TopicId]) REFERENCES [dbo].[Topics] ([Id])
    CONSTRAINT [FK_QuestionsTakers] FOREIGN KEY ([QuestionId]) REFERENCES [dbo].[Questions] ([Id])
);
