using Microsoft.EntityFrameworkCore;

namespace Project.Models
{
    public class ToDosContext : DbContext
    {
        public ToDosContext(DbContextOptions<ToDosContext> options) : base(options) { }

        public DbSet<ToDos> ToDosItems { get; set; }
    }
}