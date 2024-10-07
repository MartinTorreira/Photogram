using System;
using System.Collections.Generic;
using Es.Udc.DotNet.PracticaMaD.Model.UserProfileDao;
using Ninject;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.Transactions;
using Es.Udc.DotNet.PracticaMaD.Model.Util;
using Es.Udc.DotNet.PracticaMaD.Model.UserProfileService.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model.Cache;

namespace Es.Udc.DotNet.PracticaMaD.Model.UserProfileService
{
    public class UserProfileService : IUserService
    {
        [Inject]
        public IUserProfileDao UserProfileDao { private get; set; }

        public bool userExists(String loginName) {
            try
            {
                UserProfile userProfile = UserProfileDao.findByLoginName(loginName);

            }catch (InstanceNotFoundException)
            { return false; }

            return true;
        }


        [Transactional]
        public long RegisterUser(String loginName, String clearPassword,
            UserDetails userDetails)
        {
            try
            {
                UserProfileDao.findByLoginName(loginName);

                throw new DuplicateInstanceException(loginName,
                    typeof(UserProfile).FullName);
            }
            catch (InstanceNotFoundException)
            {
                String encryptedPassword = PasswordEncrypter.Crypt(clearPassword);

                UserProfile user= new UserProfile();

                user.loginName= loginName;
                user.enPassword= encryptedPassword;

                user.firstName= userDetails.firstName;
                user.lastName = userDetails.lastname;
                user.email = userDetails.email;
                user.language= userDetails.language;
                user.country = userDetails.country;

                UserProfileDao.Create(user);

                return user.userId;
            }
        }

        public LoginResult Login(String loginName, String password, Boolean passwordIsEncrypted)
        {
            UserProfile user= UserProfileDao.findByLoginName(loginName);


            if ((passwordIsEncrypted && !password.Equals(user.enPassword)) ||
                (!passwordIsEncrypted && !PasswordEncrypter.IsClearPasswordCorrect(password, user.enPassword)))
            {
                throw new IncorrectPasswordException(loginName);
            }

            return new LoginResult(user.userId,user.firstName,user.enPassword,user.country,user.language);
        }

        /// <summary>
        /// Update user details
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="usuarioDetails"></param>
        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        public void UpdateUsuarioDetails(long userId, UserDetails usuarioDetails)
        {
            UserProfile usuario = UserProfileDao.Find(userId);

            usuario.firstName = usuarioDetails.firstName;
            usuario.lastName = usuarioDetails.lastname;
            usuario.email = usuarioDetails.email;
            usuario.language = usuarioDetails.language;
            usuario.country = usuarioDetails.country;

            UserProfileDao.Update(usuario);
        }

        /// <summary>
        /// Finds user details
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>User details</returns>
        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        public UserDetails FindUsuarioDetails(long userId)
        {
            UserProfile user= UserProfileDao.Find(userId);
            UserDetails usuarioDetail = new UserDetails(user.firstName, user.lastName, user.email, user.language, user.country);
            usuarioDetail.NumberPhotos = user.Image.Count;
            usuarioDetail.NumberCreators = user.UserProfile1.Count;
            usuarioDetail.NumberFollowed = user.UserProfile2.Count;
            return usuarioDetail;

        }

        /// <summary>
        /// Adds an user to followed
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="followId"></param>
        /// <returns></returns>
        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        public void Follow(long userId, long followId)
        {
            UserProfile user = UserProfileDao.Find(userId);
            UserProfile followed = UserProfileDao.Find(followId);
            
            followed.UserProfile1.Add(user);
            UserProfileDao.Update(user);

        }


        /// <summary>
        /// Removes an user from followers
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="followId"></param>
        /// <returns></returns>
        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        public void UnFollow(long userId, long followId)
        {
            UserProfile user = UserProfileDao.Find(userId);
            UserProfile followed = UserProfileDao.Find(followId);
            
            followed.UserProfile1.Remove(user);
            UserProfileDao.Update(user);

        }

        public List<UserProfile> FindAllFollowers(long userId, int start, int size)
        {
            UserProfile user = UserProfileDao.Find(userId);
            return UserProfileDao.findFollowers(user, start, size);
        }

        public List<UserProfile> FindAllCreators(long userId, int start, int size)
        {
            UserProfile user = UserProfileDao.Find(userId);
            return UserProfileDao.findCreators(user, start, size);
        }

        public FollowersBlock FindAllFollowersBlock(long userId, int start, int size)
        {
            UserProfile user = UserProfileDao.Find(userId);
            List<UserProfile> followers = UserProfileDao.findFollowers(user, start, size + 1);
            bool moreFollowed = (followers.Count == size + 1);

            if (moreFollowed)
                followers.RemoveAt(size);

            FollowersBlock block = new FollowersBlock(followers, moreFollowed);
            return block;
        }

        public FollowedBlock FindAllCreatorsBlock(long userId, int start, int size)
        {
            UserProfile user = UserProfileDao.Find(userId);
            List<UserProfile> followed = UserProfileDao.findCreators(user, start, size + 1);
            bool moreFollowed = (followed.Count == size + 1);

            if (moreFollowed)
                followed.RemoveAt(size);

            FollowedBlock block = new FollowedBlock(followed, moreFollowed);
            return block;
        }

        public void ChangePassword(long userId, string oldPassword, string newPassword)
        {
            UserProfile userProfile = UserProfileDao.Find(userId);
            String storedPassword = userProfile.enPassword;

            if (!PasswordEncrypter.IsClearPasswordCorrect(oldPassword,
                 storedPassword))
            {
                throw new IncorrectPasswordException(userProfile.loginName);
            }

            userProfile.enPassword = PasswordEncrypter.Crypt(newPassword);
            UserProfileDao.Update(userProfile);
        }

        [Transactional]
        public UserBlock FindAllUsers(string loginName, int start, int size)
        {
            List<UserProfile> finalList = new List<UserProfile>();

            //Checks if its in the cache
            if (UserCache.Exists(loginName))
            {
                finalList = UserCache.Get<List<UserProfile>>(loginName + start);
            }
            else
            {
                finalList = UserProfileDao.findByLoginName(loginName, start, size + 1);
                UserCache.Add(loginName + start, finalList);
            }

            bool moreUsers = (finalList.Count == size + 1);
            if (moreUsers)
                finalList.RemoveAt(size);

            return new UserBlock(finalList, moreUsers);
        }

        [Transactional]
        public UserProfile FindUser(long userId)
        {
            UserProfile user = null;
            try
            {
                user = UserProfileDao.Find(userId);
                return user;

            }
            catch (InstanceNotFoundException)
            {
                return null;
            }
           
        }


    }
}