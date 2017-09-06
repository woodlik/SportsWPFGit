using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBase;

namespace DataAccess.Data
{
    public class SPORTSFACTORY
    {
        SPORTSDATALAYER spda;
        public SPORTSFACTORY()
        {
            spda = new SPORTSDATALAYER();
        }

        public Trainer AddTrainer(Trainer trn)
        {
            return spda.AddTrainer(trn);
        }
        
        public Service AddService(Service service)
        {
            return spda.AddService(service);
        }

        public Client AddClient(Client client)
        {
            return spda.AddClient(client);
        }
        public List<Service> getServices()
        {
            return spda.getServices();
        }

        public List<Client> getClients()
        {
            return spda.getClients();
        }

        public List<Trainer> getTrainers()
        {
            return spda.getTrainers();
        }
        
        public Contract AddContract(Contract contract)
        {
            return spda.AddContract(contract);
        }

        public List<Service> GetServices()
        {
            return spda.GetServices();
        }

        public List<Client> GetClients()
        {
            return spda.GetClients();
        }

        public List<Trainer> GetTrainers()
        {
            return spda.GetTrainers();
        }

        public Trainer GetTrainerId(int TrainerId)
        {
            return spda.GetTrainerId(TrainerId);
        }
        
        public Client GetClientId(int ClientId)
        {
            return spda.GetClientId(ClientId);
        }
        public Service GetServiceId(int ServiceId)
        {
            return spda.GetServiceId(ServiceId);
        }
    }
}
