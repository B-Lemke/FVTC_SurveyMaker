BEGIN
	DECLARE @QuestionResp1Id uniqueidentifier
	DECLARE @QuestionResp2Id uniqueidentifier
	DECLARE @QuestionResp3Id uniqueidentifier
	
	
	SELECT @QuestionResp1Id = Id FROM tblQuestion WHERE [Text] = 'Which character is theorized by many fans to be the Scranton Strangler?';
	SELECT @QuestionResp2Id = Id FROM tblQuestion WHERE [Text] = 'Who sprouts mung beans in their desk drawers?';
	SELECT @QuestionResp3Id = Id FROM tblQuestion WHERE [Text] = 'Which character left Dunder Mifflin the the fifth season to start a rival company under their own name?';

	
	DECLARE @AnswerResp1Id uniqueidentifier
	DECLARE @AnswerResp2Id uniqueidentifier
	DECLARE @AnswerResp3Id uniqueidentifier
	DECLARE @AnswerResp4Id uniqueidentifier

	SELECT @AnswerResp1Id = Id FROM tblAnswer WHERE [Text] = 'Toby Flenderson';
	SELECT @AnswerResp2Id = Id FROM tblAnswer WHERE [Text] = 'Michael Scott';
	SELECT @AnswerResp3Id = Id FROM tblAnswer WHERE [Text] = 'Holly Flax';
	SELECT @AnswerResp4Id = Id FROM tblAnswer WHERE [Text] = 'Phyllis Vance';



	INSERT INTO [dbo].tblResponse(Id, QuestionId, AnswerId)
	Values
	(NEWID(), @QuestionResp1Id, @AnswerResp1Id),
	(NEWID(), @QuestionResp1Id, @AnswerResp2Id),
	(NEWID(), @QuestionResp2Id, @AnswerResp3Id),
	(NEWID(), @QuestionResp3Id, @AnswerResp4Id)
END
