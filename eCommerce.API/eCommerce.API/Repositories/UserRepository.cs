using Dapper;
using eCommerce.API.Models;
using System.Data;
using System.Data.SqlClient;

namespace eCommerce.API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private IDbConnection _connection;
        public UserRepository(IDbConnection connection)
        {
            _connection = connection;
        }
        private static List<User> _db = new List<User>()
        {
            new User() {Id = 1, Name = "Maretti", Email = "maretti@gmail.com"},
            new User() {Id = 2, Name = "Teste", Email = "teste@gmail.com"},
            new User() {Id = 2, Name = "Pedro Henrique", Email = "pedro@gmail.com"}
        };
        
        public List<User> Get()
        {
            var query = "SELECT * FROM Users";
            return _connection.Query<User>(query).ToList();
        }

        public User GetById(int id)
        {
            var query = "SELECT * FROM Users WHERE Id = @Id";

            return _connection.QuerySingleOrDefault<User>(query, new { Id = id });
        }
        public void Insert(User user)
        {
            var sql = @"INSERT INTO Users 
                            (Name, Email, Gender, Rg, CPF, MotherName, RegistrationSituation, RegistrationDate)
                            VALUES
                            (@Name, @Email, @Gender, @Rg, @CPF, @MotherName, @RegistrationSituation, @RegistrationDate);
                        SELECT CAST(SCOPE_IDENTITY() AS INT);
                        ";

            user.Id = _connection.Query<int>(sql, user).Single();
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
