using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgileTeamFour.BL.Models
{
    public class Review
    {
        public int ReviewID { get; set; }

        private int starsOutOf5 = 1;

        public int StarsOutOf5
        {  //Minimum of 1, maximum of 5
            
            get { return starsOutOf5; } 
            set { if (value < 1 || value > 5) 
                    starsOutOf5 = 1;
                else starsOutOf5 = value; } 
        }

        [DisplayName("Review")]
        public string ReviewText { get; set; }

        //Foreign Key references a PlayerID, the primary key in the player Table
        public int AuthorID { get; set; }
        [DisplayName("Author")]
        public string AuthorName { get; set; }

        //Foreign Key references a PlayerID, the primary key in the player Table
        public int RecipientID { get; set; }
        [DisplayName("Recipient")]
        public string RecipientName { get; set; }

        [DisplayName("Review Date")]
        public DateTime DateTime { get; set; }
        
    }
}
