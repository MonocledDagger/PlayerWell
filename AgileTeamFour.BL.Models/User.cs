using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgileTeamFour.BL.Models
{
    public class User
    {
        [DisplayName("User ID")]
        public int UserID { get; set; }
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        [DisplayName("User Name")]
        public string UserName { get; set; }
        public string Password { get; set; }
        [DisplayName("Date Of Birth")]
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        [DisplayName("Icon Pic")]
        public string IconPic { get; set; }
        public string Bio { get; set; }
        [DisplayName("Access Level")]
        public string AccessLevel { get; set; }
    }
}
