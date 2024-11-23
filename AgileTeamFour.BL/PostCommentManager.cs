
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
                // Fetching the post comments and their responses
                var comments = dc.tblPostComments
                                 .Where(pc => pc.PostID == postId && pc.ParentCommentID==0)
                                 .Select(pc => new
                                 {
                                     ParentComment = pc,
                                     // Fetch replies for each comment
                                     Replies = dc.tblPostComments
                                                 .Where(reply => reply.ParentCommentID == pc.CommentID)
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
                                                           // Fetch replies for nested replies
                                                           Replies = dc.tblPostComments
                                                                       .Where(subReply => subReply.ParentCommentID == reply.CommentID)
                                                                       .Join(dc.tblUsers,
                                                                             subReply => subReply.AuthorID,
                                                                             user => user.UserID,
                                                                             (subReply, user) => new PostComment
                                                                             {
                                                                                 CommentID = subReply.CommentID,
                                                                                 PostID = subReply.PostID,
                                                                                 Text = subReply.Text,
                                                                                 TimePosted = subReply.TimePosted,
                                                                                 AuthorID = subReply.AuthorID,
                                                                                 UserName = user.UserName,
                                                                                 IconPic = user.IconPic
                                                                             }).ToList()
                                                       }).ToList()
                                 })
                                 .Join(dc.tblUsers,
                                       pc => pc.ParentComment.AuthorID,
                                       user => user.UserID,
                                       (pc, user) => new PostComment
                                       {
                                           CommentID = pc.ParentComment.CommentID,
                                           PostID = pc.ParentComment.PostID,
                                           Text = pc.ParentComment.Text,
                                           TimePosted = pc.ParentComment.TimePosted,
                                           AuthorID = pc.ParentComment.AuthorID,
                                           UserName = user.UserName,
                                           IconPic = user.IconPic,
                                           Replies = pc.Replies // Assign the replies to this comment
                                       })
                                 .ToList();

                // Returning the comments with replies (including nested replies)
                return comments;
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
