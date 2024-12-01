
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

                    tblPost  entity = new tblPost();
                    entity.PostID=dc.tblPosts.Any() ? dc.tblPosts.Max(s => s.PostID) + 1 : 1;
                    entity.AuthorID = post.AuthorID;
                    entity.Text =post.Text;
                    entity.Image=post.Image;
                    entity.TimePosted = post.TimePosted;
                    dc.tblPosts.Add(entity);
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


        public static List<Post> Load()
        {
            try
            {
                List<Post> list = new List<Post>();

                using (AgileTeamFourEntities dc = new AgileTeamFourEntities())
                {
                    var postsWithAverage = (from c in dc.tblPosts
                                            join u in dc.tblUsers on c.AuthorID equals u.UserID
                                            orderby c.PostID descending
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
                                                ReviewSummary = GetReviewSummaryForUser(u.UserID),
                                                Comments = PostCommentManager.LoadByPostId(c.PostID) 
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
                        ReviewSummary = post.ReviewSummary,
                        Comments = post.Comments 
                    }));
                }

                return list;
            
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static Post GetPostWithComments(int postId)
        {
            using (var dc = new AgileTeamFourEntities())
            {
                var post = dc.tblPosts
                    .Where(p => p.PostID == postId)
                    .Select(p => new Post
                    {
                        PostID = p.PostID,
                        AuthorID = p.AuthorID,
                        Text = p.Text,
                        TimePosted = p.TimePosted,
                        Comments = dc.tblPostComments
                            .Where(c => c.PostID == postId)
                            .Select(c => new PostComment
                            {
                                CommentID = c.CommentID,
                                ParentCommentID = c.ParentCommentID,
                                AuthorID = c.AuthorID,
                                Text = c.Text,
                                TimePosted = c.TimePosted
                            }).ToList()
                    }).FirstOrDefault();
                return post;
            }
        }

        public static string GetReviewSummaryForUser(int userId)
        {
            try
            {
                using (AgileTeamFourEntities dc = new AgileTeamFourEntities())
                {
                    var reviews = dc.tblReviews
                                    .Where(r => r.RecipientID == userId)
                                    .Select(r => r.ReviewText)
                                    .ToList();

                    if (reviews.Count == 0)
                    {
                        return "No reviews available.";
                    }

                    var allReviewsText = string.Join(" ", reviews);
                    var wordFrequency = allReviewsText
                        .Split(new char[] { ' ', '.', ',', '!', '?' }, StringSplitOptions.RemoveEmptyEntries)
                        .Where(word => word.Length > 2) 
                        .GroupBy(word => word.ToLower())
                        .ToDictionary(g => g.Key, g => g.Count());
                    var topWords = wordFrequency.OrderByDescending(w => w.Value)
                                                .Take(30)
                                                .Select(w => w.Key)
                                                .ToList();

                   
                    var summaryPhrase = string.Join(" ", topWords);
                    return summaryPhrase;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static List<string> GetPostLikes(int postId)
        {
            using (var dc = new AgileTeamFourEntities())
            {
                return dc.tblPostLikes
                         .Where(like => like.PostID == postId)
                         .Join(dc.tblUsers,
                               like => like.UserID,
                               user => user.UserID,
                               (like, user) => user.UserName)
                         .ToList();
            }
        }
      

        public static bool toggleLike(int postId, int userId)
        {
            using (var dc = new AgileTeamFourEntities())
            {
                tblPostLike entity = new tblPostLike();
                entity.LikeID = dc.tblPostLikes.Any() ? dc.tblPostLikes.Max(s => s.LikeID) + 1 : 1;



                var existingLike = dc.tblPostLikes.FirstOrDefault(like => like.PostID == postId && like.UserID == userId);
                if (existingLike != null)
                {
                    dc.tblPostLikes.Remove(existingLike);
                }
                else
                {
                    dc.tblPostLikes.Add(new tblPostLike
                    {
                        LikeID=entity.LikeID,
                        PostID = postId,
                        UserID = userId
                    });
                }
                dc.SaveChanges();
                return existingLike == null; 
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
