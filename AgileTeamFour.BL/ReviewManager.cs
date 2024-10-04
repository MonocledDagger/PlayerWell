
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
                     join author in dc.tblUsers on r.AuthorID equals author.UserID
                     join recipient in dc.tblUsers on r.RecipientID equals recipient.UserID
                     select new
                     {
                         r.ReviewID,
                         r.StarsOutOf5,
                         r.ReviewText,
                         r.AuthorID,
                         AuthorName = author.UserName,
                         r.RecipientID,
                         RecipientName = recipient.UserName,
                         r.DateTime
                     })
             .ToList()
             .ForEach(review => list.Add(new Review
             {
                 ReviewID = review.ReviewID,
                 StarsOutOf5 = review.StarsOutOf5,
                 ReviewText = review.ReviewText,
                 AuthorID = review.AuthorID,
                 AuthorName = review.AuthorName,
                 RecipientID = review.RecipientID,
                 RecipientName = review.RecipientName,
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



        public static int DeleteIncompleteReviews(int id, bool rollback = false)
        {
            int rowsAffected = 0;

            // Get all the events that have completed in the past 30 to 60 days
            List<Events> lastMonthsEvents = EventManager.Load()
                .Where(e => e.DateTime >= DateTime.Now.AddDays(-30)  // Move than 30 days ago
                         && e.DateTime <= DateTime.Now.AddDays(-60)) // Less than 60 days ago
                .OrderByDescending(e => e.DateTime)
                .ToList();

            // Find reviews that were incomplete that are over 30 days old
            List<Review> incompleteReviews = ReviewManager.Load()
                .Where(r => r.ReviewText == "87|6#x4A|tkg"
                && r.DateTime <= DateTime.Now.AddDays(-30)).ToList();

            // Delete them all 
            foreach (Review review in incompleteReviews)
            {
                Delete(review.ReviewID);
                rowsAffected++;
            }
            return rowsAffected;
        }
        public static int CreatePlayerReviewsAfterEvent(int id, bool rollback = false)
        {
            int rowsAffected = 0;

            // Get all the events that have completed in the past week
            List<Events> events = EventManager.Load()
                .Where(e => e.DateTime >= DateTime.Now.AddDays(-7)
                && e.DateTime <= DateTime.Now)
                .OrderByDescending(e => e.DateTime).ToList();

            // Iterate through each event
            foreach (var evt in events)
            {
                // Get all participants for the event
                List<PlayerEvent> playerEvents = PlayerEventManager.LoadByEventID(evt.EventID);

                // Create a list of PlayerId attributes from the playerEvents
                List<int> playerIds = playerEvents.Select(pe => pe.PlayerID).ToList();

                // Iterate through each participant
                foreach (var authorId in playerIds)
                {
                    foreach (var recipientId in playerIds)
                    {
                        if (recipientId != authorId)
                        {
                            bool reviewExists = ReviewManager.ReviewExists(authorId, recipientId);

                            // If no review exists, create an empty review
                            if (!reviewExists)
                            {
                                int result = InsertEmptyReview(authorId, recipientId); // Assuming author and recipient are not the same
                                rowsAffected++; 
                            }
                        }
                    }                   
                }
            }
            return rowsAffected;
        }
        public static List<Review> LoadPlayerReviews(int id)
        {
            List<Review> reviews = ReviewManager.Load()
                .Where(r => r.AuthorID == id)
                .OrderByDescending(e => e.DateTime).ToList();
            return reviews;
        }
        private static bool ReviewExists(int authorId, int recipientId)
        {
            try
            {
                using (AgileTeamFourEntities dc = new AgileTeamFourEntities())
                {
                    // Check if a review exists for the given event and participant
                    return dc.tblReviews.Any(r => r.AuthorID == authorId && r.RecipientID == recipientId);
                }
            }
            catch (Exception ex)
            {
                // Log the exception (if you have a logging mechanism)
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw; // Consider whether to throw or handle it in a different way based on your needs
            }
        }
        private static int InsertEmptyReview(int authorId, int recipientId, bool rollback = false)
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





    }
}

