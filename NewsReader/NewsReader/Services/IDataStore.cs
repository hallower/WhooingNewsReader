﻿using System.Collections.Generic;
using System.Threading.Tasks;
using NewsReader.Models;

namespace NewsReader.Services
{
    public interface IDataStore<T>
    {
        int MaxItemsCount
        {
            get;
        }

        Task<bool> AddItemAsync(T item);
        Task<bool> UpdateItemAsync(T item);
        Task<bool> DeleteItemAsync(T item);
        Task<T> GetItemAsync(string id);
        Task<IEnumerable<T>> GetItemsAsync();

        Task InitializeAsync();
        Task<IEnumerable<Item>> GetLatestItemsAsync();
        Task<IEnumerable<Item>> GetOldItemsAsync();
    }
}
