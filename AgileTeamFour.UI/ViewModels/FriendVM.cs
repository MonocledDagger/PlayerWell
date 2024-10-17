namespace AgileTeamFour.UI.ViewModels
{
    public class FriendVM
    {
        public Friend Friend { get; set; }
        public User User { get; set; }

        public IEnumerable<FriendVM> Friends { get; set; }
        public IEnumerable<FriendVM> PendingRequests { get; set; }
    }
}
