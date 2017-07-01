using System;

namespace WhooingNewsReader.Helpers
{
    public class MessagingCenterAlert
    {
        public static void Init()
        {
            var time = DateTime.UtcNow;
        }

        public string Title { get; set; }

        public string Message { get; set; }

        public string Cancel { get; set; }

        public Action OnCompleted { get; set; }
    }
}