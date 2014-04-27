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
    public partial class MainWindow : Window
    {
        FeedReader reader = new FeedReader();
        Viewmodel vm = new Viewmodel();

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = vm;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            CheckButton.Visibility = System.Windows.Visibility.Collapsed;
            vm.LoadPodcast(await reader.GetNewPodcastAsync(feedurlInput.Text));
            CheckButton.Visibility = System.Windows.Visibility.Visible;
        }
    }
}
