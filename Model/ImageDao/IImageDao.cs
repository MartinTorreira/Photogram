using System;
using System.Collections.Generic;
using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;

namespace Es.Udc.DotNet.PracticaMaD.Model.ImageDao
{
    public interface IImageDao : IGenericDao<Image, Int64>
    {
        /// <summary>
        /// Finds an image by user
        /// </summary>
        /// <param name="user">user</param>
        /// <returns>The Image list</returns>
        /// <exception cref="InstanceNotFoundException"/>
        List<Image> findByUser(UserProfile user, int start, int size);

        /// <summary>
        /// Gets images from creatators
        /// </summary>
        /// <param name="user">user</param>
        /// <returns>The Image list</returns>
        /// <exception cref="InstanceNotFoundException"/>
        List<Image> findByFollowed(UserProfile user, int start, int size);


        /// <summary>
        /// Finds an image by category,title or description
        /// </summary>
        /// <param name="title">string</param>
        /// /// <param name="description">string</param>
        /// /// <param name="category">Category</param>
        /// <returns>An image list</returns>
        /// <exception cref="InstanceNotFoundException"/>
        List<Image> findByFilter(string title, string description, long categoryId, int start, int size);


        /// <summary>
        /// User gives a like
        /// </summary>
        /// <param name="user">user</param>
        /// <param name="image">img</param>
        /// <returns>void</returns>
        void doLike(Image image, UserProfile user);

        /// <summary>
        /// Deletes like from an user
        /// </summary>
        /// <param name="user">user</param>
        /// <param name="image">image</param>
        /// <returns>void</returns>
        void deleteLike(Image image, UserProfile user);

        /// <summary>
        /// Returns the like count of a image
        /// </summary>
        /// <param name="image">image</param>
        /// <returns>The number of likes</returns>
        int getNumberOfLikes(Image image);

        /// <summary>
        /// Returns a list of images filtered by a keyword in title or description
        /// </summary>
        /// <param name="imageList">image list</param>
        /// <returns>A image list</returns>
        List<Image> findByKeyword(string keyword, int start, int size);

        List<Image> filterByCategory(List<Image> list, long cateogoryId, int start, int size);
    }
}

