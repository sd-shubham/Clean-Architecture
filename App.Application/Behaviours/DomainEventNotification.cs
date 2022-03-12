using App.Domain.Common;
using MediatR;

namespace App.Application.Behaviours
{
    public class DomainEventNotification<TEvent>: INotification where TEvent : DomainEvent
    {
        public DomainEventNotification(TEvent domainEvent)
        {
             DomainEvent = domainEvent;
        }
        public TEvent DomainEvent { get; set; }
    }
}
