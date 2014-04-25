using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PodSharp.Model
{
    public class AlternateFeed
    {
        public string Title { get; set; }

        public string URL { get; set; }
    }

    public class Payment
    {
        public string Title { get; set; }

        public string URL { get; set; }
    }

    public class Contributor
    {
        public string Name { get; set; }

        public string URI { get; set; }
    }

    public class ItunesCategory
    {
        public string Name { get; set; }

        public List<string> SubCategorys { get; set; }
    }

    public class ItunesOwner
    {
        public string Name { get; set; }

        public string EMail { get; set; }
    }

    public class Chapter
    {
        public string Title { get; set; }

        public TimeSpan StartTime { get; set; }
    }

    public class MediaCredit
    {
        public string Role { get; set; }

        public string Name { get; set; }
    }

    public class MediaItem
    {
        public string URL { get; set; }
        public MediaFileType FileType { get; set; }
        public MediaFileFormat FileFormat { get; set; }
        public long FileSize { get; set; }
        public TimeSpan Duration { get; set; }
    }

    public enum MediaFileFormat
    {
        unknown,
        other,
        mpg,
        mpeg,
        mp3,
        m4a,
        oga,
        opus,
        mp4,
        aac
    }

    public enum MediaFileType
    {
        unknown,
        audio,
        video
    }
}
