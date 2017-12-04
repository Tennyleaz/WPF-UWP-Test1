using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Contacts;

namespace WPF_UWP_Test1
{
    public class DisplayContact
    {
        public string DisplayName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public Contact contact;

        public DisplayContact() { }
        public DisplayContact(Contact c)
        {
            DisplayName = c.DisplayName;
            if (c.Phones.Count > 0)
                Phone = c.Phones.FirstOrDefault().Number;
            if (c.Emails.Count > 0)
                Email = c.Emails.FirstOrDefault().Address;
            contact = c;
        }
    }
}
