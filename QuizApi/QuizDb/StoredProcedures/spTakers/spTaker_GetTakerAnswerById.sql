CREATE PROCEDURE [dbo].[spTaker_GetTakerAnswerById]
	@Id INT
AS
BEGIN
	SELECT 
		t.Id, t.Name, t.Address, t.Email, qa.Id, qa.Question, ta.Id, ta.Answer, ta.Status
	FROM 
		Takers t 
	INNER JOIN 
		TakersAnswers ta ON ta.TakerId = t.Id
	INNER JOIN 
		Questions qa ON ta.QuestionId = qa.Id
	WHERE  
		t.Id = @Id;
END
