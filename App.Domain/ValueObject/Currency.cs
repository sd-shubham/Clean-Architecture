namespace App.Domain.ValueObject
{
    public record Currency
    {
        public string CurrencyCode { get; init; }

        private static IReadOnlyDictionary<string, string> _lookup = new Dictionary<string, string>()
        {

            ["USD"] = ("USD"),
            ["EUR"] = ("EUR"),
            ["JPY"] = ("JPY"),
            ["GBP"] = ("GBP"),
            ["CAD"] = ("CAD"),
            ["AUD"] = ("AUD"),
            ["CHF"] = ("CHF"),
            ["NZD"] = ("NZD"),
            ["RUB"] = ("RUB"),
            ["HUF"] = ("HUF"),
        };
        private Currency() { }
        private Currency(string code)
        {
            if (!_lookup.TryGetValue(code.ToUpper(), out var record))
                throw new ArgumentException($"system doesn't support {code}");
            (CurrencyCode) = (code);
        }
        public static Currency From(string code)
        {
            if (!_lookup.TryGetValue(code.ToUpper(), out var record))
                throw new ArgumentException($"system doesn't support {code}");
            return new Currency(record);
        }
        public static bool IsValidCurrency(string code)
            => _lookup.ContainsKey(code);
        public static Currency Default => USD;
        public static Currency USD = new(_lookup["USD"]);
        public static Currency EUR = new(_lookup["EUR"]);
        public static Currency JPY = new(_lookup["JPY"]);
        public static Currency GBP = new(_lookup["GBP"]);
        public static Currency CAD = new(_lookup["CAD"]);
        public static Currency AUD = new(_lookup["AUD"]);
        public static Currency CHF = new(_lookup["CHF"]);
        public static Currency NZD = new(_lookup["NZD"]);
        public static Currency RUB = new(_lookup["RUB"]);
        public static Currency HUF = new(_lookup["HUF"]);
        public override string ToString()
            => CurrencyCode;

        public static implicit operator Currency(string code) => new(code);
        //private readonly struct CurrencyBase
        //{
        //    public readonly string Code;
        //    public readonly string Symbole;
        //    public CurrencyBase(string code, string symbole)
        //   => (Code, Symbole) = (code, symbole);
        //}
    }


}
