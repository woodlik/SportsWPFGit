using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sports.Core
{
    public class ErrorList
    {
        public static string Name = "Name has only english letters and begins with an uppercase letter\n" +
                                    "Example : Andre, Jhon, Alexsandra";
        public static string Surname = "Surname has only english letters and begins with an uppercase letter\n" +
                                        "Example : Fury, Anderson, Petrov";
        public static string PatronymicName = "Name has only english letters and begins with an uppercase letter\n" +
                                    "Example : Viktorovich, Petrovich";
        public static string Address = "Addres";
        public static string Phone = "Phone number has 7 numbers\n" +
                                    "Example : 1234567";
        public static readonly string Price = "Enter price, 0 - free";
        public static readonly string OnlyText = "In the text must be only latin letters, digits and signs(,.-!;'\")";
        public static readonly string ClientMax = "Max number of members can not be less than 2 and more than 30";
    }
}
