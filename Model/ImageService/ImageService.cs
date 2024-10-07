using Es.Udc.DotNet.ModelUtil.Transactions;
using Es.Udc.DotNet.PracticaMaD.Model.CommentDao;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Instrumentation;
using Es.Udc.DotNet.PracticaMaD.Model.CategoryDao;
using System.IO;
using Es.Udc.DotNet.PracticaMaD.Model.UserProfileDao;
using Es.Udc.DotNet.PracticaMaD.Model.TagDao;
using Es.Udc.DotNet.PracticaMaD.Model.ImageDao;

namespace Es.Udc.DotNet.PracticaMaD.Model.ImageService
{
    public class ImageService : IImageService
    {
        [Inject]
        public IUserProfileDao UserProfileDao { private get; set; }
        [Inject]
        public IImageDao imageDao { private get; set; }
        [Inject]
        public ITagDao tagDao { private get; set; }
        [Inject]
        public ICategoryDao categoryDao { private get; set; }
        [Inject]
        public ICommentDao commentDao { private get; set; }


        /// <summary>
        /// Add tags to an image
        /// </summary>
        /// <param name="tagList">List<string></param>
        /// <param name="imageId">imageId</param>
        /// <param name="userId">userId</param>
        /// <returns>void</returns>
        public void addTags(List<string> tagList, long imageId, long userId)
        {
            UserProfile user = UserProfileDao.Find(userId);
            Image image = imageDao.Find(imageId);
            if (user == null)
                throw new InstanceNotFoundException();

            tagList.ForEach(tag =>
            {
                Tag tag1 = null;
                List<Tag> foundTags = tagDao.findByTitle(tag);

                if (foundTags.Count == 0) 
                {
                    tag1 = new Tag();
                    tag1.title = tag.ToLower();
                    tagDao.Create(tag1);
                }else
                    tag1 = foundTags.ElementAt(0);

                tagDao.addImage(image, tag1);
            });
        }


        /// <summary>
        /// Delete a comment
        /// </summary>
        /// <param name="userId">userId</param>
        /// <param name="content">content</param>
        /// <param name="imgId">imgId</param>
        /// <returns>void</returns>
        public void deleteComment(long imgId, long commentId)
        {
            Image image = imageDao.Find(imgId);
            Comment comment = commentDao.Find(commentId);
            if (comment == null || image == null)
                throw new InstanceNotFoundException();
            if (image.Comment.Contains(comment))
                image.Comment.Remove(comment);
            commentDao.Remove(comment.commentId);
        }

        /// <summary>
        /// Deletes a like from a user
        /// </summary>
        /// <param name="userId">userId</param>
        /// <param name="imageId">imageId</param>
        /// <returns>Void</returns>
        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        public void deleteLike(long userId, long imageId)
        {
            UserProfile user = UserProfileDao.Find(userId);
            Image image = imageDao.Find(imageId);
            if (user == null || image == null)
                throw new InstanceNotFoundException();
            imageDao.deleteLike(image, user);
        }

        public void deleteTags(long tagId, long imageId, long userId)
        {
            UserProfile user = UserProfileDao.Find(userId);
            Tag tag = tagDao.Find(tagId);
            Image image = imageDao.Find(imageId);

            if (user == null || image == null || tag == null)
                throw new InstanceNotFoundException();

            if (tag.Image.Contains(image))
                tag.Image.Remove(image);
        }

        /// <summary>
        /// Creates a like from a user
        /// </summary>
        /// <param name="userId">userId</param>
        /// <param name="imageId">imageId</param>
        /// <returns>Void</returns>
        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        public void doLike(long userId, long imageId)
        {
            Image image = imageDao.Find(imageId);
            UserProfile user = UserProfileDao.Find(userId);
            if (user == null || image == null)
                throw new InstanceNotFoundException();
            imageDao.doLike(image, user);
        }

        /// <summary>
        /// Finds all comments of an Image
        /// </summary>
        /// <param name="imageId">imageId</param>
        /// <returns>The Comment list</returns>
        /// <exception cref="InstanceNotFoundException"/>
        public List<Comment> GetComments(long imageId, int start, int size)
        {
            Image image = imageDao.Find(imageId);
            if (image == null)
                throw new InstanceNotFoundException();
            return commentDao.findComments(image, start, size);
        }

        public List<Image> GetFollowedImages(long userId, int start, int size)
        {
            UserProfile user = UserProfileDao.Find(userId);
            if (user == null)
                throw new InstanceNotFoundException();
            return imageDao.findByFollowed(user, start, size);
        }


        /// <summary>
        /// Finds a Image by user
        /// </summary>
        /// <param name="usuario">usuario</param>
        /// <returns>The Usuario list</returns>
        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        public List<Image> GetImages(long userId, int start, int size)
        {
            List<Image> finalList = new List<Image>();
            UserProfile user = UserProfileDao.Find(userId);
            try
            {
                finalList = imageDao.findByUser(user, start, size);

            }
            catch (InstanceNotFoundException)
            {

            }
            return new List<Image>(finalList);
        }

        public List<Image> GetImagesByTag(string tag, int start, int size)
        {
            try
            {
                Tag tag1 = tagDao.findByTitle(tag).ElementAt(0);
                List<Image> imageList = tag1.Image.Skip(start).Take(size).ToList();

                return imageList;
            }
            catch (InstanceNotFoundException)
            {
                throw new InstanceNotFoundException();
            }
            
        }

