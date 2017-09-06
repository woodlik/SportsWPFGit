using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using System.Data;
using Sports.Classes;
using System.ComponentModel;
using System.Windows.Data;
using DataBase;
using Sports.NewObject;

namespace Sports.Forms
{
    /// <summary>
    /// Interaction logic for Trainers.xaml
    /// </summary>
    public partial class Trainers : Page
    {
        public static SportsEntities DataEntitiesTrainers { get; set; }
        ObservableCollection<Trainer> ListTrainers;

        #region Конструктор
        public Trainers()
        {

            DataEntitiesTrainers = new SportsEntities();
            InitializeComponent();
            ListTrainers = new ObservableCollection<Trainer>();

        }
        #endregion

        #region Метод загрузки страницы
        private void Page_LoadedTrainers(object sender, RoutedEventArgs e)
        {

            ZagrTra();
            tbSt.Text = "ЗАГРУЖЕНО";
        }
        #endregion

        #region Метод заполнения DataGrid
        private void ZagrTra()

        {

            ListTrainers.Clear();
            var trainers = DataEntitiesTrainers.Trainers;
            var queryTrainer = from trainer in trainers
                                orderby trainer.TrainerId
                                select trainer;
            foreach (Trainer trainer in queryTrainer)
            {
                ListTrainers.Add(trainer);
            }
            dgTrainers.ItemsSource = ListTrainers;
            tbCount.Text = Convert.ToString(ListTrainers.Count());
            tbDate.Text = Convert.ToString(DateTime.Today.ToString("dd MMMM yyyy"));
        }
        #endregion

        #region Метод вызова окна новый сотрудник
        private void clNewTrainer(object sender, RoutedEventArgs e)
        {

            var newTra = new wNewTrainers(ListTrainers);
            newTra.Closed += newTra_Closed;
            newTra.ShowDialog();

        }
        #endregion

        #region Метод делегат на закрытие окна новый сотрудник
        void newTra_Closed(object sender, EventArgs e)
        {

            ZagrTra();

        }
        #endregion


        private void clDeleteTrainer(object sender, RoutedEventArgs e)
        {
            Trainer tra = dgTrainers.SelectedItem as Trainer;
            if (tra != null)
            {
                MessageBoxResult result =
                    MessageBox.Show("Удалить данные", "Предупреждение", MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    DataEntitiesTrainers.Trainers.Remove(tra);

                    dgTrainers.SelectedIndex = dgTrainers.SelectedIndex == 0 ? 1 : dgTrainers.SelectedIndex - 1;
                    ListTrainers.Remove(tra);
                    DataEntitiesTrainers.SaveChanges();
                    tbCount.Text = Convert.ToString(ListTrainers.Count());
                }
            }
            else
            {
                MessageBox.Show("Выберите строку для удаления");
            }
        }

        private void clSaveTrainer(object sender, RoutedEventArgs e)
        {
            DataEntitiesTrainers.SaveChanges();
            dgTrainers.IsReadOnly = true;
            tbSt.Text = "СОХРАНЕНО";
        }
        private void clEditTrainer(object sender, RoutedEventArgs e)
        {
            dgTrainers.IsReadOnly = false;
            dgTrainers.BeginEdit();
            tbSt.Text = "РЕДАКТИРУЕТСЯ";
        }
        private void clRefreshTrainer(object sender, RoutedEventArgs e)
        {
            ZagrTra();
        }

        private void search_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox t = (TextBox)sender;
            String filter = t.Text;
            ICollectionView cv = CollectionViewSource.GetDefaultView(dgTrainers.ItemsSource);
            if (filter == "")
                cv.Filter = null;
            else
            {
                cv.Filter = o =>
                {
                    Trainer tr = o as Trainer;
                    if (cbosearch.SelectedValue != null)
                    {
                        String selected = cbosearch.Text.ToString().ToLower();
                        if (selected == "фамилия")
                            return (tr.Family.ToLower().StartsWith(filter.ToLower()));
                        else if (selected == "имя")
                            return (tr.Name.ToLower().StartsWith(filter.ToLower()));
                        else if (selected == "специализация")
                            return (tr.Post.ToLower().StartsWith(filter.ToLower()));
                        else
                            return false;
                    }
                    else
                        return false;
                };
            }
        }

        #region Метод выход из программы
        private void clExit(object sender, RoutedEventArgs e)
        {
            Other.Exit();
        }
        #endregion

       
    }
}
