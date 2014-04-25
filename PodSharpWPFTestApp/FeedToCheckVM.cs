using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PodSharpWPFTestApp
{
    class FeedToCheckVM : INotifyPropertyChanged
    {
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
