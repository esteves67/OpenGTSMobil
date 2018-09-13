using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using OpenGTSMobil.Models;

namespace OpenGTSMobil.ViewModels
{
    public class RestClient
    {
        public static string urlServer = Global.urlServer;

        //Login App
        public async Task<T> Login<T>(string AccountId = "", string Mail = "", string Password = "")
        {
            if(Global.isConected)
            {
                try
                {
                    HttpClient client = new HttpClient();
                    var postData = new List<KeyValuePair<string, string>>();
                    postData.Add(new KeyValuePair<string, string>("a", AccountId));
                    postData.Add(new KeyValuePair<string, string>("u", Mail));
                    postData.Add(new KeyValuePair<string, string>("p", Password));
                    postData.Add(new KeyValuePair<string, string>("g", "all"));
                    postData.Add(new KeyValuePair<string, string>("l", "1"));
                    var content = new FormUrlEncodedContent(postData);
                    var response = await client.PostAsync(urlServer, content);
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        if (response.Content != null)
                        {
                            var jsonstring = await response.Content.ReadAsStringAsync();
                            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(jsonstring);
                        }
                    }
                }
                catch (HttpRequestException e)
                {
                    return default(T);
                    throw new Exception(e.Message);
                }
                return default(T);
            }
            else
            {
                return default(T);
            }
        }
    }
}
