using System.Net.Http;
using System.Threading;
using Bolt;
using Bolt.Client;
using Bolt.Client.Pipeline;
using Bolt.Client.Proxy;
using ContactList.Contracts;

namespace ContactList.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            ClientConfiguration configuration = new ClientConfiguration().UseDynamicProxy();
            IContactListProvider proxy = configuration.CreateProxy<IContactListProvider>("http://localhost:5000");
            proxy.GetContactsAsync(CancellationToken.None).GetAwaiter().GetResult();

            for (int i = 0; i < 10; i++)
            {
                proxy.AddContactAsync(new Contact() {Name = "Name_" + i, Surname = "Surname_" + i},  CancellationToken.None).GetAwaiter().GetResult();
                System.Console.WriteLine("Added new contact ... ");
            }

            System.Console.WriteLine("Contacts: {0}", proxy.GetContactsAsync(CancellationToken.None).GetAwaiter().GetResult().Count);
        }
    }
}
