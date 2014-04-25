using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PodSharp.Model
{
    public class PodcastRaw
    {
        public List<EpisodeRaw> Episodes { get; set; }


        public string Title { get; set; }

        public string LinkWebsiteURL { get; set; }

        public string LinkFeedURL { get; set; }

        public string Language { get; set; }

        public string Description { get; set; }

        public string Category { get; set; }

        public string PubDate { get; set; }

        public string LastBuildDate { get; set; }

        public string ImageURL { get; set; }

        public string Copyright { get; set; }

        public string Webmaster { get; set; }

        public string ManagingEditor { get; set; }


        public List<AlternateFeed> LinkAlternateFeeds { get; set; }

        public string LinkFeedFirstPageURL { get; set; }

        public string LinkFeedNextPageURL { get; set; }

        public string LinkFeedLastPageURL { get; set; }

        public List<Contributor> Contributors { get; set; }


        public string ItunesAuthor { get; set; }

        public string ItunesSubtitle { get; set; }

        public string ItunesSummary { get; set; }

        public List<string> ItunesKeywords { get; set; }

        public List<ItunesCategory> ItunesCategorys { get; set; }

        public ItunesOwner ItunesOwner { get; set; }

        public string ItunesImageURL { get; set; }


        public string FeedGenerator { get; set; }

        public List<Payment> LinkPayments { get; set; }
    }
}
