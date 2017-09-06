using System;
using System.Windows;
using System.Collections.ObjectModel;
using DataAccess.Data;
using DataBase;
using System.Linq;
using System.Windows.Controls;
using System.Collections.Generic;

namespace Sports.NewObject
{
    /// <summary>
    /// Interaction logic for wNewContract.xaml
    /// </summary>
    public partial class wNewContract : Window
    {
        SPORTSFACTORY spfac;
        ObservableCollection<Contract> contractList;
        SportsEntities Db;
        public wNewContract(ObservableCollection<Contract> contractlist)
        {
            InitializeComponent();
            Db = new SportsEntities();
            spfac = new SPORTSFACTORY();
            this.contractList = contractlist;
            dpDateStart.DisplayDateStart = DateTime.Now;
            dpDateEnd.DisplayDateStart = DateTime.Now;
        }

        private void Stack_Loaded(object sender, RoutedEventArgs e)
        {
            cbClient.ItemsSource = spfac.getClients();
            cbClient.DisplayMemberPath = "Family";
            cbClient.SelectedValuePath = "ClientId";
            cbTrainer.ItemsSource = spfac.getTrainers();
            cbTrainer.DisplayMemberPath = "Family";
            cbTrainer.SelectedValuePath = "TrainerId";
            cbService.ItemsSource = spfac.getServices();
            cbService.DisplayMemberPath = "Title";
            cbService.SelectedValuePath = "ServiceId";
        }
       

        private void AddNewContract(object sender, RoutedEventArgs e)
        {
            
            Contract c = new Contract();
            try
            {
                
                int trainerid = int.Parse(cbTrainer.SelectedValue.ToString());
                int clientid = int.Parse(cbClient.SelectedValue.ToString());
                int serviceid = int.Parse(cbService.SelectedValue.ToString());
                
                Client cl = new Client();
                cl = spfac.GetClientId(clientid);
                Service se = new Service();
                se = spfac.GetServiceId(serviceid);
                Trainer tr = new Trainer();
                tr = spfac.GetTrainerId(trainerid);
                DateTime dateStart = new DateTime();
                DateTime dateEnd = new DateTime();
                dateStart = (DateTime)dpDateStart.SelectedDate;
                if (dpDateStart.SelectedDate != null)
                {
                    dateStart = (DateTime)dpDateStart.SelectedDate;
                }
                dateEnd = (DateTime)dpDateEnd.SelectedDate;
                if (dpDateEnd.SelectedDate != null)
                {
                    dateEnd = (DateTime)dpDateEnd.SelectedDate;
                }
                if (dateEnd > dateStart)
                {
                    MessageBox.Show("Проверьте корректность ввода даты\nДата начала занятий не может быть раньше даты окончания занятий");
                    return;
                }
                int quantity = Convert.ToInt32(tbQuantity.Text);
                string type = cbType.Text;
                string payment = cbPrices.Text;
                double serprice = 0;
                
                var serchg = Db.Services.Where(x => x.ServiceId == serviceid).Select(x => x.Price).ToList();
                foreach (double s in serchg)
                {
                    serprice += s;
                }
                double sumoffer = 0;
                
                if (cbType.Text == "Персональные")
                 {
                     sumoffer = serprice * quantity;
                 }
                 else if (cbType.Text == "Групповые")
                 {
                     sumoffer = (serprice * quantity) - ((serprice * quantity) * 5.00 / 100);
                }
                 lbSumOffer.Content = sumoffer.ToString();
                
                MessageBoxResult result =
                        MessageBox.Show("Оформить новый контракт?", "Проверка данных", MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    
                    c.DateStart = dateStart;
                    c.DateEnd = dateEnd;
                    c.TrainerId = trainerid;
                    c.ClientId = clientid;
                    c.ServiceId = serviceid;
                    c.Quantity = quantity;                    
                    c.Payment = payment;
                    c.Type = type;
                    c.Sum = sumoffer;
                    contractList.Add(spfac.AddContract(c));
                    
                    MessageBox.Show("Новый контракт оформлен!", "Статус операции");
                    ClearItems();
                    
                    this.add_btn.IsEnabled = false;
                }
            }
            catch
            {
                MessageBox.Show(" Добавление невозможно \n Проверьте заполнение полей!!!", "Ошибка добавления");
            }
            this.add_btn.IsEnabled = true;
        }

        private void ClearNewCon(object sender, RoutedEventArgs e)
        {
            ClearItems();
        }
        
        private void ClearItems()
        {
            dpDateStart.SelectedDate = null;
            dpDateEnd.SelectedDate = null;
            cbTrainer.Text = "";
            cbService.Text = "";
            cbClient.Text = "";
            cbPrices.Text = "";
            cbType.Text = "";
            tbQuantity.Text = "";
            lbSumOffer.Content = "";
        }
    }
}
