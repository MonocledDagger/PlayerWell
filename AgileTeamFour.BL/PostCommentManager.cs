
namespace AgileTeamFour.BL
{
    public static class PostCommentManager
    {
        public static int Insert(PostComment postcomment, bool rollback = false)
        {
            try
            {
                int results = 0;
                using (AgileTeamFourEntities dc = new AgileTeamFourEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblPostComment entity = new tblPostComment();
                    entity.CommentID = dc.tblPostComments.Any() ? dc.tblPostComments.Max(s => s.CommentID) + 1 : 1;
                    entity.PostID = postcomment.PostID;
                    entity.AuthorID = postcomment.AuthorID;
                    entity.Text = postcomment.Text;
                    entity.TimePosted = postcomment.TimePosted;
                    entity.ParentCommentID = (int)postcomment.ParentCommentID;
                    dc.tblPostComments.Add(entity);
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

        public static PostComment LoadById(int id)
        {
            try
            {
                PostComment post = new PostComment();
                using (AgileTeamFourEntities dc = new AgileTeamFourEntities())
                {
                    tblPostComment entity = dc.tblPostComments.SingleOrDefault(e => e.PostID == id);
                    if (entity == null)
                        throw new Exception("Post not found");
                    post.CommentID = id;
                    post.PostID = entity.PostID; ;
                    post.TimePosted = entity.TimePosted;
                    post.Text = entity.Text;
                    post.AuthorID = entity.AuthorID;
                    post.ParentCommentID = (int)entity.ParentCommentID;

                    return post;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static List<PostComment> LoadByPostId(int postId)
        {
            using (var dc = new AgileTeamFourEntities())
            {
                return dc.tblPostComments
                         .Where(pc => pc.PostID == postId)
                         .Select(pc => new PostComment
                         {
                             CommentID = pc.CommentID,
                             PostID = pc.PostID,
                             Text = pc.Text,
                             TimePosted = pc.TimePosted,
                             AuthorID = pc.AuthorID
                         })
                         .ToList();
            }

        }

        public static List<PostComment> Load()
        {
            try
            {
                List<PostComment> list = new List<PostComment>();

                using (AgileTeamFourEntities dc = new AgileTeamFourEntities())
                {
                    var postsComments = (from c in dc.tblPostComments
                                         select new
                                         {
                                             c.CommentID,
                                             c.PostID,
                                             c.AuthorID,
                                             c.TimePosted,
                                             c.Text,
                                             c.ParentCommentID
                                         }).ToList();

                    // Step 1: Create a dictionary to hold all comments by ID
                    var commentDict = postsComments.ToDictionary(
                        c => c.CommentID,
                        c => new PostComment
                        {
                            CommentID = c.CommentID,
                            PostID = c.PostID,
                            TimePosted = c.TimePosted,
                            Text = c.Text,
                            AuthorID = c.AuthorID,
                            ParentCommentID = c.ParentCommentID
                        });

                    // Step 2: Organize comments into hierarchy
                    foreach (var comment in commentDict.Values)
                    {
                        if (comment.ParentCommentID.HasValue && commentDict.ContainsKey(comment.ParentCommentID.Value))
                        {
                            // Add comment as a reply to its parent
                            commentDict[comment.ParentCommentID.Value].Replies.Add(comment);
                        }
                        else
                        {
                            // Add top-level comments to the main list
                            list.Add(comment);
                        }
                    }
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
