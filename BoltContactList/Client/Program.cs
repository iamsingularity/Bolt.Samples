using Bolt;
using Bolt.Client;
using Bolt.Helpers;
using Server;
using Service.Contracts;
using System.Collections.Generic;
using System.Threading;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            ISerializer serializer = new JsonSerializer();
            ClientConfiguration configuration = new ClientConfiguration(serializer, new JsonExceptionSerializer(serializer));

            ContactListProviderProxy proxy = configuration.CreateProxy<ContactListProviderProxy>(ServerConstants.ServerUrl);

            List<Contact> result = proxy.GetContactsAsync(CancellationToken.None).Result;

            for (int i = 0; i < 10; i++)
            {
                proxy.AddContactAsync(new Contact() { Name = "Name_" + i, Surname = "Surname_" + i }, CancellationToken.None).Wait();

            }
        }
    }
}
