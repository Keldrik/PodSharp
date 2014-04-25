using PodSharp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace PodSharp.Parser
{
    class ParserEpisodeRaw
    {
        public List<EpisodeRaw> ParseNewEpisodeRawList(IEnumerable<XElement> elist)
        {
            List<EpisodeRaw> episodeList = new List<EpisodeRaw>();

            foreach (var e in elist)
            {
                episodeList.Add(ParseNewEpisodeRaw(e));
            }

            return episodeList;
        }

        public EpisodeRaw ParseNewEpisodeRaw(XElement e)
        {
            EpisodeRaw episode = new EpisodeRaw();

            foreach (var ee in e.Elements())
            {
                ParseEpisodeMeta(ee, episode);
                ParseEpisodeMetaItunes(ee, episode);
                ParseEpisodeMetaExtra(ee, episode);
                ParseEpisodeMetaMedia(ee, episode);
                ParseEpisodeMetaPSC(ee, episode);
            }

            return episode;
        }

        private void ParseEpisodeMeta(XElement e, EpisodeRaw episode)
        {
            switch (e.Name.ToString().ToLower())
            {
                case "title":
                    episode.Title = e.Value;
                    break;

                case "link":
                    episode.LinkWebsiteURL = e.Value;
                    break;

                case "description":
                    episode.Description = e.Value;
                    break;

                case "pubdate":
                    episode.PubDate = e.Value;
                    break;

                case "guid":
                    episode.guid = e.Value;
                    break;

                case "comments":
                    episode.LinkCommentsURL = e.Value;
                    break;

                case "{" + FeedNamespaceCollection.content + "}encoded":
                    episode.ContentEncoded = e.Value;
                    break;

                case "enclosure":
                    if (e.HasAttributes)
                    {
                        if (e.Attribute("url") != null && e.Attribute("url").Value != "")
                        {
                            episode.MediaItemURL = e.Attribute("url").Value;
                        }
                        if (e.Attribute("length") != null && e.Attribute("length").Value != "")
                        {
                            episode.MediaItemLength = e.Attribute("length").Value;
                        }
                        if (e.Attribute("type") != null && e.Attribute("type").Value != "")
                        {
                            episode.MediaItemType = e.Attribute("type").Value;
                        }
                    }
                    break;
            }
        }

        private void ParseEpisodeMetaItunes(XElement e, EpisodeRaw episode)
        {
            switch (e.Name.ToString().ToLower())
            {
                case "{" + FeedNamespaceCollection.itunes + "}subtitle":
                    episode.ItunesSubtitle = e.Value;
                    break;

                case "{" + FeedNamespaceCollection.itunes + "}author":
                    episode.ItunesAuthor = e.Value;
                    break;

                case "{" + FeedNamespaceCollection.itunes + "}summary":
                    episode.ItunesSummary = e.Value;
                    break;

                case "{" + FeedNamespaceCollection.itunes + "}duration":
                    episode.ItunesDuration = e.Value;
                    break;

                case "{" + FeedNamespaceCollection.itunes + "}keywords":
                    if (e.Value != "")
                    {
                        if (episode.ItunesKeywords == null)
                        {
                            episode.ItunesKeywords = new List<string>();
                        }
                        string k = e.Value;
                        string[] kk = k.Split(',');
                        foreach (var kkk in kk)
                        {
                            episode.ItunesKeywords.Add(kkk.Trim().ToLower());
                        }
                    }
                    break;

                case "payment":
                    if (e.HasAttributes && e.Attribute("href") != null && e.Attribute("title") != null)
                    {
                        if (episode.LinkPayments == null)
                        {
                            episode.LinkPayments = new List<Payment>();
                        }
                        episode.LinkPayments.Add(new Payment() { URL = e.Attribute("href").Value, Title = e.Attribute("title").Value });
                    }
                    break;
            }
        }

        private void ParseEpisodeMetaMedia(XElement e, EpisodeRaw episode)
        {
            switch (e.Name.ToString().ToLower())
            {
                case "{" + FeedNamespaceCollection.media + "}category":
                    if (!e.IsEmpty)
                    {
                        if (episode.MediaCategorys == null)
                        {
                            episode.MediaCategorys = new List<string>();
                        }
                        episode.MediaCategorys.Add(e.Value);
                    }
                    break;

                case "{" + FeedNamespaceCollection.media + "}credit":
                    if (!e.IsEmpty)
                    {
                        if (episode.MediaCredits == null)
                        {
                            episode.MediaCredits = new List<MediaCredit>();
                        }
                        MediaCredit c = new MediaCredit();
                        c.Name = e.Value;
                        if (e.HasAttributes && e.Attribute("role") != null)
                        {
                            c.Role = e.Attribute("role").Value;
                        }
                        episode.MediaCredits.Add(c);
                    }
                    break;
            }
        }

        private void ParseEpisodeMetaExtra(XElement e, EpisodeRaw episode)
        {
            switch (e.Name.ToString().ToLower())
            {
                case "{" + FeedNamespaceCollection.wfw + "}commentrss":
                    episode.WFWCommentFeed = e.Value;
                    break;

                case "{" + FeedNamespaceCollection.slash + "}comments":
                    episode.SlashComments = e.Value;
                    break;

                case "{" + FeedNamespaceCollection.dc + "}creator":
                    episode.DCCreator = e.Value;
                    break;

                case "{" + FeedNamespaceCollection.atom + "}contributor":
                    if (e.HasElements)
                    {
                        Contributor c = new Contributor();
                        foreach (var ee in e.Elements())
                        {
                            if (ee.Name.ToString().ToLower() == "{" + FeedNamespaceCollection.atom + "}name" && !ee.IsEmpty)
                            {
                                c.Name = ee.Value;
                            }
                            if (ee.Name.ToString().ToLower() == "{" + FeedNamespaceCollection.atom + "}uri" && !ee.IsEmpty)
                            {
                                c.URI = ee.Value;
                            }
                        }
                        if (c.Name != null && c.Name != "")
                        {
                            if (episode.Contributors == null)
                            {
                                episode.Contributors = new List<Contributor>();
                            }
                            episode.Contributors.Add(c);
                        }
                    }
                    break;
            }
        }

        private void ParseEpisodeMetaPSC(XElement e, EpisodeRaw episode)
        {
            switch (e.Name.ToString().ToLower())
            {
                case "{" + FeedNamespaceCollection.psc + "}chapters":
                    if (e.HasElements)
                    {
                        episode.PSCChapters = new List<Chapter>();
                        foreach (var ee in e.Elements())
                        {
                            if (ee.HasAttributes && ee.Attribute("start") != null &&
                                    ee.Attribute("start").Value != "" && ee.Attribute("title")
                                        != null && ee.Attribute("title").Value != "")
                            {
                                TimeSpan start;
                                if (TimeSpan.TryParse(ee.Attribute("start").Value.ToString(), out start))
                                {
                                    episode.PSCChapters.Add(new Chapter() { StartTime = start, Title = ee.Attribute("title").Value });
                                }
                            }
                        }
                    }
                    break;
            }
        }
    }
}
