using Flunt.Validations;
using PaymentContext.Shared.ValueObjects;

namespace PaymentContext.Domain.ValueObjects
{
    public class Name : ValueObject
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        
        public Name(string firstName, string lastName)
        {
            this.FirstName = firstName;
            this.LastName = lastName;

            // Ao invés de executar as validações usando if/else, que aumenta a complexidade ciclomática do código
            // devemos usar um Design por Contratos

            // if(string.IsNullOrEmpty(FirstName)) {
            //     AddNotification("Name.FirstName", "Nome Inválido");
            // }

            // if(string.IsNullOrEmpty(LastName) {
            //     AddNotification("Name.LastName", "Sobrenome Inválido");
            // }
            
            AddNotifications(new Contract()
                .Requires()
                .HasMinLen(FirstName, 3, "Name.FirstName", "Nome deve contem minimo de 3 caracteres")
                .HasMinLen(LastName, 3, "Name.LastName", "Sobrenome deve contem minimo de 3 caracteres")
                .HasMaxLen(FirstName, 40, "Name.FirstName", "Nome deve contem maximo de 40 caracteres")
                .HasMaxLen(LastName, 40, "Name.LastName", "Sobrenome deve contem maximo de 40 caracteres")
            );
        }

        public override string ToString() 
        {
            return $"{FirstName} {LastName}";
        }
    }
}