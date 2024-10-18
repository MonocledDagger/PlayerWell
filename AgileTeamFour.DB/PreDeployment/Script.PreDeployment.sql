/*
 Pre-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be executed before the build script.	
 Use SQLCMD syntax to include a file in the pre-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the pre-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
DROP TABLE IF EXISTS tblPost;
DROP TABLE IF EXISTS tblGames;
DROP TABLE IF EXISTS tblComments;
DROP TABLE IF EXISTS tblPlayerEvents;
DROP TABLE IF EXISTS tblEvents;
DROP TABLE IF EXISTS tblReviews;
DROP TABLE IF EXISTS tblUsers;
DROP TABLE IF EXISTS tblFriend;
DROP TABLE IF EXISTS tblGuild;
DROP TABLE IF EXISTS tblPlayerGuild;





