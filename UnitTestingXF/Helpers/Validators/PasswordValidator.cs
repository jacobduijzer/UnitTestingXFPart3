using System;
using System.Text.RegularExpressions;

namespace UnitTestingXF.Helpers
{
    public static class PasswordValidator
    {
        // Minimum eight characters, at least one letter, one number and one special character
        private const string passwordRegexString = @"^(?=.*[A-Za-z])(?=.*\d)(?=.*[$@$!%*#?&])[A-Za-z\d$@$!%*#?&]{8,}$";

        private static readonly Regex passwordRegex = new Regex(passwordRegexString, RegexOptions.Singleline, TimeSpan.FromMilliseconds(250));

        public static bool IsValidPassword(string password)
        {
            return !string.IsNullOrWhiteSpace(password) && passwordRegex.Match(password).Success;
        }
    }
}
