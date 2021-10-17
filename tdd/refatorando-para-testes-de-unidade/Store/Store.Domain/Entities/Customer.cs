using System;

namespace Store.Domain.Entities
{
    public class Customer : Entity
    {
        public string Name { get; private set; }
        public string Email { get; private set; }
        public Customer(string name, string email)
        {
            this.Name = name;
            this.Email = email;
        }
    }
}