using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBase;

namespace Serialize
{
    public class Database
    {
        public List<Client> Clients { get; set; }
        public List<Trainer> Trainers { get; set; }
        public List<Service> Services { get; set; }
        public List<Contract> Contracts { get; set; }
    }
}
