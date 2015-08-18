using ContactList.Contracts;
using Microsoft.Data.Entity;

namespace ContactList.Server
{
    public class ContactsDbContext : DbContext
    {
        public DbSet<Contact> Contacts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase();
            base.OnConfiguring(optionsBuilder);
        }
    }
}
