/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

:r .\DefaultData\tblPosts.sql
:r .\DefaultData\tblEvents.sql
:r .\DefaultData\tblComments.sql
:r .\DefaultData\tblReviews.sql
:r .\DefaultData\tblPlayerEvents.sql
:r .\DefaultData\tblGames.sql
/* Their is no default data for User */
:r .\DefaultData\tblGuilds.sql
:r .\DefaultData\tblPlayerGuilds.sql
:r .\DefaultData\tblFriends.sql
:r .\DefaultData\tblFriendComments.sql
