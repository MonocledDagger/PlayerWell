//using AgileTeamFour.BL.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using AgileTeamFour.PL;

//namespace AgileTeamFour.BL
//{
//    public static class PlayerEventManager
//    {
//        public static void Insert(int studentId, int advisorId, string Role, bool rollback = false)
//        {
//            try
//            {
//                //Need to Scaffold
//                using (AgileTeamFourEntities dc = new AgileTeamFourEntities())
//                {
//                    tblPlayerEvent tblPlayerEvent = new tblPlayerEvent();
//                    tblPlayerEvent.PlayerID = studentId;
//                    tblPlayerEvent.EventID = advisorId;
//                    tblPlayerEvent.PlayerEventID = dc.tblPlayerEvents.Any() ? dc.tblPlayerEvents.Max(sa => sa.Id) + 1 : 1;

//                    dc.tblPlayerEvents.Add(tblPlayerEvent);
//                    dc.SaveChanges();
//                }
//            }
//            catch (Exception)
//            {
//                throw;
//            }
//        }

//        public static void Delete(int studentId, int advisorId, bool rollback = false)
//        {
//            try
//            {
//                using (AgileTeamFourEntities dc = new AgileTeamFourEntities())
//                {
//                    tblPlayerEvent? tblPlayerEvent = dc.tblPlayerEvents
//                        .FirstOrDefault(sa => sa.StudentId == studentId
//                        && sa.AdvisorId == advisorId);

//                    if (tblPlayerEvent != null)
//                    {
//                        dc.tblPlayerEvents.Remove(tblPlayerEvent);
//                        dc.SaveChanges();
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                throw;
//            }
//        }
//    }
//}
