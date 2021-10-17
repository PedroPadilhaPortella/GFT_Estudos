using System;
using System.Collections.Generic;
using Flunt.Validations;
using PaymentContext.Shared.Entities;

namespace PaymentContext.Domain.Entities
{
    public class Subscription : Entity
    {
        public DateTime CreateDate { get; private set; }
        public DateTime LastUpdateDate { get; private set; }
        public DateTime? ExpireDate { get; private set; }
        public bool Active { get; private set; }
        private List<Payment> _payments;
        public IReadOnlyCollection<Payment> Payments { get { return _payments.ToArray(); } }

        public Subscription(DateTime? expireDate)
        {
            this.CreateDate = DateTime.Now;
            this.LastUpdateDate = DateTime.Now;
            this.Active = true;
            this.ExpireDate = expireDate;
        }

        public void AddPayment(Payment payment)
        {
            AddNotifications(new Contract()
                .Requires()
                .IsGreaterThan(DateTime.Now, payment.PaidDate, "Subscription.Payments", "A data do pagamento deve ser futura")
            );

            // if(Valid) só adiciona ser for válido
            _payments.Add(payment);
        }

        public void Activate()
        {
            this.Active = true;
            this.LastUpdateDate = DateTime.Now;
        }

        public void Deactivate()
        {
            this.Active = false;
            this.LastUpdateDate = DateTime.Now;
        }
    }
}