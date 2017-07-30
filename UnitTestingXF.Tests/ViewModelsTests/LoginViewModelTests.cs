﻿using System;
using System.Net.Http;
using Moq;
using NUnit.Framework;
using UnitTestingXF.Interfaces;
using UnitTestingXF.Tests.Helpers;
using UnitTestingXF.ViewModels;
using UnitTestingXF.Views;

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
            var navigation = new NavigationStub();
            var vm = new LoginViewModel(navigation, _dependencyService);
            Assert.AreEqual(typeof(LoginViewModel), vm.GetType());
        }

        [Test]
        public void CannotLoginWithEmptyFormTest()
        {
            var navigation = new NavigationStub();
            var vm = new LoginViewModel(navigation, _dependencyService);
            Assert.IsFalse(vm.LoginCommand.CanExecute(null));
        }

        [Test]
        public void CanLoginWithValidFormTest()
        {
            var navigation = new NavigationStub();
            var vm = new LoginViewModel(navigation, _dependencyService);
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
            var navigation = new NavigationStub();
            var vm = new LoginViewModel(navigation, _dependencyService);
            vm.Username = _correctUsername;
            vm.Password = _correctPassword;

            Assert.IsTrue(vm.LoginCommand.CanExecute(null));
            vm.LoginCommand.Execute(null);

            Assert.AreEqual(typeof(HomeView), navigation.CurrentPage.GetType());
        }

		[Test]
		public void DoLoginWithIncorrectValuesTest()
		{
            var navigation = new NavigationStub();
			var vm = new LoginViewModel(navigation, _dependencyService);
            vm.Username = _wrongUsername;
            vm.Password = _wrongPassword;

			Assert.IsTrue(vm.LoginCommand.CanExecute(null));
			vm.LoginCommand.Execute(null);
            Assert.That(vm.ErrorMessage, Is.EqualTo("Error logging in"));
            Assert.That(vm.HasErrorMessage, Is.True);
            Assert.IsNull(navigation.CurrentPage);

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

        [Test]
        public void CanGoToRegisterViewTest()
        {
            var navigation = new NavigationStub();
            var vm = new LoginViewModel(navigation, _dependencyService);

            Assert.That(vm.RegisterAccountCommand.CanExecute(null), Is.True);
            vm.RegisterAccountCommand.Execute(null);

            Assert.AreEqual(typeof(RegisterView), navigation.CurrentModalPage.GetType());
            Assert.IsNull(navigation.CurrentPage);
        }

        [Test]
        public void CanGoToForgotPasswordViewTest()
        {
            var navigation = new NavigationStub();
            var vm = new LoginViewModel(navigation, _dependencyService);

            Assert.That(vm.ForgotPasswordCommand.CanExecute(null), Is.True);
            vm.ForgotPasswordCommand.Execute(null);

            Assert.AreEqual(typeof(ForgotPasswordView), navigation.CurrentModalPage.GetType());
            Assert.IsNull(navigation.CurrentPage);
        }
    }
}
