using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using System.Data;
using Sports.Classes;
using System.Windows.Data;
using System.ComponentModel;
using DataBase;
using Sports.NewObject;

namespace Sports.Forms
{
    /// <summary>
    /// Interaction logic for Services.xaml
    /// </summary>
    public partial class Services : Page
    {
        public static SportsEntities DataEntitiesServices { get; set; }
        ObservableCollection<Service> ListServices;

        public Services()
        {
            DataEntitiesServices = new SportsEntities();
            InitializeComponent();
            ListServices = new ObservableCollection<Service>();
        }


        private void Page_LoadedServices(object sender, RoutedEventArgs e)
        {
            ZagrServ();
            tbSt.Text = "ЗАГРУЖЕНО";
        }

        private void ZagrServ()
        {
            ListServices.Clear();
            var services = DataEntitiesServices.Services;
            var queryService = from service in services
                               orderby service.ServiceId
                               select service;
            foreach (Service service in queryService)
            {
                ListServices.Add(service);
            }
            dgServices.ItemsSource = ListServices;
            tbCount.Text = Convert.ToString(ListServices.Count());
            tbDate.Text = Convert.ToString(DateTime.Today.ToString("dd MMMM yyyy"));
        }

        private void clNewService(object sender, RoutedEventArgs e)
        {

            var newServ = new wNewServices(ListServices);
            newServ.Closed += newServ_Closed;
            newServ.ShowDialog();

        }

        void newServ_Closed(object sender, EventArgs e)
        {
            ZagrServ();
        }

        private void clDeleteServ(object sender, RoutedEventArgs e)
        {
            Service serv = dgServices.SelectedItem as Service;
            if (serv != null)
            {
                MessageBoxResult result =
                    MessageBox.Show("Удалить данные", "Предупреждение", MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    DataEntitiesServices.Services.Remove(serv);

                    dgServices.SelectedIndex = dgServices.SelectedIndex == 0 ? 1 : dgServices.SelectedIndex - 1;
                    ListServices.Remove(serv);
                    DataEntitiesServices.SaveChanges();
                    tbCount.Text = Convert.ToString(ListServices.Count());
                }
            }
            else
            {
                MessageBox.Show("Выберите строку для удаления");
            }
        }

        private void clSaveServ(object sender, RoutedEventArgs e)
        {
            DataEntitiesServices.SaveChanges();
            dgServices.IsReadOnly = true;
            tbSt.Text = "СОХРАНЕНО";
        }

        private void clEditServ(object sender, RoutedEventArgs e)
        {
            dgServices.IsReadOnly = false;
            dgServices.BeginEdit();
            tbSt.Text = "РЕДАКТИРУЕТСЯ";
        }

        private void clRefreshServ(object sender, RoutedEventArgs e)
        {
            ZagrServ();
        }

        private void search_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox t = (TextBox)sender;
            String filter = t.Text;
            ICollectionView cv = CollectionViewSource.GetDefaultView(dgServices.ItemsSource);
            if (filter == "")
                cv.Filter = null;
            else
            {
                cv.Filter = o =>
                {
                    Service co = o as Service;

                    if (cbosearch.SelectedValue != null)
                    {
                        String selected = cbosearch.Text.ToString().ToLower();
                        if (selected == "наименование услуги")
                            return (co.Title.ToLower().StartsWith(filter.ToLower()));
                        else if (selected == "код услуги")
                            return (co.ServiceId.ToString().StartsWith(filter.ToLower()));
                      
                        else
                            return false;
                    }
                    else
                        return false;
                };
            }
        }

        private void pdExit(object sender, RoutedEventArgs e)
        {
            Other.Exit();
        }

    }
}
