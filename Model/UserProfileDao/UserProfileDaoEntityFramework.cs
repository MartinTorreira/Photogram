using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using System.Data.Common;
using System.Data.Entity;

namespace Es.Udc.DotNet.PracticaMaD.Model.UserProfileDao
{
    public class UserProfileDaoEntityFramework : GenericDaoEntityFramework<UserProfile, Int64>, IUserProfileDao
    {

        #region Public constructor
        /// <summary>
        /// Public Constructor
        /// </summary>
        public UserProfileDaoEntityFramework(){}
        #endregion

        /// <summary>
        /// Finds an user by login name
        /// </summary>
        /// <param name="loginName">loginName</param>
        /// <returns>An user profile</returns>
        /// <exception cref="InstanceNotFoundException"/>
        public UserProfile findByLoginName(String loginName) {

            UserProfile user = null;
            DbSet<UserProfile> users = Context.Set<UserProfile>();

            var result =
                (from u in users
                 where u.loginName == loginName
                 select u);

            user = result.FirstOrDefault();
            if (user == null)
                throw new InstanceNotFoundException(loginName,
                    typeof(UserProfile).FullName);
            
            return user;
        }

        /// <summary>
        /// Returns all images from an user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="startIndex"></param>
        /// <param name="count"></param>
        /// <returns>A list of images</returns>
        public List<Image> findImages(UserProfile user, int start, int size) 
        {
            return user.Image.Skip(start).Take(size).ToList();
        }

        /// <summary>
        /// Returns all followers from an user
        /// </summary>
        /// <param name="user"></param>
        /// <returns>A list of followers</returns>
        public List<UserProfile> findFollowers(UserProfile user, int start, int size)
        {
            var result = user.UserProfile1.Skip(start).Take(size).ToList();
            return result;
        }

        /// <summary>
        /// Returns all creators from an user
        /// </summary>
        /// <param name="user"></param>
        /// <returns>A list of creators</returns>
        public List<UserProfile> findCreators(UserProfile user, int start, int size)
        {
            var result = user.UserProfile2.Skip(start).Take(size).ToList();
            return result;
        }

        public List<UserProfile> findByLoginName(string loginName, int first, int last)
        {
            DbSet<UserProfile> usuarios = Context.Set<UserProfile>();
            loginName = loginName.ToLower();

            var result =
            (from u in usuarios
             where u.loginName.Contains(loginName) || u.firstName.Contains(loginName)
             select u).OrderBy(u => u.userId).Skip(first).Take(last).ToList();

            return result;
        }
    }

}
