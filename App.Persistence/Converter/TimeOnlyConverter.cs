using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace App.Persistence.Converter
{
    internal class TimeOnlyConverter : ValueConverter<TimeOnly, TimeSpan>
    {
        public TimeOnlyConverter() : base(
            t => t.ToTimeSpan(),
            ts => TimeOnly.FromTimeSpan(ts)
            )
        { }
    }
}
