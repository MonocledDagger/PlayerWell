namespace AgileTeamFour.UI.ViewModels
{
    public class UserReviewsVM
    {
        public List<User> Users { get; set; }
        public List<Review> Reviews { get; set; }

        public UserReviewsVM()
        {
            Users = UserManager.Load();
            Reviews = ReviewManager.Load();
        }
    }


}
