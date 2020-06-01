using CarRent.WebApi.Models;
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

        public static bool IsValidCustSearch(CustSearchView cust)
        {
            try
            {
                if (cust.Email != null)
                {
                    var addr = new MailAddress(cust.Email);
                    return true;
                }
                if (cust.PhoneNumber != null && cust.PhoneNumber.Length >= 10)
                {
                    var phoneNum = Convert.ToInt32(cust.PhoneNumber.Substring(0, 2));
                    if (phoneNum == 05)
                    {
                        return true;
                    }
                }
                return false;
            }
            catch
            {
                return false;
            }

        }
    }
}
