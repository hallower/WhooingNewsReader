using System;
using System.Diagnostics;
using System.Web;
using System.Windows.Input;
using NewsReader.Models;
using Xamarin.Forms;

namespace NewsReader.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public Item Item { get; set; }

        public DetailItem DetailItem { get; set; }

        public string Username
        {
            get => DetailItem.Username;
        }

        public string Image_url
        {
            get => DetailItem.Image_url;
        }

        public string Content
        {
            get
            {
                Debug.WriteLine("Contents:" + DetailItem.Content);
                return DetailItem.Content;
            }
        }

        public ICommand OpenNewsCommand { protected set; get; }

        public ItemDetailViewModel(Item item = null)
        {
            Title = item.Subject;
            Item = item ?? new Item();
            DetailItem = new DetailItem();

            GetDetail();

            OpenNewsCommand = new Command(() =>
            {
                var content = DetailItem.Content;
                int lastPos = content.IndexOf("</a>");
                if (lastPos == -1)
                {
                    return;
                }

                int firstPos = content.LastIndexOf(">", lastPos);

                var url = HttpUtility.HtmlDecode(content.Substring(firstPos + 1, lastPos - firstPos - 1));
                Debug.WriteLine("url1, " + url);
                Device.OpenUri(new System.Uri(url));
            });

        }

        int quantity = 1;
        public int Quantity
        {
            get { return quantity; }
            set { SetProperty(ref quantity, value); }
        }

        public async void GetDetail()
        {
            if (Item.Bbs_id == string.Empty)
            {
                return;
            }

            DetailItem received = await NewsGetter.Instance.GetNewsDetail(Item.Bbs_id);

            if (received == null)
            {
                return;
            }

            DetailItem = received;

            OnPropertyChanged("Username");
            OnPropertyChanged("Image_url");
            OnPropertyChanged("Content");
        }
    }
}