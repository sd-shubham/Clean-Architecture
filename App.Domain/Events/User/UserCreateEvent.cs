using App.Domain.Common;
using App.Domain.Enities;

namespace App.Domain.Events
{
    public class UserCreateEvent: DomainEvent
    {
        public UserCreateEvent(User user) => User = user;
        public User User { get; }
    }
}
