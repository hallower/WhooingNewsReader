﻿using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NewsReader.Models
{
    public class NewsGetter
    {
        private static readonly NewsGetter instance = new NewsGetter();

        public static NewsGetter Instance
        {
            get
            {
                return instance;
            }
        }

        private NewsGetter()
        {

        }

        public async Task<ICollection<Item>> GetLatestNews(string baseID = "", int limit = 20)
        {
            String uri = String.Format(@"http://whooing.com/api/bbs/moneynews.json?limit={0}", limit);
            if (baseID.Length > 0)
            {
                uri += ("&since=" + baseID);
            }

            return await MakeNewsTransaction(uri);
        }

        public async Task<ICollection<Item>> GetOldNews(string baseID, int limit = 20)
        {
            if (baseID.Length == 0)
            {
                return new List<Item>();
            }

            String uri = $"http://whooing.com/api/bbs/moneynews.json?limit={ limit }&max={ baseID }";
            return await MakeNewsTransaction(uri);
        }

        private async Task<ICollection<Item>> MakeNewsTransaction(string uri)
        {
            List<Item> news = new List<Item>();
            var request = HttpWebRequest.Create(uri);
            request.ContentType = "application/xml";
            request.Method = "GET";

            Debug.WriteLine("REST, URI=" + uri);
            try
            {
                using (HttpWebResponse response = await request.GetResponseAsync() as HttpWebResponse)
                {
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        Debug.WriteLine($"Error fetching data. Server returned status code: {response.StatusCode}");
                        return news;
                    }

                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        var content = reader.ReadToEnd();
                        if (string.IsNullOrWhiteSpace(content))
                        {
                            Debug.WriteLine("Response contained empty body...");
                            return news;
                        }

                        var newsArray = JObject.Parse(content);

                        foreach (var result in newsArray["results"])
                        {
                            Item newsItem = result.ToObject<Item>();
                            newsItem.Icon = result["writer"]["image_url"].ToString();
                            news.Add(newsItem);
                        }

                        Debug.WriteLine($"Response Body: \r\n {content}");
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error occured, {e.Message}");
                news.Clear();
            }

            return news;
        }

        public async Task<DetailItem> GetNewsDetail(string newsID)
        {
            String uri = String.Format(@"http://whooing.com/api/bbs/moneynews/{0}.json", newsID);

            var request = HttpWebRequest.Create(uri);
            request.ContentType = "application/xml";
            request.Method = "GET";

            try
            {
                using (HttpWebResponse response = await request.GetResponseAsync() as HttpWebResponse)
                {
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        Debug.WriteLine($"Error fetching data. Server returned status code: {response.StatusCode}");
                        return null;
                    }

                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        var content = reader.ReadToEnd();
                        if (string.IsNullOrWhiteSpace(content))
                        {
                            Debug.WriteLine("Response contained empty body...");
                            return null;
                        }

                        var news = JObject.Parse(content);

                        //DetailItem newsItem = news.ToObject<DetailItem>();
                        DetailItem newsItem = new DetailItem();
                        newsItem.Username = news["results"]["writer"]["username"].ToString();
                        newsItem.Image_url = news["results"]["writer"]["image_url"].ToString();
                        newsItem.Subject = news["results"]["subject"].ToString();
                        newsItem.Content = news["results"]["contents"].ToString().Replace("\n", "<br>");

                        return newsItem;
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return null;
            }
        }


    }
}
