using App.Application.Behaviours;
using App.Domain.Enities;
using App.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Application.Services.Auth.Commonds.Register.EventHandler
{
    internal class UserCreatedEventHandler : INotificationHandler<DomainEventNotification<UserCreateEvent>>
    {
        private readonly ILogger<UserCreatedEventHandler> _logger;
        public UserCreatedEventHandler(ILogger<UserCreatedEventHandler> logger)
        {
            _logger = logger;
        }
        public Task Handle(DomainEventNotification<UserCreateEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;
            _logger.LogInformation("Domain Event: {DomainEvent}", domainEvent.GetType().Name);
            return Task.CompletedTask;
        }
    }
}
