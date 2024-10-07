using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Es.Udc.DotNet.ModelUtil.Dao;

namespace Es.Udc.DotNet.PracticaMaD.Model.TagDao
{
    public interface ITagDao : IGenericDao<Tag, Int64>
    {
        /// <summary>
        /// Finds a Tag by title
        /// </summary>
        /// <param name="title">string</param>
        /// <returns>The tag</returns>
        /// <exception cref="InstanceNotFoundException"/>
        List<Tag> findByTitle(string title);

        /// <summary>
        /// Asociate an image to a tag
        /// </summary>
        /// <param name="tag">tag</param>
        /// <param name="image">img</param>
        /// <returns>void</returns>
        void addImage(Image image, Tag tag);

        /// <summary>
        /// Finds all tags order by size
        /// </summary>
        /// <returns>The tag list</returns>
        List<Tag> FindAllTags(int start, int size);


        /// <summary>
        /// Finds all tags order by size
        /// </summary>
        /// <returns>The tag list</returns>
        List<Tag> FindAllTags();

        /// <summary>
        /// Get number of images in a tag
        /// </summary>
        /// <param name="tag">tag</param>
        /// <returns>number of images in a tag </returns>
        int getNumberOfImages(Tag tag);


        /// <summary>
        /// Gets images in a tag
        /// </summary>
        /// <param name="tag">tag</param>
        /// <returns>Images in a tag </returns>
        List<Image> getImages(Tag tag, int start, int size);


        /// <summary>
        /// Gets images in a tag
        /// </summary>
        /// <param name="tag">tag</param>
        /// <returns>Images in a tag </returns>
        List<Image> getImages(Tag tag);
    }
}
