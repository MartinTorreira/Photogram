// TODO IMPORT AND FINISH
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.Transactions;
using Es.Udc.DotNet.PracticaMaD.Model.UserProfileDao;
using Ninject;
using System;
using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Model.UserProfileService
{
    public interface IUserService
    {
        [Inject]
        IUserProfileDao UserProfileDao { set; }

        /// <summary>
        /// Changes the password.
        /// </summary>
        /// <param name="userProfileId"> The user profile id. </param>
        /// <param name="oldClearPassword"> The old clear password. </param>
        /// <param name="newClearPassword"> The new clear password. </param>
        /// <exception cref="IncorrectPasswordException"/>
        /// <exception cref="InstanceNotFoundException"/>
        void ChangePassword(long userId, String oldPassword,
            String newPassword);

        /// <summary>
        /// Cheks if an user exists by login name
        /// </summary>
        /// <param name="loginName">loginName</param>
        /// <returns>True if user exists and false if not</returns>
        /// <exception cref="InstanceNotFoundException"/>
        bool userExists(String loginName);


        /// <summary>
        /// Registers an user
        /// </summary>
        /// <param name="loginName"></param>
        /// <param name="clearPassword"></param>
        /// <param name="userDetails"></param>
        /// <exception cref="DuplicateInstanceException"/>
        /// <returns>UserId</returns>
        [Transactional]
        long RegisterUser(String loginName, String clearPassword,
            UserDetails userDetails);


        /// <summary>
        /// Logins the specified login name.
        /// </summary>
        /// <param name="loginName"> Name of the login. </param>
        /// <param name="password"> The password. </param>
        /// <param name="passwordIsEncrypted"> if set to <c> true </c> [password is encrypted]. </param>
        /// <returns> LoginResult </returns>
        /// <exception cref="InstanceNotFoundException"/>
        /// <exception cref="IncorrectPasswordException"/>
        [Transactional]
        LoginResult Login(String loginName, String password,
            Boolean passwordIsEncrypted);

        /// <summary>
        /// Update user details
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="usuarioDetails"></param>
        [Transactional]
        void UpdateUsuarioDetails(long userId, UserDetails usuarioDetails);


        /// <summary>
        /// Finds user details
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>User details</returns>
        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        UserDetails FindUsuarioDetails(long userId);

        /// <summary>
        /// Adds followers to an user
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="followId"></param>
        /// <returns></returns>
        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        void Follow(long userId, long followId);

        /// <summary>
        /// Removes followers to an user
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="followId"></param>
        /// <returns></returns>
        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        void UnFollow(long userId, long followId);


        /// <summary>
        /// Finds all followers of an user id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="start"></param>
        /// <param name="size"></param>
        /// <returns>List of users</returns>
        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        List<UserProfile> FindAllFollowers(long userId, int start, int size);

        /// <summary>
        /// Finds all creators of an user id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="start"></param>
        /// <param name="size"></param>
        /// <returns>List of users</returns>
        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        List<UserProfile> FindAllCreators(long userId, int start, int size);

        /// <summary>
        /// Finds all users by login name
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="start"></param>
        /// <param name="size"></param>
        /// <returns>List of users</returns>
        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        UserBlock FindAllUsers(string loginName, int start, int size);

        /// <summary>
        /// Finds a user by id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>user</returns>
        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        UserProfile FindUser(long userId);

        /// <summary>
        /// Finds all creators 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="start"></param>
        /// <param name="size"></param>
        /// <returns>block of creators</returns>
        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        FollowedBlock FindAllCreatorsBlock(long userId, int start, int size);

        /// <summary>
        /// Finds all followers 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="start"></param>
        /// <param name="size"></param>
        /// <returns>block of followers</returns>
        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        FollowersBlock FindAllFollowersBlock(long userId, int start, int size);


    }
}
