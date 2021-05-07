using System.Collections.Generic;
using Flunt.Validations;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Entities;

namespace PaymentContext.Domain.Entities
{
    public class Student: Entity
    {
        public Name Name { get; private set; }
        public Document Document { get; private set; }
        public Email Email { get; private set; }
        public Address Adrress { get; private set; }
        private List<Subscription> _subscriptions { get; set; }
        public IReadOnlyCollection<Subscription> Subscriptions { get { return _subscriptions.ToArray(); } }

        public Student(Name name, Document document, Email email)
        {
            this.Name = name;
            this.Document = document;
            this.Email = email;
            _subscriptions = new List<Subscription>();

            AddNotifications(name, document, email);
        }

        public void AddSubscription(Subscription subscription)
        {
            bool hasSubscriptionActive = false;
            foreach(var sub in _subscriptions) {

                if(sub.Active)
                    hasSubscriptionActive = true;
            }

            AddNotifications(new Contract()
                .Requires()
                .IsFalse(hasSubscriptionActive, "Student.Subscriptions", "Você já tem uma inscrição ativa")
                .AreEquals(0, subscription.Payments.Count, "Student.Subscription.Payments", "Essa assinatura não possui pagamentos")
            );

            //Alternativa
            // if(hasSubscriptionActive)
            //     AddNotification("Student.Subscriptions", "Você já tem uma inscrição ativa");
        }
    }
}