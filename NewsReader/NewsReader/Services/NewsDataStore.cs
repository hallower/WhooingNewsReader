using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

using NewsReader.Models;

using Xamarin.Forms;

[assembly: Dependency(typeof(NewsReader.Services.NewsDataStore))]
namespace NewsReader.Services
{
    public class NewsDataStore : IDataStore<Item>
    {
        static readonly int maxNewsCount = 100;

        bool isInitialized;
        int oldNewsID = int.MaxValue;
        int lastestNewsID = int.MinValue;
        List<Item> items = new List<Item>();

        public int MaxItemsCount
        {
            get => maxNewsCount;
        }

        public async Task<bool> AddItemAsync(Item item)
        {
            await InitializeAsync();

            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Item item)
        {
            await InitializeAsync();

            var _item = items.Where((Item arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(_item);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(Item item)
        {
            await InitializeAsync();

            var _item = items.Where((Item arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(_item);

            return await Task.FromResult(true);
        }

        public async Task<Item> GetItemAsync(string id)
        {
            await InitializeAsync();

            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Item>> GetItemsAsync()
        {
            await GetLatestItemsAsync();
            return await Task.FromResult(items);
        }

        public async Task<IEnumerable<Item>> GetLatestItemsAsync()
        {
            if (lastestNewsID == int.MinValue)
            {
                return await GetLatestNews(String.Empty);
            }

            return await GetLatestNews(lastestNewsID.ToString());
        }

        public async Task<IEnumerable<Item>> GetOldItemsAsync()
        {
            if (items.Count() > MaxItemsCount)
            {
                return await Task.FromResult(items);
            }

            if (oldNewsID == int.MaxValue)
            {
                return await GetOldNews(String.Empty);
            }

            return await GetOldNews(oldNewsID.ToString());
        }

        private void Reset()
        {
            isInitialized = false;
            lastestNewsID = int.MinValue;
            oldNewsID = int.MaxValue;
            items.Clear();
        }

        public async Task InitializeAsync()
        {
            if (isInitialized)
                return;

            await GetLatestNews(String.Empty);

            isInitialized = true;
        }

        private async Task<IEnumerable<Item>> GetLatestNews(string lastID)
        {
            var news = await NewsGetter.Instance.GetLatestNews(lastID);
            SetNews(news);
            return news;
        }

        private async Task<IEnumerable<Item>> GetOldNews(string firstID)
        {
            var news = await NewsGetter.Instance.GetOldNews(firstID);
            SetNews(news);
            return news;
        }

        private void SetNews(IEnumerable<Item> news)
        {
            foreach (var newsItem in news)
            {
                Debug.WriteLine($"item {newsItem.Bbs_id}, {newsItem.Subject}");
                if (int.TryParse(newsItem.Bbs_id, out int newsID))
                {
                    if (newsID > lastestNewsID)
                    {
                        lastestNewsID = newsID;
                    }

                    if (newsID < oldNewsID)
                    {
                        oldNewsID = newsID;
                    }
                }

                items.Add(newsItem);
                items.Sort();
            }
        }
    }
}
