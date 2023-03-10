using eCommerce.API.Models;

namespace eCommerce.API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private static List<User> _db = new List<User>()
        {
            new User() {Id = 1, Name = "Maretti", Email = "maretti@gmail.com"},
            new User() {Id = 2, Name = "Teste", Email = "teste@gmail.com"},
            new User() {Id = 2, Name = "Pedro Henrique", Email = "pedro@gmail.com"}
        };
        
        public List<User> Get()
        {
            return _db;
        }

        public User GetById(int id)
        {
            return _db.FirstOrDefault(x => x.Id == id);
        }
        public void Insert(User user)
        {
            var lastUser = _db.LastOrDefault();

            user.Id = lastUser == null ? 1 : lastUser.Id + 1;

            _db.Add(user);
        }
        public void Update(User user)
        {
            var userFound = _db.FirstOrDefault(x => x.Id == user.Id);

            _db.Remove(userFound);
            _db.Add(user);
        }
        public void Delete(int id)
        {
            _db.Remove(_db.FirstOrDefault(x => x.Id == id));
        }


    }
}
