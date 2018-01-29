using System;
using System.Diagnostics;
using System.Threading.Tasks;

using NewsReader.Helpers;
using NewsReader.Models;
using NewsReader.Views;

using Xamarin.Forms;

namespace NewsReader.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        public ObservableRangeCollection<Item> Items { get; set; }
        public Command LoadItemsCommand { get; set; }
        public Command LoadOldItemsCommand { get; set; }

        public Item LastItem
        {
            get
            {
                return Items[Items.Count - 1];
            }
        }

        public ItemsViewModel()
        {
            Title = "Whooing News Reader";
            Items = new ObservableRangeCollection<Item>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadNewItemsCommand());
            LoadOldItemsCommand = new Command(async () => await ExecuteLoadOldItemsCommand());
        }

        async Task ExecuteLoadNewItemsCommand()
        {
            if (IsBusy)
            {
                return;
            }

            Debug.WriteLine("Get New items");
            IsBusy = true;

            try
            {
                var items = await DataStore.GetLatestItemsAsync();
                Items.AddRange(items);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                MessagingCenter.Send<Object, MessagingCenterAlert>(this, "alert", new MessagingCenterAlert
                {
                    Title = "Error",
                    Message = "Unable to load news.",
                    Cancel = "OK"
                });
            }
            finally
            {
                IsBusy = false;
            }
        }

        async Task ExecuteLoadOldItemsCommand()
        {
            if (IsBusy)
            {
                return;
            }

            if (Items.Count > DataStore.MaxItemsCount)
            {
                MessagingCenter.Send<Object, MessagingCenterAlert>(this, "alert", new MessagingCenterAlert
                {
                    Title = "Oops",
                    Message = "Too much news loaded, this is enough :)",
                    Cancel = "OK"
                });
                Debug.WriteLine("Too much Old items");
                return;
            }

            Debug.WriteLine("Get Old items");
            IsBusy = true;

            try
            {
                var items = await DataStore.GetOldItemsAsync();
                Items.AddRange(items);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                MessagingCenter.Send(new MessagingCenterAlert
                {
                    Title = "Error",
                    Message = "Unable to load items.",
                    Cancel = "OK"
                }, "message");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}