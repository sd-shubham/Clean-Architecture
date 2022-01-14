using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace App.Persistence.Converter
{
    internal class DateOnlyConverter : ValueConverter<DateOnly, DateTime>
    {
        public DateOnlyConverter() : base(
            d => d.ToDateTime(TimeOnly.MinValue), // min val set to mid night
            dt => DateOnly.FromDateTime(dt))
        { }
    }
}
