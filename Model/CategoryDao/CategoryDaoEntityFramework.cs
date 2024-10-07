using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;

namespace Es.Udc.DotNet.PracticaMaD.Model.CategoryDao
{
    public class CategoryDaoEntityFramework : GenericDaoEntityFramework<Category, Int64>, ICategoryDao
    {
        public override bool Equals(object obj)
        {
            return obj is CategoryDaoEntityFramework framework &&
                   EqualityComparer<DbContext>.Default.Equals(Context, framework.Context);
        }

        /// <summary>
        /// Finds all categories paginated
        /// <returns>A category list</returns>
        public List<Category> findAllPaginated(int start, int size)
        {
            List<Category> categoryList = new List<Category>();
            DbSet<Category> categories = Context.Set<Category>();

            var result =
                (from i in categories select i)
                .OrderBy(p => p.categoryId);

            categoryList = result.Skip(start).Take(size).ToList();
            return categoryList;

        }

        /// <summary>
        /// Finds all categories
        /// <returns>A category list</returns>
        public List<Category> findAll()
        {
            List<Category> categoryList = new List<Category>();
            DbSet<Category> categories = Context.Set<Category>();

            var result =
                (from i in categories select i)
                .OrderBy(p => p.categoryId);

            categoryList = result.ToList();
            return categoryList;

        }

        /// <summary>
        /// Finds a category by name
        /// </summary>
        /// <param name="type">string</param>
        /// <returns>The category list</returns>
        public List<Category> findByName(string name, int start, int size)
        {
            List<Category> categoryList = new List<Category>();
            DbSet<Category> categories = Context.Set<Category>();

            var result =
                (from i in categories
                 where i.name == name
                 select i);

            categoryList = result.Skip(start).Take(size).ToList();
            return categoryList;

        }


        /// <summary>
        /// Finds a category by name
        /// </summary>
        /// <param name="type">string</param>
        /// <returns>The category list</returns>
        public List<Category> findByName(string name)
        {
            List<Category> categoryList = new List<Category>();
            DbSet<Category> categories = Context.Set<Category>();

            var result =
                (from i in categories
                 where i.name == name
                 select i);

            categoryList = result.ToList();
            return categoryList;
        }



        public override int GetHashCode()
        {
            return -59922564 + EqualityComparer<DbContext>.Default.GetHashCode(Context);
        }
    }

}