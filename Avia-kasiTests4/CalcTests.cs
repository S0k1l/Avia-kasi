using Microsoft.VisualStudio.TestTools.UnitTesting;
using Avia_kasi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avia_kasi.Tests
{
    [TestClass()]
    public class CalcTests
    {
        [TestMethod()]
        public void CalculateTest()
        {
            double a = 488.44;
            int b = 1;
            int c = 1;
            double expected = 732.66;

            double actual = Calc.Calculate(a, b, c);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void SameInputErrorTest()
        {
            string a = "Віниця";
            string b = "Дніпро";
            bool expected = true;

            bool actual = Errors.SameInputError(a, b);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void NullInputErrorTest()
        {
            string a = "Віниця";
            string b = "Віниця";
            string c = "місця";
            bool expected = true;

            bool actual = Errors.NullInputError(a, b, c);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void ErrorDateInputTest()
        {
            string a = "01.11.2022";
            string b = "05.11.2022";
            bool expected = true;

            bool actual = Errors.ErrorDateInput(a, b);

            Assert.AreEqual(expected, actual);
        }       
        
        [TestMethod()]
        public void ErrorNumberOfAduldsTest()
        {
            string a = "1";
            bool expected = true;

            bool actual = Errors.ErrorNumberOfAdulds(a);

            Assert.AreEqual(expected, actual);
        }
        
        [TestMethod()]
        public void ErrorNumberOfChildrenTest()
        {
            string a = "0";
            string b = "Ви неправильно ввели кількість дітей від 2 до 12 років";
            bool expected = true;

            bool actual = Errors.ErrorNumberOfChildren(a, b);

            Assert.AreEqual(expected, actual);
        }
        
        [TestMethod()]
        public void ErrorClassTest()
        {
            string a = "Перший";
            bool expected = true;

            bool actual = Errors.ErrorClass(a);

            Assert.AreEqual(expected, actual);
        }
        
        [TestMethod()]
        public void ErrorCheckBoxTest()
        {
            bool a = true;
            bool b = false;
            bool c = false;
            bool expected = true;

            bool actual = Errors.ErrorCheckBox(a,b,c);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void EmailErrorTest()
        {
            string a = "asdfg@i.ua";
            bool expected = true;

            bool actual = Errors.EmailError(a);

            Assert.AreEqual(expected, actual);
        }
    }
}