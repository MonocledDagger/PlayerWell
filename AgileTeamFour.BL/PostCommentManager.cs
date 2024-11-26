
using AgileTeamFour.BL.Models;

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
                    entity.ParentCommentID = postcomment.ParentCommentID;

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
                
                var comments = dc.tblPostComments
                                 .Where(pc => pc.PostID == postId && pc.ParentCommentID == 0)
                                 .Join(dc.tblUsers,
                                       pc => pc.AuthorID,
                                       user => user.UserID,
                                       (pc, user) => new PostComment
                                       {
                                           CommentID = pc.CommentID,
                                           PostID = pc.PostID,
                                           Text = pc.Text,
                                           TimePosted = pc.TimePosted,
                                           AuthorID = pc.AuthorID,
                                           UserName = user.UserName,
                                           IconPic = user.IconPic,
                                           Replies = GetReplies(pc.CommentID) // Fetch all replies recursively
                                       })
                                 .ToList();

                return comments;
            }
        }

        private static List<PostComment> GetReplies(int parentCommentId)
        {
            using (var dc = new AgileTeamFourEntities())
            {
                return dc.tblPostComments
                         .Where(reply => reply.ParentCommentID == parentCommentId)
                         .Join(dc.tblUsers,
                               reply => reply.AuthorID,
                               user => user.UserID,
                               (reply, user) => new PostComment
                               {
                                   CommentID = reply.CommentID,
                                   PostID = reply.PostID,
                                   Text = reply.Text,
                                   TimePosted = reply.TimePosted,
                                   AuthorID = reply.AuthorID,
                                   UserName = user.UserName,
                                   IconPic = user.IconPic,
                                   Replies = GetReplies(reply.CommentID) 
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
                                         join u in dc.tblUsers on c.AuthorID equals u.UserID
                                         
                                         select new
                                         {
                                             c.CommentID,
                                             c.PostID,
                                             c.AuthorID,
                                             c.TimePosted,
                                             c.Text,
                                             c.ParentCommentID,
                                             u.UserName,
                                             u.IconPic
                                         }).ToList();

                    
                    var commentDict = postsComments.ToDictionary(
                        c => c.CommentID,
                        c => new PostComment
                        {
                            CommentID = c.CommentID,
                            PostID = c.PostID,
                            TimePosted = c.TimePosted,
                            Text = c.Text,
                            AuthorID = c.AuthorID,
                            ParentCommentID = c.ParentCommentID,
                            UserName = c.UserName,
                            IconPic = c.IconPic,
                            
                        });

                    
                    foreach (var comment in commentDict.Values)
                    {
                        if (comment.ParentCommentID!=0 && commentDict.ContainsKey(comment.ParentCommentID))
                        {
                            // Add comment as a reply to its parent
                            commentDict[comment.ParentCommentID].Replies.Add(comment);
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
