BEGIN
	INSERT INTO [dbo].tblQuestion(Id, [Text])
	Values(NEWID(), 'Which character is theorized by many fans to be the Scranton Strangler?'),
	(NEWID(), 'Which character is married to the owner of a refrigeration company?'),
	(NEWID(), 'Which character left Dunder Mifflin the the fifth season to start a rival company under their own name?'),
	(NEWID(), 'Which character worked in the Nashua branch of Dunder Mifflin?'),
	(NEWID(), 'Who sprouts mung beans in their desk drawers?'),
	(NEWID(), 'Who proposes to Pamela Beesely in the rain out front of a gas-station?')
END