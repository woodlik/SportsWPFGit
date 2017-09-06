using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.Windows.Controls.DataVisualization.Charting;
using DataBase;

namespace Sports
{
    /// <summary>
    /// Interaction logic for StatControl.xaml
    /// </summary>
    public partial class StatControl : UserControl
    {
        private ObservableCollection<object> MyCollection;
        public StatControl()
        {
            InitializeComponent();
        }

        public void MyPie_OnLoaded(object sender, RoutedEventArgs e)
        {
            using (var db = new SportsEntities())
            {
                MyCollection = new ObservableCollection<object>();

                var query = from c in db.Contracts
                            from x in db.Trainers
                            where c.TrainerId == x.TrainerId 
                            group c by x.Family
                            into g
                            select new { Name = g.Key, ClientCount = g.Count() };
                foreach (var item in query)
                {
                    MyCollection.Add(item);
                }
                ((PieSeries)MyPie.Series[0]).ItemsSource = MyCollection;

            }
        }
    }
}
