CREATE PROCEDURE [dbo].[spTaker_DeleteTakerById]
	@Id INT
AS
BEGIN
	DELETE FROM TakersAnswers WHERE TakerId = @Id
    DELETE FROM QuizResults WHERE TakerId = @Id
    DELETE FROM TakerQuiz WHERE TakerId = @Id
    DELETE FROM Takers WHERE Id = @Id;
END