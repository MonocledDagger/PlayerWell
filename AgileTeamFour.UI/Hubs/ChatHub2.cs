using AgileTeamFour.UI.Models;
using Microsoft.AspNetCore.SignalR;

namespace AgileTeamFour.UI.Hubs
{
    public class ChatHub2 : Hub
    {
        public async Task SendMessage2(string message, string guildID, string AuthorID, string UserName)
        {
            GuildComment comment = new GuildComment();
            comment.TimePosted = DateTime.Now;
            comment.Text = message;
            comment.GuildId = int.Parse(guildID);
            comment.AuthorID = int.Parse(AuthorID);

            if (comment.Text != null && comment.Text.Trim() != "")
            {
                await Clients.All.SendAsync("ReceiveMessage2", UserName.ToString(), comment.AuthorID, guildID, message, comment.TimePosted.ToString("hh:mm tt"));
                GuildCommentManager.Insert(comment);
            }
        }
    }
}