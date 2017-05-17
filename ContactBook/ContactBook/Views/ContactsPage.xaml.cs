using System;
using System.Collections.ObjectModel;
using ContactBook.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ContactBook
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContactsPage : ContentPage
    {
        private ObservableCollection<Contact> _contacts = new ObservableCollection<Contact>
        {
            new Contact {Id = 2, FirstName = "Francois", LastName = "Duchemin"},
            new Contact {Id = 3, FirstName = "Malis", LastName = "Duchemin"}
        };

        public ContactsPage()
        {
            InitializeComponent();

            ContactListView.ItemsSource = _contacts;
        }

        private async void AddMenuItem_OnClicked(object sender, EventArgs e)
        {
            var page = new ContactDetailsPage(new Contact());
            page.ContactAdded += HandleContactAdded;
            await Navigation.PushAsync(page);
        }

        private async void ContactListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;

            var selectedContact = e.SelectedItem as Contact;
            ContactListView.SelectedItem = null; // Deselect selected item

            var page = new ContactDetailsPage(selectedContact);
            page.ContactUpdated += (source, contact) =>
            {
                selectedContact.Id = contact.Id;
                selectedContact.FirstName = contact.FirstName;
                selectedContact.LastName = contact.LastName;
                selectedContact.Phone = contact.Phone;
                selectedContact.EmailAddress = contact.EmailAddress;
                selectedContact.IsBlocked = contact.IsBlocked;
            };
            await Navigation.PushAsync(page);
        }

        private async void DeleteContactMenuItem_OnClicked(object sender, EventArgs e)
        {
            var contact = (sender as MenuItem).CommandParameter as Contact;

            if (await DisplayAlert("Warning", $"Are you sure you want to delete {contact.FullName}?", "Yes", "No"))
                _contacts.Remove(contact);
        }

        private void HandleContactAdded(object sender, Contact contact)
        {
            _contacts.Add(contact);
        }
    }
}
