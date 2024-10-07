using System;
using System.Collections.Generic;
using System.IO;

namespace Es.Udc.DotNet.PracticaMaD.Model.ImageService
{
    [Serializable]
    public class UploadImageDto
    {
        public long userId { get; private set; }
        public string title { get; private set; }
        public string description { get; private set; }
        public string category { get; private set; }
        public DateTime releaseDate { get; private set; }
        public long apertureSize { get; private set; }
        public long whiteBalance { get; private set; }
        public long exposureTime { get; private set; }
        public Stream file { get; private set; }
        public string filename { get; private set; }
        public string path { get; private set; }
        public List<String> tagList { get; private set; }


        public UploadImageDto(long userId, string title, string description, string category, long exposureTime ,
            long apertureSize, long whiteBalance, Stream file, string filename, string path, List<String> tagList)
        {
            this.userId = userId;
            this.title = title;
            this.description = description;
            this.category = category;
            this.apertureSize = apertureSize;
            this.exposureTime = exposureTime;
            this.whiteBalance = whiteBalance;
            this.file = file;
            this.filename = filename;
            this.path = path;
            this.tagList = tagList;

        }
    }
}
