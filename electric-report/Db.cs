using System.Data;
using MySql.Data.MySqlClient;

namespace electric_report;

public static class Db
{
    public static void ConfigureDb(this IServiceCollection services, string connectionString)
    {
        var connection = new MySqlConnection(connectionString);
        services.AddSingleton<IDbConnection>(connection);
    }
}
