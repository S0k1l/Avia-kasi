using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
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

namespace Avia_kasi
{
    /// <summary>
    /// Interaction logic for Payment.xaml
    /// </summary>
    public partial class Payment : Window
    {
        public Payment()
        {
            InitializeComponent();
            MainWindow mainWindow = new MainWindow();           
            Price.Text += mainWindow.fullPrice.ToString();
        }

        private void Cart_Checked(object sender, RoutedEventArgs e)
        {
            if (sender.Equals(Cart))
            {
                Cash.IsChecked = false;
                NFC.IsChecked = false;
            }

            if (sender.Equals(Cash))
            {
                Cart.IsChecked = false;
                NFC.IsChecked = false;
            }

            if (sender.Equals(NFC))
            {
                Cart.IsChecked = false;
                Cash.IsChecked = false;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string email = Email.Text.Trim().ToLower();
            bool a = Convert.ToBoolean(Cart.IsChecked);
            Cart.Content = a.ToString();
            if (Errors.EmailError(Email.Text.Trim().ToLower()))
            {
                if (Errors.ErrorCheckBox(Convert.ToBoolean(Cart.IsChecked), Convert.ToBoolean(NFC.IsChecked), Convert.ToBoolean(Cash.IsChecked)))
                {
                    SendEmail(email);
                    MessageBox.Show("ok", "1", MessageBoxButton.OK);
                }
            }                
        }

        private void SendEmail(string email)
        {
            using (var mail = new MailMessage())
            {
                mail.From = new MailAddress("martynuky16@gmail.com");
                mail.To.Add(email);
            }
        }
    }
}
