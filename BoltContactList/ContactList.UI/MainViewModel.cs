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
        private bool _isExecuting;

        public MainViewModel()
        {
            ClientConfiguration clientConfiguration = new ClientConfiguration();

            _proxy = clientConfiguration.CreateProxy<IContactListProvider>("http://localhost:5000");
            Contacts = new ObservableCollection<Contact>();

            AddContactCommand = new RelayCommand(async () =>
            {
                try
                {
                    IsExecuting = true;
                    Contact contact =
                        await
                            _proxy.AddContactAsync(new Contact {Name = Name, Surname = Surname}, CancellationToken.None);
                    Contacts.Add(contact);
                }
                finally
                {
                    IsExecuting = false;
                }

            }, () => !string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(Surname) && !IsExecuting);

            RemoveContactCommand = new RelayCommand(async () =>
            {
                try
                {

                    await _proxy.DeleteContactAsync(SelectedContact.Id, CancellationToken.None);
                    Contacts.Remove(SelectedContact);
                }
                finally
                {
                    IsExecuting = false;
                }

            }, () => SelectedContact != null && !IsExecuting);

            LoadContactsCommand = new RelayCommand(async () =>
            {
                try
                {
                    List<Contact> contacts = await _proxy.GetContactsAsync(CancellationToken.None);
                    Contacts.Clear();

                    foreach (Contact contact in contacts)
                    {
                        Contacts.Add(contact);
                    }
                }
                finally
                {
                    IsExecuting = false;
                }

            }, () => !IsExecuting);
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

        private bool IsExecuting
        {
            get { return _isExecuting; }
            set
            {
                _isExecuting = value;
                RemoveContactCommand.RaiseCanExecuteChanged();
                AddContactCommand.RaiseCanExecuteChanged();
                LoadContactsCommand.RaiseCanExecuteChanged();
            }
        }

        public ObservableCollection<Contact> Contacts { get; set; }

        public RelayCommand AddContactCommand { get; set; }

        public RelayCommand RemoveContactCommand { get; set; }

        public RelayCommand LoadContactsCommand { get; set; }
    }
}