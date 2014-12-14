using Service.Contracts;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading;
using System.Threading.Tasks;

namespace Server
{
    /// <summary>
    /// Implementations of our service
    /// </summary>
    public class ContactListProvider : IContactListProvider
    {
        public async Task<List<Contact>> GetContactsAsync(CancellationToken cancellation)
        {
            using (ContactsDbContext ctxt = new ContactsDbContext())
            {
                List<Contact> result = await ctxt.Contacts.ToListAsync(cancellation);
                return result;
            }
        }

        public async Task<Contact> AddContactAsync(Contact contact, CancellationToken cancellation)
        {
            using (ContactsDbContext ctxt = new ContactsDbContext())
            {
                Contact result = ctxt.Contacts.Add(contact);
                await ctxt.SaveChangesAsync(cancellation);
                return result;
            }
        }

        public async Task DeleteContactAsync(int contactId, CancellationToken cancellation)
        {
            using (ContactsDbContext ctxt = new ContactsDbContext())
            {
                ctxt.Contacts.Remove(ctxt.Contacts.Attach(new Contact() { Id = contactId }));
                await ctxt.SaveChangesAsync(cancellation);
            }
        }

        public bool DoesContactExist(int contactId, CancellationToken cancellation)
        {
            using (ContactsDbContext ctxt = new ContactsDbContext())
            {
                return (ctxt.Contacts.Find(cancellation)) != null;
            }
        }
    }
}