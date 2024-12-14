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
                        DateOfBirth = new DateTime(1988, 1, 29),
                        Email = "will@them.com",
                        IconPic = "wheel.jpg",
                        Bio = " Will engages with the gaming community and shares tips and insights.",
                        AccessLevel = "admin"
                    };
                    Insert(user);

                    user = new User
                    {
                        UserID = 2,
                        UserName = "glen",
                        FirstName = "Glen",
                        LastName = "Lehrer",
                        Password = "drift",
                        DateOfBirth = new DateTime(1996, 4, 18),
                        Email = "glen@them.com",
                        IconPic = "cat.jpg",
                        Bio = "Glen plays games casually to reduce stress and meet with people with common interests.",
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
                        DateOfBirth = new DateTime(1981, 5, 4),
                        Email = "ricardo@them.com",
                        IconPic = "tiger.jpg",
                        Bio = "Ricardo likes joining events and showing up early.",
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
                        DateOfBirth = new DateTime(1995, 11, 12),
                        Email = "james@them.com",
                        IconPic = "dragon.jpg",
                        Bio = "Jimmy likes to prepare well and respond effectively to new challenges.",
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
                        DateOfBirth = new DateTime(2000, 3, 3),
                        Email = "bill@them.com",
                        IconPic = "default.jpg",
                        Bio = "Bill has been playing games actively for over a decade.",
                        AccessLevel = "player"
                    };
                    Insert(user);

                    user = new User
                    {
                        UserID = 6,
                        UserName = "MorganGames",
                        FirstName = "Morgan",
                        LastName = "Smith",
                        Password = "test",
                        DateOfBirth = new DateTime(2001, 12, 14),
                        Email = "debug@them.com",
                        IconPic = "dragon.jpg",
                        Bio = "Morgan prefers playing games for stress relief, and does not invest much time into reading game strategy guides.",
                        AccessLevel = "player"
                    };
                    Insert(user);

                    user = new User
                    {
                        UserID = 7,
                        UserName = "Franklin",
                        FirstName = "Frank",
                        LastName = "Turplin",
                        Password = "FrankMasta",
                        DateOfBirth = new DateTime(1976, 6, 6),
                        Email = "debug@them.com",
                        IconPic = "default.jpg",
                        Bio = "Frank tends to play the newest games that are trending, and follows new developments in the gaming community.",
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

        public static bool UsernameExists(string userName)
        {    // return true if user exists with the propsed userName else return false
             return Load().Where(u => u.UserName == userName).Any();
        }
    }
}
