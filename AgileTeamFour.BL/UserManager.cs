using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AgileTeamFour.BL
{
    public class LoginFailureException : Exception
    {
        public LoginFailureException() : base("Login failed. Please check your login information and try again. ")
        {

        }

        public LoginFailureException(string message) : base(message)
        {

        }
    }

    public static class UserManager
    {
        public static string GetHash(string password)
        {
            using (var hasher = SHA1.Create())
            {   
                var hashbytes = Encoding.UTF8.GetBytes(password);
                return Convert.ToBase64String(hasher.ComputeHash(hashbytes));
            }
        }

        public static int DeleteAll()
        {
            try
            {
                using (AgileTeamFourEntities dc = new AgileTeamFourEntities())
                {
                    dc.tblUsers.RemoveRange(dc.tblUsers.ToList());
                    return dc.SaveChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static int Insert(User user, bool rollback = false)
        {
            try
            {
                int results = 0;
                using (AgileTeamFourEntities dc = new AgileTeamFourEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblUser entity = new tblUser();

                    entity.UserID = dc.tblUsers.Any() ? dc.tblUsers.Max(s => s.UserID) + 1 : 1;
                    entity.FirstName = user.FirstName;
                    entity.LastName = user.LastName;
                    entity.UserName = user.UserName;
                    entity.Password = GetHash(user.Password);
                    entity.DateOfBirth = user.DateOfBirth;
                    entity.Email = user.Email;
                    entity.IconPic = user.IconPic;
                    entity.Bio = user.Bio;
                    entity.AccessLevel = user.AccessLevel;

                    user.UserID = entity.UserID;

                    dc.tblUsers.Add(entity);
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
        //RGO fixed the update issue because it was creating a new user each time the update was called. 
        public static int Update(User user, bool rollback = false)
        {
            try
            {
                int result = 0;
                using (AgileTeamFourEntities dc = new AgileTeamFourEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    //Get the row that we are trying to update
                   // tblUser entity = dc.tblUsers.FirstOrDefault(user => user.UserID == user.UserID);

                    // RGO Get the row that we are trying to update by using the correct lambda expression
                    tblUser entity = dc.tblUsers.FirstOrDefault(e => e.UserID == user.UserID);

                    if (entity != null)
                    {
                        entity.FirstName = user.FirstName;
                        entity.LastName = user.LastName;
                        entity.UserName = user.UserName;
                        entity.Password = GetHash(user.Password);
                        entity.DateOfBirth = user.DateOfBirth;
                        entity.Email = user.Email;
                        entity.IconPic = user.IconPic;
                        entity.Bio = user.Bio;
                        entity.AccessLevel = user.AccessLevel;
                        result = dc.SaveChanges();
                    }
                    else
                    {
                        throw new Exception("Row does not exist.");
                    }

                    if (rollback) transaction.Rollback();
                }
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static bool Login(User user)
        {   // Include custom error handling before connection
            try
            {
                if (!string.IsNullOrEmpty(user.UserName))
                {
                    if (!string.IsNullOrEmpty(user.Password))
                    {
                        using (AgileTeamFourEntities dc = new AgileTeamFourEntities())
                        {
                            tblUser tblUser = dc.tblUsers.FirstOrDefault(u => u.UserName == user.UserName);
                            if (tblUser != null)
                            {
                                if (tblUser.Password == GetHash(user.Password))
                                {
                                    // Login successful
                                    user.UserID = tblUser.UserID;
                                    user.FirstName = tblUser.FirstName;
                                    user.LastName = tblUser.LastName;
                                    user.UserName = tblUser.UserName;
                                    user.DateOfBirth = tblUser.DateOfBirth;
                                    user.AccessLevel = tblUser.AccessLevel;
                                    if (user.AccessLevel != "deactivated")
                                    {
                                        return true;
                                    }
                                    else
                                    {
                                        throw new LoginFailureException("Account is deactivated.");
                                    }
                                }
                                else
                                {
                                    throw new LoginFailureException();
                                }
                            }
                            else
                            {
                                throw new LoginFailureException("UserId was not found.");
                            }
                        }
                    }
                    else
                    {
                        throw new LoginFailureException("Password was not set.");
                    }
                }
                else
                {
                    throw new LoginFailureException("UserId was not set.");
                }
            }
            catch (LoginFailureException)
            {
                throw;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static void Seed()
        {
            using (AgileTeamFourEntities dc = new AgileTeamFourEntities())
            {
                if (!dc.tblUsers.Any())
                {
                    User user = new User
                    {
                        UserID=1,
                        UserName = "will",
                        FirstName = "Will",
                        LastName = "Garvey",
                        Password = "harbor",
                        DateOfBirth = new DateTime(1990, 7, 15),
                        Email = "will@them.com",
                        IconPic = "wheel.jpg",
                        Bio = "Riley finds joy in the diverse worlds of gaming, from action-packed adventures to strategic puzzles. They enjoy engaging with the gaming community and sharing tips and insights.",
                        AccessLevel = "admin"
                    };
                    Insert(user);

                    user = new User
                    {
                        UserID = 2,
                        UserName = "glenn",
                        FirstName = "Glenn",
                        LastName = "Lehrer",
                        Password = "drift",
                        DateOfBirth = new DateTime(1990, 7, 15),
                        Email = "glenn@them.com",
                        IconPic = "cat.jpg",
                        Bio = "Alex is an avid gamer who enjoys exploring various genres and titles. They value teamwork and strategy in gameplay and always look for new challenges and experiences.",
                        AccessLevel = "user"
                    };
                    Insert(user);

                    user = new User
                    {
                        UserID = 3,
                        UserName = "ricardo",
                        FirstName = "Ricardo",
                        LastName = "Guzman Ortiz",
                        Password = "craft",
                        DateOfBirth = new DateTime(1990, 7, 15),
                        Email = "ricardo@them.com",
                        IconPic = "tiger.jpg",
                        Bio = "Casey spends their free time immersed in gaming worlds. They appreciate the creativity and effort that goes into developing games and enjoy sharing their experiences with others.",
                        AccessLevel = "admin"
                    };
                    Insert(user);

                    user = new User
                    {
                        UserID = 4,
                        UserName = "james",
                        FirstName = "James",
                        LastName = "Dictus",
                        Password = "price",
                        DateOfBirth = new DateTime(1990, 7, 15),
                        Email = "james@them.com",
                        IconPic = "dragon.jpg",
                        Bio = "Taylor is a dedicated gamer who appreciates both casual and competitive play. They enjoy connecting with other players and discovering new games and strategies.",
                        AccessLevel = "admin"
                    };
                    Insert(user);

                    user = new User
                    {
                        UserID = 5,
                        UserName = "bill",
                        FirstName = "Bill",
                        LastName = "Wild",
                        Password = "bill",
                        DateOfBirth = new DateTime(1990, 7, 15),
                        Email = "bill@them.com",
                        IconPic = "default.jpg",
                        Bio = "Jordan enjoys gaming as a way to unwind and explore new stories. They are always on the lookout for exciting updates and fresh content in their favorite games.",
                        AccessLevel = "player"
                    };
                    Insert(user);

                    user = new User
                    {
                        UserID = 6,
                        UserName = "test",
                        FirstName = "Helpful",
                        LastName = "Debugger",
                        Password = "test",
                        DateOfBirth = new DateTime(1990, 7, 15),
                        Email = "debug@them.com",
                        IconPic = "dragon.jpg",
                        Bio = "Morgan is passionate about gaming and loves to try out different game mechanics and genres. They believe in the importance of fair play and teamwork in the gaming community.",
                        AccessLevel = "player"
                    };
                    Insert(user);
                }
            }
        }

        public static List<User> Load()
        {
            try
            {
                List<User> list = new List<User>();

                using (AgileTeamFourEntities dc = new AgileTeamFourEntities())
                {
                    (from u in dc.tblUsers
                     select new
                     {
                         u.UserID,
                         u.UserName,
                         u.FirstName,
                         u.LastName,
                         u.Password,
                         u.DateOfBirth,
                         u.Email,
                         u.IconPic,
                         u.Bio,
                         u.AccessLevel
                     })
                     .ToList()
                     .ForEach(user => list.Add(new User
                     {
                         UserID = user.UserID,
                         UserName = user.UserName,
                         FirstName = user.FirstName,
                         LastName = user.LastName,
                         Password = user.Password,
                         //RGO was not adding date of birth to deh list
                         DateOfBirth=user.DateOfBirth,
                         Email = user.Email,
                         IconPic = user.IconPic,
                         Bio = user.Bio,
                         AccessLevel = user.AccessLevel

                     }));


                }
                return list;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static User LoadById(int id)
        {
            try
            {
                using (AgileTeamFourEntities dc = new AgileTeamFourEntities())
                {
                    tblUser entity = dc.tblUsers.FirstOrDefault(d => d.UserID == id);
                    if (entity != null)
                    {
                        return new User
                        {
                            UserID = entity.UserID,
                            UserName = entity.UserName,
                            FirstName = entity.FirstName,
                            LastName = entity.LastName,
                            Password = entity.Password,
                            DateOfBirth = entity.DateOfBirth,
                            Email = entity.Email,
                            IconPic = entity.IconPic,
                            Bio = entity.Bio,
                            AccessLevel = entity.AccessLevel

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
    }
}
