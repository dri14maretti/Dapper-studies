namespace eCommerce.API.Models
{
    public class Contact
    {
        public int Id{ get; set; }
        public int UserId { get; set; }
        public string PhoneNumber{ get; set; }
        public string Cellphone{ get; set; }

        public User User{ get; set; }
    }
}
