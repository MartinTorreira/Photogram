using System.Collections.Generic;
using Ninject;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Transactions;
using Es.Udc.DotNet.PracticaMaD.Model.UserProfileDao;
using Es.Udc.DotNet.PracticaMaD.Model.UserProfileService;
using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.PracticaMaD.Model.Util;
using Es.Udc.DotNet.PracticaMaD.UnitTest;

namespace Es.Udc.DotNet.PracticaMaD.Test
{

    [TestClass]
    public class IUserProfileServiceTest
    {
        private TransactionScope transanctionScope;
        private static IUserProfileDao userProfileDao;
        private static IKernel kernel;
        private static IUserService userProfileService;
        private const string loginName = "loginNameTest";
        private const string enPassword = "password";
        private const string language = "es";
        private const string language1 = "en";
        private const string firstName = "firstName";
        private const string firstName1 = "firstName1";
        private const string lastName = "lastName";
        private const string lastName1 = "lastName1";
        private const string email = "a@a";
        private const string email1 = "a@1";
        private const string country = "es";
        private const string country1 = "en";


        public TestContext TestContext { get; set; }



        [TestInitialize()]
        public void MyTestInitialize()
        {
            transanctionScope = new TransactionScope();
        }

        [TestCleanup()]
        public void MyTestCleanup()
        {
            transanctionScope.Dispose();
        }

        [ClassInitialize()]
        public static void MyCkassInitialize(TestContext testContext)
        {
            kernel = TestManager.ConfigureNInjectKernel();
            userProfileDao = kernel.Get<IUserProfileDao>();
            userProfileService = kernel.Get<IUserService>();

        }

        [ClassCleanup()]
        public static void MyClassCleanup()
        {
            TestManager.ClearNInjectKernel(kernel);
        }


          [TestMethod]
          public void TestRegisterUser()
          {
              using (TransactionScope scope = new TransactionScope())
              {
                  long userId = userProfileService.RegisterUser(loginName, enPassword,
                          new UserDetails(firstName, lastName, email, language, country));

                  UserProfile usuario = userProfileDao.Find(userId);

                  Assert.AreEqual(userId, usuario.userId);
                  Assert.AreEqual(loginName, usuario.loginName);
                  Assert.AreEqual(PasswordEncrypter.Crypt(enPassword), usuario.enPassword);
                  Assert.AreEqual(firstName, usuario.firstName);
                  Assert.AreEqual(lastName, usuario.lastName);
                  Assert.AreEqual(email, usuario.email);
                  Assert.AreEqual(language, usuario.language);
                  Assert.AreEqual(country, usuario.country);
              }
          } 



         [TestMethod]
          public void TestUsuarioExists()
          {
              using (var scope = new TransactionScope())
              {
                  userProfileService.RegisterUser(loginName, enPassword,
                     new UserDetails(firstName, lastName, email, language, country));
                  bool existe = userProfileService.userExists(loginName);
                  Assert.IsTrue(existe);
              }
          }

        [TestMethod]
        public void TestLoginUser()
        {
            using (var scope = new TransactionScope())
            {
                long usrId = userProfileService.RegisterUser(loginName, enPassword,
                    new UserDetails(firstName, lastName, email, language, country));

                UserProfile user= userProfileDao.Find(usrId);
                LoginResult result = userProfileService.Login(user.loginName, enPassword, false);

                Assert.AreEqual(result.userId, usrId);
                Assert.AreEqual(result.firstName, user.firstName);
                Assert.AreEqual(result.language, user.language);
                Assert.AreEqual(result.country, user.country);
                Assert.AreEqual(result.encryptedPassword, user.enPassword);
            }

        }

        [TestMethod]
        public void TestUpdateUserDetails()
        {
            using (var scope = new TransactionScope())
            {
                long usrId = userProfileService.RegisterUser(loginName, enPassword, new UserDetails(firstName, lastName, email, language, country));

                UserProfile usuario = userProfileDao.Find(usrId);

                userProfileService.UpdateUsuarioDetails(usrId, new UserDetails(firstName1, lastName1, email1, language1, country1));

                UserProfile usuarioUpdated = userProfileDao.Find(usrId);

                Assert.AreEqual(usuario, usuarioUpdated);
                Assert.AreEqual(firstName1, usuarioUpdated.firstName);
                Assert.AreEqual(lastName1, usuarioUpdated.lastName);
                Assert.AreEqual(email1, usuarioUpdated.email);
                Assert.AreEqual(language1, usuarioUpdated.language);
                Assert.AreEqual(country1, usuarioUpdated.country);
            }
        }

        [TestMethod]
        public void TestFindDetails()
        {
            using (var scope = new TransactionScope())
            {
                long usrId = userProfileService.RegisterUser(loginName, enPassword, new UserDetails(firstName, lastName, email, language, country));

                UserDetails details = userProfileService.FindUsuarioDetails(usrId);

                Assert.AreEqual(firstName, details.firstName);
                Assert.AreEqual(lastName, details.lastname);
                Assert.AreEqual(email, details.email);
                Assert.AreEqual(language, details.language);
                Assert.AreEqual(country, details.country);
                
            }
        }


        //TODO CHANGE PASSWORD


         ///<sumary>
        /// Test que comprueba que se puede seguir a un usuario y dejar de seguirlo
        ///</sumary>
        [TestMethod]
        public void TestFollowUnfollowUser()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                long userId = userProfileService.RegisterUser(loginName, enPassword, new UserDetails(firstName, lastName, email, language, country));
                UserProfile usuario = userProfileDao.Find(userId);

                long userId2 = userProfileService.RegisterUser("manuel", enPassword, new UserDetails("manuel", lastName, "email@example.com", language, country));
                UserProfile usuario2 = userProfileDao.Find(userId2);

                long userId3 = userProfileService.RegisterUser("jesus", enPassword, new UserDetails("jesus", lastName, "email@example.com", language, country));
                UserProfile usuario3 = userProfileDao.Find(userId3);

                userProfileService.Follow(userId, userId2);
                userProfileService.Follow(userId3, userId2);

                List<UserProfile> followers = userProfileService.FindAllFollowers(userId2, 0, 9);
                Assert.AreEqual(followers[1], usuario3);
                Assert.AreEqual(followers.Count, 2);

                userProfileService.UnFollow(userId, userId2);
                userProfileService.UnFollow(userId3, userId2);

                followers = userProfileService.FindAllFollowers(userId2, 0, 9);

                Assert.AreEqual(followers.Count, 0);

            }
        }
    }
}
