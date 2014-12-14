using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Bolt;

namespace Service.Contracts
{
    public interface IContactListProvider
    {
        Task<List<Contact>> GetContactsAsync(CancellationToken cancellation);

        Task<Contact> AddContactAsync(Contact contact, CancellationToken cancellation);

        Task DeleteContactAsync(int contactId, CancellationToken cancellation);

        [AsyncOperation]
        bool DoesContactExist(int contactId, CancellationToken cancellation);
    }
}