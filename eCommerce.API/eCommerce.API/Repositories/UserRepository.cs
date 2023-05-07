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
            var query = @"
                SELECT * FROM Users as U 
                    LEFT JOIN Contacts C ON U.Id = C.UserId 
                WHERE U.Id = @Id";

            return _connection.Query<User, Contact, User>(
                query,
                (user, contact) =>
                {
                    user.Contact = contact;
                    return user;
                },
                new { Id = id }
            ).SingleOrDefault();
        }
        public void Insert(User user)
        {
            _connection.Open();
            var transaction = _connection.BeginTransaction();
            try
            {
                var sql = @"INSERT INTO Users SET
                            (Name, Email, Gender, Rg, CPF, MotherName, RegistrationSituation, RegistrationDate)
                            VALUES
                            (@Name, @Email, @Gender, @Rg, @CPF, @MotherName, @RegistrationSituation, @RegistrationDate);
                        SELECT CAST(SCOPE_IDENTITY() AS INT);
                        ";

                user.Id = _connection.Query<int>(sql, user, transaction).Single();

                if (user.Contact == null) return;

                user.Contact.UserId = user.Id;

                var sqlContact = @"INSERT INTO Contacts SET
                                    (UserId, Phone, Cellphone)
                                    VALUES
                                    (@UserId, @Phone, @Cellphone)
                               SELECT CAST(SCOPE_IDENTITY() AS INT);";

                user.Contact.Id = _connection.Query<int>(sqlContact, user, transaction).Single();

                transaction.Commit();
            }

            catch (Exception ex)
            {
                transaction.Rollback();
            }

            finally { _connection.Close(); }


        }
        public void Update(User user)
        {
            _connection.Open();
            var transaction = _connection.BeginTransaction();
            try
            {
                var sql = @"UPDATE Users 
                        SET
                            Name = @Name, Email = @Email, Gender = @Gender, Rg = @Rg, CPF = @CPF, MotherName = @MotherName, RegistrationSituation = @RegistrationSituation, RegistrationDate = @RegistrationDate
                        WHERE Id = @Id";


                _connection.Execute(sql, user);

                if (user.Contact == null) return;
                
                var sqlContact = "UPDATE Contacts SET UsuarioId = @UsuarioId, Telefone = @Telefone, Celular = @Celular WHERE Id = @Id";
                _connection.Execute(sqlContact, user.Contact);

                transaction.Commit();
            }
            catch(Exception ex) { transaction.Rollback(); }
            finally
            {
                _connection.Close();
            }

        }
        public void Delete(int id)
        {
            var sql = @"DELETE FROM User
                            WHERE Id = @id";

            _connection.Execute(sql, id);
        }


    }
}
