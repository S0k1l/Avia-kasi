using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avia_kasi
{
    public static class Calc
    {
        public static double Calculate(double price, int adults, int childs, int clas = 1)
        {
            return Math.Round((price * adults + (price / 2 * childs)) * clas, 2);
        }
    }
}
