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
        public static bool SameInputError(string elemen1, string elemen2 ,string text)
        {
            if (elemen1 == elemen2 && elemen1 != "" && elemen2 != "")
            {
                MessageBox.Show($"Ви обрали однакові {text} вильоту і прильоту", strError, MessageBoxButton.YesNo);
                return false;
            }
            return true;
        }
        public static bool NullInputError(string elemen1, string elemen2)
        {
            if (elemen1 == "" || elemen2 == "")
            {
                MessageBox.Show("Ви не обрали місця вильоту і/або прильоту", strError, MessageBoxButton.YesNo);
                return false;
            }
            return true;

        }
        public static bool ErrorDateInput(string datePickerDeparture, string datePickerReturn)
        {
            string curentDate = DateTime.Now.ToShortDateString();
            if (DateTime.Parse(datePickerDeparture).AddHours(1) <= DateTime.Parse(curentDate) || DateTime.Parse(datePickerReturn) < DateTime.Parse(curentDate))
            {
                MessageBox.Show("Ви неправильно обрали дату вильоту і/або прильоту", strError, MessageBoxButton.YesNo);
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
                MessageBox.Show("Ви неправильно ввели кількість дорослих", strError, MessageBoxButton.YesNo);
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
                MessageBox.Show(message, strError, MessageBoxButton.YesNo);
                return false;
            }
        }
        public static bool ErrorClass(string comboBoxClass)
        {
            if (comboBoxClass == "")
            {
                MessageBox.Show("Ви не обрали клас переольоту", strError, MessageBoxButton.YesNo);
                return false;
            }
            return true;
        } 
    }
}