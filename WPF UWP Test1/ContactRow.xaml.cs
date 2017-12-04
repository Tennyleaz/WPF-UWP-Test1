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

namespace WPF_UWP_Test1
{
    public enum RowType
    {

    }

    /// <summary>
    /// ContactRow.xaml 的互動邏輯
    /// </summary>
    public partial class ContactRow : UserControl
    {
        public ContactRow()
        {
            InitializeComponent();
        }

        private enum ButtonState
        {
            Add,
            Save,
        }

        private void btnAddOne_Click(object sender, RoutedEventArgs e)
        {
            TextBox tb = new TextBox();
            tb.HorizontalAlignment = HorizontalAlignment.Stretch;
            tb.Height = 20;
            tb.TextWrapping = TextWrapping.NoWrap;
            tb.AcceptsReturn = false;
            contentPanel.Children.Add(tb);
            //SwitchState();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Window parentWindow = Window.GetWindow(this);
            if (parentWindow is ContactWindow)
            {

            }
            //SwitchState();
        }

        public void SetSingle()
        {
            btnAddOne.Visibility = Visibility.Hidden;
        }

        //public void AddDefault(string input)
        //{
        //    tbContent.Text = input;
        //}

        public void AddOne(string input)
        {
            if (input == null)
                return;

            TextBox tb = new TextBox();
            tb.HorizontalAlignment = HorizontalAlignment.Stretch;
            tb.Height = 20;
            tb.TextWrapping = TextWrapping.NoWrap;
            tb.AcceptsReturn = false;
            tb.Text = input;
            contentPanel.Children.Add(tb);
            //SwitchState();
        }

        private ButtonState buttonState = ButtonState.Add;
        private void SwitchState()
        {
            if (buttonState == ButtonState.Add)
            {
                btnAddOne.Content = "Save";
                btnAddOne.Click -= btnAddOne_Click;
                btnAddOne.Click += btnSave_Click;
                buttonState = ButtonState.Save;
            }
            else
            {
                btnAddOne.Content = "Add";
                btnAddOne.Click -= btnSave_Click;
                btnAddOne.Click += btnAddOne_Click;
                buttonState = ButtonState.Add;
            }
        }
    }
}
