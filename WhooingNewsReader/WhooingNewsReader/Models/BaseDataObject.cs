using System;
using WhooingNewsReader.Helpers;

namespace WhooingNewsReader.Models
{
    public class BaseDataObject : ObservableObject
    {
        public BaseDataObject()
        {
            Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public DateTimeOffset UpdatedAt { get; set; }

        public string AzureVersion { get; set; }
    }
}
