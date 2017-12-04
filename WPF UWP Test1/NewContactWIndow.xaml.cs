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
    /// NewContactWIndow.xaml 的互動邏輯
    /// </summary>
    public partial class NewContactWIndow : Window
    {
        public Contact contact;
        public NewContactWIndow()
        {
            InitializeComponent();
            contact = new Contact();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // name
            contact.FirstName = tbFName.Text;
            contact.MiddleName = tbMName.Text;
            contact.LastName = tbLName.Text;
            contact.Nickname = tbNickName.Text;
            contact.HonorificNamePrefix = tbPrefix.Text;
            contact.HonorificNameSuffix = tbSuffix.Text;
            contact.YomiGivenName = tbFNamePinyin.Text;
            contact.YomiFamilyName = tbLNamePinyin.Text;

            // website
            ContactWebsite web = new ContactWebsite();
            web.RawValue = tbWeb.Text;
            contact.Websites.Add(web);


            // job
            ContactJobInfo job = new ContactJobInfo();
            job.CompanyName = tbCompany.Text;
            job.Title = tbPosition.Text;
            job.Office = tbOfficeLocation.Text;
            contact.JobInfo.Add(job);

            // Significant others
            ContactSignificantOther soSpause = new ContactSignificantOther();
            soSpause.Relationship = ContactRelationship.Spouse;
            soSpause.Name = tbSpause.Text;
            contact.SignificantOthers.Add(soSpause);
            ContactSignificantOther soChild = new ContactSignificantOther();
            soChild.Relationship = ContactRelationship.Child;
            soChild.Name = tbChild.Text;
            contact.SignificantOthers.Add(soChild);

            // note
            contact.Notes = tbNote.Text;

            // phone 
            ContactPhone cell1 = new ContactPhone();
            ContactPhone cell2 = new ContactPhone();
            ContactPhone home1 = new ContactPhone();
            ContactPhone home2 = new ContactPhone();
            ContactPhone work1 = new ContactPhone();
            ContactPhone work2 = new ContactPhone();
            ContactPhone companyphone = new ContactPhone();
            ContactPhone companyfax = new ContactPhone();
            ContactPhone homefax = new ContactPhone();
            ContactPhone bbcall = new ContactPhone();
            cell1.Kind = ContactPhoneKind.Mobile;
            cell1.Number = tbCellphone1.Text;
            cell2.Kind = ContactPhoneKind.Mobile;
            cell2.Number = tbCellphone2.Text;
            home1.Kind = ContactPhoneKind.Home;
            home1.Number = tbHomephone1.Text;
            home2.Kind = ContactPhoneKind.Home;
            home2.Number = tbHomephone2.Text;
            work1.Kind = ContactPhoneKind.Work;
            work1.Number = tbWorkphone1.Text;
            work2.Kind = ContactPhoneKind.Work;
            work2.Number = tbWorkphone2.Text;
            companyphone.Kind = ContactPhoneKind.Company;
            companyphone.Number = tbOfficephone.Text;
            companyfax.Kind = ContactPhoneKind.BusinessFax;
            companyfax.Number = tbOfficeFax.Text;
            homefax.Kind = ContactPhoneKind.HomeFax;
            homefax.Number = tbHomeFax.Text;
            bbcall.Kind = ContactPhoneKind.Pager;
            bbcall.Number = tbBBCall.Text;
            contact.Phones.Add(cell1);
            contact.Phones.Add(cell2);
            contact.Phones.Add(home1);
            contact.Phones.Add(home2);
            contact.Phones.Add(work1);
            contact.Phones.Add(work2);
            contact.Phones.Add(companyphone);
            contact.Phones.Add(companyfax);
            contact.Phones.Add(homefax);
            contact.Phones.Add(bbcall);

            // email
            ContactEmail homemail = new ContactEmail();
            ContactEmail workmail = new ContactEmail();
            ContactEmail othermail = new ContactEmail();
            homemail.Kind = ContactEmailKind.Personal;
            homemail.Address = tbPersonalEmail.Text;
            workmail.Kind = ContactEmailKind.Work;
            workmail.Address = tbOfficeEmail.Text;
            othermail.Kind = ContactEmailKind.Other;
            othermail.Address = tbOtherEmail.Text;
            contact.Emails.Add(homemail);
            contact.Emails.Add(workmail);
            contact.Emails.Add(othermail);
        }

        private void btnCompanyAddress_Click(object sender, RoutedEventArgs e)
        {
            btnCompanyAddress.IsEnabled = false;
            btnCompanyAddress.Content = AddAddress(ContactAddressKind.Work);
        }

        private void btnHomeAddress_Click(object sender, RoutedEventArgs e)
        {
            btnHomeAddress.IsEnabled = false;
            btnHomeAddress.Content = AddAddress(ContactAddressKind.Home);

        }

        private void btnOtheryAddress_Click(object sender, RoutedEventArgs e)
        {
            btnOtheryAddress.IsEnabled = false;
            btnOtheryAddress.Content = AddAddress(ContactAddressKind.Other);
        }

        private string AddAddress(ContactAddressKind kind)
        {
            AddressWindow wnd = new AddressWindow();
            wnd.Owner = this;
            wnd.address.Kind = kind;
            wnd.ShowDialog();            
            contact.Addresses.Add(wnd.address);
            return wnd.address.Country + " " + wnd.address.Region + " " + wnd.address.Locality + " " + wnd.address.StreetAddress;
        }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            var picker = sender as DatePicker;
            DateTime? date = picker.SelectedDate;
            if (date == null)
            {
                // ... A null object.
                //this.Title = "No date";
            }
            else
            {
                // ... No need to display the time.
                //this.Title = date.Value.ToShortDateString();
                ContactDate cd = new ContactDate();
                cd.Year = date.Value.Year;
                cd.Month = (uint)date.Value.Month;
                cd.Day = (uint)date.Value.Day;
                cd.Kind = ContactDateKind.Anniversary;
                contact.ImportantDates.Add(cd);
            }
        }

        private void BirthDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            var picker = sender as DatePicker;
            DateTime? date = picker.SelectedDate;
            if (date == null)
            {
                // ... A null object.
                //this.Title = "No date";
            }
            else
            {
                // ... No need to display the time.
                //this.Title = date.Value.ToShortDateString();
                ContactDate cd = new ContactDate();
                cd.Year = date.Value.Year;
                cd.Month = (uint)date.Value.Month;
                cd.Day = (uint)date.Value.Day;
                cd.Kind = ContactDateKind.Birthday;
                contact.ImportantDates.Add(cd);
            }
        }
    }
    
}
