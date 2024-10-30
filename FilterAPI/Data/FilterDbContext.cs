using FilterAPI.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace FilterAPI.Data
{
    public class FilterDbContext : DbContext
    {
        public FilterDbContext(DbContextOptions<FilterDbContext> dbContextOptions)
            : base(dbContextOptions) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<SearchHistory> SearchHistory { get; set; }
    }
}
