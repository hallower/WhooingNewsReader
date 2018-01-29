using System;
using NewsReader.Helpers;

namespace NewsReader.Models
{
    public class BaseDataObject : ObservableObject
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public DateTimeOffset CreatedAt { get; set; }

        public DateTimeOffset UpdatedAt { get; set; }        
    }
}
