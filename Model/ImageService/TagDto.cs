using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.ImageService
{
    public class TagDto
    {
        public long tagId { get; private set; }
        public String title { get; private set; }
        public int usage { get; private set; }

        public TagDto(long tagId, string title, int usage)
        {
            this.tagId = tagId;
            this.title = title;
            this.usage = usage;
        }
    }
}
