using AgileTeamFour.UI.Models;
using Microsoft.AspNetCore.SignalR;

namespace AgileTeamFour.UI.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string message, string EventID, string AuthorID, string UserName)
        {   
            await Clients.All.SendAsync("ReceiveMessage", UserName.ToString(), message);

            Comment comment = new Comment();
            comment.TimePosted = DateTime.Now;
            comment.Text = message;
            comment.EventID = int.Parse(EventID);
            comment.AuthorID = int.Parse(AuthorID);

            CommentManager.Insert(comment);
        }
    }
}