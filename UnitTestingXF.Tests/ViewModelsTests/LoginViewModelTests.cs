﻿using System;
using System.Net.Http;
using Moq;
using NUnit.Framework;
using UnitTestingXF.Interfaces;
using UnitTestingXF.Tests.Helpers;
using UnitTestingXF.ViewModels;

namespace UnitTestingXF.Tests.ViewModelsTests
{
    [TestFixture]
    public class LoginViewModelTests
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

			customerApi.Setup(x => x.LoginAsync(_wrongUsername, _wrongPassword)).ThrowsAsync(new Exception("Error logging in"));
			customerApi.Setup(x => x.LoginAsync(_correctUsername, _correctPassword)).ReturnsAsync(true);

			_dependencyService.Register<ICustomerApi>(customerApi.Object);
		}

        [Test]
        public void VMCanConstruct()
        {
            var vm = new LoginViewModel(_dependencyService);
            Assert.AreEqual(typeof(LoginViewModel), vm.GetType());
        }

        [Test]
        public void CannotLoginWithEmptyFormTest()
        {
            var vm = new LoginViewModel(_dependencyService);
            Assert.IsFalse(vm.LoginCommand.CanExecute(null));
        }

        [Test]
        public void CanLoginWithValidFormTest()
        {
            var vm = new LoginViewModel(_dependencyService);
            Assert.IsFalse(vm.IsFormValid);
            Assert.IsFalse(vm.LoginCommand.CanExecute(null));

            vm.Username = "testuser";
            Assert.IsFalse(vm.IsFormValid);
            Assert.IsFalse(vm.LoginCommand.CanExecute(null));

            vm.Password = "testpassword";
            Assert.IsFalse(vm.IsFormValid);
            Assert.IsFalse(vm.LoginCommand.CanExecute(null));

            vm.Username = string.Empty;
            Assert.IsFalse(vm.IsFormValid);
            Assert.IsFalse(vm.LoginCommand.CanExecute(null));

            vm.Username = "     ";
            Assert.IsFalse(vm.IsFormValid);
            Assert.IsFalse(vm.LoginCommand.CanExecute(null));

            vm.Password = "      ";
            Assert.IsFalse(vm.IsFormValid);
            Assert.IsFalse(vm.LoginCommand.CanExecute(null));

            vm.Username = "test@test.com";
            vm.Password = "test123!";
            Assert.IsTrue(vm.IsFormValid);
            Assert.IsTrue(vm.LoginCommand.CanExecute(null));
        }

        [Test]
        public void DoLoginWithCorrectValuesTest()
        {
            var vm = new LoginViewModel(_dependencyService);
            vm.Username = _correctUsername;
            vm.Password = _correctPassword;

            Assert.IsTrue(vm.LoginCommand.CanExecute(null));
            vm.LoginCommand.Execute(null);
        }

		[Test]
		public void DoLoginWithIncorrectValuesTest()
		{
			var vm = new LoginViewModel(_dependencyService);
            vm.Username = _wrongUsername;
            vm.Password = _wrongPassword;

			Assert.IsTrue(vm.LoginCommand.CanExecute(null));
			vm.LoginCommand.Execute(null);
            Assert.That(vm.ErrorMessage, Is.EqualTo("Error logging in"));
            Assert.That(vm.HasErrorMessage, Is.True);

            vm.Username = string.Empty;

            Assert.That(vm.ErrorMessage, Is.Empty);
            Assert.That(vm.HasErrorMessage, Is.False);

			vm.Username = _wrongUsername;
			vm.Password = _wrongPassword;

			Assert.IsTrue(vm.LoginCommand.CanExecute(null));
			vm.LoginCommand.Execute(null);
			Assert.That(vm.ErrorMessage, Is.EqualTo("Error logging in"));
			Assert.That(vm.HasErrorMessage, Is.True);

            vm.Password = string.Empty;

			Assert.That(vm.ErrorMessage, Is.Empty);
			Assert.That(vm.HasErrorMessage, Is.False);
		}
    }
}
