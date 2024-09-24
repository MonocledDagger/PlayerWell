
using AgileTeamFour.BL.Models;

namespace AgileTeamFour.BL
{
    public static class ReviewManager
    {
        
        //Adding a value to any foreign key requires the foreign key value first exist as a a primary key value of the referenced table
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
                    entity.DateTime = review.DateTime;

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
                    entity.DateTime = review.DateTime;

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
        
    }
}

