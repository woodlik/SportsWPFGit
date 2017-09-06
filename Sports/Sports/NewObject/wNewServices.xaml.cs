using System;
using System.Windows;
using System.Collections.ObjectModel;
using DataAccess.Data;
using DataBase;
using System.Linq;
using System.Text.RegularExpressions;
using Sports.Core;

namespace Sports.NewObject
{
    /// <summary>
    /// Interaction logic for wNewServices.xaml
    /// </summary>
    public partial class wNewServices : Window
    {
        SportsEntities db;
        SPORTSFACTORY spfac;
        ObservableCollection<Service> ServiceList;
        public wNewServices(ObservableCollection<Service> servicelist)
        {
            db = new SportsEntities();
            InitializeComponent();
            spfac = new SPORTSFACTORY();
            this.ServiceList = servicelist;
        }
        private void AddNewServ(object sender, RoutedEventArgs e)
        {
            try
            {
                String title = tbNameServ.Text;
                if (!Regex.IsMatch(title, RagexMask.Text))
                    {
                    MessageBox.Show("В тексте должна быть только буквы кирилицы и знаки(,.-!;'\")");
                    }
                
                Double price = Convert.ToDouble(tbPrice.Text);
                if (price < 0 || price >= 1000)
                {
                    MessageBox.Show("Введите цену");
                    return;
                }

                if (db.Services.Where(i => i.Title == title).FirstOrDefault() != null)
                {
                    MessageBox.Show("Такой сервис уже существует");
                    return;
                }

                MessageBoxResult result =
                MessageBox.Show("Добавить новую услугу?", "Проверка данных", MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    Service ser = new Service();

                    ser.Title = title;
                    ser.Price = price;

                    ServiceList.Add(spfac.AddService(ser));
                    this.add_btn.IsEnabled = true;

                    MessageBox.Show("Услуга добавлена!", "Статус операции");
                    TbClear();
                }
            }
            catch
            {
                MessageBox.Show(" Добавление невозможно,\n проверьте заполнение полей!!!", "Ошибка добавления");
            }

        }
        private void ClearNewServ(object sender, RoutedEventArgs e)
        {
            TbClear();
        }
        private void TbClear()
        {
            tbNameServ.Clear();
            tbPrice.Clear();
        }
    }
}
