using System;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using Windows.ApplicationModel.Contacts;
using Windows.Storage.Streams;

namespace WPF_UWP_Test1
{
    /// <summary>
    /// ContactWindow.xaml 的互動邏輯
    /// </summary>
    public partial class ContactWindow : Window
    {
        private Contact m_contact;
        
        public ContactWindow(Contact contact)
        {
            m_contact = contact;
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            InitName();
            InitPhone();
            InitEmail();
            InitAddress();
            InitWeb();
            InitJob();
            InitJobTitle();
            InitJobOffice();
            InitImportantDate();
            InitSignificantOthers();
            //InitChild();
            InitNote();
            InitImage();
        }

        private void InitName()
        {
            ContactRow row = new ContactRow();
            row.labelTitle.Content = "Name";
            row.SetSingle();
            if (m_contact != null)
            {
                row.AddOne(m_contact.FullName);
            }
            else
            {
                row.AddOne(null);
            }
            mypanel.Children.Add(row);
        }

        private void InitPhone()
        {
            ContactRow row = new ContactRow();
            row.labelTitle.Content = "Phone";
            if (m_contact != null && m_contact.Phones.Count > 0)
            {
                foreach (var phone in m_contact.Phones)
                {
                    row.AddOne(phone.Number);
                }
            }
            else
            {
                row.AddOne(null);
            }
            mypanel.Children.Add(row);
        }

        private void InitEmail()
        {
            ContactRow row = new ContactRow();
            row.labelTitle.Content = "Email";
            if (m_contact != null && m_contact.Emails.Count > 0)
            {
                foreach (var mail in m_contact.Emails)
                {
                    row.AddOne(mail.Address);
                }
            }
            else
            {
                row.AddOne(null);
            }
            mypanel.Children.Add(row);
        }

        private void InitAddress()
        {
            ContactRow row = new ContactRow();
            row.labelTitle.Content = "Address";
            if (m_contact != null && m_contact.Addresses.Count > 0)
            {
                foreach (var addr in m_contact.Addresses)
                {
                    row.AddOne(addr.StreetAddress);
                }
            }
            else
            {
                row.AddOne(null);
            }
            mypanel.Children.Add(row);
        }

        private void InitWeb()
        {
            ContactRow row = new ContactRow();
            row.labelTitle.Content = "Website";
            if (m_contact != null && m_contact.Websites.Count > 0)
            {
                foreach (var web in m_contact.Websites)
                {
                    row.AddOne(web.RawValue);
                }
            }
            else
            {
                row.AddOne(null);
            }
            mypanel.Children.Add(row);
        }

        private void InitJob()
        {
            ContactRow row = new ContactRow();
            row.labelTitle.Content = "Job";
            row.SetSingle();
            if (m_contact != null && m_contact.JobInfo.Count > 0)
            {
                foreach (var job in m_contact.JobInfo)
                {
                    row.AddOne(job.CompanyName);
                    break;
                }
            }
            else
            {
                row.AddOne(null);
            }
            mypanel.Children.Add(row);
        }

        private void InitJobTitle()
        {
            ContactRow row = new ContactRow();
            row.labelTitle.Content = "Job Title";
            row.SetSingle();
            if (m_contact != null && m_contact.JobInfo.Count > 0)
            {
                foreach (var job in m_contact.JobInfo)
                {
                    row.AddOne(job.Title);
                    break;
                }
            }
            else
            {
                row.AddOne(null);
            }
            mypanel.Children.Add(row);
        }

        private void InitJobOffice()
        {
            ContactRow row = new ContactRow();
            row.labelTitle.Content = "Job Office";
            row.SetSingle();
            if (m_contact != null && m_contact.JobInfo.Count > 0)
            {
                foreach (var job in m_contact.JobInfo)
                {
                    row.AddOne(job.Office);
                    break;
                }
            }
            else
            {
                row.AddOne(null);
            }
            mypanel.Children.Add(row);
        }

        private void InitImportantDate()
        {
            ContactRow row = new ContactRow();
            row.labelTitle.Content = "Important Date";
            //row.SetSingle();
            if (m_contact != null && m_contact.ImportantDates.Count > 0)
            {
                foreach (var d in m_contact.ImportantDates)
                {
                    int? year = d.Year;
                    uint? month = d.Month;
                    uint? date = d.Day;
                    string s = d.Kind.ToString() + ": " + year + "-" + month + "-" + date;
                    row.AddOne(s);
                }
            }
            else
            {
                row.AddOne(null);
            }
            mypanel.Children.Add(row);
        }

        private void InitSignificantOthers()
        {
            ContactRow row = new ContactRow();
            row.labelTitle.Content = "Significant Other";
            //row.SetSingle();
            if (m_contact != null && m_contact.SignificantOthers.Count > 0)
            {
                foreach (var d in m_contact.SignificantOthers)
                {
                    string s = d.Relationship + ": " + d.Name;
                    row.AddOne(s);
                }
            }
            else
            {
                row.AddOne(null);
            }
            mypanel.Children.Add(row);
        }

        //private void InitChild()
        //{
        //    ContactRow row = new ContactRow();
        //    row.labelTitle.Content = "Childrens";
        //    row.SetSingle();
        //    if (m_contact != null && m_contact.Fields.Count > 0)
        //    {
        //        foreach (var f in m_contact.Fields)
        //        {
        //            row.AddOne(f.Name + ", " + f.Value);
        //            break;
        //        }
        //    }
        //    else
        //    {
        //        row.AddOne(null);
        //    }
        //    mypanel.Children.Add(row);
        //}

        private void InitNote()
        {
            ContactRow row = new ContactRow();
            row.labelTitle.Content = "Note";
            row.SetSingle();
            if (m_contact != null)
            {
                row.AddOne(m_contact.Notes);
            }
            else
            {
                row.AddOne(null);
            }
            mypanel.Children.Add(row);
        }

        private async void InitImage()
        {
            /// https://stackoverflow.com/questions/5346727/convert-memory-stream-to-bitmapimage
            /// https://msdn.microsoft.com/zh-tw/library/windows/apps/xaml/hh973051.aspx?f=255&MSPPError=-2147217396
            if (m_contact != null)
            {
                if (m_contact.Thumbnail != null)
                {
                    IRandomAccessStreamWithContentType thumbnailStream = await m_contact.Thumbnail.OpenReadAsync();
                    Stream stream = thumbnailStream.AsStreamForRead();
                    BitmapImage thumbnailImage = new BitmapImage();
                    thumbnailImage.BeginInit();
                    thumbnailImage.StreamSource = stream;
                    thumbnailImage.CacheOption = BitmapCacheOption.OnLoad;
                    thumbnailImage.EndInit();
                    thumbnailImage.Freeze();
                    thumbnail.Source = thumbnailImage;
                }
                else if (m_contact.SourceDisplayPicture != null)
                {
                    IRandomAccessStreamWithContentType thumbnailStream = await m_contact.SourceDisplayPicture.OpenReadAsync();
                    Stream stream = thumbnailStream.AsStreamForRead();
                    BitmapImage thumbnailImage = new BitmapImage();
                    thumbnailImage.BeginInit();
                    thumbnailImage.StreamSource = stream;
                    thumbnailImage.CacheOption = BitmapCacheOption.OnLoad;
                    thumbnailImage.EndInit();
                    thumbnailImage.Freeze();
                    thumbnail.Source = thumbnailImage;
                }
            }
        }
    }
}
