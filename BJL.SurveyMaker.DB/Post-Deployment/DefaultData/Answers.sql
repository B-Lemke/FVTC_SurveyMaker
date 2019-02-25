BEGIN
	INSERT INTO dbo.tblAnswer (Id, [Text])
	Values(NEWID(), 'Toby Flenderson'),
	(NEWID(), 'Michael Scott'),
	(NEWID(), 'Pam Halpert'),
	(NEWID(), 'Jim Halpert'),
	(NEWID(), 'Kelly Kapoor'),
	(NEWID(), 'Holly Flax'),
	(NEWID(), 'Phyllis Vance'),
	(NEWID(), 'Creed Bratton')
END