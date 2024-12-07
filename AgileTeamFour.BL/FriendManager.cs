using NuGet.Protocol.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AgileTeamFour.BL
{
    public class FriendManager
    {
        public class FriendInsertResult
        {
            public bool Success { get; set; }
            public string ErrorMessage { get; set; }
        }

        public static int Insert(int senderID, int receiverID, bool rollback = false)
        {
            try
            {
                Friend friend = new Friend
                {
                    Status = "Pending",
                    SenderID = senderID,
                    ReceiverID = receiverID
                };

                
                return Insert(friend, rollback);
            }
            catch (Exception ex)
            {
                
                throw;
            }
        }

        public static int Insert(Friend friend, bool rollback = false)
        {
            try
            {
                int results = 0;

                using (var dc = new AgileTeamFourEntities())
                {

                    // Check if there is an existing friendship
                    var existingRelationship = dc.tblFriends
                    .FirstOrDefault(f =>
                    (f.SenderID == friend.SenderID && f.ReceiverID == friend.ReceiverID) ||
                    (f.SenderID == friend.ReceiverID && f.ReceiverID == friend.SenderID)
                        );

                    // If a relationship exists, return 0 to indicate no new insert
                    if (existingRelationship != null)
                    {
                        return 0;
                    }
                    else if (friend.SenderID == friend.ReceiverID) //Check if Friending self
                    {
                        return 0;
                    }

                    //Otherwise, insert continues
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    // Create new tblFriend entity
                    var entity = new tblFriend
                    {
                        // Generate new ID based on max current ID in the table
                        ID = dc.tblFriends.Any() ? dc.tblFriends.Max(s => s.ID) + 1 : 1,
                        Status = friend.Status,
                        SenderID = friend.SenderID,
                        ReceiverID = friend.ReceiverID
                    };

                    // Add the new entity
                    dc.tblFriends.Add(entity);
                    results = dc.SaveChanges();

                    if (rollback) transaction.Rollback();
                }

                return results;
            }
            catch (Exception ex)
            {
                
                throw;
            }
        }

        public static int Update(Friend friends, bool rollback = false)
        {
            try
            {
                int results = 0;
                using (AgileTeamFourEntities dc = new AgileTeamFourEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    // Get the row that we are trying to update
                    tblFriend entity = dc.tblFriends.FirstOrDefault(e => e.ID == friends.ID);

                    if (entity != null)
                    {
                        entity.ID = friends.ID;
                        entity.Status = friends.Status;
                        entity.SenderID = friends.SenderID;
                        entity.ReceiverID = friends.ReceiverID;
                        

                        results = dc.SaveChanges();
                    }
                    else
                    {
                        throw new Exception("Row does not exist");
                    }

                    if (rollback) transaction.Rollback();
                }
                return results;
            }
            catch (Exception)
            {

                throw;
            }

        }


        public static int Delete(int FriendID, bool rollback = false)
        {

            try
            {
                int results = 0;
                using (AgileTeamFourEntities dc = new AgileTeamFourEntities())
                {
                    IDbContextTransaction dbContextTransaction = null;
                    if (rollback) dbContextTransaction = dc.Database.BeginTransaction();

                    tblFriend row = dc.tblFriends.FirstOrDefault(d => d.ID == FriendID);


                    dc.tblFriends.Remove(row);

                    results = dc.SaveChanges();

                    if (rollback) dbContextTransaction.Rollback();

                }
                return results;

            }
            catch (Exception)
            {

                throw;
            }

        }


        public static Friend LoadByID(int id)
        {
            try
            {
                using (AgileTeamFourEntities dc = new AgileTeamFourEntities())
                {
                    tblFriend entity = dc.tblFriends.FirstOrDefault(e => e.ID == id);

                    if (entity != null)
                    {
                        return new Friend
                        {
                            ID = entity.ID,
                            Status = entity.Status,
                            SenderID = entity.SenderID,
                            ReceiverID = entity.ReceiverID,
                            

                        };
                    }
                    else
                    {
                        throw new Exception();
                    }
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        public static List<Friend> Load()
        {
            try
            {
                List<Friend> list = new List<Friend>();

                using (AgileTeamFourEntities dc = new AgileTeamFourEntities())
                {
                    (from e in dc.tblFriends
                     select new
                     {
                         e.ID,
                         e.Status,
                         e.SenderID,
                         e.ReceiverID,

                     })
                     .ToList()
                     .ForEach(friends => list.Add(new Friend
                     {
                         ID = friends.ID,
                         Status = friends.Status,
                         SenderID = friends.SenderID,
                         ReceiverID = friends.ReceiverID,

                     }));
                }
                return list;
            }
            catch (Exception)
            {
                throw;
            }



        }












        public static void AcceptFriendRequest(int friendId)
        {
            using (var context = new AgileTeamFourEntities())
            {
                var request = context.tblFriends.FirstOrDefault(f => f.ID == friendId);
                if (request != null)
                {
                    request.Status = "Approved";
                    context.SaveChanges();
                }
            }
        }

        public static void RejectFriendRequest(int friendId)
        {
            //Reject Friend Request

            using (var context = new AgileTeamFourEntities())
            {
                var request = context.tblFriends.FirstOrDefault(f => f.ID == friendId);
                if (request != null)
                {
                    request.Status = "Rejected";
                    context.SaveChanges();
                }
            }
        }



        public static void BlockPlayer(int friendId)
        {
            //Use to Block a Player from friend invite

            using (var context = new AgileTeamFourEntities())
            {
                var request = context.tblFriends.FirstOrDefault(f => f.ID == friendId);
                if (request != null)
                {
                    
                    request.Status = "Blocked";
                    context.SaveChanges();
                }
            }
        }

        public static bool AreFriends(int userId1, int userId2)
        {
            //Check if Players are Friends

            using (var context = new AgileTeamFourEntities())
            {
                return context.tblFriends.Any(f =>
                    (f.SenderID == userId1 && f.ReceiverID == userId2 ||
                     f.SenderID == userId2 && f.ReceiverID == userId1) &&
                     f.Status == "Accepted");
            }
        }

        public static List<Friend> GetFriendsForUser(int userId)
        {
            //Get Friends for User
            using (var context = new AgileTeamFourEntities())
            {
                var tblFriends = context.tblFriends
                    .Where(f => (f.SenderID == userId || f.ReceiverID == userId)
                                && f.Status == "Accepted")
                    .ToList();

                // Map each tblFriend to Friend
                return tblFriends.Select(f => new Friend
                {
                    ID = f.ID,
                    Status = f.Status,
                    SenderID = f.SenderID,
                    ReceiverID = f.ReceiverID
                }).ToList();
            }
        }




        public static List<Friend> GetPendingRequestsForUser(int userId)
        {
            //Get Pending Requests
            using (var context = new AgileTeamFourEntities())
            {
                var tblPendingFriends = context.tblFriends
                    .Where(f => (f.ReceiverID == userId || f.SenderID == userId)
                                && f.Status == "Pending")
                    .ToList();

                // Map to Friend
                return tblPendingFriends.Select(f => new Friend
                {
                    ID = f.ID,
                    Status = f.Status,
                    SenderID = f.SenderID,
                    ReceiverID = f.ReceiverID
                }).ToList();
            }
        }

    }
}
