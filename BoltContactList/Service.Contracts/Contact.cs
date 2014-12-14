using System.Runtime.Serialization;

namespace Service.Contracts
{
    [DataContract]
    public class Contact
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Surname { get; set; }

        [DataMember]
        public string TelephoneNumber { get; set; }
    }
}
