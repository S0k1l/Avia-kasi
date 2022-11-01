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
        public string From { get; set; } = "Null";
        public string To { get; set; } = "Null";
        public string Price { get; set; } = "Null";
        public string Ad { get; set; } = "Null";
        public string Ch12 { get; set; } = "Null";
        public string Ch2 { get; set; } = "Null";
        public string Departure { get; set; } = "Null";
        public string Return { get; set; } = "Null";
        public string Сl { get; set; } = "Null";
        public string FromCity { get; set; } = "Null";
        public string FromName { get; set; } = "Null";
        public string ToCity { get; set; } = "Null";
        public string ToName { get; set; } = "Null";

        public Payment()
        {
            InitializeComponent();
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
                mail.From = new MailAddress("martynuky16@gmail.com");
                mail.To.Add(email);
                mail.Subject = "Квиток";
                mail.Body =
                    $"<p>Дата вильоту: {Departure}</p>" +
                    $"<p>Місто: {FromCity}</p>" +
                    $"<p>Аеропорт: {FromName}</p>" +
                    $"<p><br>Кількість дорослих: {Ad}</p>" +
                    $"<p>Кількість від 2 до 12 років: {Ch12}</p>" +
                    $"<p>Кількість до 2 років: {Ch2}</p>" +
                    $"<p>Клас: {Сl}</p>" +
                    $"<p><br>Дата прильоту: {Return}</p>" +
                    $"<p>Місто: {ToCity}</p>" +
                    $"<p>Аеропорт: {ToName}</p>" +
                    $"<p><br>Ціна: {Price} ₴</p>";
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
            textBlockTicketInfo.Text =
                $"\nДата вильоту: {Departure}" +
                $"\nМісто: {FromCity}" +
                $"\nАеропорт: {FromName}" +
                $"\n\nКількість дорослих: {Ad}" +
                $"\nКількість від 2 до 12 років: {Ch12}" +
                $"\nКількість до 2 років: {Ch2}" +
                $"\nКлас: {Сl}" +
                $"\n\nДата прильоту: {Return}" +
                $"\nМісто: {ToCity}" +
                $"\nАеропорт: {ToName}" +
                $"\nДо сплати: {Price} ₴";
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