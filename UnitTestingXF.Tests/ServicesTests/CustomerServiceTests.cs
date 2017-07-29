using System;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using UnitTestingXF.Interfaces;
using UnitTestingXF.Services;
using UnitTestingXF.Tests.Helpers;

namespace UnitTestingXF.Tests.ServicesTests
{
    [TestFixture]
    public class CustomerServiceTests
    {
		private const string _correctUsername = "test@test.co";
		private const string _correctPassword = "test1235!";

        private const string _wrongUsername = "test@test.com";
        private const string _wrongPassword = "test1234!";

        private IDependencyService _dependencyService;

        [OneTimeSetUp]
        public void Setup()
        {
            _dependencyService = new DependencyServiceStub();

            var customerApi = new Mock<ICustomerApi>();

            // TODO: Refit exception
            customerApi.Setup(x => x.LoginAsync(_wrongUsername, _wrongPassword)).ThrowsAsync(new Exception("error logging in"));
            customerApi.Setup(x => x.LoginAsync(_correctUsername, _correctPassword)).ReturnsAsync(true);

            _dependencyService.Register<ICustomerApi>(customerApi.Object);
        }

        [Test]
        public void ServiceCanConstruct()
        {
            var service = new CustomerService(_dependencyService);
            Assert.AreEqual(typeof(CustomerService), service.GetType());
        }

        [Test]
        public void CannotLoginWithoutUsernameAndPasswordTest()
        {
            var service = new CustomerService(_dependencyService);
			
            var ex = Assert.ThrowsAsync<ArgumentNullException>(async () => await service.LoginAsync("", ""));

			Assert.That(ex.Message, Is.EqualTo("Value cannot be null.\nParameter name: username"));
            Assert.That(ex.ParamName, Is.EqualTo("username"));

			var ex2 = Assert.ThrowsAsync<ArgumentNullException>(async () => await service.LoginAsync("test@test.com", ""));

			Assert.That(ex2.Message, Is.EqualTo("Value cannot be null.\nParameter name: password"));
			Assert.That(ex2.ParamName, Is.EqualTo("password"));

			var ex3 = Assert.ThrowsAsync<ArgumentNullException>(async () => await service.LoginAsync("", "test1234!"));

			Assert.That(ex3.Message, Is.EqualTo("Value cannot be null.\nParameter name: username"));
			Assert.That(ex3.ParamName, Is.EqualTo("username"));
		}

		[Test]
		public void CannotLoginWithWrongUsernameAndPasswordTest()
		{
			var service = new CustomerService(_dependencyService);

            // TODO: Refit exception
            var ex = Assert.ThrowsAsync<Exception>(async () => await service.LoginAsync(_wrongUsername, _wrongPassword));

            Assert.That(ex.Message, Is.EqualTo("error logging in"));
		}

		[Test]
		public async Task CanLoginWithUsernameAndPasswordTest()
		{
			var service = new CustomerService(_dependencyService);

            Assert.IsTrue(await service.LoginAsync(_correctUsername, _correctPassword));
		}
    }
}
