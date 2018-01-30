using NewsReader.Models;
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
    public partial class LandscapeMain : MainBase
    {
        ItemsViewModel ivm;

        public LandscapeMain()
        {
            InitializeComponent();

            ivm = ItemsViewModel.Instance;
            var itemsView = new ItemsView(ivm);
            itemsView.ItemSelected += ((sender, args) =>
            {
                if (LandscapeGrid.Children.Count() > 1)
                {
                    LandscapeGrid.Children.RemoveAt(1);
                }

                var detailPage = new ItemDetailView(new ItemDetailViewModel(args.Selected));
                LandscapeGrid.Children.Add(detailPage, 2, 0);
            });

            LandscapeGrid.Children.Add(itemsView, 0, 0);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            ivm.LoadItemsCommand.Execute(true);
            Debug.WriteLine("No Contentss, Loading New");
        }
    }
}