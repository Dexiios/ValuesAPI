using Microsoft.EntityFrameworkCore;
using ValuesAPI.Models;

namespace ValuesAPI.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options) {}
        public DbSet<Values> Values {get; set;}
        public DbSet<Students> Students {get; set;}
        public DbSet<Students_Description> Students_Description {get; set;}
    }
}
