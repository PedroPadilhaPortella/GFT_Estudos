using System;

namespace Store.Domain.Entities
{
    public class Discount : Entity
    {
        public decimal Amount { get; private set; }
        public DateTime ExpireDate { get; private set; }

        public Discount(decimal amount, DateTime expireDate)
        {
            this.Amount = amount;
            this.ExpireDate = expireDate;
        }

        public bool isValid()
        {
            return DateTime.Compare(DateTime.Now, this.ExpireDate) < 0;
        }

        public decimal Value ()
        {
            if(isValid())
                return this.Amount;
            else
                return 0;
        }
    }
}