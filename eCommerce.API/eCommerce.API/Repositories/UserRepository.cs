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
            var sql = @"UPDATE Users 
                        SET
                            Name = @Name, Email = @Email, Gender = @Gender, Rg = @Rg, CPF = @CPF, MotherName = @MotherName, RegistrationSituation = @RegistrationSituation, RegistrationDate = @RegistrationDate
                        WHERE Id = @Id";

            _connection.Execute(sql, user);
        }
        public void Delete(int id)
        {
            var sql = @"DELETE FROM User
                            WHERE Id = @id";

            _connection.Execute(sql, id);
        }


    }
}
