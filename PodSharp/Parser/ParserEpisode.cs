using PodSharp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PodSharp.Parser
{
    class ParserEpisode
    {
        public List<Episode> ParseEpisodeList(List<EpisodeRaw> erawlist)
        {
            List<Episode> episodelist = new List<Episode>();

            foreach (var e in erawlist)
            {
                episodelist.Add(ParseEpisode(e));
            }

            return episodelist;
        }

        public Episode ParseEpisode(EpisodeRaw eraw)
        {
            Episode episode = new Episode();

            episode.Title = eraw.Title;
            episode.GUID = eraw.guid;

            if (!string.IsNullOrEmpty(eraw.LinkWebsiteURL))
            {
                episode.WebsiteURL = eraw.LinkWebsiteURL.ToLower();
            }

            DateTime pubdate;
            if (DateTime.TryParse(eraw.PubDate, out pubdate))
            {
                episode.PubDate = pubdate;
            }
            else
            {
                episode.PubDate = DateTime.Now;
            }

            if (!string.IsNullOrEmpty(eraw.Description))
            {
                episode.Description = eraw.Description;
                if (PodHelper.CheckTextForHtmlEncode(episode.Description))
                {
                    System.Net.WebUtility.HtmlDecode(episode.Description);
                }
                episode.DescriptionContainsMarkup = PodHelper.CheckTextForMarkup(episode.Description);
            }

            if (!string.IsNullOrEmpty(eraw.ContentEncoded))
            {
                episode.Content = eraw.ContentEncoded;
                if (PodHelper.CheckTextForHtmlEncode(episode.Content))
                {
                    System.Net.WebUtility.HtmlDecode(episode.Content);
                }
                episode.ContentContainsMarkup = PodHelper.CheckTextForMarkup(episode.Content);
            }

            episode.Subtitle = eraw.ItunesSubtitle;
            episode.Summary = eraw.ItunesSummary;

            episode.Keywords = eraw.ItunesKeywords;

            episode.Contributors = eraw.Contributors;
            if (eraw.MediaCredits != null && eraw.MediaCredits.Count > 0)
            {
                if (episode.Contributors == null)
                {
                    episode.Contributors = new List<Contributor>();
                }
                foreach (var mc in eraw.MediaCredits)
                {
                    episode.Contributors.Add(new Contributor() { Name = mc.Name });
                }
            }
            if (episode.HasContributors)
            {
                foreach (var c in episode.Contributors)
                {
                    if (!string.IsNullOrEmpty(c.URI))
                    {
                        c.URI = c.URI.ToLower();
                    }
                }
            }

            episode.Chapters = eraw.PSCChapters;

            episode.PaymentLinks = eraw.LinkPayments;
            if (episode.HasPaymentLinks)
            {
                foreach (var p in episode.PaymentLinks)
                {
                    p.URL = p.URL.ToLower();
                }
            }

            if (!string.IsNullOrEmpty(eraw.MediaItemURL))
            {
                episode.MediaContent = ParseMediaItem(eraw);
            }

            return episode;
        }

        private MediaItem ParseMediaItem(EpisodeRaw eraw)
        {
            MediaItem item = new MediaItem();

            item.URL = eraw.MediaItemURL.ToLower();

            TimeSpan duration;
            if (!string.IsNullOrEmpty(eraw.ItunesDuration) && TimeSpan.TryParse(eraw.ItunesDuration, out duration))
            {
                item.Duration = duration;
            }
            else
            {
                item.Duration = TimeSpan.Zero;
            }

            long filesize;
            if (!string.IsNullOrEmpty(eraw.MediaItemLength) && long.TryParse(eraw.MediaItemLength, out filesize))
            {
                item.FileSize = filesize;
            }
            else
            {
                item.FileSize = 0;
            }

            if (!string.IsNullOrEmpty(eraw.MediaItemType))
            {
                string[] mtsa = eraw.MediaItemType.Split('/');
                string mts = mtsa.FirstOrDefault().Trim().ToLower();
                MediaFileType mt;
                switch (mts)
                {
                    case "audio":
                        mt = MediaFileType.audio;
                        break;
                    case "video":
                        mt = MediaFileType.video;
                        break;
                    default:
                        mt = MediaFileType.unknown;
                        break;
                }
                item.FileType = mt;
            }

            return item;
        }
    }
}
