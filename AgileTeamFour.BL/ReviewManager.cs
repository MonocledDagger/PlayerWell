
using AgileTeamFour.BL.Models;
using AgileTeamFour.BL;

namespace AgileTeamFour.BL
{
    public static class ReviewManager
    {
        
        //Adding a value to any foreign key requires the foreign key value first exist as a primary key value of the referenced table
        //Reviews.AuthorID uses Users.UserID as a foreign key
        //Reviews.RecipientID uses Users.UserID as a foreign key
        public static int Insert(Review review, bool rollback = false)
        {
            try
            {
                int results = 0;
                using (AgileTeamFourEntities dc = new AgileTeamFourEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblReview entity = new tblReview();
                    entity.ReviewID = dc.tblReviews.Any() ? dc.tblReviews.Max(s => s.ReviewID) + 1 : 1;
                    entity.StarsOutOf5 = review.StarsOutOf5;
                    entity.ReviewText = review.ReviewText;
                    entity.DateTime = DateTime.Now;

                    //Must Check that AuthorID and RecipientID are valid values in the Users Table
                    //*********************

                    int id = UserManager.LoadById(review.AuthorID).UserID;          
                    if (id == -99) //If -99, Must not be a primary key with the AuthorID (foreign key value) in User Table
                        throw new Exception();
                    else
                        entity.AuthorID = review.AuthorID;

                    id = UserManager.LoadById(review.RecipientID).UserID;                
                    if (id == -99) //If -99, Must not be a primary key with the AuthorID (foreign key value) in User Table
                        throw new Exception();
                    else
                        entity.RecipientID = review.RecipientID;

                    //*********************



                    // IMPORTANT - BACK FILL THE ID
                    review.ReviewID = entity.ReviewID;

                    dc.tblReviews.Add(entity);
                    results = dc.SaveChanges();

                    if (rollback) transaction.Rollback();

                }
                return results;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public static int Update(Review review, bool rollback = false)
        {
            try
            {
                int results = 0;
                using (AgileTeamFourEntities dc = new AgileTeamFourEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblReview entity = dc.tblReviews.Where(e => e.ReviewID == review.ReviewID).FirstOrDefault();
                    entity.StarsOutOf5 = review.StarsOutOf5;
                    entity.ReviewText = review.ReviewText;
                    entity.DateTime = DateTime.Now;

                    //Must Check that AuthorID and RecipientID are valid values in the Users Table
                    //*********************

                    int id = UserManager.LoadById(review.AuthorID).UserID;
                    if (id == -99) //If -99, Must not be a primary key with the AuthorID (foreign key value) in User Table
                        throw new Exception();
                    else
                        entity.AuthorID = review.AuthorID;

                    id = UserManager.LoadById(review.RecipientID).UserID;
                    if (id == -99) //If -99, Must not be a primary key with the AuthorID (foreign key value) in User Table
                        throw new Exception();
                    else
                        entity.RecipientID = review.RecipientID;

                    //*********************

                    results = dc.SaveChanges();

                    if (rollback) transaction.Rollback();

                }
                return results;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public static int Delete(int id, bool rollback = false)
        {
            try
            {
                int results = 0;
                using (AgileTeamFourEntities dc = new AgileTeamFourEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblReview entity = dc.tblReviews.Where(e => e.ReviewID == id).FirstOrDefault();

                    dc.tblReviews.Remove(entity);
                    results = dc.SaveChanges();

                    if (rollback) transaction.Rollback();

                }
                return results;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public static Review LoadById(int id)
        {
            try
            {
                Review review = new Review();
                using (AgileTeamFourEntities dc = new AgileTeamFourEntities())
                {
                    tblReview entity = dc.tblReviews.Where(e => e.ReviewID == id).FirstOrDefault();
                    review.ReviewID = id;
                    review.StarsOutOf5 = entity.StarsOutOf5;
                    review.ReviewText = entity.ReviewText;
                    review.DateTime = entity.DateTime;

                    review.AuthorID = entity.AuthorID;
                    review.RecipientID = entity.RecipientID;


                    return review;
                }

            }
            catch (Exception)
            {
                throw;
            }
        }
        public static List<Review> Load()
        {
            try
            {
                List<Review> list = new List<Review>();

                using (AgileTeamFourEntities dc = new AgileTeamFourEntities())
                {
                    (from r in dc.tblReviews
                     select new
                     {
                        r.ReviewID,
                        r.StarsOutOf5,
                        r.ReviewText,
                        r.AuthorID,
                        r.RecipientID,
                        r.DateTime
                })
                     .ToList()
                     .ForEach(review => list.Add(new Review
                     {
                         ReviewID = review.ReviewID,
                         StarsOutOf5 = review.StarsOutOf5,
                         ReviewText = review.ReviewText,
                         AuthorID = review.AuthorID,
                         RecipientID = review.RecipientID,
                         DateTime = review.DateTime
                     }));
                }

                return list;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static int InsertEmptyReview(int authorId, int recipientId, bool rollback = false)
        {
            int rowsAffected = 0;
            Review emptyReview = new Review
            {
                StarsOutOf5 = 5,
                ReviewText = "87|6#x4A|tkg",
                AuthorID = authorId,
                RecipientID = recipientId,
            };

            rowsAffected = Insert(emptyReview);

            return rowsAffected;
        }

        public static int DeleteIncompletedReviews(int id, bool rollback = false)
        {
            int rowsAffected = 0;

            // Get all the events that have completed in the past 30 to 60 days
            List<Events> lastMonthsEvents = EventManager.Load()
                .Where(e => e.DateTime.AddDays(-30) >= DateTime.Now.AddDays(-60)
                && e.DateTime <= DateTime.Now)
                .OrderByDescending(e => e.DateTime).ToList();

            // if review is password and DateTime set is 30 days behind
            // and body is still password then delete pending review

            return rowsAffected;
        }

        public static int CreateEventReviews(int id, bool rollback = false)
        {
            int rowsAffected = 0;
            // Get all the events that have completed in the past week
            List<Events> events = EventManager.Load()
                .Where(e => e.DateTime >= DateTime.Now.AddDays(-7)
                && e.DateTime <= DateTime.Now)
                .OrderByDescending(e => e.DateTime).ToList();

            //Check if reviews were created for these event players if not create


            return rowsAffected;
        }





    }
}

