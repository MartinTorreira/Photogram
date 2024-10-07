using Es.Udc.DotNet.PracticaMaD.Model.UserProfileService;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.View.ApplicationObjects;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.IoC;
using System;
using System.Web;
using System.Web.Security;
using Es.Udc.DotNet.PracticaMaD.Model.UserProfileDao;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Util;
using Es.Udc.DotNet.PracticaMaD.Model;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Image = Es.Udc.DotNet.PracticaMaD.Model.Image;
using Es.Udc.DotNet.PracticaMaD.Model.ImageService;
using Es.Udc.DotNet.PracticaMaD.Model.ImageDao;
using Es.Udc.DotNet.PracticaMaD.Model.CategoryService;

namespace Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session
{
 
    public class SessionManager
    {
        public static readonly String LOCALE_SESSION_ATTRIBUTE = "locale";

        public static readonly String USER_SESSION_ATTRIBUTE =
               "userSession";

        private static IUserService userService;
        private static IImageService imageService;
        private static IImageDao imageDao;
        private static ICategoryService categoryService;
        private static TagDto tagDto;

        public IUserService UserService
        {
            set { userService = value; }
        }

        public IImageService ImageService {
            set { imageService = value;  }
        }

        public IImageDao ImageDao
        {
            set { imageDao = value; }
        }

        public ICategoryService CatergoryService
        {
            set { categoryService = value; }
        }
       


        static SessionManager()
        {
            IIoCManager iocManager =
                (IIoCManager)HttpContext.Current.Application["managerIoC"];

            userService = iocManager.Resolve<IUserService>();
            imageService = iocManager.Resolve<IImageService>();
            imageDao = iocManager.Resolve<IImageDao>();
            categoryService = iocManager.Resolve<ICategoryService>();

        }

        /// <summary>
        /// Registers the user.
        /// </summary>
        /// <param name="context">Http Context includes request, response, etc.</param>
        /// <param name="loginName">Username</param>
        /// <param name="clearPassword">Password in clear text</param>
        /// <param name="userProfileDetails">The user profile details.</param>
        /// <exception cref="DuplicateInstanceException"/>
        public static void RegisterUser(HttpContext context,
            String loginName, String clearPassword,
            UserDetails userProfileDetails)
        {
                long usrId = userService.RegisterUser(loginName, clearPassword,
                                userProfileDetails);

                /* Insert necessary objects in the session. */
                UserSession userSession = new UserSession();
                userSession.UserProfileId = usrId;
                userSession.Firstname = userProfileDetails.firstName;

                Locale locale = new Locale(userProfileDetails.language,
                    userProfileDetails.country);

                UpdateSessionForAuthenticatedUser(context, userSession, locale);
                FormsAuthentication.SetAuthCookie(loginName, false);
        }

        /// <summary>
        /// Login method. Authenticates an user in the current context.
        /// </summary>
        /// <param name="context">Http Context includes request, response, etc.</param>
        /// <param name="loginName">Username</param>
        /// <param name="clearPassword">Password in clear text</param>
        /// <param name="rememberMyPassword">Remember password to the next logins</param>
        /// <exception cref="IncorrectPasswordException"/>
        /// <exception cref="InstanceNotFoundException"/>
        public static void Login(HttpContext context, String loginName,
           String clearPassword, Boolean rememberMyPassword)
        {
            /* Try to login, and if successful, update session with the necessary
             * objects for an authenticated user. */
            LoginResult loginResult = DoLogin(context, loginName,
                clearPassword, false, rememberMyPassword);

            /* Add cookies if requested. */
            if (rememberMyPassword)
            {
                CookiesManager.LeaveCookies(context, loginName,
                    loginResult.encryptedPassword);
            }
        }

