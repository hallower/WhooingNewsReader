using NewsReader.Helpers;
using NewsReader.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NewsReader.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PortraitMain : MainBase
    {
        ItemsViewModel vm;

        public PortraitMain()
        {
            InitializeComponent();

            BindingContext = vm = new ItemsViewModel();

            var itemsView = new ItemsView(vm);
            itemsView.ItemSelected += (async (sender, args) =>
            {
                await Navigation.PushAsync(new ContentPage
                {
                    Title = args.Selected.Subject,
                    Content = new ItemDetailView(new ItemDetailViewModel(args.Selected)),
                });
            });

            Content = itemsView;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            vm.LoadItemsCommand.Execute(true);
            Debug.WriteLine("No Contentss, Loading New");
        }
    }
}