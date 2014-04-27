using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PodSharp.Model;
using System.Collections.ObjectModel;

namespace PodSharpWPFTestApp
{
    class Viewmodel : INotifyPropertyChanged
    {
        public Viewmodel()
        {
            Episodes = new ObservableCollection<Episode>();
        }

        public void LoadPodcast(Podcast p)
        {
            Podcast = p;
            Episodes.Clear();
            foreach (var e in p.Episodes)
            {
                Episodes.Add(e);
            }
        }

        private Podcast _Podcast;
        public Podcast Podcast
        {
            get { return _Podcast; }
            set
            {
                _Podcast = value;
                NotifyPropertyChanged("Podcast");
            }
        }

        public ObservableCollection<Episode> Episodes { get; set; }

        private Episode _SelectedEpisode;
        public Episode SelectedEpisode
        {
            get { return _SelectedEpisode; }
            set
            {
                _SelectedEpisode = value;
                NotifyPropertyChanged("SelectedEpisode");
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
