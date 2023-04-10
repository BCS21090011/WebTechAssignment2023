namespace Web_tech.wwwroot
{
    public void ConfigureServices(IServiceCollection services)
    {
        // Add a MySQL database context using the connection string from appsettings.json
        services.AddDbContext<MyDbContext>(options =>
            options.UseMySQL(Configuration.GetConnectionString("MySqlConnection")));

        // Add other services and middleware as needed

        services.AddMySqlConnector();

        services.AddControllersWithViews();
    }
}
