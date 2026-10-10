using Microsoft.EntityFrameworkCore;

namespace Data.Context;

public class ApDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
}