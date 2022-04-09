create --drop
table Accounts
(
	Id uniqueidentifier,
	RoleId int,
	Email nvarchar(50),
	FirstName nvarchar(50),
	LastName nvarchar(50),
	PasswordHash varbinary(100)
)