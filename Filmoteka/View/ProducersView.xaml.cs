using Filmoteka.DAO;
using Filmoteka.Model;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Filmoteka.View
{
    /// <summary>
    /// Interaction logic for ProducersView.xaml
    /// </summary>
    public partial class ProducersView : UserControl
    {
        public ProducersView()
        {
            InitializeComponent();
            var producers = ProducerDAO.GetProducers();
            if (producers.Count > 0)
                ListViewProducers.ItemsSource = producers;
        }

        private void ProducerClicked(object sender, MouseButtonEventArgs e)
        {
            new AddOrEditProducerView((Producer)(((StackPanel)sender).DataContext)).Show();
        }

        private void AddAProducer_Btn_Click(object sender, RoutedEventArgs e)
        {
            new AddOrEditProducerView().Show();
        }
    }
}
