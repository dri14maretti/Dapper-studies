using System.Reflection.Metadata.Ecma335;

namespace eCommerce.API.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string Rg { get; set; }
        public string Cpf { get; set; }
        public string MotherName{ get; set; }
        public string RegistrationSituation { get; set; }
        public DateTimeOffset RegistrationDate { get; set; }
        public Contact? Contact { get; set; }
        public ICollection<Address>? DeliveryAddresses { get; set; }
        public ICollection<Departament>? Departaments { get; set; }
    }
}
