using AgileTeamFour.BL.Models;
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

        public async Task SendMessageFriend(string message, string RecieverID, string AuthorID, string UserName)
        {
            FriendComment comment = new FriendComment();
            comment.TimePosted = DateTime.Now;
            comment.Text = message;
            comment.FriendSentToID = int.Parse(RecieverID);
            comment.AuthorID = int.Parse(AuthorID);

            if (comment.Text != null && comment.Text.Trim() != "")
            {
                await Clients.All.SendAsync("ReceiveMessageFriend", UserName.ToString(), comment.AuthorID, comment.FriendSentToID, message, comment.TimePosted.ToString("hh:mm tt"));
                FriendCommentManager.Insert(comment);
            }
        }
        public async Task GetAllFriendMessages(string friendID, string playerID)
        {
            int FriendID = int.Parse(friendID);
            int PlayerID = int.Parse(playerID);

            string FriendName = UserManager.LoadById(FriendID).UserName;
            string PlayerName = UserManager.LoadById(PlayerID).UserName;
            List<FriendComment> list = FriendCommentManager.Load().
                Where(friend => (friend.FriendSentToID == FriendID || friend.FriendSentToID == PlayerID)
                && (friend.AuthorID == FriendID || friend.AuthorID == PlayerID)).OrderBy(friend => friend.TimePosted).ToList();

            foreach(FriendComment comment in list)
            {
                string UserName;
                if (comment.AuthorID == PlayerID)
                    UserName = PlayerName;
                else if (comment.AuthorID == FriendID)
                    UserName = FriendName;
                else
                    UserName = "1";
                await Clients.Caller.SendAsync("ReceiveMessageFriend", UserName, comment.AuthorID, comment.FriendSentToID, comment.Text, comment.TimePosted.ToString("hh:mm tt"));

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