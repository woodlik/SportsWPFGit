using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using System.Data;
using System.Xml.Linq;
using System.Windows.Navigation;
using System.Windows.Forms;
using DataBase;


namespace Sports.Forms
{
    /// <summary>
    /// Interaction logic for SelectedPage.xaml
    /// </summary>
    public partial class SelectedPage : Page
    {
        ObservableCollection<Client> ListClients;
        SportsEntities db;

        public SelectedPage()
        {
            InitializeComponent();
            ListClients = new ObservableCollection<Client>();
            db = new SportsEntities();

        }

        private void SaveDatabase(object sender, RoutedEventArgs e)
        {
            SportsEntities db = new SportsEntities();
            
            XDocument xlinq = new XDocument(new XElement("Client",
                                             from c in db.Clients.AsEnumerable()
                                             select new XElement("Client",
                                             new XAttribute("ClientId", c.ClientId),
                                             new XElement("Family", c.Family),
                                             new XElement("Name", c.Name),
                                             new XElement("ThirdName", c.ThirdName),
                                             new XElement("Adress", c.Adress),
                                             new XElement("Phone", c.Phone))));

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "XML files (*.xml)|*.xml";
            saveFileDialog.FilterIndex = 0;
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.CreatePrompt = true;
            saveFileDialog.FileName = null;
            saveFileDialog.Title = "Save path of the file to be exported";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                xlinq.Save(saveFileDialog.FileName);
            }
        }

        private void LoadDatabase(object sender, RoutedEventArgs e)
        {
            string s = "default.xml";
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter =
               "xml files (*.xml)|*.xml|All files (*.*)|*.*";
            dialog.Title = "Select a xml file";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                s = dialog.FileName;
            }
           
        }
    
        private void View_Clients_Window(object sender, RoutedEventArgs e)
        {
            NavigationService nav;
            nav = NavigationService.GetNavigationService(this);
            nav.Navigate(new System.Uri("Forms/Clients.xaml", UriKind.RelativeOrAbsolute));
        }
       
        private void View_Trainers_Window(object sender, RoutedEventArgs e)
        {
            NavigationService nav;
            nav = NavigationService.GetNavigationService(this);
            nav.Navigate(new System.Uri("Forms/Trainers.xaml", UriKind.RelativeOrAbsolute));
        }
        private void View_Contracts_Window(object sender, RoutedEventArgs e)
        {
            NavigationService nav;
            nav = NavigationService.GetNavigationService(this);
            nav.Navigate(new System.Uri("Forms/ContractsPage.xaml", UriKind.RelativeOrAbsolute));
        }

        private void View_Stat_Control(object sender, RoutedEventArgs e)
        {
            NavigationService nav;
            nav = NavigationService.GetNavigationService(this);
            nav.Navigate(new System.Uri("StatControl.xaml", UriKind.RelativeOrAbsolute));
        }
        private void View_Services_Window(object sender, RoutedEventArgs e)
        {
            NavigationService nav;
            nav = NavigationService.GetNavigationService(this);
            nav.Navigate(new System.Uri("Forms/Services.xaml", UriKind.RelativeOrAbsolute));
        }

        private void AboutProgramm(object sender, RoutedEventArgs e)
        {

        }
    }
}
