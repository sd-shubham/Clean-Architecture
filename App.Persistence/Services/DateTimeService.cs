using App.Application.Interfaces;

namespace App.Persistence.Services
{
    internal class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.UtcNow; // change here for utc
    }
}
