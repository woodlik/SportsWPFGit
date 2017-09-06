using System;
using System.Collections.ObjectModel;
using System.Windows;
using DataAccess.Data;
using DataBase;
using Sports.Core;
using System.Text.RegularExpressions;
using System.Linq;

namespace Sports.NewObject
{
    /// <summary>
    /// Interaction logic for wNewTrainer.xaml
    /// </summary>
    public partial class wNewTrainers : Window
    {

        SPORTSFACTORY spfac;
        SportsEntities db;
        ObservableCollection<Trainer> TrainerList;
        public wNewTrainers(ObservableCollection<Trainer> tralist)
        {
            db = new SportsEntities();
            InitializeComponent();
            spfac = new SPORTSFACTORY();
            this.TrainerList = tralist;

        }
        private void AddNewTrainer(object sender, RoutedEventArgs e)
        {
            String family = tbFamTra.Text;
            if (!Regex.IsMatch(family, RagexMask.FioMask))
            {
                MessageBox.Show(ErrorList.Surname);
                return;
            }
            String name = tbNameTra.Text;
            if (!Regex.IsMatch(name, RagexMask.Name))
            {
                MessageBox.Show(ErrorList.Name);
                return;
            }
            String thirdname = tbPatTra.Text;
            if (!Regex.IsMatch(thirdname, RagexMask.Name))
            {
                MessageBox.Show(ErrorList.PatronymicName);
                return;
            }
            String post = tbTitTra.Text;
            if (!Regex.IsMatch(post, RagexMask.Text))
            {
                MessageBox.Show(ErrorList.OnlyText);
                return;
            }
            String phone = tbTelTra.Text;
            if (!Regex.IsMatch(phone, RagexMask.PhoneMask))
            {
                MessageBox.Show(ErrorList.Phone);
                return;
            }
            if (db.Trainers.Where(i => i.Family == family).FirstOrDefault() != null
                && db.Trainers.Where(i => i.Name == name).FirstOrDefault() != null
                && db.Clients.Where(i => i.ThirdName == thirdname).FirstOrDefault() != null)
            {
                MessageBox.Show("Такой сотрудник уже есть в базе!");
                return;
            }
            if (!string.IsNullOrEmpty(family)
                && !string.IsNullOrEmpty(name)
                && !string.IsNullOrEmpty(thirdname)
                && !string.IsNullOrEmpty(post)
                && !string.IsNullOrEmpty(phone))
            {
                MessageBoxResult result =
                                        MessageBox.Show("Оформить новый контракт?", "Проверка данных", MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    try
                    {
                        Trainer tra = new Trainer();

                        tra.Family = family;
                        tra.Name = name;
                        tra.ThirdName = thirdname;
                        tra.Post = post;
                        tra.Phone = phone;
                        TrainerList.Add(spfac.AddTrainer(tra));
                        this.add_btn.IsEnabled = true;
                        MessageBox.Show("Новый тренер добавлен, Статус операции");
                        ClearItems();
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
          
        }
        private void ClearNewTrainer(object sender, RoutedEventArgs e)
        {
            ClearItems();
        }
        private void ClearItems()
        {
            tbFamTra.Clear();
            tbNameTra.Clear();
            tbPatTra.Clear();
            tbTitTra.Clear();
            tbTelTra.Clear();
        }
    }
}
