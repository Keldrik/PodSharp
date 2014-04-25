using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PodSharp.Model
{
    public class EpisodeRaw
    {
        public string Title { get; set; }

        public string LinkWebsiteURL { get; set; }

        public string LinkCommentsURL { get; set; }

        public string PubDate { get; set; }

        public string guid { get; set; }

        public string Description { get; set; }

        public string ContentEncoded { get; set; }


        public string MediaItemType { get; set; }

        public string MediaItemURL { get; set; }

        public string MediaItemLength { get; set; }


        public List<Contributor> Contributors { get; set; }


        public string ItunesAuthor { get; set; }

        public string ItunesSubtitle { get; set; }

        public string ItunesSummary { get; set; }

        public string ItunesDuration { get; set; }

        public List<string> ItunesKeywords { get; set; }


        public List<string> MediaCategorys { get; set; }

        public List<MediaCredit> MediaCredits { get; set; }


        public string WFWCommentFeed { get; set; }

        public string SlashComments { get; set; }

        public string DCCreator { get; set; }

        public List<Chapter> PSCChapters { get; set; }

        public List<Payment> LinkPayments { get; set; }
    }
}
