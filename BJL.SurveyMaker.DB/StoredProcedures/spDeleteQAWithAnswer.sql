CREATE PROCEDURE [dbo].[spDeleteQAWithAnswer]
	@AnswerId uniqueidentifier
AS
	DELETE FROM tblQuestionAnswer WHERE AnswerId = @AnswerId; 
RETURN 0
