using WhooingNewsReader.Models;

namespace WhooingNewsReader.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public Item Item { get; set; }

        public DetailItem DetailItem { get; set; }

        public string Username
        {
            get
            {
                return DetailItem.Username;
            }
        }

        public string Image_url
        {
            get
            {
                return DetailItem.Image_url;
            }
        }

        public string Contents
        {
            get
            {
                return DetailItem.Contents;
            }
        }

        public ItemDetailViewModel(Item item = null)
        {
            Title = item.Subject;
            Item = item;
            DetailItem = new DetailItem();

            GetDetail();
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

            if(received == null)
            {
                return;
            }

            DetailItem = received;

            OnPropertyChanged("Username");
            OnPropertyChanged("Image_url");
            OnPropertyChanged("Contents");
        }
    }
}