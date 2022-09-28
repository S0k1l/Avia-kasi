using System;
using System.Collections.Generic;
using System.ComponentModel;
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
            buttonSearch.Content = db.DbPath;

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
                winMinHeight = 605;

                if (expanderPassengers.IsExpanded)
                    Window1.MinHeight = winMinHeight + 105;

                ticketsBorder.Visibility = Visibility.Visible; 
                Window1.MinHeight = winMinHeight;

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
            var fromAirport = db.Airports.Where(s => s.City == comboBoxFrom.Text).ToList();
            var toAiport = db.Airports.Where(s => s.City == comboBoxTo.Text).ToList();



        }
    }
}
