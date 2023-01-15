CREATE PROCEDURE [dbo].[spTaker_GetAllTakers]
AS
BEGIN
	SELECT 
        t.Id, t.Name, t.Address, t.Email, q.Id, q.Name, q.Description 
    FROM 
        Takers t 
    INNER JOIN 
        TakerQuiz tq 
    ON 
        tq.TakerId = t.Id
    INNER JOIN 
        Quizzes q 
    ON 
        q.Id = tq.QuizId;
END
