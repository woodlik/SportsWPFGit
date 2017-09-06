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
    class ListContractTrainers : ObservableCollection<Trainer>
    {
        public ListContractTrainers()
        {
            var listConTr = ContractsPage.DataEntitiesContracts.Trainers;

            var queryConTr = from em in listConTr
                             orderby em.Family
                             select em;

            foreach (var em in queryConTr)
            {
                this.Add(em);
            }
        }
    }
}
