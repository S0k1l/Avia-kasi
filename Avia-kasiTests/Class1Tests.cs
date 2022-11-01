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
    public class Class1Tests
    {
        [TestMethod()]
        public void calcTest()
        {
            double price = 488.44;
            int adults = 1;
            int childs = 0;
            double expexted = 2442.2;

            Class1 c = new Class1();
            double actual = c.calc(price,adults,childs);
            Assert.AreEqual(expexted,actual);
        }
    }
}