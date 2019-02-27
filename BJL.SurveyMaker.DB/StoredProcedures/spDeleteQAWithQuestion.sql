CREATE PROCEDURE [dbo].[spDeleteQAWithQuestion]
	@QuestionId uniqueidentifier
AS
	DELETE FROM tblQuestionAnswer WHERE AnswerId = @QuestionId; 
RETURN 0
