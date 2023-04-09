namespace App.Application.Extensions;

internal static class HelperExtension
{
    // string exension
    public static int ValueOrDefault(this string @this, int defaultValue) =>
        string.IsNullOrWhiteSpace(@this) || !int.TryParse(@this, out var result) ? defaultValue : result;
    public static string ValueOrDefault(this string @this, string defaultValue) =>
        string.IsNullOrWhiteSpace(@this) ? defaultValue : @this;

    // IEnumerable extension
    public static IEnumerable<IEnumerable<string>> Parser(this string input,
                                                           string lineSplit,
                                                           string fieldSplit) =>
        input.Split(new[] { lineSplit }, StringSplitOptions.RemoveEmptyEntries)
             .Select(x => x.Split(new[] { fieldSplit }, StringSplitOptions.RemoveEmptyEntries));

    public static IEnumerable<TSource> EmptyIfNull<TSource>(this IEnumerable<TSource> sources) =>
        sources ?? Enumerable.Empty<TSource>();
    public static bool IsNullOrEmpty<TSource>(this IEnumerable<TSource> sources) =>
        sources?.Any() ?? false;

    public static IDictionary<TKey, TValue> ToDictionary<TKey, TValue>(this IEnumerable<(TKey, TValue)> @this)
        where TKey : notnull
        =>
        @this.ToDictionary(x => x.Item1, x => x.Item2)!; // check for null if req.

    public static IDictionary<TKey, TValue> ToDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> @this)
       where TKey : notnull
       =>
       @this.ToDictionary(x => x.Key, x => x.Value)!; // check for null if req.

    // map
    public static TOut Map<TIN, TOut>(this TIN @this, Func<TIN, TOut> fn)
        => fn(@this);

    // Dictionary

    public static Func<TKey,TValue>ToLookupWithDefault<TKey,TValue>(this IDictionary<TKey,TValue> @this ,TValue defaultValue)
        => x => @this.ContainsKey(x) ? @this[x] : defaultValue;


    public static Func<TKey, TValue> ToLookupWithDefault<TKey, TValue>(this IDictionary<TKey, TValue> @this)
       => x => @this.ContainsKey(x) ? @this[x] : default;
    public static bool Validate<T>(this T @this, params Func<T, bool>[] predicates)
        => predicates.All(x => x(@this));


}

