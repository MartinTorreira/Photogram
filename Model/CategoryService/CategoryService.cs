using Es.Udc.DotNet.ModelUtil.Transactions;
using Es.Udc.DotNet.PracticaMaD.Model.CategoryDao;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.CategoryService
{
    public class CategoryService : ICategoryService
    {
        [Inject]
        public ICategoryDao categoryDao { private get; set; }


        /// <summary>
        /// Finds all categories
        /// </summary>
        /// <returns>A Category list</returns>
        [Transactional]
        public List<Category> findAll()
        {
            return categoryDao.findAll();
        }

        /// <summary>
        /// Finds all categories paginated
        /// </summary>
        /// <returns>A Category list</returns>
        [Transactional]
        public List<Category> FindAllCategories(int start, int size)
        {
            return categoryDao.findAllPaginated(start, size);
        }

        [Transactional]
        public List<Category> FindAllCategories()
        {
            return categoryDao.findAll();
        }

        [Transactional]
        public long RegisterCategory(string name)
        {
            if (categoryDao.findByName(name).Count == 0)
            {
                Category category = new Category();
                category.name = name;
                categoryDao.Create(category);
                return category.categoryId;
            }
            else
            {
                return 0;
            }

        }

        [Transactional]
        public Category getCategory(long id)
        {
            return categoryDao.Find(id);
        }

        [Transactional]
        public Category getCategoryByName(string name)
        {
            return categoryDao.findByName(name).First();
        }

    }
}
