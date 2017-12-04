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
using System.Windows.Shapes;
using Windows.ApplicationModel.Contacts;

namespace WPF_UWP_Test1
{
    /// <summary>
    /// AddressWindow.xaml 的互動邏輯
    /// </summary>
    public partial class AddressWindow : Window
    {
        public ContactAddress address;
        public AddressWindow(ContactAddress addr = null)
        {
            InitializeComponent();
            if (addr != null)
                address = addr;
            else
                address = new ContactAddress();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            address.Country = tbCountry.Text;
            address.Locality = tbLocality.Text;
            address.PostalCode = tbPostcode.Text;
            address.Region = tbRegion.Text;
            address.StreetAddress = tbStreetAddress.Text;
        }
    }
}
