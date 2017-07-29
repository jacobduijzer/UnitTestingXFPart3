using System;
using System.Threading.Tasks;
using UnitTestingXF.Helpers;
using UnitTestingXF.Interfaces;

namespace UnitTestingXF.Services
{
    public class CustomerService
    {
        private ICustomerApi _customerApi;

		public CustomerService() : this(DependencyServiceWrapper.Instance)
        {
			// NO CODE HERE!
		}

        public CustomerService(IDependencyService dependencyService)
        {
			_customerApi = dependencyService.Get<ICustomerApi>();
		}
		
		public async Task<bool> LoginAsync(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentNullException(nameof(username));

            if(string.IsNullOrWhiteSpace(password))
                throw new ArgumentNullException(nameof(password));
            
            return await _customerApi.LoginAsync(username, password).ConfigureAwait(false);
        }
    }
}
