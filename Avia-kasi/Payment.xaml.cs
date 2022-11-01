using System;
using System.Collections.Generic;
using System.IO;
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
            TicketInfo();
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

            if (Errors.EmailError(Email.Text.Trim().ToLower()))
            {
                if (Errors.ErrorCheckBox(Convert.ToBoolean(Cart.IsChecked), Convert.ToBoolean(NFC.IsChecked), Convert.ToBoolean(Cash.IsChecked)))
                {
                    SendEmail(email);
                    MessageBox.Show("Квиток надісланий", "Квиток", MessageBoxButton.OK);
                }
            }                
        }
        private void SendEmail(string email)
        {
            using (var mail = new MailMessage())
            {
                var data = Singleton.Initialize();

                mail.From = new MailAddress("martynuky16@gmail.com");
                mail.To.Add(email);
                mail.Subject = "Квиток";
                mail.Body =
                    $"<p>Дата вильоту: {data.Departure}</p>" +
                    $"<p>Місто: {data.FromCity}</p>" +
                    $"<p>Аеропорт: {data.FromName}</p>" +
                    $"<p><br>Кількість дорослих: {data.Ad}</p>" +
                    $"<p>Кількість від 2 до 12 років: {data.Ch12}</p>" +
                    $"<p>Кількість до 2 років: {data.Ch2}</p>" +
                    $"<p>Клас: {data.Сl}</p>" +
                    $"<p><br>Дата прильоту: {data.Return}</p>" +
                    $"<p>Місто: {data.ToCity}</p>" +
                    $"<p>Аеропорт: {data.ToName}</p>" +
                    $"<p><br>Ціна: {data.Price} ₴</p>";
                mail.IsBodyHtml = true;

                using (var smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.Credentials = new System.Net.NetworkCredential("martynuky16@gmail.com", "ebqoigfzltnyxamt");
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }
            }
        }
        public void TicketInfo()
        {
            var data = Singleton.Initialize();
            textBlockTicketInfo.Text = data.TicketInfo();    
        }
        private void buttonBack_Click(object sender, RoutedEventArgs e)
        {
            if (true)
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                Close();
            }
        }
    }
}