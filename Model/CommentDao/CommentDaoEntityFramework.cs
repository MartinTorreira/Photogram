using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Management.Instrumentation;
using System.Text;
using System.Threading.Tasks;
using Es.Udc.DotNet.ModelUtil.Dao;

namespace Es.Udc.DotNet.PracticaMaD.Model.CommentDao
{
    public class CommentDaoEntityFramework : GenericDaoEntityFramework<Comment, Int64>, ICommentDao
    {
        public CommentDaoEntityFramework() { }

        /// <summary>
        /// Finds all comments of an Image
        /// </summary>
        /// <param name="image">image</param>
        /// <returns>List of comments</returns>
        /// <exception cref="InstanceNotFoundException"/>
        public List<Comment> findComments(Image image, int start, int size)
        {
            DbSet<Comment> comments = Context.Set<Comment>();

            var result =
                (from i in comments
                 where i.imageId == image.imageId
                 select i).OrderByDescending(i => i.releaseDate);

            List<Comment> commentList = result.Skip(start).Take(size).ToList();
            if (commentList == null)
                throw new InstanceNotFoundException();

            return commentList;
        }

        /// <summary>
        /// Returns the comment count of a image
        /// </summary>
        /// <param name="image">image</param>
        /// <returns>The number of comments</returns>
        public int getNumberOfComments(Image image)
        {

            return image.Comment.Count;
        }
    }
}
