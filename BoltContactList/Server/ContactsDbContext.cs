using Service.Contracts;
using System.Data.Entity;

namespace Server
{
    public class ContactsDbContext : DbContext
    {
        public DbSet<Contact> Contacts { get; set; }
    }
}
