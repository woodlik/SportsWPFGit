using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Collections.ObjectModel;
using System.Data;
using System.ComponentModel;
using DataBase;
using System.Xml.Linq;
using Sports.NewObject;
using Sports.Classes;
using Sports.Core;
using System.Text.RegularExpressions;

namespace Sports.Forms
{
    /// <summary>
    /// Interaction logic for Clients.xaml
    /// </summary>
    public partial class Clients : Page
    {
        public static SportsEntities DataEntitiesClients { get; set; }
        ObservableCollection<Client> ListClients;

        public Clients()
        {
            DataEntitiesClients = new SportsEntities();
            InitializeComponent();
            ListClients = new ObservableCollection<Client>();
        }

        private void Page_LoadedClients(object sender, RoutedEventArgs e)
        {
            ZagruzkaCl();
            tbSt.Text = "ЗАГРУЖЕНО";
        }


        private void ZagruzkaCl()
        {
            ListClients.Clear();
            var clients = DataEntitiesClients.Clients;
            var queryClient = from client in clients
                              orderby client.ClientId
                              select client;
            foreach (Client client in queryClient)
            {
                ListClients.Add(client);
            }
            dgClients.ItemsSource = ListClients;
            tbCount.Text = Convert.ToString(ListClients.Count());
            tbDate.Text = Convert.ToString(DateTime.Today.ToString("dd MMMM yyyy"));
        }
        
        private void clNewClient(object sender, RoutedEventArgs e)
        {

            var newClient = new wNewClient(ListClients);
            newClient.Closed += newClient_Closed;
            newClient.ShowDialog();

        }

        void newClient_Closed(object sender, EventArgs e)
        {
            ZagruzkaCl();
        }
        
        private void clDeleteClient(object sender, RoutedEventArgs e)
        {
            Client cl = dgClients.SelectedItem as Client;
            if (cl != null)
            {
                MessageBoxResult result =
                    MessageBox.Show("Удалить данные", "Предупреждение", MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    DataEntitiesClients.Clients.Remove(cl);

                    dgClients.SelectedIndex = dgClients.SelectedIndex == 0 ? 1 : dgClients.SelectedIndex - 1;
                    ListClients.Remove(cl);

                    DataEntitiesClients.SaveChanges();
                    tbCount.Text = Convert.ToString(ListClients.Count());
                }
            }
            else
            {
                MessageBox.Show("Выберите строку для удаления");
            }
        }

        private void clSaveClient(object sender, RoutedEventArgs e)
        {
            
            DataEntitiesClients.SaveChanges();
            dgClients.IsReadOnly = true;
            tbSt.Text = "СОХРАНЕНО";
        }

        private void clEditClient(object sender, RoutedEventArgs e)
        {
            dgClients.IsReadOnly = false;
            dgClients.BeginEdit();
            
            tbSt.Text = "РЕДАКТИРУЕТСЯ";
        }

        private void clRefreshClient(object sender, RoutedEventArgs e)
        {
            ZagruzkaCl();
        }

        private void clientsearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox t = (TextBox)sender;
            String filter = t.Text;
            ICollectionView cv = CollectionViewSource.GetDefaultView(dgClients.ItemsSource);
            if (filter == "")
                cv.Filter = null;
            else
            {
                cv.Filter = o =>
                {
                    Client cl = o as Client;
                    if (cbosearch.SelectedValue != null)
                    {
                        String selected = cbosearch.Text.ToString().ToLower();
                        if (selected == "фамилия")
                            return (cl.Family.ToLower().StartsWith(filter.ToLower()));                        
                        else if (selected == "телефон")
                            return (cl.Phone.ToLower().StartsWith(filter.ToLower()));
                        else if (selected == "имя")
                            return (cl.Name.ToLower().StartsWith(filter.ToLower()));
                        else
                            return false;
                    }
                    else
                        return false;
                };
            }
        }

        private void clExit(object sender, RoutedEventArgs e)
        {
            Other.Exit();
        }

        private void Save_Table_Click(object sender, RoutedEventArgs e)
        {
            SportsEntities db = new SportsEntities();

            var clients = from c in db.Clients
                           select new
                           {
                               c.ClientId,
                               c.Family,
                               c.Name,
                               c.ThirdName,
                               c.Adress,
                               c.Phone,
                               
                           };
           
            XElement xlinq = new XElement("Client",
                                             from c in db.Clients.AsEnumerable()
                                             select new XElement("Client", 
                                             new XAttribute("ClientId", c.ClientId),
                                             new XElement("Family", c.Family),
                                             new XElement("Name", c.Name),
                                             new XElement("ThirdName", c.ThirdName),
                                             new XAttribute("Adress", c.Adress),
                                             new XAttribute("Phone", c.Phone)));
            xlinq.Save("NewXML.xml");
            Console.WriteLine(xlinq);
            Console.Read();
               
        }
        private void Open_Table_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
