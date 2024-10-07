using Es.Udc.DotNet.PracticaMaD.Model.ImageDao;
using Es.Udc.DotNet.PracticaMaD.Model.UserProfileDao;
using Ninject;
using System;
using System.Collections.Generic;


namespace Es.Udc.DotNet.PracticaMaD.Model.ImageService
{
    public interface IImageService
    {
        [Inject]
        IUserProfileDao UserProfileDao { set; }
        [Inject]
        IImageDao imageDao { set; }


        /// <summary>
        /// Finds an image by category,title,description
        /// </summary>
        /// <param name="title">string</param>
        /// /// <param name="description">string</param>
        /// /// <param name="categoryId">long</param>
        /// <returns>An image list</returns>
        /// <exception cref="InstanceNotFoundException"/>
        List<Image> findByFilter(string title, string description, long categoryId, int start, int size);


        /// <summary>
        /// Returns a list of images filtered by a keyword in title or description
        /// </summary>
        /// <param name="imageList">image list</param>
        /// <returns>A image list</returns>
        List<Image> findByKeyword(string title,int start, int size);


      
        List<Image> filterByCategory(List<Image> list, long categoryId, int start, int size);



        /// <summary>
        /// Finds a Image by user
        /// </summary>
        /// <param name="usuario">usuario</param>
        /// <returns>The Usuario list</returns>
        /// <exception cref="InstanceNotFoundException"/>
        List<Image> GetImages(long userId, int start, int size);

        /// <summary>
        /// Finds a Image by user
        /// </summary>
        /// <param name="usuario">usuario</param>
        /// <returns>The Usuario list</returns>
        /// <exception cref="InstanceNotFoundException"/>
        List<Image> GetFollowedImages(long userId, int start, int size);



        /// <summary>
        /// Gives a like for a Image by user
        /// </summary>
        /// <param name="user">user</param>
        /// <param name="image">image</param>
        /// <returns>void</returns>
        void doLike(long userId, long imageId);

        /// <summary>
        ///Removes a like to an image from an user
        /// </summary>
        /// <param name="user">usuario</param>
        /// <param name="image">image</param>
        /// <returns>void</returns>
        void deleteLike(long userId, long imageId);

        /// <summary>
        /// Create an image
        /// </summary>
        /// <param name="image">img</param>
        /// <returns>boolean</returns>
        /// long userId, string title, string description, long category, long shutSpeed, long diaph, long whiteB, long ISO, string path, List<String> tagList
        long uploadImage(UploadImageDto uploadImageDto);

        /// <summary>
        /// Delete an image
        /// </summary>
        /// <param name="image">img</param>
        /// <returns>boolean</returns>
        //    void deleteImage(long userId, long imageId, string filePath);



        /// <summary>
        /// Create a comment
        /// </summary>
        /// <param name="userId">userId</param>
        /// <param name="content">content</param>
        /// <param name="imgId">imgId</param>
        /// <returns>comment id</returns>
        long uploadComment(long userId, string content, long imgId);

        /// <summary>
        /// Delete a comment
        /// </summary>
        /// <param name="userId">userId</param>
        /// <param name="content">content</param>
        /// <param name="imgId">imgId</param>
        /// <returns>void</returns>
        void deleteComment(long imgId, long commentId);

        /// <summary>
        /// Update a comment
        /// </summary>
        /// <param name="comment">comment</param>
        /// <returns>boolean</returns>
        void updateComment(long userId,long commentId,String content);

        /// <summary>
        /// Finds all comments of an Image
        /// </summary>
        /// <param name="imageId">imageId</param>
        /// <returns>The Comment list</returns>
        /// <exception cref="InstanceNotFoundException"/>
        List<Comment> GetComments(long imageId, int start, int size);

        // <summary>
        /// Returns the comment count of a image
        /// </summary>
        /// <param name="imageId">imageId</param>
        /// <returns>The number of comments</returns>
        int getNumberOfComments(long imageId);

        /// <summary>
        /// Finds a comment
        /// </summary>
        /// <param name="imageId">commentId</param>
        /// <returns>The Comment </returns>
        /// <exception cref="InstanceNotFoundException"/>
        Comment GetComment(long commentId);


        // <summary>
        /// Returns the like count of a image
        /// </summary>
        /// <param name="imageId">imageId</param>
        /// <returns>The number of likes</returns>
        int getNumberOfLikes(long imageId);

        /// <summary>
        /// Return an image by an id
        /// </summary>
        /// <param name="imageId">imageId</param>
        /// <returns>the image</returns>
        Image getImage(long imageId);

        /// <summary>
        /// Finds a list of images in a tag
        /// </summary>
        /// <param name="tag">usuario</param>
        /// <returns>The image list</returns>
        /// <exception cref="InstanceNotFoundException"/>
        List<Image> GetImagesByTag(String tag, int start, int size);

        /// <summary>
        /// Finds all tags
        /// </summary>
        /// <returns>The tag list</returns>
        List<Tag> GetTags(int start, int size);

        /// <summary>
        /// Add tags to an image
        /// </summary>
        /// <param name="tagList">List<string></param>
        /// <param name="imageId">imageId</param>
        /// <param name="userId">userId</param>
        /// <returns>void</returns>
        void addTags(List<String> tagList, long imageId, long userId);

        /// <summary>
        /// Delete tags of an image
        /// </summary>
        /// <param name="tagId">tagId</param>
        /// <param name="imageId">imageId</param>
        /// <param name="userId">userId</param>
        /// <returns>void</returns>
        void deleteTags(long tagId, long imageId, long userId);


        /// <summary>
        /// Update a tag
        /// </summary>
        /// <param name="Tag">Tag</param>
        /// <returns>boolean</returns>
        void updateTag(Tag tag);


        // <summary>
        /// Returns the image count of a tag
        /// </summary>
        /// <param name="title">String</param>
        /// <returns>The number of images</returns>
        int getNumberOfImagesTag(String title);



        // <summary>
        /// Returns the comment count of a tag
        /// </summary>
        /// <param name="title">String</param>
        /// <returns>The number of comments</returns>
        int getNumberOfCommentsTag(String title);


        // <summary>
        /// Returns a list of tags ordered by usage
        /// </summary>
        /// <returns>Tags ordered</returns>
        List<TagDto> getTagsOrderByUsage(int start, int size);

         // <summary>
        /// Returns a list of tags ordered by usage
        /// </summary>
        /// <returns>Tags ordered</returns>
        List<TagDto> getTagsOrderByUsage();
    }
}
