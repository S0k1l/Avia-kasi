using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Avia_kasi
{
    static class Errors
    {
        static string strError = "Помилка";
        public static bool SameInputError(string elemen1, string elemen2, string text)
        {
            if (elemen1 == elemen2 && elemen1 != "" && elemen2 != "")
            {
                MessageBox.Show($"Ви обрали однакові {text} вильоту і прильоту", strError, MessageBoxButton.OK);
                return false;
            }
            return true;
        }
        public static bool NullInputError(string elemen1, string elemen2, string text)
        {
            if (elemen1 == "" || elemen2 == "")
            {
                MessageBox.Show($"Ви не обрали {text} вильоту і/або прильоту", strError, MessageBoxButton.OK);
                return false;
            }
            return true;
        }
        public static bool ErrorDateInput(string datePickerDeparture, string datePickerReturn)
        {
            string curentDate = DateTime.Now.ToShortDateString();
            if (DateTime.Parse(datePickerDeparture).AddHours(1) <= DateTime.Parse(curentDate) || DateTime.Parse(datePickerReturn) < DateTime.Parse(curentDate) || DateTime.Parse(datePickerDeparture).AddHours(1) > DateTime.Parse(datePickerReturn))
            {
                MessageBox.Show("Ви неправильно обрали дату вильоту і/або прильоту", strError, MessageBoxButton.OK);
                return false;
            }
            return true;
        }
        public static bool ErrorNumberOfAdulds(string inputTextBoxAdult)
        {
            try
            {
                int adults = Int32.Parse(inputTextBoxAdult);
                if (adults <= 0)
                {
                    throw new Exception();
                }
                return true;
            }
            catch (Exception)
            {
                MessageBox.Show("Ви неправильно ввели кількість дорослих", strError, MessageBoxButton.OK);
                return false;
            }
        }
        public static bool ErrorNumberOfChildren(string inputTextBoxChildren, string message)
        {
            try
            {
                int child = Int32.Parse(inputTextBoxChildren);
                if (child < 0)
                {
                    throw new Exception();
                }
                return true;
            }
            catch (Exception)
            {
                MessageBox.Show(message, strError, MessageBoxButton.OK);
                return false;
            }
        }
        public static bool ErrorClass(string comboBoxClass)
        {
            if (comboBoxClass == "")
            {
                MessageBox.Show("Ви не обрали клас переольоту", strError, MessageBoxButton.OK);
                return false;
            }
            return true;
        }

        public static void MainError()
        {
            MessageBox.Show("Поля неправильно заповнені", strError, MessageBoxButton.OK);
        }

        public static bool EmailError(string email)
        {
            if (email.Length > 5 & email.Contains("@") & email.Contains("."))
                return true;
            else
            {
                MessageBox.Show("Ви неправильно ввели E-mail", strError, MessageBoxButton.OK);
                return false;
            }
        }

        public static bool ErrorCheckBox( bool cart, bool nfc, bool cash)
        {
            if (cart == false && nfc == false && cash == false)
            {
                MessageBox.Show("Ви не обрали тип оплати", strError, MessageBoxButton.OK);
                return false;
            }
            return true;
        }
    }
}