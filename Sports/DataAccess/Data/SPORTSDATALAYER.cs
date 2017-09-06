using System.Collections.Generic;
using System.Linq;
using DataBase;

namespace DataAccess.Data
{
    class SPORTSDATALAYER
    {
        SportsEntities db;

        public SPORTSDATALAYER()
        {
            db = new SportsEntities();
        }

        public Trainer AddTrainer(Trainer trn)
        {
            db.Trainers.Add(trn);
            db.SaveChanges();
            Trainer tran = db.Trainers.Where(x => x.TrainerId == trn.TrainerId).FirstOrDefault();
            return tran;
        }

        public Service AddService(Service service)
        {
            db.Services.Add(service);
            db.SaveChanges();
            Service serv = db.Services.Where(x => x.ServiceId == service.ServiceId).FirstOrDefault();
            return serv;
        }

        public Client AddClient(Client client)
        {
            db.Clients.Add(client);
            db.SaveChanges();
            Client clie = db.Clients.Where(x => x.ClientId == client.ClientId).FirstOrDefault();
            return clie;
        }

        public Contract AddContract(Contract contract)
        {
            db.Contracts.Add(contract);
            db.SaveChanges();
            Contract cont = db.Contracts.Where(x => x.ContractId == contract.ContractId).FirstOrDefault();
            return cont;
        }
        
        public List<Client> GetClients()
        {
            return db.Clients.ToList();
        }

        public List<Service> GetServices()
        {
            return db.Services.ToList();
        }

        public List<Trainer> GetTrainers()
        {
            return db.Trainers.ToList();
        }
        public List<Client> getClients()
        {
            return db.Clients.ToList();
        }

        public List<Service> getServices()
        {
            return db.Services.ToList();
        }

        public List<Trainer> getTrainers()
        {
            return db.Trainers.ToList();
        }
        public Trainer GetTrainerId(int TrainerId)
        {
            return db.Trainers.Where(x => (x.TrainerId == TrainerId)).FirstOrDefault();
        }
        public Client GetClientId(int ClientId)
        {
            return db.Clients.Where(x => (x.ClientId == ClientId)).FirstOrDefault();
        }

        
        public Service GetServiceId(int ServiceId)
        {
            return db.Services.Where(x => (x.ServiceId == ServiceId)).FirstOrDefault();
        }
    }
}
