CREATE PROCEDURE [dbo].[spQuiz_DeleteQuiz]
	@Id INT
AS
BEGIN
	DELETE FROM QuizResults WHERE QuizId = @Id
    DELETE FROM TakerQuiz WHERE QuizId = @Id
    DELETE FROM Topics WHERE QuizId = @Id 
    DELETE FROM TakersAnswers WHERE QuizId = @Id 
    DELETE FROM QuizResults WHERE QuizId = @Id
    DELETE FROM Quizzes WHERE Id = @id;
END