        /// <summary>
        /// Tries to log in with the corresponding method of
        /// <c>UserService</c>, and if successful, inserts in the
        /// session the necessary objects for an authenticated user.
        /// </summary>
        /// <param name="context">Http Context includes request, response, etc.</param>
        /// <param name="loginName">Login name</param>
        /// <param name="password">User Password</param>
        /// <param name="passwordIsEncrypted">Password is either encrypted or
        /// in clear text</param>
        /// <param name="rememberMyPassword">Remember password to the next
        /// logins</param>
        /// <exception cref="IncorrectPasswordException"/>
        /// <exception cref="InstanceNotFoundException"/>
        private static LoginResult DoLogin(HttpContext context,
             String loginName, String password, Boolean passwordIsEncrypted,
             Boolean rememberMyPassword)
        {
            LoginResult loginResult =
                userService.Login(loginName, password,
                    passwordIsEncrypted);

            /* Insert necessary objects in the session. */

            UserSession userSession = new UserSession();
            userSession.UserProfileId = loginResult.userId;
            userSession.Firstname = loginResult.firstName;

            Locale locale =
                new Locale(loginResult.language, loginResult.country);

            UpdateSessionForAuthenticatedUser(context, userSession, locale);

            return loginResult;
        }


        /// <summary>
        /// Updates the session values for an previously authenticated user
        /// </summary>
        /// <param name="context">Http Context includes request, response, etc.</param>
        /// <param name="userSession">The user data stored in session.</param>
        /// <param name="locale">The locale info.</param>
        private static void UpdateSessionForAuthenticatedUser(
            HttpContext context, UserSession userSession, Locale locale)
        {
            /* Insert objects in session. */
            context.Session.Add(USER_SESSION_ATTRIBUTE, userSession);
            context.Session.Add(LOCALE_SESSION_ATTRIBUTE, locale);
        }

        /// <summary>
        /// Checks if an user is authenticated
        /// </summary>
        /// <param name="context">Http Context (request, response ...)</param>
        /// <returns>
        /// 	<c>true</c> if is user authenticated
        ///     <c>false</c> otherwise
        /// </returns>
        public static Boolean IsUserAuthenticated(HttpContext context)
        {
            if (context.Session == null)
                return false;

            return (context.Session[USER_SESSION_ATTRIBUTE] != null);
        }

        public static Locale GetLocale(HttpContext context)
        {
            Locale locale =
                (Locale)context.Session[LOCALE_SESSION_ATTRIBUTE];

            return locale;
        }

        /// <summary>
        /// Updates the user profile details.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="userProfileDetails">The user profile details.</param>
        public static void UpdateUserProfileDetails(HttpContext context,
            UserDetails userProfileDetails)
        {
            /* Update user's profile details. */

            UserSession userSession =
                (UserSession)context.Session[USER_SESSION_ATTRIBUTE];

            userService.UpdateUsuarioDetails(userSession.UserProfileId,
                userProfileDetails);

            /* Update user's session objects. */

            Locale locale = new Locale(userProfileDetails.language,
                userProfileDetails.country);

            userSession.Firstname = userProfileDetails.firstName;

            UpdateSessionForAuthenticatedUser(context, userSession, locale);
        }

        /// <summary>
        /// Finds the user profile with the id stored in the session.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public static UserDetails FindUserProfileDetails(HttpContext context)
        {
            UserSession userSession =
                (UserSession)context.Session[USER_SESSION_ATTRIBUTE];

            UserDetails userProfileDetails =
                userService.FindUsuarioDetails(userSession.UserProfileId);

            return userProfileDetails;
        }

        /// <summary>
        /// Gets the user info stored in the session.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public static UserSession GetUserSession(HttpContext context)
        {
            if (IsUserAuthenticated(context))
                return (UserSession)context.Session[USER_SESSION_ATTRIBUTE];
            else
                return null;
        }

        /// <summary>
        /// Changes the user's password
        /// </summary>
        /// <param name="context">Http Context includes request, response, etc.</param>
        /// <param name="oldClearPassword">The old password in clear text</param>
        /// <param name="newClearPassword">The new password in clear text</param>
        /// <exception cref="IncorrectPasswordException"/>
        public static void ChangePassword(HttpContext context,
               String oldClearPassword, String newClearPassword)
        {
            UserSession userSession =
                (UserSession)context.Session[USER_SESSION_ATTRIBUTE];

            userService.ChangePassword(userSession.UserProfileId,
                oldClearPassword, newClearPassword);

            /* Remove cookies. */
            CookiesManager.RemoveCookies(context);
        }

        /// <summary>
        /// Destroys the session, and removes the cookies if the user had
        /// selected "remember my password".
        /// </summary>
        /// <param name="context">Http Context includes request, response, etc.</param>
        public static void Logout(HttpContext context)
        {
            /* Remove cookies. */
            CookiesManager.RemoveCookies(context);

            /* Invalidate session. */
            context.Session.Abandon();

            /* Invalidate Authentication Ticket */
            FormsAuthentication.SignOut();
        }

