using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;

namespace Es.Udc.DotNet.PracticaMaD.Model.UserProfileDao
{
    public interface IUserProfileDao : IGenericDao<UserProfile, Int64>
    {
        /// <summary>
        /// Finds an user by login name
        /// </summary>
        /// <param name="loginName">loginName</param>
        /// <returns>An user profile</returns>
        /// <exception cref="InstanceNotFoundException"/>
        UserProfile findByLoginName(String loginName);



        /// <summary>
        /// Finds an user by login name paginated
        /// </summary>
        /// <param name="loginName">loginName</param>
        /// <returns>An user profile</returns>
        /// <exception cref="InstanceNotFoundException"/>
        List<UserProfile> findByLoginName(String loginName, int first, int last);


        /// <summary>
        /// Returns all followed creators of an user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="startIndex"></param>
        /// <param name="count"></param>
        /// <returns>A list of user profiles</returns>
        List<UserProfile> findCreators(UserProfile user, int start, int size);


        /// <summary>
        /// Returns all images from an user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="startIndex"></param>
        /// <param name="count"></param>
        /// <returns>A list of images</returns>
        List<Image> findImages(UserProfile user, int start, int size);


        /// <summary>
        /// Returns all followers 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns> List of users </returns>
        List<UserProfile> findFollowers(UserProfile user, int start, int size);


    }
}