        public int getNumberOfComments(long imageId)
        {
            Image image = imageDao.Find(imageId);
            if (image == null)
                throw new InstanceNotFoundException();
            return commentDao.getNumberOfComments(image);
        }
        // <summary>
        /// Returns the comment count of a tag
        /// </summary>
        /// <param name="title">String</param>
        /// <returns>The number of comments</returns>
        public int getNumberOfCommentsTag(string title)
        {
            Tag tag = tagDao.findByTitle(title)[0];
            List<Image> imageL = tagDao.getImages(tag);
            int count = 0;

            imageL.ForEach(image =>
            {
                count += getNumberOfComments(image.imageId);
            });
            return count;
        }

        // <summary>
        /// Returns the image count of a tag
        /// </summary>
        /// <param name="title">String</param>
        /// <returns>The number of images</returns>
        public int getNumberOfImagesTag(string title)
        {
            List<Tag> t = tagDao.findByTitle(title);
            return tagDao.getNumberOfImages(t[0]);
        }

        public int getNumberOfLikes(long imageId)
        {
            Image image = imageDao.Find(imageId);
            if (image == null)
                throw new InstanceNotFoundException();
            return imageDao.getNumberOfLikes(image);  
        }

        public List<Tag> GetTags(int start, int size)
        {
            try
            {
                return tagDao.FindAllTags(start, size);

            }
            catch (InstanceNotFoundException e)
            {
                throw new InstanceNotFoundException(e.Message);
            }

        }

        public List<TagDto> getTagsOrderByUsage(int start, int size)
        {
            List<Tag> tags = tagDao.FindAllTags();
            List<TagDto> result = new List<TagDto>();

            tags.ForEach(tag =>
            {
                TagDto temp = new TagDto(tag.tagId,tag.title,getNumberOfCommentsTag(tag.title));
                result.Add(temp);
            
            });

            return result.OrderByDescending(x => x.usage).Skip(start).Take(size).ToList();
        }

        /// <summary>
        /// Update a comment
        /// </summary>
        /// <param name="comment">comment</param>
        /// <returns>boolean</returns>
        public void updateComment(long userId, long commentId, String content)
        {
            Comment comment = commentDao.Find(commentId);
            if (userId == comment.UserProfile.userId)
            {
                comment.content = content;            
                comment.releaseDate = DateTime.Now;
                commentDao.Update(comment);
            }
        }

        /// <summary>
        /// Update a tag
        /// </summary>
        /// <param name="Tag">Tag</param>
        /// <returns>boolean</returns>
        public void updateTag(Tag tag)
        {
            tag.title = tag.title.ToLower();
            tagDao.Update(tag);
        }

        /// <summary>
        /// Create a comment
        /// </summary>
        /// <param name="userId">userId</param>
        /// <param name="content">content</param>
        /// <param name="imgId">imgId</param>
        /// <returns>comment id</returns>
        public long uploadComment(long userId, string content, long imgId)
        {
            Comment comment = new Comment();
            comment.userId = userId;
            comment.content = content;
            comment.imageId = imgId;
            comment.releaseDate = DateTime.Now;
            commentDao.Create(comment);
            Comment newComment = commentDao.Find(comment.commentId);

            return newComment.commentId;
        }

        /// <summary>
        /// Create an image
        /// </summary>
        /// <param name="image">img</param>
        /// <returns>boolean</returns>
        [Transactional]
        public long uploadImage(UploadImageDto uploadImageDto)
        {
            Image img = new Image();
            img.userId = uploadImageDto.userId;
            img.title = uploadImageDto.title;
            img.description = uploadImageDto.description;
            img.filename = uploadImageDto.filename;
            img.releaseDate = uploadImageDto.releaseDate;
            img.apertureSize = uploadImageDto.apertureSize;
            img.whiteBalance = uploadImageDto.whiteBalance;
            img.releaseDate = uploadImageDto.releaseDate;
            img.exposureTime = uploadImageDto.exposureTime;
            img.releaseDate = DateTime.Now;

            img.categoryId = categoryDao.findByName(uploadImageDto.category).ElementAt(0).categoryId;

            if (uploadImageDto.file != FileStream.Null)
            {
                string path = uploadImageDto.path + "/Images/" + uploadImageDto.filename;
                FileStream fileStream = File.Create(path, (int)uploadImageDto.file.Length);
                byte[] bytesInStream = new byte[uploadImageDto.file.Length];
                uploadImageDto.file.Read(bytesInStream, 0, bytesInStream.Length);
                fileStream.Write(bytesInStream, 0, bytesInStream.Length);
                fileStream.Close();
            }

            img.path = "/Images/" + uploadImageDto.filename;
            imageDao.Create(img);

            Image newImg = imageDao.Find(img.imageId);

            if(uploadImageDto.tagList != null )
            {
                addTags(uploadImageDto.tagList, newImg.imageId, (long)img.userId);
            }

            return newImg.imageId;
        }


        public List<Image> findByFilter(string title, string description, long categoryId, int start, int size)
        {
            return imageDao.findByFilter(title, description, categoryId, start, size);
        }

        public Image getImage(long imageId)
        {
            return imageDao.Find(imageId);
        }

        public List<Image> findByKeyword(string title, int start, int size)
        {
            return imageDao.findByKeyword(title, 0, 10);
        }

        public List<Image> filterByCategory(List<Image> list, long categoryId, int start, int size)
        {
            return imageDao.filterByCategory(list, categoryId, start, size);
        }

        public Comment GetComment(long commentId)
        {
            return commentDao.Find(commentId);
        }

        public List<TagDto> getTagsOrderByUsage()
        {
            List<Tag> tags = tagDao.FindAllTags();
            List<TagDto> result = new List<TagDto>();

            tags.ForEach(tag =>
            {
                TagDto temp = new TagDto(tag.tagId, tag.title, getNumberOfCommentsTag(tag.title));
                result.Add(temp);

            });

            return result.OrderByDescending(x => x.usage).ToList();
        }


    }
}