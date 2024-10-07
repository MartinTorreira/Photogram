using Es.Udc.DotNet.PracticaMaD.Model.CategoryDao;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.CategoryService
{
    public interface ICategoryService
    {
        /// <summary>
        /// Finds all categories paginated
        /// </summary>
        /// <param name="user">usuario</param>
        /// <returns>The user list</returns>
        /// <exception cref="InstanceNotFoundException"/>
        List<Category> FindAllCategories(int start, int size);

        /// <summary>
        /// Register a category
        /// </summary>
        /// <param name="name">string</param>
        long RegisterCategory(string name);

        /// <summary>
        /// Finds all categories 
        /// </summary>
        /// <param name="user">usuario</param>
        /// <returns>The user list</returns>
        /// <exception cref="InstanceNotFoundException"/>
        List<Category> findAll();

        /// <summary>
        /// Finds all categories 
        /// </summary>
        /// <returns>The category list</returns>
        /// <exception cref="InstanceNotFoundException"/>
        List<Category> FindAllCategories();

        Category getCategory(long id);

        Category getCategoryByName(string name);
    }
}
