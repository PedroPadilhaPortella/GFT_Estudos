using System;
using Flunt.Notifications;

namespace PaymentContext.Shared.Entities
{
    public abstract class Entity : Notifiable
    {
        public Guid Id { get; set; }
        protected Entity()
        {
            Id = Guid.NewGuid();
        }
    }
}