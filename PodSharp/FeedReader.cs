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
        private async Task<XElement> LoadFeedFromWebAsync(string url)
        {
            var request = HttpWebRequest.CreateHttp(url);
            var response = await request.GetResponseAsync();
            return XElement.Load(response.GetResponseStream());
        }

        public async Task<PodcastRaw> GetNewPodcastRawAsync(string url)
        {
            PodcastRaw podcast = new PodcastRaw();
            podcast.LinkFeedURL = url.ToLower();
            ParserPodcastRaw pparse = new ParserPodcastRaw();
            ParserEpisodeRaw eparse = new ParserEpisodeRaw();

            XElement root = await LoadFeedFromWebAsync(url);

            var channel = root.Descendants("channel").FirstOrDefault();
            podcast = pparse.ParseNewPodcast(channel);

            var items = root.Descendants("item");
            podcast.Episodes = eparse.ParseNewEpisodeRawList(items);

            return podcast;
        }

        public async Task<Podcast> GetNewPodcastAsync(string url)
        {
            PodcastRaw praw = await GetNewPodcastRawAsync(url);
            praw.Episodes.AddRange(await GetNextpageEpisodeList(praw));

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
                        PodcastRaw pr = await GetNewPodcastRawAsync(af.URL);
                        pr.Episodes.AddRange(await GetNextpageEpisodeList(pr));
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

        private async Task<List<EpisodeRaw>> GetNextpageEpisodeList(PodcastRaw praw)
        {
            List<EpisodeRaw> episodes = new List<EpisodeRaw>();
            string next = praw.LinkFeedNextPageURL;
            while (!string.IsNullOrEmpty(next))
            {
                PodcastRaw p = await GetNewPodcastRawAsync(next);
                episodes.AddRange(p.Episodes);
                next = p.LinkFeedNextPageURL;
            }
            return episodes;
        }
    }
}