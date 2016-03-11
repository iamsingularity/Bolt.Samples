### Bolt.Samples
The repository containing samples of Bolt based services.

#### BoltContactList
<https://github.com/justkao/Bolt.Samples/tree/master/BoltContactList>

Example of simple Bolt based stateless service. EntityFramework 7 with in memory database 
is used as data store.

* **ContactList.UI** - WPF application using Bolt to communicate with server
* **ContactList.Console** - console application using Bolt to communicate with server
* **ContactList.Server** - ASP.NET based server using Bolt middleware 
* **ContactList.Contracts** - Interface that is describing the service

#####Contract interface
```c#
public interface IContactListProvider
{
    Task<List<Contact>> GetContactsAsync(CancellationToken cancellation);

    Task<Contact> AddContactAsync(Contact contact, CancellationToken cancellation);

    Task DeleteContactAsync(int contactId, CancellationToken cancellation);

    [AsyncOperation]
    bool DoesContactExist(int contactId, CancellationToken cancellation);
}
```

#### MemoService
<https://github.com/justkao/Bolt.Samples/tree/master/MemoService>

Bolt based service with in-memory session support.

* **MemoService.Client** - WPF application with multiple windows ( one window per session )
* **MemoService.Server** - ASP.NET based server using Bolt middleware 
```c#
public interface IMemoService
{
    [InitSession]
    void Login(string userName);

    void AddMemo(string memo);

    void RemoveMemo(string memo);

    List<string> GetAllMemos();

    [DestroySession]
    void Logoff();
}
``` 



