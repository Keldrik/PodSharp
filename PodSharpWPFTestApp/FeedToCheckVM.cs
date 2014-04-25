using PodSharp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PodSharpWPFTestApp
{
    class FeedToCheckVM : INotifyPropertyChanged
    {
        public FeedToCheckVM()
        {
            Episodes = new ObservableCollection<Episode>();
        }

        public ObservableCollection<Episode> Episodes { get; set; }

        private Episode _Episode;
        public Episode Episode
        {
            get { return _Episode; }
            set
            {
                _Episode = value;
                NotifyPropertyChanged("Episode");

                //
                EpisodeTitle = Episode.Title;
                EpisodeGUID = Episode.GUID;
                EpisodePubDate = Episode.PubDate.ToShortDateString();
                EpisodeMediaURL = Episode.MediaContent.URL;
                EpisodeSubtitle = Episode.Subtitle;
                EpisodeSummary = Episode.Summary;

            }
        }

        private string _EpisodeSubtitle;
        public string EpisodeSubtitle
        {
            get { return _EpisodeSubtitle; }
            set
            {
                _EpisodeSubtitle = value;
                NotifyPropertyChanged("EpisodeSubtitle");
            }
        }

        private string _EpisodeSummary;
        public string EpisodeSummary
        {
            get { return _EpisodeSummary; }
            set
            {
                _EpisodeSummary = value;
                NotifyPropertyChanged("EpisodeSummary");
            }
        }

        private string _EpisodeMediaURL;
        public string EpisodeMediaURL
        {
            get { return _EpisodeMediaURL; }
            set
            {
                _EpisodeMediaURL = value;
                NotifyPropertyChanged("EpisodeMediaURL");
            }
        }

        private string _EpisodeTitle;
        public string EpisodeTitle
        {
            get { return _EpisodeTitle; }
            set
            {
                _EpisodeTitle = value;
                NotifyPropertyChanged("EpisodeTitle");
            }
        }

        private string _EpisodeGUID;
        public string EpisodeGUID
        {
            get { return _EpisodeGUID; }
            set
            {
                _EpisodeGUID = value;
                NotifyPropertyChanged("EpisodeGUID");
            }
        }

        private string _EpisodePubDate;
        public string EpisodePubDate
        {
            get { return _EpisodePubDate; }
            set
            {
                _EpisodePubDate = value;
                NotifyPropertyChanged("EpisodePubDate");
            }
        }

        private string _Title;
        public string Title
        {
            get { return _Title; }
            set
            {
                _Title = value;
                NotifyPropertyChanged("Title");
            }
        }

        private string _Author;
        public string Author
        {
            get { return _Author; }
            set
            {
                _Author = value;
                NotifyPropertyChanged("Author");
            }
        }

        private string _Website;
        public string Website
        {
            get { return _Website; }
            set
            {
                _Website = value;
                NotifyPropertyChanged("Website");
            }
        }

        private Uri _ImageURI;
        public Uri ImageURI
        {
            get { return _ImageURI; }
            set
            {
                _ImageURI = value;
                NotifyPropertyChanged("ImageURI");
            }
        }

        private string _Language;
        public string Language
        {
            get { return _Language; }
            set
            {
                _Language = value;
                NotifyPropertyChanged("Language");
            }
        }

        private string _Copyright;
        public string Copyright
        {
            get { return _Copyright; }
            set
            {
                _Copyright = value;
                NotifyPropertyChanged("Copyright");
            }
        }

        private string _LastBuild;
        public string LastBuild
        {
            get { return _LastBuild; }
            set
            {
                _LastBuild = value;
                NotifyPropertyChanged("LastBuild");
            }
        }

        private string _Subtitle;
        public string Subtitle
        {
            get { return _Subtitle; }
            set
            {
                _Subtitle = value;
                NotifyPropertyChanged("Subtitle");
            }
        }

        private string _Summary;
        public string Summary
        {
            get { return _Summary; }
            set
            {
                _Summary = value;
                NotifyPropertyChanged("Summary");
            }
        }


        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        // Used to notify Silverlight that a property has changed.
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
