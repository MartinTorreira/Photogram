using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Es.Udc.DotNet.ModelUtil.Dao;

namespace Es.Udc.DotNet.PracticaMaD.Model.CommentDao
{
    public interface ICommentDao : IGenericDao<Comment, Int64>
    {
        /// <summary>
        /// Finds all comments of an Image
        /// </summary>
        /// <param name="image">image</param>
        /// <returns>List of comments</returns>
        /// <exception cref="InstanceNotFoundException"/>
        List<Comment> findComments(Image image, int start, int size);

        /// <summary>
        /// Returns the comment count of a image
        /// </summary>
        /// <param name="image">image</param>
        /// <returns>The number of comments</returns>
        int getNumberOfComments(Image image);
    }
}
