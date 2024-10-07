using Es.Udc.DotNet.ModelUtil.Transactions;
using Es.Udc.DotNet.PracticaMaD.Model.CategoryDao;
using Es.Udc.DotNet.PracticaMaD.Model.ImageService;
using Es.Udc.DotNet.PracticaMaD.Model.TagDao;
using Es.Udc.DotNet.PracticaMaD.Model.TagService;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.CategoryService
{
    public class TagService : ITagService
    {
        [Inject]
        public ITagDao tagDao{ private get; set; }


        /// <summary>
        /// Creates a tag
        /// </summary>
        [Transactional]
        public void createTag(String title)
        {
            TagDto tag = new TagDto(title);
            tagDao.Create(tag);
        }


    }
}
