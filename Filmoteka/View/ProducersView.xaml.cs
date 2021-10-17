using Filmoteka.Model;
using Filmoteka.Repository;
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
