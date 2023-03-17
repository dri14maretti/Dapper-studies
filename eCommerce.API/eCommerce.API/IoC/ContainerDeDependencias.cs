﻿using eCommerce.API.Constantes;
using eCommerce.API.Repositories;
using System.Data;
using System.Data.SqlClient;

namespace eCommerce.API.IoC
{
    public static class ContainerDeDependencias
    {
        private static string? _conexaoSqlServer { get; set; }
        private static IConfiguration _configuration;

        /// <summary>
        /// Inicializa as propriedades necessárias para injeção de dependencia do negócio.
        /// </summary>
        /// <param name="configuration"></param>
        public static void InicializarPropriedadesDeInjecao(this IConfiguration configuration)
        {
            _configuration = configuration;
            _conexaoSqlServer = configuration.GetConnectionString("SqlServerConnection");
        }

        /// <summary>
        /// Faz a injeção de dependencia das classes e interfaces do negócio 
        /// e também da conexão com a base de dados.
        /// .: Necessário primeiro invocar 'InicializarVariavelDeInjecao' para inicializar as propriedades para a injeção. :.
        /// </summary> 
        /// <param name="services"></param>
        /// <exception cref="NotImplementedException"></exception>
        public static void AdicionarInjecaoNegocio(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();

            // Sql Server
            if (string.IsNullOrEmpty(_conexaoSqlServer))
                throw new NotImplementedException(Mensagens.InjetarPropriedades);

            services.AddSingleton<IConfiguration>(_configuration);

            services.AddTransient<IDbConnection>((sp) => new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=eCommerce;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False"););
            // PostGreSql
            //string postgreSqlConnectionString = configuration.GetConnectionString("PostgreSqlConnection");
            //services.AddTransient<IDbConnection>((sp) => new NpgsqlConnection(postgreSqlConnectionString));
        }
    }
