using System;
using System.Collections.Generic;
using Flunt.Notifications;
using Flunt.Validations;
using Store.Domain.Commands.Interfaces;

namespace Store.Domain.Commands
{
    public class CreateOrderCommand: Notifiable, ICommand
    {
        public string Customer { get; set; }
        public string ZipCode { get; set; }
        public string PromoCode { get; set; }
        public IList<CreateOrderItemCommand> Items { get; set; }

        public CreateOrderCommand() {
            Items = new List<CreateOrderItemCommand>();
        }

        public CreateOrderCommand(string customer, string zipcode, string promoCode, IList<CreateOrderItemCommand> items) 
        {
            this.Customer = customer;
            this.ZipCode = zipcode;
            this.PromoCode = promoCode;
            this.Items = items;
        }

        public void Validate()
        {
            AddNotifications(new Contract()
                .Requires()
                .HasLen(this.Customer, 11, "Customer", "Cliente Inválido")
                .HasLen(this.ZipCode, 8, "ZipCode", "CEP Inválido")
                .HasLen(this.PromoCode, 8, "PromoCode", "Promoção Inválida")
            );
        }
    }
}