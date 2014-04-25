using PodSharp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PodSharp.Parser
{
    class ParserPodcast
    {
        public Podcast ParsePodcast(PodcastRaw praw)
        {
            Podcast podcast = new Podcast();

            podcast.Title = praw.Title;
            podcast.Author = praw.ItunesAuthor;

            if (!string.IsNullOrEmpty(praw.LinkWebsiteURL))
            {
                podcast.WebsiteURL = praw.LinkWebsiteURL.ToLower();
            }

            if (!string.IsNullOrEmpty(praw.ItunesImageURL))
            {
                podcast.ImageURL = praw.ItunesImageURL.ToLower();
            }
            else
            {
                if (!string.IsNullOrEmpty(praw.ImageURL))
                {
                    podcast.ImageURL = praw.ImageURL.ToLower();
                }
            }

            podcast.Language = praw.Language;
            podcast.Copyright = praw.Copyright;

            DateTime lastbuilddate;
            if (DateTime.TryParse(praw.PubDate, out lastbuilddate))
            {
                podcast.LastBuild = lastbuilddate;
            }
            else
            {
                podcast.LastBuild = DateTime.Now;
            }

            podcast.FeedAlt = praw.LinkAlternateFeeds;
            if (podcast.HasFeedAlt)
            {
                foreach (var f in podcast.FeedAlt)
                {
                    f.URL = f.URL.ToLower();
                }
            }

            podcast.Subtitle = praw.ItunesSubtitle;
            podcast.Summary = praw.ItunesSummary;
            podcast.Description = praw.Description;
            podcast.Keywords = praw.ItunesKeywords;
            podcast.Categorys = praw.ItunesCategorys;

            podcast.Contributors = praw.Contributors;
            if (podcast.HasContributors)
            {
                foreach (var c in podcast.Contributors)
                {
                    if (!string.IsNullOrEmpty(c.URI))
                    {
                        c.URI = c.URI.ToLower();
                    }
                }
            }

            podcast.PaymentLinks = praw.LinkPayments;
            if (podcast.HasPaymentLinks)
            {
                foreach (var p in podcast.PaymentLinks)
                {
                    p.URL = p.URL.ToLower();
                }
            }

            return podcast;
        }
    }
}