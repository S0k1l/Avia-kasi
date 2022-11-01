using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
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

namespace Avia_kasi
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int winMinHeight = 365;
        double fullPrice;
        bool yesNo = false;

        public MainWindow()
        {
            InitializeComponent();
            datePickerDeparture.Text = DateTime.Now.ToShortDateString();
            datePickerReturn.Text = DateTime.Now.AddDays(7).ToShortDateString();

            using var db = new AirportContext();
            List<Airport> airport = db.Airports.ToList();

            foreach (var item in airport)
            {
                comboBoxFrom.Items.Add(item.City);
                comboBoxTo.Items.Add(item.City);
            }
        }
        private void Expander_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (!expanderPassengers.IsExpanded)
                Window1.MinHeight = winMinHeight + 105;
            else
                Window1.MinHeight = winMinHeight;
        }
        private void Search()
        {
            if (CheckForErrors())
            {
                winMinHeight = 475;
                Window1.MinHeight = winMinHeight;

                if (expanderPassengers.IsExpanded)
                    Window1.MinHeight = winMinHeight + 105;

                ticketsBorder.Visibility = Visibility.Visible;
                Buy.Visibility = Visibility.Visible;

                FindTickets();

                yesNo = true;
            }
        }
        private bool CheckForErrors()
        {
            if (Errors.NullInputError(datePickerDeparture.Text, datePickerReturn.Text, "дати") &&
                Errors.SameInputError(comboBoxFrom.Text, comboBoxTo.Text) &
                Errors.NullInputError(comboBoxFrom.Text, comboBoxTo.Text, "місця") &
                Errors.NullInputError(datePickerDeparture.Text, datePickerReturn.Text, "місця") &
                Errors.ErrorDateInput(datePickerDeparture.Text, datePickerReturn.Text) &
                Errors.ErrorNumberOfAdulds(inputTextBoxAdult.Text) &
                Errors.ErrorNumberOfChildren(inputTextBoxChildren12.Text, "Ви неправильно ввели кількість дітей від 2 до 12 років") &
                Errors.ErrorNumberOfChildren(inputTextBoxChildren2.Text, "Ви неправильно ввели кількість дітей до 2 років") &
                Errors.ErrorClass(comboBoxClass.Text))
            {
                return true;
            }
            return false;
        }
        private void FindTickets()
        {
            using var db = new AirportContext();

            var Find = (from marshrut in db.Marshruts
                        join airport in db.Airports on marshrut.From.Id equals airport.Id
                        join a in db.Airports on marshrut.To.Id equals a.Id
                        select new
                        {
                            FromName = airport.Name,
                            FromCity = airport.City,
                            ToName = a.Name,
                            ToCity = a.City,
                            Price = marshrut.Price
                        }).Where(s => s.FromCity == comboBoxFrom.Text &&
                        s.ToCity == comboBoxTo.Text).ToList();

            switch (comboBoxClass.Text)
            {
                case "Економ":
                    FindOutput(Find.First().FromName, Find.First().FromCity, Find.First().ToName, Find.First().ToCity, Find.First().Price);
                    break;
                case "Бізнес":
                    FindOutput(Find.First().FromName, Find.First().FromCity, Find.First().ToName, Find.First().ToCity, Find.First().Price, 2);
                    break;
                case "Перший":
                    FindOutput(Find.First().FromName, Find.First().FromCity, Find.First().ToName, Find.First().ToCity, Find.First().Price, 5);
                    break;
            }
        }
        private void FindOutput(string fromName,string fromCity,string toName,string toCity, double price ,int clas= 1)
        {
            ticketsBlock.Text =
                $"{fromName} | " +
                $"{fromCity} => {toName} | " +
                $"{toCity} = " +
                $"{fullPrice = Calc.Calculate(price, Int32.Parse(inputTextBoxAdult.Text), Int32.Parse(inputTextBoxChildren12.Text), clas)} ₴";
        } 
        private void Buy_Click(object sender, RoutedEventArgs e)
        {
            using var db = new AirportContext();
            var airport = db.Airports.Where(s => s.City == comboBoxFrom.Text).ToList();

            if (CheckForErrors())
            {
                var payment = new Payment();

                payment.From = comboBoxFrom.Text;
                payment.To = comboBoxTo.Text;
                payment.Price = fullPrice.ToString();
                payment.Ad = inputTextBoxAdult.Text;
                payment.Ch12 = inputTextBoxChildren12.Text;
                payment.Ch2 = inputTextBoxChildren2.Text;
                payment.Departure = datePickerDeparture.Text;
                payment.Return = datePickerReturn.Text;
                payment.Сl = comboBoxClass.Text;
                payment.FromCity = airport.First().City;
                payment.FromName = airport.First().Name;

                airport = db.Airports.Where(s => s.City == comboBoxTo.Text).ToList();

                payment.ToCity = airport.First().City;
                payment.ToName = airport.First().Name;
                payment.TicketInfo();
                payment.Show();

                Close();
            }
        }
        private void SearchByClick(object sender, MouseButtonEventArgs e)
        {
            if (yesNo)
                Search();
        }
        private void SearchByClick(object sender, EventArgs e)
        {
            if (yesNo)
                Search();
        }
        private void buttonSearch_Click(object sender, RoutedEventArgs e)
        {
            Search();
        }
        private void SearchByClick(object sender, RoutedEventArgs e)
        {
            if (yesNo)
                Search();
        }
    }
}