using System;
using System.Threading.Tasks;
using UnitTestingXF.Interfaces;

namespace UnitTestingXF.Services
{
    public class FakeCustomerApi : ICustomerApi
    {
		private const string _correctUsername = "test@test.com";
		private const string _correctPassword = "test1234!";

		private const string _wrongUsername = "test@test.co";
		private const string _wrongPassword = "test1235!";

        public async Task<bool> LoginAsync(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentNullException(nameof(username));
            
			if (string.IsNullOrWhiteSpace(password))
				throw new ArgumentNullException(nameof(password));
            
            if (username.ToLower().Equals(_correctUsername) && password.Equals(_correctPassword))
                return await Task.FromResult(true);

            throw new Exception("Error logging in");
        }
    }
}
