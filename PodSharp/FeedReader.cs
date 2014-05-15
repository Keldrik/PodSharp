using PodSharp.Model;
using PodSharp.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PodSharp
{
    public class FeedReader
    {
        private async Task<XElement> LoadWebFeedAsync(string url)
        {
            var request = HttpWebRequest.CreateHttp(url);
            var response = await request.GetResponseAsync();
            return XElement.Load(response.GetResponseStream());
        }

        public async Task<PodcastRaw> GetPodcastRawAsync(string url)
        {
            PodcastRaw podcast = new PodcastRaw();
            podcast.LinkFeedURL = url.ToLower();
            ParserPodcastRaw pparse = new ParserPodcastRaw();
            ParserEpisodeRaw eparse = new ParserEpisodeRaw();

            XElement root = await LoadWebFeedAsync(url);

            var channel = root.Descendants("channel").FirstOrDefault();
            podcast = pparse.ParseNewPodcast(channel);

            var items = root.Descendants("item");
            podcast.Episodes = eparse.ParseNewEpisodeRawList(items);

            return podcast;
        }

        public async Task<Podcast> GetPodcastAsync(string url)
        {
            PodcastRaw praw = await GetPodcastRawAsync(url);
            praw.Episodes.AddRange(await MultipageEpisodeListAsync(praw));

            ParserPodcast pparse = new ParserPodcast();
            ParserEpisode eparse = new ParserEpisode();

            Podcast podcast = pparse.ParsePodcast(praw);

            if (praw.Episodes.Count > 0)
            {
                podcast.Episodes = eparse.ParseEpisodeList(praw.Episodes);

                if (podcast.HasFeedAlt)
                {
                    List<EpisodeRaw> altepisodes = new List<EpisodeRaw>();
                    foreach (var af in podcast.FeedAlt)
                    {
                        PodcastRaw pr = await GetPodcastRawAsync(af.URL);
                        pr.Episodes.AddRange(await MultipageEpisodeListAsync(pr));
                        altepisodes.AddRange(pr.Episodes);
                    }
                    foreach (var e in podcast.Episodes)
                    {
                        e.MediaContentAlt = new List<MediaItem>();
                        foreach (var i in altepisodes.Where(x => x.guid == e.GUID))
                        {
                            e.MediaContentAlt.Add(eparse.ParseMediaItem(i));
                        }
                    }
                }
            }

            return podcast;
        }

        private async Task<List<EpisodeRaw>> MultipageEpisodeListAsync(PodcastRaw praw)
        {
            List<EpisodeRaw> episodes = new List<EpisodeRaw>();
            string next = praw.LinkFeedNextPageURL;
            while (!string.IsNullOrEmpty(next))
            {
                PodcastRaw p = await GetPodcastRawAsync(next);
                episodes.AddRange(p.Episodes);
                next = p.LinkFeedNextPageURL;
            }
            return episodes;
        }
    }
}