using UnitTestingXF.ViewModels;
using Xamarin.Forms;

namespace UnitTestingXF.Views
{
    public partial class LoginView : ContentPage
    {
        private LoginViewModel vm;

        public LoginView()
        {
            InitializeComponent();

            BindingContext = vm = new LoginViewModel();
        }
    }
}