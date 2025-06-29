namespace NZWalks.API.IdentitySeeder
{
    public static class RoleSeeder
    {
        public static async Task SeedRolesAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
            var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
            var logger = loggerFactory.CreateLogger("RolesSeeder");

            try
            {
                string[] roles = new string[] { "Admin", "Read", "Write", "Support" };

                foreach (string role in roles)
                {
                    if (!await roleManager.RoleExistsAsync(role))
                    {
                        await roleManager.CreateAsync(new ApplicationRole()
                        {
                            Id = Guid.NewGuid(),
                            Name = role,
                            NormalizedName = role.ToUpper(),
                            ConcurrencyStamp = DateTime.UtcNow.Ticks.ToString()
                        });
                        logger.LogInformation("Roles seeded was succesfully!.");
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while seeding the roles.");
            }
        }
    }
}
