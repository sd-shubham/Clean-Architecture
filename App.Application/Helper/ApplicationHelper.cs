using App.Application.Enums;

namespace App.Application.Helper
{
    internal static class ApplicationHelper
    {

        public static IReadOnlyDictionary<Bank, String> Banks = new Dictionary<Bank, String>
        {
            [Bank.ICICI] = "Some bank code"
        };
        // set name according to requirment
        public static IReadOnlyDictionary<Currency, string> CurrencyNames = new Dictionary<Currency, string>
        {
            [Currency.USD] = "USD$",
            [Currency.INR] = "INR₹",
            [Currency.HKD] = "HK$",
            [Currency.AUD] = "AUD"
        };
    }
}
