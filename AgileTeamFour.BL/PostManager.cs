//using AgileTeamFour.UI; // Namespace for AgileTeamFourEntities and tblPost
using AgileTeamFour.PL; // Any additional namespaces if needed


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

        public static List<Post> Load()
        {
            try
            {
                List<Post> list = new List<Post>();

                using (AgileTeamFourEntities dc = new AgileTeamFourEntities())
                {
                    (from c in dc.tblPosts
                     select new
                     {
                         c.PostID,
                         c.TimePosted,
                         c.Text,
                         c.Image,
                         c.AuthorID,
                     })
                    .ToList()
                    .ForEach(post => list.Add(new Post
                    {
                        PostID = post.PostID,
                        TimePosted = post.TimePosted,
                        Text = post.Text,
                        Image = post.Image,
                        AuthorID = post.AuthorID,
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
