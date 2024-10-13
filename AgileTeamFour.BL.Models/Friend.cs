using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgileTeamFour.BL.Models
{
    public class Friend
    {
        public int ID { get; set; } //ID for Friend
        public string Status { get; set; } //Accepted, Blocked, Pending
        public int SenderID { get; set; } //ID for Sending Player
        public int ReceiverID { get; set; } //ID for Player being Friended
    }
}
