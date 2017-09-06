using System;
using System.Windows;
using DataBase;
using DataAccess.Data;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;

namespace Sports.Core
{
    public class Valid
    {
        
        public static void ClientValid()
        {
            SportsEntities db = new SportsEntities();
            if (!Regex.IsMatch("Family", RagexMask.FioMask))
            {
                MessageBox.Show(ErrorList.Surname);
                return;
            }
            if (!Regex.IsMatch("Name", RagexMask.FioMask))
            {
                MessageBox.Show(ErrorList.Name);
                return;
            }
            if (!Regex.IsMatch("ThirdName", RagexMask.FioMask))
            {
                MessageBox.Show(ErrorList.PatronymicName);
                return;
            }
            if (!Regex.IsMatch("Adress", RagexMask.AddressMask))
            {
                MessageBox.Show(ErrorList.Address);
                return;
            }
            if (!Regex.IsMatch("Phone", RagexMask.PhoneMask))
            {
                MessageBox.Show(ErrorList.Phone);
                return;
            }
            if (db.Clients.Where(i => i.Family == "Family").FirstOrDefault() != null
                && db.Clients.Where(i => i.Name == "Name").FirstOrDefault() != null
                && db.Clients.Where(i => i.ThirdName == "ThirdName").FirstOrDefault() != null)
            {
                MessageBox.Show("Такой клиент уже есть в базе!");
                return;
            }
            
        }
    }
}
