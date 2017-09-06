using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DataBase;

namespace Serialize
{    
    public class XmlSerialize
    {
        SportsEntities db;

        public void SaveDatabase()
        {

            var xlinq = (from c in db.Clients.AsEnumerable()
                                             select new XElement("Client",
                                             new XAttribute("ClientId", c.ClientId),
                                             new XElement("Family", c.Family),
                                             new XElement("Name", c.Name),
                                             new XElement("ThirdName", c.ThirdName),
                                             new XElement("Adress", c.Adress),
                                             new XElement("Phone", c.Phone)));

            Database database = new Database()
            {
                
            };
        }
    }

    
}
