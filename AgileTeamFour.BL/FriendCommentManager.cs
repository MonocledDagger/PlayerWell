
namespace AgileTeamFour.BL
{
    public static class FriendCommentManager
    {
        
        public static int Insert(FriendComment friendComment, bool rollback = false)
        {
            try
            {
                int results = 0;
                using (AgileTeamFourEntities dc = new AgileTeamFourEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblFriendComment entity = new tblFriendComment();
                    entity.ID = dc.tblFriendComments.Any() ? dc.tblFriendComments.Max(s => s.ID) + 1 : 1;
                    entity.FriendSentToID = friendComment.FriendSentToID;
                    entity.TimePosted = friendComment.TimePosted;
                    entity.AuthorID = friendComment.AuthorID;
                    entity.Text = friendComment.Text;

                    dc.tblFriendComments.Add(entity);
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

        public static int Update(FriendComment friendComment, bool rollback = false)
        {
            try
            {
                int results = 0;
                using (AgileTeamFourEntities dc = new AgileTeamFourEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblFriendComment entity = dc.tblFriendComments.SingleOrDefault(e => e.ID == friendComment.ID);
                    if (entity == null)
                        throw new Exception("FriendComment not found");

                    entity.TimePosted = friendComment.TimePosted;
                    entity.Text = friendComment.Text;

                    // Check that AuthorID is a valid value in the Players Table
                    int id = UserManager.LoadById(friendComment.AuthorID).UserID;
                    if (id == -99) // If -99, it must not be a primary key in the Players Table
                        throw new Exception("Invalid AuthorID");
                    else
                        entity.AuthorID = friendComment.AuthorID;

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

                    tblFriendComment entity = dc.tblFriendComments.SingleOrDefault(e => e.ID == id);
                    if (entity == null)
                        throw new Exception("FriendComment not found");

                    dc.tblFriendComments.Remove(entity);
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

        public static FriendComment LoadById(int id)
        {
            try
            {
                FriendComment friendComment = new FriendComment();
                using (AgileTeamFourEntities dc = new AgileTeamFourEntities())
                {
                    tblFriendComment entity = dc.tblFriendComments.SingleOrDefault(e => e.ID == id);
                    if (entity == null)
                        throw new Exception("FriendComment not found");

                    friendComment.ID = id;
                    friendComment.TimePosted = entity.TimePosted;
                    friendComment.AuthorID = entity.AuthorID;
                    friendComment.Text = entity.Text;
                    friendComment.FriendSentToID = entity.FriendSentToID;

                    return friendComment;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static List<FriendComment> Load()
        {
            try
            {
                List<FriendComment> list = new List<FriendComment>();

                using (AgileTeamFourEntities dc = new AgileTeamFourEntities())
                {
                    var friendCommentsWithAverage = (from c in dc.tblFriendComments
                                                     join u in dc.tblUsers on c.AuthorID equals u.UserID
                                                     orderby c.ID descending
                                                     select new
                                                     {
                                                         c.ID,
                                                         c.TimePosted,
                                                         c.AuthorID,
                                                         c.FriendSentToID,
                                                         c.Text,
                                                     })
                                           .ToList();

                    friendCommentsWithAverage.ForEach(friendComment => list.Add(new FriendComment
                    {
                        ID = friendComment.ID,
                        TimePosted = friendComment.TimePosted,
                        AuthorID = friendComment.AuthorID,
                        FriendSentToID = friendComment.FriendSentToID,
                        Text = friendComment.Text,
                    }));

                    return list;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
         
    }

}
