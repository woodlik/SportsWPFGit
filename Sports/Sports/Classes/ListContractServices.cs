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
    class ListContractServices : ObservableCollection<Service>
    {
        public ListContractServices()
        {
            var listConSe = ContractsPage.DataEntitiesContracts.Services;

            var queryZaksv = from sv in listConSe
                             orderby sv.Title
                             select sv;

            foreach (var sv in listConSe)
            {
                this.Add(sv);
            }
        }
    }
}
