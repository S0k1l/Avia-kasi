using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avia_kasi
{
    class Singleton
    {
        private static Singleton single = null;
        protected Singleton() { }
        public static Singleton Initialize()
        {
            if (single == null)
                single = new Singleton();
            return single;
        }
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
        public string Info { get; set; } = "Null";

        public string TicketInfo()
        {
            return
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
    }
}