namespace AgileTeamFour.UI.ViewModels
{
    public class FriendVM
    {
        public Friend Friend { get; set; }
        public User UserReceiver { get; set; }
        public User UserSender { get; set; }

        public User User { get; set; }

        public List<User> FriendsList { get; set; }
        public List<User> Users { get; set; }
        //public IEnumerable<FriendVM> Friends { get; set; }
        //public IEnumerable<FriendVM> PendingRequests { get; set; }
    }
}
