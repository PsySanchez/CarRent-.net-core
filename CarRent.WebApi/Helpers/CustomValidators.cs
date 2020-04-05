using System;
using System.Net.Mail;


namespace CarRent.WebApi.Helpers
{
    public static class CustomValidators
    {
        public static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsValidDateOfBirth(DateTime dateOfBirth)
        {
            if (dateOfBirth > DateTime.UtcNow.AddYears(-18) || dateOfBirth < DateTime.UtcNow.AddYears(-100))
            {
                return false;
            }
            return true;
        }
    }
}
