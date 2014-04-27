using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PodSharp.Model
{
	public class Podcast
	{
		public string Title { get; set; }
		public string Author { get; set; }
		public string FeedURL { get; set; }
		public string WebsiteURL { get; set; }
		public string ImageURL { get; set; }
        public Uri ImageURI { get; set; }
		public string Language { get; set; }
		public string Copyright { get; set; }
		public DateTime LastBuild { get; set; }
        public string LastBuildString
        {
            get
            {
                return LastBuild.ToLocalTime().Date.ToString();
            }
        }

		public List<AlternateFeed> FeedAlt { get; set; }
		public bool HasFeedAlt
		{
			get
			{
				if (FeedAlt != null && FeedAlt.Count > 0)
				{
					return true;

				}
				else
				{
					return false;
				}
			}
		}

		public string Subtitle { get; set; }
		public string Summary { get; set; }
		public string Description { get; set; }

		public List<string> Keywords { get; set; }
		public bool HasKeywords {
			get
			{
				if (Keywords != null && Keywords.Count > 0)
				{
					return true;

				}
				else
				{
					return false;
				}
			}
		}

		public List<ItunesCategory> Categorys { get; set; }
		public bool HasCategorys
		{
			get
			{
				if (Categorys != null && Categorys.Count > 0)
				{
					return true;
				}
				else
				{
					return false;
				}
			}
		}

		public List<Contributor> Contributors { get; set; }
		public bool HasContributors
		{
			get
			{
				if (Contributors != null && Contributors.Count > 0)
				{
					return true;
				}
				else
				{
					return false;
				}
			}
		}

		public List<Payment> PaymentLinks { get; set; }
		public bool HasPaymentLinks
		{
			get
			{
				if (PaymentLinks != null && PaymentLinks.Count > 0)
				{
					return true;
				}
				else
				{
					return false;
				}
			}
		}

		public List<Episode> Episodes { get; set; }
		public int EpisodesCount
		{
			get
			{
				if (Episodes == null)
				{
					return 0;
				}
				return Episodes.Count;
			}
		}
	}
}
