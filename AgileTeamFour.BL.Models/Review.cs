using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgileTeamFour.BL.Models
{
    public class Review
    {
        //Primary Key
        public int ReviewID { get; set; }

        //Minimum of 1, maximum of 5
        private int starsOutOf5 = 1;
        public int StarsOutOf5 
        { 
            get { return starsOutOf5; } 
            set { if (value < 1 || value > 5) 
                    starsOutOf5 = 1;
                else starsOutOf5 = value; } 
        }

        public string ReviewText { get; set; }

        //Foreign Key references a PlayerID, the primary key in the player Table
        public int AuthorID { get; set; }

        //Foreign Key references a PlayerID, the primary key in the player Table
        public int RecipientID { get; set; }

        public DateTime DateTime { get; set; }
        
    }
}
