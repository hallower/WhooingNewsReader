using System;
using System.Diagnostics;
using System.Linq;
using NewsReader.Helpers;
using NewsReader.Models;
using NewsReader.ViewModels;

using Xamarin.Forms;

namespace NewsReader.Views
{
    public partial class ItemsPage : ContentPage
    {
        ItemsViewModel vm;

        public ItemsPage()
        {
            InitializeComponent();

            BindingContext = vm = new ItemsViewModel();

            MessagingCenter.Subscribe<Object, MessagingCenterAlert>(this, "alert", (sender, arg) =>
            {
                Debug.WriteLine("recv");
                if (arg is MessagingCenterAlert alert)
                {
                    DisplayAlert(alert.Title, alert.Message, "OK");
                }
            });

            ItemsListView.ItemAppearing += (sender, e) =>
            {
                if (ItemsListView.IsRefreshing ||
                vm.Items?.Count == 0)
                    return;

                if (e.Item is Item current)
                {
                    if (current.Bbs_id == vm.LastItem.Bbs_id)
                    {
                        Debug.WriteLine("Hit last item. Loading Old");
                        vm.LoadOldItemsCommand.Execute(null);
                    }
                }
            };
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as Item;
            if (item == null)
                return;

            await Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(item)));

            ItemsListView.SelectedItem = null;
        }

        void WhooingClicked(object sender, EventArgs e)
        {
            Device.OpenUri(new Uri("https://new.whooing.com"));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (vm.Items.Count == 0)
            {
                vm.LoadItemsCommand.Execute(null);
                Debug.WriteLine("No Contentss, Loading New");
            }

        }
    }
}
