using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTestingXF.Helpers;

namespace UnitTestingXF.Tests.HelpersTests.Validators
{
    [TestFixture]
    public class EmailValidatorTests
    {
        // Minimum eight characters, at least one letter, one number and one special character
        string[] incorrectPasswords = new string[] {
                        "",
                        "plainaddress",
                        "12345678",
                        "test123",
                        "t3st1ng"
            };

        string[] correctPasswords = new string[] {
                "@test1256$",
                "test123!",
                "t3st1ng!",
            };

        [Test]
        public void PasswordCannotBeNullTest()
        {
            Assert.IsFalse(PasswordValidator.IsValidPassword(null));
        }

        [Test]
        public void IncorrectPasswordTests()
        {
            foreach (var password in incorrectPasswords)
            {
                Assert.IsFalse(PasswordValidator.IsValidPassword(password), $"Password {password} should be invalid");
            }
        }

        [Test]
        public void CorrectPasswordTests()
        {
            foreach (var password in correctPasswords)
            {
                Assert.IsTrue(PasswordValidator.IsValidPassword(password), $"Password {password} should be valid");
            }
        }
    }
}
