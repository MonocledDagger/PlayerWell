using AgileTeamFour.UI.Models;
using Microsoft.AspNetCore.SignalR;

namespace AgileTeamFour.UI.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string message, string EventID, string AuthorID, string UserName)
        {
            Comment comment = new Comment();
            comment.TimePosted = DateTime.Now;
            comment.Text = message;
            comment.EventID = int.Parse(EventID);
            comment.AuthorID = int.Parse(AuthorID);

            if (comment.Text != null && comment.Text.Trim() != "")
            {
                await Clients.All.SendAsync("ReceiveMessage", UserName.ToString(), comment.AuthorID, message, comment.TimePosted.ToString("hh:mm tt"));
                CommentManager.Insert(comment);
            }
        }
    }
}