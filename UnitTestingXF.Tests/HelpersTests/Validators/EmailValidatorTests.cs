using System;
using NUnit.Framework;
using UnitTestingXF.Helpers;

namespace UnitTestingXF.Tests.HelpersTests
{
    [TestFixture]
    public class EmailValidatorTests
    {
        string[] IncorrectAddresses = new string[] {
                        "",
                        "plainaddress",
                        "#@%^%#$@#$@#.com",
                        "@example.com",
                        "test@test",
                        "test@test.n",
                        "@test.n",
                        "a@a.a",
                        "Joe Smith <email @example.com>",
                        "email.example.com",
                        "email@example @example.com",
                        ".email @example.com",
                        "email.@example.com",
                        "email..email @example.com",
                        "あいうえお@example.com",
                        "email@example.com (Joe Smith)",
                        "email @example",
                        "email@-example.com",
                        "email @example..com",
                        "Abc..123@example.com"
            };

        string[] CorrectAddresses = new string[] {
                "email@subdomain.example.com",
                "1234567890@example.com",
                "email@example-one.com",
                "email@example.name",
                "email@example.museum",
                "email@example.co.jp",
                "email@example.web",
            };

        [Test]
        public void EmailCannotBeNullTest()
        {
            Assert.IsFalse(EmailValidator.IsValidEmail(null));
        }

        [Test]
        public void IncorrectEmailCheckTests()
        {
            foreach (var email in IncorrectAddresses)
            {
                Assert.IsFalse(EmailValidator.IsValidEmail(email), $"Email {email} should be invalid");
            }
        }

        [Test]
        public void CorrectEmailValidationTests()
        {
            foreach (var email in CorrectAddresses)
            {
                Assert.IsTrue(EmailValidator.IsValidEmail(email), $"Email {email} should be valid");
            }
        }
    }
}
