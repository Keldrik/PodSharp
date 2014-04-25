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

            ParserPodcast pparse = new ParserPodcast();
            ParserEpisode eparse = new ParserEpisode();

            Podcast podcast = pparse.ParsePodcast(praw);

            if (praw.Episodes != null && praw.Episodes.Count > 0)
            {
                podcast.Episodes = eparse.ParseEpisodeList(praw.Episodes);
            }

            return podcast;
        }
    }
}