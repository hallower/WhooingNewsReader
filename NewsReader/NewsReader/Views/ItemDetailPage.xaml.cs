
using NewsReader.ViewModels;

using Xamarin.Forms;

namespace NewsReader.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        ItemDetailViewModel vm;

        public ItemDetailPage()
        {
            InitializeComponent();
        }

        public ItemDetailPage(ItemDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.vm = viewModel;
        }
    }
}
