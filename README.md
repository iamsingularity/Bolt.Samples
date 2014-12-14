### Bolt.Samples
The repository containing samples of Bolt based services.

#### BoltContactList
<https://github.com/justkao/Bolt.Samples/tree/master/BoltContactList>

Example of simple async based service. The bolt is used as communication framework between WPF based client 
and server running on Katana plaform that is using Entity framework as database backend. 

The service contract is defined as:

```
    public interface IContactListProvider
    {
        Task<List<Contact>> GetContactsAsync(CancellationToken cancellation);

        Task<Contact> AddContactAsync(Contact contact, CancellationToken cancellation);

        Task DeleteContactAsync(int contactId, CancellationToken cancellation);

        [AsyncOperation]
        bool DoesContactExist(int contactId, CancellationToken cancellation);
    }
```

The client and server code is automaticaly generated by Bolt framework. We can see that Bolt fully supports async operations with cancellation support. 
Marking synchronous method with `AsyncOperation` attribute will generate asynchronous version of such method.
