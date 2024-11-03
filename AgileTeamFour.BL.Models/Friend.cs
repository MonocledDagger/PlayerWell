using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgileTeamFour.BL.Models
{
    public class Friend
    {
        public int ID { get; set; } //ID for Friend
        public string Status { get; set; } //Accepted, Blocked, Pending

        [DisplayName("Sending user")]
        public int SenderID { get; set; } //ID for Sending Player

        [DisplayName("Receiving User")]
        public int ReceiverID { get; set; } //ID for Player being Friended

        //public User Sender { get; set; }
        //public User Receiver { get; set; }
    }
}
