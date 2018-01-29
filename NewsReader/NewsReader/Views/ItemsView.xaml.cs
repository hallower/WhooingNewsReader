using System;
using System.Diagnostics;
using System.Linq;
using NewsReader.Helpers;
using NewsReader.Models;
using NewsReader.ViewModels;

using Xamarin.Forms;

namespace NewsReader.Views
{
    public class ItemSelectedEventArgs : EventArgs
    {
        public Item Selected { get; set; }
    }

    public partial class ItemsView : ContentView
    {
        public EventHandler<ItemSelectedEventArgs> ItemSelected;

        public ItemsView(ItemsViewModel vm)
        {
            BindingContext = vm;

            InitializeComponent();

            ItemsListView.ItemAppearing += (sender, e) =>
            {
                if (ItemsListView.IsRefreshing)
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

        void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as Item;
            if (item == null)
                return;

            ItemSelected.Invoke(this, new ItemSelectedEventArgs
            {
                Selected = item,
            });

            ItemsListView.SelectedItem = null;
        }


    }
}
