//video*/
/*
CREATE PROC Sp_UploadVideo
(
@ChannelId UNIQUEIDENTIFIER,
@PlayListId UNIQUEIDENTIFIER,
@Title NVARCHAR(MAX),
@Description NVARCHAR(MAX),
@Thumbnail NVARCHAR(MAX),
@Duration BIGINT,
@Url NVARCHAR(MAX),
@Response SMALLINT OUTPUT
)
AS
BEGIN
		BEGIN TRY
			BEGIN TRANSACTION

			-- (1) FIRST WILL CHECK THE CHANNEL EXISTS
			IF(NOT EXISTS(SELECT Id FROM Channels WHERE Id = @ChannelId))
			BEGIN
					SET @Response = 0;
ROLLBACK TRANSACTION;
RETURN;
END
--(1) FIRST WILL CHECK THE CHANNEL EXISTS


			DECLARE @Id UNIQUEIDENTIFIER = NEWID();

--(2) INSERTING INTO VIDEOS
			INSERT INTO Videos VALUES
			(
			@Id,
@ChannelId,
@PlayListId,
@Title,
@Description,
@Thumbnail,
			1,
@Duration,
GETDATE(),
GETDATE(),
@Url
			)

			--(2) INSERTING INTO VIDEOS

			--(3) NOW GET ALL THE SUBSCRIBERS OF THIS CHANNEL WHO HAVE NOTIFY TRUE
			   IF(EXISTS(SELECT * FROM Subscribers WHERE ChannelId = '861B6371-426C-4868-ACD7-F96DBE227456' AND Notify = 1))
			   BEGIN
					INSERT INTO Notifications (Id, Title, ChannelId, UserId, HasRead, CreatedAt, UpdatedAt)
					SELECT NEWID(), 'New video uploaded: ' + @Title, ChannelId, UserId, 0, GETDATE(), GETDATE()
					FROM Subscribers
					'WHERE ChannelId = @ChannelId AND Notify = 1;
				END=
			--(3) NOW GET ALL THE SUBSCRIBERS OF THIS CHANNEL WHO HAVE NOTIFY TRUE

			SET @Response = 1;
COMMIT TRANSACTION;
END TRY

		BEGIN CATCH
		SET @Response = -1;
ROLLBACK TRANSACTION;
END CATCH
END;

//video*/