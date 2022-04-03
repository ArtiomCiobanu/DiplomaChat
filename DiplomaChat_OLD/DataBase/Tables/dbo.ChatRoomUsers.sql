create --drop
table ChatRoomUsers
(
	ChatRoomId uniqueidentifier,
	UserId uniqueidentifier,

	constraint PK_ChatRoomUsers primary key (ChatRoomId, UserId)
)