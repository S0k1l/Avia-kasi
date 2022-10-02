using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avia_kasi
{
    public class Marshrut
    {
        public int Id { get; set; }

        public Airport From { get; set; }

        public Airport To { get; set; }

        public float Price { get; set; }
    }
}
