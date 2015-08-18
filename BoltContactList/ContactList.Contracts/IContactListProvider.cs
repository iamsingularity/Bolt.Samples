using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ContactList.Contracts
{
    public interface IContactListProvider
    {
        Task<List<Contact>> GetContactsAsync(CancellationToken cancellation);

        Task<Contact> AddContactAsync(Contact contact, CancellationToken cancellation);

        Task DeleteContactAsync(int contactId, CancellationToken cancellation);

        bool DoesContactExist(int contactId, CancellationToken cancellation);

        Task<bool> DoesContactExistAsync(int contactId, CancellationToken cancellation);
    }
}