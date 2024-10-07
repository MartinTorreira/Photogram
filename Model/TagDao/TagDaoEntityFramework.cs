using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Management.Instrumentation;
using Es.Udc.DotNet.ModelUtil.Dao;




namespace Es.Udc.DotNet.PracticaMaD.Model.TagDao
{
    public class TagDaoEntityFramework : GenericDaoEntityFramework<Tag, Int64>, ITagDao
    {
        /// <summary>
        /// Asociate an image to a tag
        /// </summary>
        /// <param name="tag">tag</param>
        /// <param name="image">img</param>
        /// <returns>void</returns>
        public void addImage(Image image, Tag tag)
        {
            if (!tag.Image.Contains(image))
                tag.Image.Add(image);

        }

        /// <summary>
        /// Finds all tags order by size
        /// </summary>
        /// <returns>The tag list</returns>
        public List<Tag> FindAllTags(int start, int size)
        {
            DbSet<Tag> tags = Context.Set<Tag>();

            return (from i in tags
                    select i).OrderByDescending(i => i.Image.Count).Skip(start).Take(size).ToList();
        }


        /// <summary>
        /// Finds all tags order by size
        /// </summary>
        /// <returns>The tag list</returns>
        public List<Tag> FindAllTags()
        {
            DbSet<Tag> tags = Context.Set<Tag>();

            return (from i in tags
                    select i).OrderByDescending(i => i.Image.Count).ToList();
        }


        /// <summary>
        /// Finds a Tag by title
        /// </summary>
        /// <param name="title">string</param>
        /// <returns>The tag</returns>
        /// <exception cref="InstanceNotFoundException"/>
        public List<Tag> findByTitle(string title)
        {
            String ltitle = title.ToLower();
            DbSet<Tag> tags = Context.Set<Tag>();

            var result =
                (from i in tags
                 where i.title == ltitle
                 select i);
                
            return result.ToList();
        }

        /// <summary>
        /// Get number of comments in a tag
        /// </summary>
        /// <param name="tag">tag</param>
        /// <returns>number of comments in a tag </returns>
        public List<Image> getImages(Tag tag, int start, int size)
        {
            return tag.Image.Skip(start).Take(size).ToList();
        }


        /// <summary>
        /// Get number of comments in a tag
        /// </summary>
        /// <param name="tag">tag</param>
        /// <returns>number of comments in a tag </returns>
        public List<Image> getImages(Tag tag)
        {
            return tag.Image.ToList();
        }



        /// <summary>
        /// Get number of images in a tag
        /// </summary>
        /// <param name="tag">tag</param>
        /// <returns>number of images in a tag </returns>
        public int getNumberOfImages(Tag  tag)
        {
            return tag.Image.Count();
        }

    }
}
