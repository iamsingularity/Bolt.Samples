using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ContactList.Contracts;
using Microsoft.Data.Entity;

namespace ContactList.Server
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
                Contact result = ctxt.Contacts.Add(contact).Entity;
                await ctxt.SaveChangesAsync(cancellation);
                return result;
            }
        }

        public async Task DeleteContactAsync(int contactId, CancellationToken cancellation)
        {
            using (ContactsDbContext ctxt = new ContactsDbContext())
            {
                ctxt.Contacts.Remove(ctxt.Contacts.Attach(new Contact() { Id = contactId }).Entity);
                await ctxt.SaveChangesAsync(cancellation);
            }
        }

        public bool DoesContactExist(int contactId, CancellationToken cancellation)
        {
            using (ContactsDbContext ctxt = new ContactsDbContext())
            {
                return (ctxt.Contacts.FirstOrDefault(c=>c.Id == contactId)) != null;
            }
        }

        public async Task<bool> DoesContactExistAsync(int contactId, CancellationToken cancellation)
        {
            using (ContactsDbContext ctxt = new ContactsDbContext())
            {
                return (await ctxt.Contacts.FirstOrDefaultAsync(c => c.Id == contactId, cancellation)) != null;
            }
        }
    }
}