using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using System.Net;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Runtime.Serialization;
using TweetSharp;

namespace RestService
{
    public class TwitterProxy
    {
          [OperationContract]
        public List<SportsBuzzTweet> /*UserStatusSvc[] GetUserTimeLine*/ GetTwitterData(string username, string filter)
        {

            List<TwitterStatus> tweets = new List<TwitterStatus>();
            TwitterService service;

            if (filter == "NBA" || filter == "All")
            {
                service = new TwitterService("evXjkuCvpwXKBWlnzPaDlw", "6WAwfe87ZAMYSpmh2JvtxfoOwS7lZdMyOQ0aZytsYU");
                service.AuthenticateWith("1146287640-bXJoH4wh5vTkFdQLIbl2xcmx4iyl4lWOybzqNfp", "yqd2izA8mvYULBM3y9Z7TQdxWabPhcRmn53GKk9eQ");
                tweets.AddRange(service.ListTweetsOnHomeTimeline(new ListTweetsOnHomeTimelineOptions() { Count = 200 }).ToList());
                //tweets = service.ListTweetsOnHomeTimeline(new ListTweetsOnHomeTimelineOptions() { Count = 200 }).ToList();
            }

            if (filter == "NFL" || filter == "All")
            {
                service = new TwitterService("Pcg1nt7s1TGHHCmfph80oQ", "Gda56oYVh7a4lKCCBLbWW6EhwYgj5sSQFwBivBYag");
                service.AuthenticateWith("1450834111-WDeHWOxcCujGYbkibfVWj8doZ9XeAczbMyLgpPA", "lClYwynFhpOWWUm3iAUFuqzIOHakQczlavFZdODJ8g");
                tweets.AddRange(service.ListTweetsOnHomeTimeline(new ListTweetsOnHomeTimelineOptions() { Count = 200 }).ToList());
                //tweets = service.ListTweetsOnHomeTimeline(new ListTweetsOnHomeTimelineOptions() { Count = 200 }).ToList();
            }
            //   var service = new TwitterService(_consumerKey, _consumerSecret);
            //service.AuthenticateWith(_accessToken, _accessTokenSecret);

            tweets = tweets.OrderBy(c => c.CreatedDate).ToList();
            List<SportsBuzzTweet> sbTweets = new List<SportsBuzzTweet>();
            SportsBuzzTweet sbTweet;
            foreach (var tweet in tweets)
            {
                sbTweet = new SportsBuzzTweet();
                sbTweet.ProfileImageUrl = tweet.User.ProfileImageUrl;
                sbTweet.ScreenName = tweet.User.ScreenName;
                //if (filter == "NBA")
                //    sbTweet.TweetText = "***NBA***" + tweet.Text;
                //else
                sbTweet.TweetText = tweet.Text;
                sbTweets.Add(sbTweet);

                Console.WriteLine("{0} says '{1}'", tweet.User.ScreenName, tweet.Text);
            }

            return sbTweets;

            //return tweets.ToList();

            //string url = "http://twitter.com/statuses/friends_timeline.xml";
            ////string user = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(username + ":" + password));
            //HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            //request.Method = "POST"; // <--- you probably need to change this to a GET after a twitter change a couple of months ago.
            //string password = "ficoand21";
            //request.Credentials = new NetworkCredential(username, password);
            //WebResponse response = request.GetResponse();
            //StreamReader reader = new StreamReader(response.GetResponseStream());
            //string responseString = reader.ReadToEnd();
            //reader.Close();
            //XmlDocument xmld = new XmlDocument();
            //xmld.LoadXml(responseString);
            //XDocument document = XDocument.Parse(responseString);

            //var query = from e in document.Root.Descendants()
            //            where e.Element("user") != null
            //            select new UserStatusSvc
            //            {
            //                UserName = e.Element("user").Element("screen_name").Value,
            //                ProfileImage = e.Element("user").Element("profile_image_url").Value,
            //                Status = HttpUtility.HtmlDecode(e.Element("text").Value),
            //                StatusDate = e.Value.ParseDateTime().ToString()
            //            };

            //var users = (from u in query
            //             where u.Status != ""
            //             select u).ToList();

            //return (users.ToArray());
        }


//Here's the definition of my UserStatusSvc object.

    [DataContract]
    public class UserStatusSvc
    {
        [DataMember]
        public string UserName { get; set; }
        [DataMember]
        public string ProfileImage { get; set; }
        [DataMember]
        public string Status { get; set; }
        [DataMember]
        public string StatusDate { get; set; }
    }

    [DataContract]
    public class SportsBuzzTweet
    {
        [DataMember]
        public string ScreenName { get; set; }
        [DataMember]
        public string ProfileImageUrl { get; set; }
        [DataMember]
        public string TweetText { get; set; }
        
    }


    }
    public static class StringExtensions
    {
        public static DateTime ParseDateTime(this string date)
        {
            string dayOfWeek = date.Substring(0, 3).Trim();
            string month = date.Substring(4, 3).Trim();
            string dayInMonth = date.Substring(8, 2).Trim();
            string time = date.Substring(11, 9).Trim();
            string offset = date.Substring(20, 5).Trim();
            string year = date.Substring(25, 5).Trim();
            string dateTime = string.Format("{0}-{1}-{2} {3}", dayInMonth, month, year, time);
            DateTime ret = DateTime.Parse(dateTime);
            return ret;
        }
    }
}