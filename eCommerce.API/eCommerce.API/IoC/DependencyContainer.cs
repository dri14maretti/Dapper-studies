using eCommerce.API.Constantes;
using eCommerce.API.Repositories;
using System.Data;
using System.Data.SqlClient;

namespace eCommerce.API.IoC
{
    public static class DependencyContainer
    {
        private static string? _conexaoSqlServer { get; set; }
        private static IConfiguration _configuration;

        /// <summary>
        /// Inicializa as propriedades necessárias para injeção de dependencia do negócio.
        /// </summary>
        /// <param name="configuration"></param>
        public static void InitInjectionProperties(this IConfiguration configuration)
        {
            _configuration = configuration;
            _conexaoSqlServer = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=eCommerce;Integrated Security=True;Connect Timeout=30;Encrypt=False";
        }

        /// <summary>
        /// Faz a injeção de dependencia das classes e interfaces do negócio 
        /// e também da conexão com a base de dados.
        /// .: Necessário primeiro invocar 'InicializarVariavelDeInjecao' para inicializar as propriedades para a injeção. :.
        /// </summary> 
        /// <param name="services"></param>
        /// <exception cref="NotImplementedException"></exception>
        public static void AddBussinessInjection(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();

            // Sql Server
            if (string.IsNullOrEmpty(_conexaoSqlServer))
                throw new NotImplementedException(Messages.InjectProperties);

            services.AddSingleton<IConfiguration>(_configuration);

            services.AddTransient<IDbConnection>((sp) => new SqlConnection(_conexaoSqlServer));
            // PostGreSql
            //string postgreSqlConnectionString = configuration.GetConnectionString("PostgreSqlConnection");
            //services.AddTransient<IDbConnection>((sp) => new NpgsqlConnection(postgreSqlConnectionString));
        }
    }
}
