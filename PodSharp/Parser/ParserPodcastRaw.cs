using PodSharp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace PodSharp.Parser
{
    class ParserPodcastRaw
    {
        public PodcastRaw ParseNewPodcast(XElement channel)
        {
            PodcastRaw podcastRaw = new PodcastRaw();

            foreach (var e in channel.Elements())
            {
                ParsePodcastMeta(e, podcastRaw);
                ParsePodcastMetaAtom(e, podcastRaw);
                ParsePodcastMetaItunes(e, podcastRaw);
            }

            return podcastRaw;
        }

        private void ParsePodcastMeta(XElement e, PodcastRaw podcastRaw)
        {
            switch (e.Name.ToString().ToLower())
            {
                case "title":
                    podcastRaw.Title = e.Value;
                    break;

                case "link":
                    podcastRaw.LinkWebsiteURL = e.Value;
                    break;

                case "language":
                    podcastRaw.Language = e.Value;
                    break;

                case "description":
                    podcastRaw.Description = e.Value;
                    break;

                case "category":
                    podcastRaw.Category = e.Value;
                    break;

                case "pubdate":
                    podcastRaw.PubDate = e.Value;
                    break;

                case "lastbuilddate":
                    podcastRaw.LastBuildDate = e.Value;
                    break;

                case "image":
                    if (e.HasElements)
                    {
                        XElement x = e.Elements("url").FirstOrDefault();
                        if (x != null && !x.IsEmpty)
                        {
                            podcastRaw.ImageURL = x.Value;
                        }
                    }
                    break;

                case "copyright":
                    podcastRaw.Copyright = e.Value;
                    break;

                case "webmaster":
                    podcastRaw.Webmaster = e.Value;
                    break;

                case "managingeditor":
                    podcastRaw.ManagingEditor = e.Value;
                    break;

                case "generator":
                    podcastRaw.FeedGenerator = e.Value;
                    break;
            }
        }

        private void ParsePodcastMetaAtom(XElement e, PodcastRaw podcastRaw)
        {
            if (e.Name.ToString().ToLower() == "{" + FeedNamespaceCollection.atom + "}link" && e.HasAttributes && e.Attribute("rel") != null)
            {
                switch (e.Attribute("rel").Value)
                {
                    case "alternate":
                        if (podcastRaw.LinkAlternateFeeds == null)
                        {
                            podcastRaw.LinkAlternateFeeds = new List<AlternateFeed>();
                        }
                        podcastRaw.LinkAlternateFeeds.Add(new AlternateFeed() { Title = e.Attribute("title").Value, URL = e.Attribute("href").Value });
                        break;

                    case "next":
                        podcastRaw.LinkFeedNextPageURL = e.Attribute("href").Value;
                        break;

                    case "first":
                        podcastRaw.LinkFeedFirstPageURL = e.Attribute("href").Value;
                        break;

                    case "last":
                        podcastRaw.LinkFeedLastPageURL = e.Attribute("href").Value;
                        break;

                    case "payment":
                        if (e.HasAttributes && e.Attribute("href") != null && e.Attribute("title") != null)
                        {
                            if (podcastRaw.LinkPayments == null)
                            {
                                podcastRaw.LinkPayments = new List<Payment>();
                            }
                            podcastRaw.LinkPayments.Add(new Payment() { URL = e.Attribute("href").Value, Title = e.Attribute("title").Value });
                        }
                        break;
                }
            }
            if (e.Name.ToString().ToLower() == "{" + FeedNamespaceCollection.atom + "}contributor" && e.HasElements)
            {
                Contributor c = new Contributor();
                foreach (var i in e.Elements())
                {
                    if (i.Name.ToString().ToLower() == "{" + FeedNamespaceCollection.atom + "}name")
                    {
                        c.Name = i.Value;
                    }
                    if (i.Name.ToString().ToLower() == "{" + FeedNamespaceCollection.atom + "}uri")
                    {
                        c.URI = i.Value;
                    }
                }
                if (c.Name != null && c.Name != "")
                {
                    if (podcastRaw.Contributors == null)
                    {
                        podcastRaw.Contributors = new List<Contributor>();
                    }
                    podcastRaw.Contributors.Add(c);
                }
            }
        }

        private void ParsePodcastMetaItunes(XElement e, PodcastRaw podcastRaw)
        {
            switch (e.Name.ToString().ToLower())
            {
                case "{" + FeedNamespaceCollection.itunes + "}subtitle":
                    podcastRaw.ItunesSubtitle = e.Value;
                    break;

                case "{" + FeedNamespaceCollection.itunes + "}author":
                    podcastRaw.ItunesAuthor = e.Value;
                    break;

                case "{" + FeedNamespaceCollection.itunes + "}summary":
                    podcastRaw.ItunesSummary = e.Value;
                    break;

                case "{" + FeedNamespaceCollection.itunes + "}keywords":
                    if (e.Value != "")
                    {
                        if (podcastRaw.ItunesKeywords == null)
                        {
                            podcastRaw.ItunesKeywords = new List<string>();
                        }
                        string k = e.Value;
                        string[] kk = k.Split(',');
                        foreach (var kkk in kk)
                        {
                            podcastRaw.ItunesKeywords.Add(kkk.Trim().ToLower());
                        }
                    }
                    break;

                case "{" + FeedNamespaceCollection.itunes + "}category":
                    if (e.HasAttributes && e.Attribute("text") != null && e.Attribute("text").Value != "")
                    {
                        ItunesCategory c = new ItunesCategory();
                        c.Name = e.Attribute("text").Value;
                        if (e.HasElements)
                        {
                            c.SubCategorys = new List<string>();
                            foreach (var cc in e.Elements())
                            {
                                if (cc.HasAttributes && cc.Attribute("text") != null && cc.Attribute("text").Value != "")
                                {
                                    c.SubCategorys.Add(cc.Attribute("text").Value);
                                }
                            }
                        }
                        if (podcastRaw.ItunesCategorys == null)
                        {
                            podcastRaw.ItunesCategorys = new List<ItunesCategory>();
                        }
                        podcastRaw.ItunesCategorys.Add(c);
                    }
                    break;

                case "{" + FeedNamespaceCollection.itunes + "}owner":
                    if (e.HasElements)
                    {
                        ItunesOwner io = new ItunesOwner();
                        foreach (var o in e.Elements())
                        {
                            if (o.Name.ToString().ToLower() == "{" + FeedNamespaceCollection.itunes + "}name")
                            {
                                io.Name = o.Value;
                            }
                            if (o.Name.ToString().ToLower() == "{" + FeedNamespaceCollection.itunes + "}email")
                            {
                                io.EMail = o.Value;
                            }
                        }
                        if (io.Name != null && io.Name != "")
                        {
                            podcastRaw.ItunesOwner = io;
                        }
                    }
                    break;

                case "{" + FeedNamespaceCollection.itunes + "}image":
                    if (e.HasAttributes && e.Attribute("href") != null && e.Attribute("href").Value != "")
                    {
                        podcastRaw.ItunesImageURL = e.Attribute("href").Value;
                    }
                    break;
            }
        }
    }
}
