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
        public ObservableRangeCollection<Item> Items { get; set; } = new ObservableRangeCollection<Item>();
        public Command LoadItemsCommand { get; set; }
        public Command LoadNewItemsCommand { get; set; }
        public Command LoadOldItemsCommand { get; set; }

        public Item LastItem
        {
            get
            {
                if(Items.Count == 0)
                {
                    return new Item();
                }

                return Items[Items.Count - 1];
            }
        }

        public ItemsViewModel()
        {
            Title = "후잉 퍼온 뉴스";
            LoadItemsCommand = new Command(async () => await ExecuteLoadNewItemsCommand(true));
            LoadNewItemsCommand = new Command(async () => await ExecuteLoadNewItemsCommand());
            LoadOldItemsCommand = new Command(async () => await ExecuteLoadOldItemsCommand());
        }

        async Task ExecuteLoadNewItemsCommand(bool isFirst = false)
        {
            Debug.WriteLine("Get New items");
            if (IsBusy)
            {
                return;
            }
            
            IsBusy = true;

            try
            {
                if (isFirst)
                {
                    Debug.WriteLine(">>> Get New items, First");
                    var items = await DataStore.GetItemsAsync();
                    Items.AddRange(items);
                }
                else
                {
                    Debug.WriteLine(">>> Get New items, Newer");
                    var items = await DataStore.GetLatestItemsAsync();
                    Items.AddRange(items);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                MessagingCenter.Send<Object, MessagingCenterAlert>(this, "alert", new MessagingCenterAlert
                {
                    Title = "헐",
                    Message = "후잉에서 퍼온 뉴스를 가져 올 수 없네요. \n 혹시 인터넷 연결에 문제가 있는지 확인해 주세요.",
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
            Debug.WriteLine("Get Old items");
            if (IsBusy)
            {
                return;
            }

            if (Items.Count > DataStore.MaxItemsCount)
            {
                MessagingCenter.Send<Object, MessagingCenterAlert>(this, "alert", new MessagingCenterAlert
                {
                    Title = "잠시만요",
                    Message = "충분히 예전 뉴스들을 많이 보셨네요. \n 더 보시려면 위 whooing.com에서 봐주세요. ^^;;;;",
                    Cancel = "OK"
                });
                Debug.WriteLine("Too much Old items");
                return;
            }

            Debug.WriteLine(">>>Get Old items");
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