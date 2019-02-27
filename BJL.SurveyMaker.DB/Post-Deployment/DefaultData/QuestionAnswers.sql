BEGIN
	DECLARE @Question1Id uniqueidentifier
	DECLARE @Question2Id uniqueidentifier
	DECLARE @Question3Id uniqueidentifier
	
	
	SELECT @Question1Id = Id FROM tblQuestion WHERE [Text] = 'Which character is theorized by many fans to be the Scranton Strangler?';
	SELECT @Question2Id = Id FROM tblQuestion WHERE [Text] = 'Who sprouts mung beans in their desk drawers?';
	SELECT @Question3Id = Id FROM tblQuestion WHERE [Text] = 'Which character left Dunder Mifflin the the fifth season to start a rival company under their own name?';

	
	DECLARE @Answer1Id uniqueidentifier
	DECLARE @Answer2Id uniqueidentifier
	DECLARE @Answer3Id uniqueidentifier
	DECLARE @Answer4Id uniqueidentifier
	DECLARE @Answer5Id uniqueidentifier
	DECLARE @Answer6Id uniqueidentifier
	DECLARE @Answer7Id uniqueidentifier

	SELECT @Answer1Id = Id FROM tblAnswer WHERE [Text] = 'Toby Flenderson';
	SELECT @Answer2Id = Id FROM tblAnswer WHERE [Text] = 'Creed Bratton';
	SELECT @Answer3Id = Id FROM tblAnswer WHERE [Text] = 'Michael Scott';
	SELECT @Answer4Id = Id FROM tblAnswer WHERE [Text] = 'Holly Flax';
	SELECT @Answer5Id = Id FROM tblAnswer WHERE [Text] = 'Phyllis Vance';
	SELECT @Answer6Id = Id FROM tblAnswer WHERE [Text] = 'Kelly Kapoor';
	SELECT @Answer7Id = Id FROM tblAnswer WHERE [Text] = 'Pam Halpert';


	INSERT INTO [dbo].tblQuestionAnswer(Id, QuestionId, AnswerId, IsCorrect)
	Values(NEWID(), @Question1Id, @Answer1Id, 1),
	(NEWID(), @Question1Id, @Answer4Id, 0),
	(NEWID(), @Question1Id, @Answer6Id, 0),
	(NEWID(), @Question1Id, @Answer7Id, 0),
	
	(NEWID(), @Question2Id, @Answer2Id, 1),
	(NEWID(), @Question2Id, @Answer4Id, 0),
	(NEWID(), @Question2Id, @Answer3Id, 0),
	(NEWID(), @Question2Id, @Answer5Id, 0),
	
	(NEWID(), @Question3Id, @Answer3Id, 1),
	(NEWID(), @Question3Id, @Answer1Id, 0),
	(NEWID(), @Question3Id, @Answer7Id, 0),
	(NEWID(), @Question3Id, @Answer6Id, 0)

END

