
namespace AgileTeamFour.BL
{
    public static class PostManager
    {

        public static int Insert(Post post, bool rollback = false)
        {
            try
            {
                int results = 0;
                using (AgileTeamFourEntities dc = new AgileTeamFourEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblPost entity = new tblPost();
                    entity.PostID = dc.tblPosts.Any() ? dc.tblPosts.Max(s => s.PostID) + 1 : 1;
                    entity.TimePosted = post.TimePosted;
                    entity.Image = post.Image;
                    entity.Text = post.Text;

                    // Must check that AuthorID is a valid value in the Players Table
                    int? id = UserManager.LoadById(post.AuthorID).UserID;
                    if (id == -99) // If -99, it must not be a primary key in the Players Table
                        throw new Exception("Invalid AuthorID");
                    else
                        entity.AuthorID = post.AuthorID;

                    // IMPORTANT - BACK FILL THE ID
                    post.PostID = entity.PostID;

                    dc.tblPosts.Add(entity);
                    results = dc.SaveChanges();

                    if (rollback)
                        transaction.Rollback();
                    else
                        transaction?.Commit();
                }
                return results;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static int Update(Post post, bool rollback = false)
        {
            try
            {
                int results = 0;
                using (AgileTeamFourEntities dc = new AgileTeamFourEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblPost entity = dc.tblPosts.SingleOrDefault(e => e.PostID == post.PostID);
                    if (entity == null)
                        throw new Exception("Post not found");

                    entity.TimePosted = post.TimePosted;
                    entity.Image = post.Image;
                    entity.Text = post.Text;

                    // Check that AuthorID is a valid value in the Players Table
                    int id = UserManager.LoadById(post.AuthorID).UserID;
                    if (id == -99) // If -99, it must not be a primary key in the Players Table
                        throw new Exception("Invalid AuthorID");
                    else
                        entity.AuthorID = post.AuthorID;

                    results = dc.SaveChanges();

                    if (rollback)
                        transaction.Rollback();
                    else
                        transaction?.Commit();
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

                    tblPost entity = dc.tblPosts.SingleOrDefault(e => e.PostID == id);
                    if (entity == null)
                        throw new Exception("Post not found");

                    dc.tblPosts.Remove(entity);
                    results = dc.SaveChanges();

                    if (rollback)
                        transaction.Rollback();
                    else
                        transaction?.Commit();
                }
                return results;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static Post LoadById(int id)
        {
            try
            {
                Post post = new Post();
                using (AgileTeamFourEntities dc = new AgileTeamFourEntities())
                {
                    tblPost entity = dc.tblPosts.SingleOrDefault(e => e.PostID == id);
                    if (entity == null)
                        throw new Exception("Post not found");

                    post.PostID = id;
                    post.TimePosted = entity.TimePosted;
                    post.Image = entity.Image;
                    post.Text = entity.Text;
                    post.AuthorID = entity.AuthorID;

                    return post;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }



        //    // Fetch the list of posts from your data source
        //    var posts = _postService.GetAllPosts();

        //    // Map to the ViewModel
        //    var postVMs = posts.Select(post => new AgileTeamFour.UI.ViewModels.PostVM
        //    {
        //        PostID = post.PostID,
        //        AuthorID = post.AuthorID,
        //        TimePosted = post.TimePosted,
        //        Image = post.Image,
        //        Text = post.Text
        //        // Add more properties as needed
        //    }).ToList();

        //// Pass the ViewModel list to the view
        //return View(postVMs);
        //}




        public static List<Post> Load()
        {
            try
            {
                List<Post> list = new List<Post>();

                using (AgileTeamFourEntities dc = new AgileTeamFourEntities())
                {
                    var postsWithAverage = (from c in dc.tblPosts
                                            join u in dc.tblUsers on c.AuthorID equals u.UserID
                                            select new
                                            {
                                                c.PostID,
                                                c.TimePosted,
                                                c.Text,
                                                c.Image,
                                                c.AuthorID,
                                                u.UserName,
                                                u.IconPic,
                                                u.Bio,
                                                AverageStars = GetAverageStarsForUser(u.UserID),
                                                ReviewSummary= GetReviewSummaryForUser(u.UserID)
                })
                                           .ToList();

                    postsWithAverage.ForEach(post => list.Add(new Post
                    {
                        PostID = post.PostID,
                        TimePosted = post.TimePosted,
                        Text = post.Text,
                        Image = post.Image,
                        AuthorID = post.AuthorID,
                        UserName = post.UserName,
                        IconPic = post.IconPic,
                        Bio = post.Bio,
                        AverageStarsOutOf5 = post.AverageStars,
                        ReviewSummary= post.ReviewSummary
                    }));

                    return list;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static string GetReviewSummaryForUser(int userId)
        {
            try
            {
                using (AgileTeamFourEntities dc = new AgileTeamFourEntities())
                {
                    // Get all reviews for the specified user
                    var reviews = dc.tblReviews
                                    .Where(r => r.RecipientID == userId)
                                    .Select(r => r.ReviewText)
                                    .ToList();

                    // Generate a summary from the review texts
                    if (reviews.Count == 0)
                    {
                        return "No reviews available.";
                    }

                    // Option 1: Concatenate all reviews to create a simple summary
                    var summary = string.Join(" ", reviews);

                    // Option 2: Limit to the first few reviews or a certain number of characters
                    // Example: Show the first 100 characters or first 3 reviews
                    var limitedSummary = string.Join(" ", reviews.Take(3));
                    if (limitedSummary.Length > 100)
                    {
                        limitedSummary = limitedSummary.Substring(0, 100) + "...";
                    }

                    return limitedSummary; // Return the summarized reviews
                }
            }
            catch (Exception)
            {
                throw;
            }
        }



        public static double GetAverageStarsForUser(int userId)
        {
            try
            {
                using (AgileTeamFourEntities dc = new AgileTeamFourEntities())
                {
                    // Get all reviews for the specified user
                    var reviews = dc.tblReviews.Where(r => r.RecipientID == userId).ToList();
                    var reviewCount = reviews.Count();

                   
                    double totalStars = 0;

                    for (int i = 0; i < reviewCount; i++)
                    {
                        double stars = reviews[i].StarsOutOf5;
                        totalStars += stars; 

                    }

                    // Calculate the average stars
                    double averageStars = reviewCount > 0 ? totalStars / reviewCount : 0;


                    return averageStars; 
                }
            
            }
            catch (Exception ex)
            {
                
                throw;
            }
        }
    }
    }
