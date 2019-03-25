BEGIN
	DECLARE @QuestionAct1Id uniqueidentifier
	DECLARE @QuestionAct2Id uniqueidentifier
	DECLARE @QuestionAct3Id uniqueidentifier
	
	
	SELECT @QuestionAct1Id = Id FROM tblQuestion WHERE [Text] = 'Which character is theorized by many fans to be the Scranton Strangler?';
	SELECT @QuestionAct2Id = Id FROM tblQuestion WHERE [Text] = 'Who sprouts mung beans in their desk drawers?';
	SELECT @QuestionAct3Id = Id FROM tblQuestion WHERE [Text] = 'Which character left Dunder Mifflin the the fifth season to start a rival company under their own name?';


	INSERT INTO [dbo].tblActivation(Id, QuestionId, StartDate, EndDate, ActivationCode)
	Values(NEWID(), @QuestionAct1Id, '2019-03-25', '2019-12-25', 'test1'),
	(NEWID(), @QuestionAct2Id, '1999-11-11', '1999-11-13', 'test2'),
	(NEWID(), @QuestionAct3Id, '2020-05-12', '2020-06-02', 'test3')

END


