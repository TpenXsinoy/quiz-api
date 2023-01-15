CREATE PROCEDURE [dbo].[spTopic_GetAllTopicsByQuizId]
	@quizId INT
AS
BEGIN
	SELECT 
		t.Id, t.Name, q.Id, q.Name 
	FROM 
		Topics t
    INNER JOIN 
		Quizzes q 
	ON 
		q.Id = t.QuizId
    WHERE 
		t.QuizId = @quizId;
END