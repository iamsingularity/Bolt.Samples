using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using Bolt.Client;
using ContactList.Contracts;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace ContactList.UI
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IContactListProvider _proxy;
        private Contact _selectedContact;
        private string _name;
        private string _surname;

        public MainViewModel()
        {
            ClientConfiguration clientConfiguration = new ClientConfiguration();

            _proxy = clientConfiguration.CreateProxy<IContactListProvider>("http://localhost:5000");
            Contacts = new ObservableCollection<Contact>();

            AddContactCommand = new RelayCommand(async () =>
            {
                Contact contact = await _proxy.AddContactAsync(new Contact { Name = Name, Surname = Surname }, CancellationToken.None);
                Contacts.Add(contact);
            }, () => !string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(Surname));

            RemoveContactCommand = new RelayCommand(async () =>
            {
                await _proxy.DeleteContactAsync(SelectedContact.Id, CancellationToken.None);
                Contacts.Remove(SelectedContact);
            }, () => SelectedContact != null);

            LoadContactsCommand = new RelayCommand(async () =>
            {
                List<Contact> contacts = await _proxy.GetContactsAsync(CancellationToken.None);
                Contacts.Clear();

                foreach (Contact contact in contacts)
                {
                    Contacts.Add(contact);
                }
            });
        }

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                RaisePropertyChanged();
                AddContactCommand.RaiseCanExecuteChanged();
            }
        }

        public string Surname
        {
            get { return _surname; }
            set
            {
                _surname = value;
                RaisePropertyChanged();
                AddContactCommand.RaiseCanExecuteChanged();
            }
        }

        public Contact SelectedContact
        {
            get { return _selectedContact; }
            set
            {
                _selectedContact = value;
                RaisePropertyChanged();
                RemoveContactCommand.RaiseCanExecuteChanged();
            }
        }

        public ObservableCollection<Contact> Contacts { get; set; }

        public RelayCommand AddContactCommand { get; set; }

        public RelayCommand RemoveContactCommand { get; set; }

        public RelayCommand LoadContactsCommand { get; set; }



    }
}