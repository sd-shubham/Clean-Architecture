using App.Application.Behaviours;
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
    internal class UserActionCompletedEventHandler : INotificationHandler<DomainEventNotification<UserActionCompleteEvent>>
    {
        private readonly ILogger<UserActionCompletedEventHandler> _logger;

        public UserActionCompletedEventHandler(ILogger<UserActionCompletedEventHandler> logger)
        {
            _logger = logger;
        }
        public Task Handle(DomainEventNotification<UserActionCompleteEvent> notification, CancellationToken cancellationToken)
        {
            var userActionCompleteEvent = notification.DomainEvent;
            _logger.LogInformation("Domain Event: {DomainEvent}", userActionCompleteEvent.GetType().Name);
            return Task.CompletedTask;
        }
    }
}
