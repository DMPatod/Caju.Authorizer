using Caju.Authorizer.Infrastructure.DataPersistence.SQLServer;
using Microsoft.EntityFrameworkCore;

internal class Program
{
    private static int Main(string[] args)
    {
        try
        {
            Console.WriteLine("Authorizer Tool Runner Starting.");

            var optionsBuilder = new DbContextOptionsBuilder<SQLServerContext>();

            var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection");

            optionsBuilder.UseSqlServer(connectionString);

            using var context = new SQLServerContext(optionsBuilder.Options, null!);

            Console.WriteLine("Applying Migrations...");

            context.Database.Migrate();

            Console.WriteLine("Migrations Applied.");

            Environment.Exit(0);
            return 0;
        }
        catch
        {
            Environment.Exit(1);
            return 1;
        }
    }
}