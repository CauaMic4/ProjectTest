using Microsoft.EntityFrameworkCore;

namespace ProjectTest.Model.Context
{
    public class SqlContext : DbContext
    {

        public SqlContext()
        {
        }
        public SqlContext(DbContextOptions<SqlContext> options) : base(options)
        {
        }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Book> Book { get; set; }
    }
}
