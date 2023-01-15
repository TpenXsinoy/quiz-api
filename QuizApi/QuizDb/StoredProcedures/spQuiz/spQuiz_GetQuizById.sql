CREATE PROCEDURE [dbo].[spQuiz_GetQuizById]
	@Id INT
AS
BEGIN
	SELECT 
		q.Id, q.Name, q.Description, tp.Id, tp.Name, tp.QuizId 
	FROM 
		Quizzes q
    INNER JOIN 
		Topics tp 
	ON 
		tp.QuizId = q.Id
    WHERE 
		q.Id = @Id;
END

