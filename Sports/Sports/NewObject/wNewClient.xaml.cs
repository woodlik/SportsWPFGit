using System;
using System.Windows;
using DataBase;
using DataAccess.Data;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using Sports.Core;

namespace Sports.NewObject
{
    /// <summary>
    /// Interaction logic for wNewClient.xaml
    /// </summary>
    public partial class wNewClient : Window
    {
        SportsEntities db;
        SPORTSFACTORY spfac;
        ObservableCollection<Client> ClientList;
        public wNewClient(ObservableCollection<Client> clilist)
        {
            db = new SportsEntities();
            InitializeComponent();
            spfac = new SPORTSFACTORY();
            this.ClientList = clilist;
        }
        

        private void AddNewClient(object sender, RoutedEventArgs e)
        {
            
            String fam = tbFamCl.Text;
            if(!Regex.IsMatch(fam, RagexMask.FioMask))
            {
                MessageBox.Show(ErrorList.Surname);
                return;
            }
            String name = tbNameCl.Text;
            if (!Regex.IsMatch(name, RagexMask.Name))
            {
                MessageBox.Show(ErrorList.Name);
                return;
            }
            String thirdname = tbPatCl.Text;
            if (!Regex.IsMatch(thirdname, RagexMask.FioMask))
            {
                MessageBox.Show(ErrorList.PatronymicName);
                return;
            }
            String ad = tbAdCl.Text;
            if (!Regex.IsMatch(ad, RagexMask.AddressMask))
            {
                MessageBox.Show(ErrorList.Address);
                return;
            }
            String tel = tbTelephoneCl.Text;
            if (!Regex.IsMatch(tel, RagexMask.PhoneMask))
            {
                MessageBox.Show(ErrorList.Phone);
                return;
            }
            if (db.Clients.Where(i => i.Family == fam).FirstOrDefault() != null 
                && db.Clients.Where(i => i.Name == name).FirstOrDefault() != null 
                && db.Clients.Where(i => i.ThirdName == thirdname).FirstOrDefault() != null)
            {
                MessageBox.Show("Такой клиент уже есть в базе!");
                return;
            }

            if (!string.IsNullOrEmpty(fam)
                && !string.IsNullOrEmpty(name)
                && !string.IsNullOrEmpty(thirdname)
                && !string.IsNullOrEmpty(ad)
                && !string.IsNullOrEmpty(tel))
            {                
                try
                {
                    MessageBoxResult result =
                            MessageBox.Show("Добавить нового клиента?", "Проверка данных", MessageBoxButton.OKCancel);
                    if (result == MessageBoxResult.OK)
                    {
                        Client cli = new Client();

                        cli.Family = fam;
                        cli.Name = name;
                        cli.ThirdName = thirdname;
                        cli.Adress = ad;
                        cli.Phone = tel;

                        ClientList.Add(spfac.AddClient(cli));
                        this.add_btn.IsEnabled = true;

                        MessageBox.Show("Новый клиент добавлен!", "Статус операции");
                        ClearItems();
                        this.add_btn.IsEnabled = true;
                    }
                }
                catch
                {
                    MessageBox.Show(" Добавление невозможно \n Проверьте заполнение полей!!!", "Ошибка добавления");
                }
            }
            else
            {
                MessageBox.Show("Все поля должны быть заполнены!", "Ошибка добавления");
                this.add_btn.IsEnabled = true;
                return;
            }
        }

        private void ClearNewCl(object sender, RoutedEventArgs e)
        {
            ClearItems();

            MessageBox.Show("Форма очищена!", "Статус операции");
        }
        private void ClearItems()
        {
            tbFamCl.Clear();
            tbNameCl.Clear();
            tbPatCl.Clear();
            tbAdCl.Clear();
            tbTelephoneCl.Clear();
        }
    }
}
