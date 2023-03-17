using System.Reflection.Metadata.Ecma335;

namespace eCommerce.API.Models
{
    public class Address
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public string AddressName { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string AddressDescription { get; set; }
        public string Complement { get; set; }

        public User User { get; set; }
    }
}
