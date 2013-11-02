using Newtonsoft.Json;
using System.Collections.Generic;
namespace RestService
{
    public class RestServiceImpl : IRestServiceImpl
    {
        #region IRestServiceImpl Members

        public string XMLData(string id)
        {
            TwitterProxy tp = new TwitterProxy();

            var tl = tp.GetTwitterData("sekretarot",id);
            //var tl = tp.GetUserTimeLine("sekretarot");

            var sz = JsonConvert.SerializeObject(tl);
            return sz;
            //return "You requested product " + id;
        }

        public List<TwitterProxy.SportsBuzzTweet> /*string*/ JSONData(string id)
        {
            TwitterProxy tp = new TwitterProxy();
            var tl = tp.GetTwitterData("sekretarot",id);

            //var tl = tp.GetUserTimeLine("sekretarot");

            //string result = "{\"results\":[";

            //foreach (var tlitem in tl)
            //{
            //    result += JsonConvert.SerializeObject(tlitem);
            //}
            //result += "]}\"";
            //return result;
            return tl;

            //var sz = JsonConvert.SerializeObject(tl);
            

            //return sz;

            //return "You requested product " + id;
        }

        #endregion

    }
}