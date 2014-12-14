//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.

//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

using Service.Contracts;
using Service.Contracts.Parameters;


namespace Service.Contracts.Parameters
{
    [DataContract]
    internal partial class AddContactAsyncParameters
    {
        [DataMember(Order = 1)]
        public Contact Contact { get; set; }
    }

    [DataContract]
    internal partial class DeleteContactAsyncParameters
    {
        [DataMember(Order = 1)]
        public int ContactId { get; set; }
    }

    [DataContract]
    internal partial class DoesContactExistParameters
    {
        [DataMember(Order = 1)]
        public int ContactId { get; set; }
    }

}

namespace Service.Contracts
{
    internal partial class ContactListProviderDescriptor : Bolt.ContractDescriptor
    {
        public ContactListProviderDescriptor() : base(typeof(Service.Contracts.IContactListProvider), "ContactListProvider")
        {
            GetContactsAsync = Add("GetContactsAsync", typeof(Bolt.Empty), typeof(IContactListProvider).GetTypeInfo().GetMethod("GetContactsAsync"));
            AddContactAsync = Add("AddContactAsync", typeof(Service.Contracts.Parameters.AddContactAsyncParameters), typeof(IContactListProvider).GetTypeInfo().GetMethod("AddContactAsync"));
            DeleteContactAsync = Add("DeleteContactAsync", typeof(Service.Contracts.Parameters.DeleteContactAsyncParameters), typeof(IContactListProvider).GetTypeInfo().GetMethod("DeleteContactAsync"));
            DoesContactExist = Add("DoesContactExist", typeof(Service.Contracts.Parameters.DoesContactExistParameters), typeof(IContactListProvider).GetTypeInfo().GetMethod("DoesContactExist"));
        }

        public static readonly ContactListProviderDescriptor Default = new ContactListProviderDescriptor();

        public  Bolt.ActionDescriptor GetContactsAsync { get; private set; }

        public  Bolt.ActionDescriptor AddContactAsync { get; private set; }

        public  Bolt.ActionDescriptor DeleteContactAsync { get; private set; }

        public  Bolt.ActionDescriptor DoesContactExist { get; private set; }
    }
}