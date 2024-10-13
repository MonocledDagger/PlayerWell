using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgileTeamFour.BL
{
    public class FriendManager
    {   //I don't think this is gonna work.
        //public static void SendFriendRequest(int senderId, int receiverId)
        //{
        //    using (var context = new AgileTeamFourEntities())
        //    {
        //        var request = new Friend
        //        {
        //            SenderID = senderId,
        //            ReceiverID = receiverId,
        //            Status = "Pending"
        //        };
        //        context.tblFriends.Add(request);
        //        context.SaveChanges();
        //    }
        //}

        public static int Insert(ref int ID,
                                string status,
                                int senderID,
                                int receiverID,
                                bool rollback = false)
        {
            try
            {
                
                Friend friend = new Friend
                {
                    ID = ID,
                    Status = status,
                    SenderID = senderID,
                    ReceiverID = receiverID,


                };

                int results = Insert(friend, rollback);



                ID = friend.ID;

                return results;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static int Insert(Friend friend, bool rollback = false)
        {
            try
            {
                int results = 0;

                using (AgileTeamFourEntities dc = new AgileTeamFourEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblFriend entity = new tblFriend();

                    //Generate ID
                    entity.ID = dc.tblFriends.Any() ? dc.tblFriends.Max(s => s.ID) + 1 : 1;
                    entity.ID = friend.ID;
                    entity.Status = friend.Status;
                    entity.SenderID = friend.SenderID;
                    entity.ReceiverID = friend.ReceiverID;
                    



                   

                    dc.tblFriends.Add(entity);
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



        public static void AcceptFriendRequest(int friendId)
        {
            using (var context = new AgileTeamFourEntities())
            {
                var request = context.tblFriends.FirstOrDefault(f => f.ID == friendId);
                if (request != null)
                {
                    request.Status = "Accepted";
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
            //Use to Block a Player

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
    }
}
