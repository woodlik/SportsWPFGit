using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sports.Core
{
    public class RagexMask
    {
        public static string FioMask = "^[А-Я]{1}[а-я]{1,20}$";
        public static string AddressMask = "^[А-Яа-я\\d,.\\s]+$";
        public static string PhoneMask = "^[+]{1}[0-9]{12}$";
        public static string CityMask = "^[А-Я]{1}[а-я]{1,20}(\\s[А-Я]{1}[а-я]{1,20}){0,20}$";
        public static string Text = "^[\\w,.\\-!'\"\\s\\n]{0,250}$";
        public static string Price = "^[0-9]*[.][0-9]*$";
        public static string Name = "^[А-Я]{1}[а-я]{1,20}[\\sА-Я]{0,2}[а-я]{0,20}$";
    }
}
