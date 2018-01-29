using System.Diagnostics;
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
            Item = item;
            DetailItem = new DetailItem();

            GetDetail();

            OpenNewsCommand = new Command(() =>
            {
                var content = DetailItem.Content;
                int lastPos = content.IndexOf("</a>");
                int firstPos = content.LastIndexOf(">", lastPos);

                Debug.WriteLine(content.Substring(firstPos + 1, lastPos - firstPos - 1));
                Device.OpenUri(new System.Uri(content.Substring(firstPos + 1, lastPos - firstPos - 1)));
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