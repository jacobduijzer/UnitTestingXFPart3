using UnitTestingXF.Interfaces;
using UnitTestingXF.Services;
using UnitTestingXF.Views;
using Xamarin.Forms;

namespace UnitTestingXF
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            DependencyService.Register<ICustomerApi, FakeCustomerApi>();

            MainPage = new LoginView();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
