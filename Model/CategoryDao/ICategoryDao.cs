using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;

namespace Es.Udc.DotNet.PracticaMaD.Model.CategoryDao
{
    public interface ICategoryDao : IGenericDao<Category, Int64>
    {
        /// <summary>
        /// Finds all categories paginated
        /// <returns>A category list</returns>
        List<Category> findAllPaginated(int start, int size);


        /// <summary>
        /// Finds all categories 
        /// <returns>A category list</returns>
        List<Category> findAll();


        /// <summary>
        /// Finds a category by name
        /// </summary>
        /// <param name="type">string</param>
        /// <returns>A category list</returns>
        List<Category> findByName(string name, int start, int size);



        /// <summary>
        /// Finds a category by name
        /// </summary>
        /// <param name="type">string</param>
        /// <returns>A category list</returns>
        List<Category> findByName(string name);

    }
}
