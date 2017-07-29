using System;
using System.Threading.Tasks;
using System.Windows.Input;
using MvvmHelpers;
using Xamarin.Forms;
using UnitTestingXF.Helpers;
using UnitTestingXF.Interfaces;
using UnitTestingXF.Services;
using UnitTestingXF.Views;

namespace UnitTestingXF.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private INavigation _navigation;
        private CustomerService _customerService;
        
		public LoginViewModel(INavigation navigation) : this(navigation, DependencyServiceWrapper.Instance)
        {
			// NO CODE HERE!
		}

		public LoginViewModel(INavigation navigation, IDependencyService dependencyService)
		{
            _navigation = navigation;
            _customerService = new CustomerService(dependencyService);
            LoginCommand = new Command(async () => await DoLoginAsync(), () => IsFormValid);
            ForgotPasswordCommand = new Command(async () => await _navigation.PushAsync(new ForgotPasswordView()));
            RegisterAccountCommand = new Command(async () => await _navigation.PushAsync(new RegisterView()));
        }

        public ICommand LoginCommand { get; private set; }
        public ICommand ForgotPasswordCommand { get; private set; }
        public ICommand RegisterAccountCommand { get; private set; }

        private string _username;
        public string Username
        {
            get { return _username; }
            set { SetProperty(ref _username, value); ErrorMessage = string.Empty; OnPropertyChanged("IsFormValid"); }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value); ErrorMessage = string.Empty; OnPropertyChanged("IsFormValid"); }
        }

        public bool HasErrorMessage 
        {
            get => !string.IsNullOrWhiteSpace(ErrorMessage);
        }

        private string _errorMessage;
        public string ErrorMessage
        {
            get { return _errorMessage; }
            set { SetProperty(ref _errorMessage, value); OnPropertyChanged("HasErrorMessage"); }
        }

        public bool IsFormValid
        {
            get => EmailValidator.IsValidEmail(Username) && PasswordValidator.IsValidPassword(Password);            
        }

		private async Task DoLoginAsync()
		{
            if(!IsBusy)
            {
                ErrorMessage = string.Empty;
                IsBusy = true;

                try
                {
					if (await _customerService.LoginAsync(Username, Password))
					{
                        await _navigation.PushAsync(new HomeView());
					}
                }
                catch (Exception ex)
                {
                    // TODO: Error handling
                    ErrorMessage = ex.Message;
                }

                IsBusy = false;
            }
		}
    }
}
