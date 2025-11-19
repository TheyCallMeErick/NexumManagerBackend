using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Tests.Utils;

public static class CreateInMemoryDatabase
{
    public static ApplicationDbContext Handle()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        var context = new ApplicationDbContext(options);
        //context.Database.Migrate();
        return context;
    }
}