        /// <sumary>
        /// Guarantees that the session will have the necessary objects if the
        /// user has been authenticated or had selected "remember my password"
        /// in the past.
        /// </sumary>
        /// <param name="context">Http Context includes request, response, etc.</param>
        public static void TouchSession(HttpContext context)
        {
            /* Check if "UserSession" object is in the session. */
            UserSession userSession = null;

            if (context.Session != null)
            {
                userSession =
                    (UserSession)context.Session[USER_SESSION_ATTRIBUTE];

                // If userSession object is in the session, nothing should be doing.
                if (userSession != null)
                {
                    return;
                }
            }

            /*
             * The user had not been authenticated or his/her session has
             * expired. We need to check if the user has selected "remember my
             * password" in the last login (login name and password will come
             * as cookies). If so, we reconstruct user's session objects.
             */
            UpdateSessionFromCookies(context);
        }

        /// <summary>
        /// Tries to login (inserting necessary objects in the session) by using
        /// cookies (if present).
        /// </summary>
        /// <param name="context">Http Context includes request, response, etc.</param>
        private static void UpdateSessionFromCookies(HttpContext context)
        {
            HttpRequest request = context.Request;
            if (request.Cookies == null)
            {
                return;
            }

            /*
             * Check if the login name and the encrypted password come as
             * cookies.
             */
            String loginName = CookiesManager.GetLoginName(context);
            String encryptedPassword = CookiesManager.GetEncryptedPassword(context);

            if ((loginName == null) || (encryptedPassword == null))
            {
                return;
            }

            /* If loginName and encryptedPassword have valid values (the user selected "remember
             * my password" option) try to login, and if successful, update session with the
             * necessary objects for an authenticated user.
             */
            try
            {
                DoLogin(context, loginName, encryptedPassword, true, true);

                /* Authentication Ticket. */
                FormsAuthentication.SetAuthCookie(loginName, true);
            }
            catch (Exception)
            { // Incorrect loginName or encryptedPassword
                return;
            }
        }


        public static UserProfile FindUser(HttpContext context)
        {
            UserSession userSession =
                (UserSession)context.Session[USER_SESSION_ATTRIBUTE];
            UserProfile user = userService.FindUser(userSession.UserProfileId);
            return user;
        }

        public static UserProfile FindUserById(HttpContext context, long userId)
        {
            UserSession userSession = (UserSession)context.Session[USER_SESSION_ATTRIBUTE];
            UserProfile user = userService.FindUser(userId);
            return user;
        }

        /// <summary>
        /// Gets the current user stored in the session.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>The current user or null if not authenticated.</returns>
        public static UserSession GetCurrentLoggedInUser(HttpContext context)
        {
            if (IsUserAuthenticated(context))
                return (UserSession)context.Session[USER_SESSION_ATTRIBUTE];
            else
                return null;
        }


        public static void Follow(HttpContext context, long followedId)
        {
            // Get the current user from the session
            UserSession follower = (UserSession)context.Session[USER_SESSION_ATTRIBUTE];

            if (follower != null)
            {
                long followerId = follower.UserProfileId;
                userService.Follow(followerId, followedId );
            }
        }

        public static void UnFollow(HttpContext context, long followedId)
        {
            // Get the current user from the session
            UserSession follower = (UserSession)context.Session[USER_SESSION_ATTRIBUTE];

            if (follower != null)
            {
                long followerId = follower.UserProfileId;
                userService.UnFollow(followerId, followedId);
            }
        }

        public static FollowedBlock FindAllFollowed(HttpContext context, int start, int size)
        {
            UserSession userSession = (UserSession)context.Session[USER_SESSION_ATTRIBUTE];
            return userService.FindAllCreatorsBlock(userSession.UserProfileId, start, size);
        }

        public static FollowersBlock FindAllFollowers(HttpContext context, int start, int size)
        {
            UserSession userSession = (UserSession)context.Session[USER_SESSION_ATTRIBUTE];
            return userService.FindAllFollowersBlock(userSession.UserProfileId, start, size);
        }

