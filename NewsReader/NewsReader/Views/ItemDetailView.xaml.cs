
using NewsReader.ViewModels;

using Xamarin.Forms;

namespace NewsReader.Views
{
    public partial class ItemDetailView : ContentView
    {
        public ItemDetailView(ItemDetailViewModel viewModel)
        {
            BindingContext = viewModel;
            InitializeComponent();
        }
    }
}
