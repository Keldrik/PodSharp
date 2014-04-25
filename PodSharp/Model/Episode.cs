using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PodSharp.Model
{
    public class Episode
    {
        public string Title { get; set; }
        public string GUID { get; set; }
        public string WebsiteURL { get; set; }
        public DateTime PubDate { get; set; }

        public string Description { get; set; }
        public bool DescriptionContainsMarkup { get; set; }

        public string Content { get; set; }
        public bool ContentContainsMarkup { get; set; }

        public string Subtitle { get; set; }
        public string Summary { get; set; }

        public MediaItem MediaContent { get; set; }
        public List<MediaItem> MediaContentAlt { get; set; }
        public bool HasMediaContentAlt
        {
            get
            {
                if (MediaContentAlt != null && MediaContentAlt.Count > 0)
                {
                    return true;

                }
                else
                {
                    return false;
                }
            }
        }

        public List<string> Keywords { get; set; }
        public bool HasKeywords
        {
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

        public List<Chapter> Chapters { get; set; }
        public bool HasChapters
        {
            get
            {
                if (Chapters != null && Chapters.Count > 0)
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
    }
}