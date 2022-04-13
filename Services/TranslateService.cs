using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;
using Google.Apis.Services;
using Google.Apis.Storage.v1;
using Google.Cloud.Translation.V2;
using Newtonsoft.Json;

namespace Hattmakarens_system.Services
{
    public class TranslateClass
    {
        public string Translate(string text)
        {
			using (var client = new HttpClient())
            {
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri("https://google-translate1.p.rapidapi.com/language/translate/v2"),
                    Headers =
                    {
                        { "X-RapidAPI-Host", "google-translate1.p.rapidapi.com" },
                        { "X-RapidAPI-Key", "e11581426amsh78d782a5e6752b3p1cba1djsn9069677d70ca" },
                    },
                    Content = new FormUrlEncodedContent(new Dictionary<string, string>
                    {
                        { "q", text },
                        { "target", "en" },
                        { "source", "sv" },
                    }),
                };

                var response = client.SendAsync(request).Result;
                var content = response.Content.ReadAsStringAsync().Result;
                var contentlength = content.Length - 49;
                var finalcontent = content.Substring(44, contentlength);

                List<char> charlist = new List<char>();

                foreach(var item in finalcontent)
                {
                    charlist.Add(item);
                }

                string translatedText = new string (charlist.ToArray());
                return translatedText;
            }
        }
    }
}