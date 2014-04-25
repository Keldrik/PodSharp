using PodSharp;
using PodSharp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PodSharpWPFTestApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private FeedToCheckVM feedvm = new FeedToCheckVM();

        FeedReader reader = new FeedReader();
        private Podcast podcast;

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = feedvm;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            podcast = await reader.GetNewPodcastAsync(feedurlInput.Text);

            feedvm.Title = podcast.Title;

            feedvm.ImageURI = new Uri(podcast.ImageURL);

            feedvm.Author = podcast.Author;
            feedvm.Copyright = podcast.Copyright;
            feedvm.Language = podcast.Language;
            feedvm.Website = podcast.WebsiteURL;

            feedvm.LastBuild = podcast.LastBuild.Day.ToString() + "/" + podcast.LastBuild.Month.ToString() + "/" + podcast.LastBuild.Year.ToString();

            feedvm.Subtitle = podcast.Subtitle;
            feedvm.Summary = podcast.Summary;

            if (podcast.EpisodesCount > 0)
            {
                foreach (var episode in podcast.Episodes)
                {
                    feedvm.Episodes.Add(episode);
                }
            }
        }
    }
}
