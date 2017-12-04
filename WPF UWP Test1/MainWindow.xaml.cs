using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Windows.ApplicationModel.Contacts;
using Windows.Foundation;

namespace WPF_UWP_Test1
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<DisplayContact> vDisplayContact;


        /// <summary>
        /// https://blogs.windows.com/buildingapps/2017/01/25/calling-windows-10-apis-desktop-application/
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            vDisplayContact = new List<DisplayContact>();
        }

        private async void btn1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<string> s = await getContacts();
                if (s != null)
                {
                    string result = string.Empty;
                    foreach (string str in s)
                    {
                        Console.WriteLine(str);
                        result += str + Environment.NewLine;
                    }
                    tbResult.Text = result;
                }
                else
                {
                    Console.WriteLine("s is null");
                    tbResult.Text = "no contacts";
                }
                lvContact.ItemsSource = vDisplayContact;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }


        public async Task<List<string>> getContacts()
        {
            List<string> listResults = new List<string>();
            ContactStore store = null;
            IReadOnlyList<ContactList> list = null;
            ContactReader reader = null;
            ContactBatch batch = null;

            // *** This RequestStoreAsync() call is where the exception is thrown. All the cases below have the same issue. ***
            //store = await ContactManager.RequestStoreAsync(ContactStoreAccessType.AllContactsReadWrite);
            //store = await ContactManager.RequestStoreAsync(ContactStoreAccessType.AppContactsReadWrite);
            store = await ContactManager.RequestStoreAsync();

            list = await store.FindContactListsAsync();
            foreach (ContactList contactList in list)
            {
                reader = contactList.GetContactReader();
                batch = await reader.ReadBatchAsync();
                foreach (Contact contact in batch.Contacts)
                {
                    string fullname = contact.FullName; //+ ", " + contact.Name;// + ", " + contact.Emails.First().Address;
                    listResults.Add(fullname);
                    DisplayContact dc = new DisplayContact(contact);
                    vDisplayContact.Add(dc);
                }
            }
            return listResults;
        }

        private string ID = "test1";
        private async void AddContact(string firstName, string lastName)
        {
            /// https://docs.microsoft.com/en-us/windows/uwp/contacts-and-calendar/integrating-with-contacts
            /// https://stackoverflow.com/questions/34647386/windows-phone-10-is-possible-to-edit-add-new-contact-programmatically-in-wind

            ContactStore contactstore = await ContactManager.RequestStoreAsync(ContactStoreAccessType.AllContactsReadWrite);
            IReadOnlyList<ContactList> contactLists = await contactstore.FindContactListsAsync();


            ContactList lists2 = await contactstore.GetContactListAsync("24,d,d");
            if (lists2 != null)
            {
                var contact1 = new Contact();
                contact1.FirstName = firstName;
                contact1.LastName = lastName;
                await lists2.SaveContactAsync(contact1);
                return;
            }


            ContactList contactList = null;
            //if there is no contact list we create one
            if (contactLists.Count == 0)
            {
                try
                {
                    contactList = await contactstore.CreateContactListAsync("MyList");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
            //otherwise if there is one then we reuse it
            else
            {
                foreach(var c in contactLists)
                {
                    if(c.DisplayName == "MyList")
                    {
                        contactList = c;
                        break;
                    }
                }
            }

            var contact = new Contact();
            contact.FirstName = "Bob";
            contact.LastName = "Doe";
            ContactEmail email = new ContactEmail();
            email.Address = "bob@penpower.com";
            email.Kind = ContactEmailKind.Other;
            contact.Emails.Add(email);
            await contactList.SaveContactAsync(contact);
        }

        private async void AddContact(Contact c)
        {
            ContactStore contactstore = await ContactManager.RequestStoreAsync(ContactStoreAccessType.AllContactsReadWrite);
            IReadOnlyList<ContactList> contactLists = await contactstore.FindContactListsAsync();


            ContactList lists2 = await contactstore.GetContactListAsync("24,d,d");
            if (lists2 != null)
            {
                await lists2.SaveContactAsync(c);
                return;
            }
            else
            {
                lists2 = await contactstore.CreateContactListAsync("MyList");
                await lists2.SaveContactAsync(c);
                return;
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                NewContactWIndow window = new NewContactWIndow();
                window.ShowDialog();
                AddContact(window.contact);
                return;

                string fn = tbName.Text;
                string ln = tbEmail.Text;
                if (string.IsNullOrEmpty(fn) || string.IsNullOrEmpty(ln))
                {
                    MessageBox.Show("empty field!");
                    return;
                }
                AddContact(fn, ln);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                MessageBox.Show(ex.Message);
            }
        }

        protected void ItemDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DisplayContact dc = ((ListViewItem)sender).Content as DisplayContact;
            if (dc != null)
            {
                ContactWindow window = new ContactWindow(dc.contact);
                window.Owner = GetWindow(this);
                window.ShowDialog();
            }
        }
    }
}
