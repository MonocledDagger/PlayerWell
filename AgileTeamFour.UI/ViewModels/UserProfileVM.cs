namespace AgileTeamFour.UI.ViewModels
{
    public class UserProfileVM
    {
        public User CurrentUser { get; set; }
        public List<User> Users { get; set; }
        public List<Review> Reviews { get; set; }
        public List<Friend> Friends {  get; set; }

        public UserProfileVM(int userId)
        {
            CurrentUser = UserManager.LoadById(userId);
            Users = UserManager.Load();
            Reviews = ReviewManager.LoadPlayerReviews(CurrentUser.UserID);
            Friends = FriendManager.GetFriendsForUser(CurrentUser.UserID);
        }
    }
}
