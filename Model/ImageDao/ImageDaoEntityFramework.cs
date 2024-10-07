using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;

namespace Es.Udc.DotNet.PracticaMaD.Model.ImageDao
{
    public class ImageDaoEntityFramework : GenericDaoEntityFramework<Image, Int64>, IImageDao
    {

        #region Public Constructor
        /// <summary>
        /// Public Constructor
        /// </summary>
        public ImageDaoEntityFramework() { }
        #endregion

        #region FindByUser
        /// <summary>
        /// Finds an user by login
        /// </summary>
        /// <param name="loginName">loginName</param>
        /// <returns>An user</returns>
        /// <exception cref="InstanceNotFoundException"/>
        public List<Image> findByUser(UserProfile user, int start, int size)
        {
            var result = user.Image.Skip(start).Take(size).ToList();
            return result;
        }

        #endregion
        #region FindByFilter
        /// <summary>
        /// Finds an image by category,title,description
        /// </summary>
        /// <param name="title">string</param>
        /// /// <param name="description">string</param>
        /// /// <param name="category">Category</param>
        /// <returns>The Image list</returns>
        /// <exception cref="InstanceNotFoundException"/>
        public List<Image> findByFilter(string title, string description, long categoryId, int start, int size)
        {
            DbSet<Image> images = Context.Set<Image>();

            var result = from i in images
                       select i;

            if (title != null)
            {
                title = title.ToLower();
                result = result.Where(i => i.title.Contains(title));
            }
                

            if (description != null) {
                result = result.Where(i => i.description.Contains(description));
                description = description.ToLower();
            }

            if (categoryId != 0)
                result = result.Where(p => p.categoryId == categoryId);

            return result.OrderBy(p => p.imageId).Skip(start).Take(size).ToList();
        }
        #endregion


        #region DoLike
        /// <summary>
        /// user likes an image
        /// </summary>
        /// <param name="image">image</param>
        /// <param name="user">user</param>
        public void doLike(Image image, UserProfile user)
        {
            if (!image.UserProfile1.Contains(user))
            {
                image.UserProfile1.Add(user);
            }
            Update(image);
        }
        #endregion

        #region DeleteLike
        /// <summary>
        /// usuario likes an image
        /// </summary>
        /// <param name="image">image</param>
        /// <param name="user">usuario</param>
        public void deleteLike(Image image, UserProfile user)
        {
            if (image.UserProfile1.Contains(user))
            {
                image.UserProfile1.Remove(user);
            }
            Update(image);
        }
        #endregion

        /// <summary>
        /// Returns the like count of a image
        /// </summary>
        /// <param name="image">img</param>
        /// <returns>The number of likes</returns>
        public int getNumberOfLikes(Image image)
        {
            return image.UserProfile1.Count;
        }

        public List<Image> findByFollowed(UserProfile user, int start, int size)
        {
            DbSet<Image> images = Context.Set<Image>();
            List<Image> result = new List<Image>();
            foreach (UserProfile u in user.UserProfile2)
            {
                result.AddRange(u.Image);
            }


            return result.OrderByDescending(p => p.releaseDate).Skip(start).Take(size).ToList();

        }

        /// <summary>
        /// Returns a list of images filtered by a keyword in title or description
        /// </summary>
        /// <param name="imageList">image list</param>
        /// <returns>A image list</returns>
        public List<Image> findByKeyword(string keyword, int start, int size)
        {
            DbSet<Image> images = Context.Set<Image>();
            var result = from i in images
                         select i;

            if (keyword != null)
            {
                keyword = keyword.ToLower();
                result = result.Where(i => i.title.Contains(keyword) || i.description.Contains(keyword));
            }


            return result.OrderBy(p => p.imageId).Skip(start).Take(size).ToList(); ;
        }


        public List<Image> filterByCategory(List<Image> list,long categoryId, int start, int size)
        {
            var result = list.AsQueryable();

            if (categoryId != 0)
                result = result.Where(p => p.categoryId == categoryId);

            return result.OrderBy(p => p.imageId).Skip(start).Take(size).ToList();

        }

    }
}
