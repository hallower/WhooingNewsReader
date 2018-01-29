using NewsReader.Views;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace NewsReader
{
    public partial class App : Application, ISystemConfiguration
    {
        public App(AppOrientation orientation)
        {
            InitializeComponent();

            SetMainPage(orientation);
        }

        public static void SetMainPage(AppOrientation orientation)
        {
            switch (orientation)
            {
                case AppOrientation.Landscape:
                    Current.MainPage = new LandscapeMain();
                    break;
                case AppOrientation.Portrait:
                    Current.MainPage = new NavigationPage(new PortraitMain());
                    break;
            }
        }

        public void OnOrientationChanged(AppOrientation orientation)
        {
            SetMainPage(orientation);
        }
    }
}