        public static void UploadImage(HttpContext context, String title, String description, String category, long exposureTime,
            long apertureSize, long whiteBalance, Stream file, String filename, String path, List<String> tagList)
        {
            UserSession userSession = (UserSession)context.Session[USER_SESSION_ATTRIBUTE];


            UploadImageDto uploadImage = new UploadImageDto(userSession.UserProfileId, title, description, category, exposureTime,
            apertureSize, whiteBalance, file, filename, path, tagList);
            imageService.uploadImage(uploadImage);

        }

        public static List<Image> getFollowed(HttpContext context, int page) {
            UserSession userSession = (UserSession)context.Session[USER_SESSION_ATTRIBUTE];
            int start = 0;

            if (page != 0) 
                start = page * 10;

            return imageService.GetFollowedImages(userSession.UserProfileId, start, 10);
            
        }

        public static Image getImage(HttpContext context, long id)
        {
            return imageService.getImage(id);

        }

        public static int getLikes(HttpContext context, long id) {
            return imageService.getNumberOfLikes(id);
        
        }

        public static void doLike(HttpContext context, long imgId) {
            UserSession userSession = (UserSession)context.Session[USER_SESSION_ATTRIBUTE];

            imageService.doLike(userSession.UserProfileId, imgId);
        }

        public static void unLike(HttpContext context, long imgId) {
            UserSession userSession = (UserSession)context.Session[USER_SESSION_ATTRIBUTE];

            imageService.deleteLike(userSession.UserProfileId, imgId);
        }


        public static List<Image> getLastImages(HttpContext context, long userId)
        {
            List<Image> images = imageService.GetImages(userId, 0, 100);
            images.Reverse();
            return images.Take(3).ToList();
        }

        public static void addComment(HttpContext context, string content, long imgId) {
            UserSession userSession = (UserSession)context.Session[USER_SESSION_ATTRIBUTE];
            List<Image> images = imageService.GetImages(userSession.UserProfileId, 0, 100);
            imageService.uploadComment(userSession.UserProfileId, content, imgId);
        }

        public static List<Comment> getComments(HttpContext context,long imgId,int page, int size) {
            int start = 0;

            if (page != 0)
                start = page * 10;
            return imageService.GetComments(imgId, start, size);
        }

        public static void deleteComment(HttpContext context,long imgId, long commentId) 
        {
            imageService.deleteComment(imgId,  commentId);
        }

        public static List<Image> findByFilter(HttpContext context, string keyword, long catId)
        {
            return imageService.findByFilter(keyword, keyword, catId, 0, 10);
        }

        public static List<Image> findByKeyword(HttpContext context, string keyword)
        {
            return imageService.findByKeyword(keyword, 0, 10);
        }
        public static Image getImageById(HttpContext context, long id)
        {
            return imageService.getImage(id);
        }

        public static List<Image> filterByCategory(HttpContext context, List<Image> list,  long id)
        {
            return imageService.filterByCategory(list, id, 0, 10);
        }

        public static Category getCategoryById(HttpContext context, long id)
        {
            return categoryService.getCategory(id);
        }

        public static Category getCategoryByName(HttpContext context, string name)
        {
            return categoryService.getCategoryByName(name);
        }
        public static Boolean compareId(HttpContext context, long id)
        {
            UserSession userSession = (UserSession)context.Session[USER_SESSION_ATTRIBUTE];

            return userSession.UserProfileId == id;
        }
        public static Comment getComment(HttpContext context, long commentId) 
        {
            return imageService.GetComment(commentId);

        }

        public static void updateComment(HttpContext context, long commentId, String content) 
        {
            UserSession userSession = (UserSession)context.Session[USER_SESSION_ATTRIBUTE];
            imageService.updateComment(userSession.UserProfileId, commentId, content);
        }

        public static List<TagDto> getTagsByUsage()
        {
            return imageService.getTagsOrderByUsage();
        }

        public static List<Image> getImagesByTag(String tag,int page) 
        {


            int start = 0;

            if (page != 0)
                start = page * 10;

            return imageService.GetImagesByTag(tag, start, 10);


        }

        public static int getNumberOfLikes(long imgId) 
        {
            return imageService.getNumberOfLikes(imgId);
        }

        public static int getNumberOfComments(long imgId) {
            return imageService.getNumberOfComments(imgId);
        }
    }
}