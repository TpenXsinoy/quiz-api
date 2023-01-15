USE [QuizDb]
GO

SET IDENTITY_INSERT [dbo].[Takers] ON
INSERT [dbo].[Takers] ([Id], [Name], [Address], [Email]) 
VALUES 
    (1, 'John Doe', 'N. Bacalso Ave, Cebu City', 'johndoe@gmail.com'),
    (2, 'Jane Doe', 'Somewhere bukid, Cebu City', 'janedoe@gmail.com'),
    (3, 'Stephine Doe', 'Cebu City', 'stephine.doe@gmail.com');

SET IDENTITY_INSERT [dbo].[Takers] OFF

SET IDENTITY_INSERT [dbo].[Quizzes] ON
INSERT [dbo].[Quizzes] ([Id], [Name], [Description])
VALUES
    (1, 'Math Quiz', 'Calculus quiz'),
    (2, 'Science Quiz', 'Quiz on life of animals'),
    (3, 'History Quiz', 'Quiz on the life of PH heroes');

SET IDENTITY_INSERT [dbo].[Quizzes] OFF

SET IDENTITY_INSERT [dbo].[TakerQuiz] ON
INSERT [dbo].[TakerQuiz] ([Id], [TakerId], [QuizId])
VALUES
    (1, 1, 1),
    (2, 1, 2),
    (3, 2, 1),
    (4, 2, 3),
    (5, 3, 3);

SET IDENTITY_INSERT [dbo].[TakerQuiz] OFF

SET IDENTITY_INSERT [dbo].[QuizResults] ON
INSERT [dbo].[QuizResults] ([Id], [QuizId], [QuizName], [TakerId], [TakerName], [Score], [Evaluation])
VALUES
    (1, 1, 'Math Quiz', 1, 'John Doe', 2, 'Passed!'),
    (2, 2, 'Science Quiz', 1,'John Doe', 0, 'Failed'),
    (3, 1, 'Math Quiz', 2, 'Jane Doe', 3, 'Perfect!'),
    (4, 3, 'History Quiz', 2,'Jane Doe', 0, 'Failed');

       SET IDENTITY_INSERT [dbo].[QuizResults] OFF

SET IDENTITY_INSERT [dbo].[Topics] ON
INSERT [dbo].[Topics] ([Id], [QuizId], [Name] )
VALUES
    (1, 1, 'Differential'),
    (2, 1, 'Integral'),
    (3, 2, 'Animal Kingdom'),
    (4, 2, 'Reptile Habitats'),
    (5, 3, 'Life of Rizal');
  
SET IDENTITY_INSERT [dbo].[Topics] OFF

SET IDENTITY_INSERT [dbo].[Questions] ON
INSERT [dbo].[Questions] ([Id], [TopicId], [Question], [CorrectAnswer] )
VALUES
    (1, 1, '1+1=3', 'False'),         
    (2, 1, '200+1=201', 'True'),
    (3, 2, '∫4x6−2x3+7x−4dx', '2x'),
    (4, 3, 'The king of the jungle is the lion', 'True'),
    (5, 3, 'Dragonflies, stink bugs, and lady bugs are what?', 'insects'),
    (6, 4, 'River is a habitat of a snake', 'True'),
    (7, 5, 'Rizal has 100 wives', 'True'),
    (8, 5, 'What weapon did Rizal used to fight for freedom?', 'pen and paper');

   SET IDENTITY_INSERT [dbo].[Questions] OFF

SET IDENTITY_INSERT [dbo].[TakersAnswers] ON
INSERT [dbo].[TakersAnswers] ([Id], [TakerId], [QuizId], [TopicId], [QuestionId], [Answer], [Status] )
VALUES
    (1, 1, 1, 1, 1, 'True', 'Wrong'),
    (2, 1, 1, 1, 2, 'True', 'Correct'),
    (3, 1, 1, 2, 3, '2x', 'Correct'),

    (4, 1, 2, 3, 4, 'False', 'Wrong'),
    (5, 1, 2, 3, 5, 'reptile', 'Wrong'),
    (6, 1, 2, 4, 6, 'False', 'Wrong'),

    (7, 2, 1, 1, 1, 'False', 'Correct'),
    (8, 2, 1, 1, 2, 'True', 'Correct'),
    (9, 2, 1, 2, 3, '2x', 'Correct'),

    (10, 2, 3, 5, 7, 'False', 'Wrong'),
    (11, 2, 3, 5, 8, 'Sword', 'Wrong'),
  
    (12, 3, 3, 5, 7, 'True', 'Correct'),
    (13, 3, 3, 5, 8, 'pen and paper', 'Correct');

SET IDENTITY_INSERT [dbo].[TakersAnswers] OFF


