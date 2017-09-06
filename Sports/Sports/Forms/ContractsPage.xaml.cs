using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using System.Data;
using System.Collections;
using Sports.Classes;
using System.Windows.Data;
using System.ComponentModel;
using DataBase;
using Sports.NewObject;

namespace Sports.Forms
{
    /// <summary>
    /// Логика взаимодействия для ContractsPage.xaml
    /// </summary>
    public partial class ContractsPage : Page
    {
        public static SportsEntities DataEntitiesContracts { get; set; }
        ObservableCollection<Contract> ListContracts;
        
        public ContractsPage()
        {
            DataEntitiesContracts = new SportsEntities();
            InitializeComponent();
            
            ListContracts = new ObservableCollection<Contract>();
        }
        private void Page_LoadedContracts(object sender, RoutedEventArgs e)
        {
            ZagrContracts();
            tbSt.Text = "ЗАГРУЖЕНО";
        }
        private void clNewContract(object sender, RoutedEventArgs e)
        {
            var newContract = new wNewContract(ListContracts);
            newContract.Closed += newContract_Closed;
            newContract.ShowDialog();
        }
        void newContract_Closed(object sender, EventArgs e)
        {
            ZagrContracts();
        }
                             
        private void ZagrContracts()
        {
            ListContracts.Clear();
            var contracts = DataEntitiesContracts.Contracts;
            var queryContract = from contract in contracts
                                orderby contract.ContractId
                             select contract;
            foreach (Contract contract in queryContract)
            {
                ListContracts.Add(contract);
            }
            dgContracts.ItemsSource = ListContracts;
            tbCount.Text = Convert.ToString(ListContracts.Count());
            tbDate.Text = Convert.ToString(DateTime.Today.ToString("dd MMMM yyyy"));
        }
       
        private void clDeleteContract(object sender, RoutedEventArgs e)
        {
            Contract con = dgContracts.SelectedItem as Contract;
            if (con != null)
            {
                MessageBoxResult result =
                    MessageBox.Show("Удалить данные о заказе?", "Предупреждение !", MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    DataEntitiesContracts.Contracts.Remove(con);

                    dgContracts.SelectedIndex = dgContracts.SelectedIndex == 0 ? 1 : dgContracts.SelectedIndex - 1;
                    ListContracts.Remove(con);

                    DataEntitiesContracts.SaveChanges();

                    tbCount.Text = Convert.ToString(ListContracts.Count());
                }
            }
            else
            {
                MessageBox.Show("Выберите строку для удаления");
            }
        }
       
        private void clSaveContract(object sender, RoutedEventArgs e)
        {
            DataEntitiesContracts.SaveChanges();
            dgContracts.IsReadOnly = true;
            tbSt.Text = "СОХРАНЕНО";
        }
        
        private void clEditContract(object sender, RoutedEventArgs e)
        {
            dgContracts.IsReadOnly = false;
            dgContracts.BeginEdit();
            tbSt.Text = "РЕДАКТИРУЕТСЯ";
        }
              
        private void clRefreshContract(object sender, RoutedEventArgs e)
        {
            ZagrContracts();
        }
        
        private void clExit(object sender, RoutedEventArgs e)
        {
            Other.Exit();
        }

        private void contrctsearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox t = (TextBox)sender;
            String filter = t.Text;
            ICollectionView cv = CollectionViewSource.GetDefaultView(dgContracts.ItemsSource);
            if (filter == "")
                cv.Filter = null;
            else
            {
                cv.Filter = o =>
                {
                    Contract co = o as Contract;
                    
                    if (cbosearch.SelectedValue != null)
                    {
                        String selected = cbosearch.Text.ToString().ToLower();
                        if (selected == "тип оплаты")
                            return (co.Payment.ToLower().StartsWith(filter.ToLower()));
                        else if (selected == "услуга")
                            return (co.Service.Title.ToLower().StartsWith(filter.ToLower()));                        
                        else if (selected == "фамилия клиента")
                            return (co.Client.Family.ToLower().StartsWith(filter.ToLower()));
                        else if (selected == "фамилия тренера")
                            return (co.Trainer.Family.ToLower().StartsWith(filter.ToLower()));
                        else
                            return false;
                    }
                    else
                        return false;
                };
            }
        }

    }
}
