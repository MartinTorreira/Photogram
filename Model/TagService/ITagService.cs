using Es.Udc.DotNet.PracticaMaD.Model.CategoryDao;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.TagService
{
    public interface ITagService
    {
        /// <summary>
        /// Creates a tag
        /// </summary>
        void createTag(Tag tag);
    }
}
