using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using DataBase;
using Sports.Forms;

namespace Sports.Classes
{
    class ListContractClients : ObservableCollection<Client>
    {
        public ListContractClients()
        {
            var listConCl = ContractsPage.DataEntitiesContracts.Clients;



            var queryConCl = from cl in listConCl
                             orderby cl.Family
                             select cl;

            foreach (var cl in queryConCl)
            {
                this.Add(cl);
            }
        }
    }
}

