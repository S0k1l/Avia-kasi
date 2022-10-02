using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        public MainWindow()
        {
            InitializeComponent();
            datePickerDeparture.Text = DateTime.Now.ToShortDateString();
            datePickerReturn.Text = DateTime.Now.AddDays(7).ToShortDateString();

            using var db = new AirportContext();
            //buttonSearch.Content = db.DbPath;

            List<Airport> airport = db.Airports.ToList();

            //var setAirport = new Airport
            //{
            //    Name = "Aеропорт «Запоріжжя»",
            //    City = "Запоріжжя",
            //    X = 47,
            //    Y = 35
            //};

            //db.Add(setAirport);
            //db.SaveChanges();

            //var getAirport = db.Airports.First();
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

        private void buttonSearch_Click(object sender, RoutedEventArgs e)
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
            }
        }

        private bool CheckForErrors()
        {
            if (Errors.SameInputError(comboBoxFrom.Text, comboBoxTo.Text, "місця") &
                Errors.NullInputError(comboBoxFrom.Text, comboBoxTo.Text) &
                Errors.SameInputError(datePickerDeparture.Text, datePickerReturn.Text, "дати") &
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


            //var Find = db.Marshruts.Where(s => s.From.City == comboBoxFrom.Text && s.To.City == comboBoxTo.Text).ToList();
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
                    ticketsBlock.Text = $"{Find.First().FromName} | " +
                        $"{Find.First().FromCity} => {Find.First().ToName} | " +
                        $"{Find.First().ToCity} = {Math.Round(Find.First().Price * Int32.Parse(inputTextBoxAdult.Text) + (Find.First().Price) / 2 * Int32.Parse(inputTextBoxChildren12.Text),2)}";
                    break;   
                case "Бізнес":
                    ticketsBlock.Text = $"{Find.First().FromName} | " +
                        $"{Find.First().FromCity} => {Find.First().ToName} | " +
                        $"{Find.First().ToCity} = {Math.Round(Find.First().Price * 2 * Int32.Parse(inputTextBoxAdult.Text) + (Find.First().Price) / 2 * Int32.Parse(inputTextBoxChildren12.Text), 2)}";
                    break; 
                case "Перший":
                    ticketsBlock.Text = $"{Find.First().FromName} | " +
                        $"{Find.First().FromCity} => {Find.First().ToName} | " +
                        $"{Find.First().ToCity} = {Math.Round(Find.First().Price * 5 * Int32.Parse(inputTextBoxAdult.Text) + (Find.First().Price) / 2 * Int32.Parse(inputTextBoxChildren12.Text), 2)}";
                    break;
            }

            //ticketsBlock.Text = $"{Find.First().FromName} | {Find.First().FromCity} => {Find.First().ToName} | {Find.First().ToCity} = {Find.First().Price}";
        }

        private void Buy_Click(object sender, RoutedEventArgs e)
        {
            using var writer = new StreamWriter(@"D:\Програми\Avia-kasi\Avia-kasi\1.txt");
            using var db = new AirportContext();
            var airport = db.Airports.Where(s => s.City == comboBoxFrom.Text).ToList();

            writer.WriteLine($"Дата вильоту: {datePickerDeparture.Text}");
            writer.WriteLine($"Місто: {airport.First().City}");
            writer.WriteLine($"Аеропорт: {airport.First().Name}");
            writer.WriteLine($"\nКількість дорослих: {inputTextBoxAdult.Text}");
            writer.WriteLine($"Кількість від 2 до 12 років: {inputTextBoxChildren12.Text}");
            writer.WriteLine($"Кількість до 2 років: {inputTextBoxChildren2.Text}");
            writer.WriteLine($"Клас: {comboBoxClass.Text}");

            airport = db.Airports.Where(s => s.City == comboBoxTo.Text).ToList();


            writer.WriteLine($"\nДата прильоту: {datePickerReturn.Text}");
            writer.WriteLine($"Місто: {airport.First().City}");
            writer.WriteLine($"Аеропорт: {airport.First().Name}");
        }
    }
}
