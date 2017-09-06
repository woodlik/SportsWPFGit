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
    class ListTrainer : ObservableCollection<Trainer>
    {
        public ListTrainer()
        {
            var listTra = Trainers.DataEntitiesTrainers.Trainers;


            var queryTra = from tra in listTra
                           orderby tra.Family
                           select tra;

            foreach (var tra in queryTra)
            {
                this.Add(tra);
            }
        }
    }
}
