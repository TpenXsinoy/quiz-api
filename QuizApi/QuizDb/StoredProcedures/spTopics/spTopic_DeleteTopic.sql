CREATE PROCEDURE [dbo].[spTopic_DeleteTopic]
	@Id INT
AS
BEGIN
	DELETE FROM TakersAnswers WHERE TopicId = @Id
    DELETE FROM Questions WHERE TopicId = @Id
    DELETE FROM Topics WHERE Id = @Id;
END
