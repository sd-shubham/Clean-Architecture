﻿namespace App.Domain.Common
{
    public interface IHasDomainEvent
    {
        public List<DomainEvent> DomainEvents { get; set; }
    }
    public abstract class DomainEvent
    {
        public bool IsPublished { get; set; }
        public DateTimeOffset DateOccurred { get; }= DateTimeOffset.UtcNow;
    }
}
