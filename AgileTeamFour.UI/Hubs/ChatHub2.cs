using AgileTeamFour.UI.Models;
using Microsoft.AspNetCore.SignalR;
using System.Text.RegularExpressions;

namespace AgileTeamFour.UI.Hubs
{
    public class ChatHub2 : Hub
    {
        public async Task SendMessage2(string GroupName, string message, string guildID, string AuthorID, string UserName)
        {
            GuildComment comment = new GuildComment();
            comment.TimePosted = DateTime.Now;
            comment.Text = message;
            comment.GuildId = int.Parse(guildID);
            comment.AuthorID = int.Parse(AuthorID);

            if (comment.Text != null && comment.Text.Trim() != "")
            {
                await Clients.Group(GroupName).SendAsync("ReceiveMessage2", UserName.ToString(), comment.AuthorID, guildID, message, comment.TimePosted.ToString("hh:mm tt"));
                GuildCommentManager.Insert(comment);
            }
        }

        public Task JoinGroup(string groupName)
        {
            return Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }

        public Task LeaveGroup(string groupName)
        {
            return Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        }
    }
}