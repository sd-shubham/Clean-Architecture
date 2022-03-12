using App.Domain.Common;
using App.Domain.Enities;

namespace App.Domain.Events
{
    public class UserActionCompleteEvent: DomainEvent
    {
        public UserActionCompleteEvent(User user) => User = user;
        public User User { get; }
    }
}
