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
    /// Interaction logic for CastView.xaml
    /// </summary>
    public partial class CastView : UserControl
    {
        public CastView()
        {
            InitializeComponent();
            var cast = StarDAO.GetCast();
            if (cast.Count > 0)
                ListViewCast.ItemsSource = cast;
        }

        private void CastClicked(object sender, MouseButtonEventArgs e)
        {
            new AddOrEditCastView((Star)(((StackPanel)sender).DataContext)).Show();
        }

        private void AddACast_Btn_Click(object sender, RoutedEventArgs e)
        {
            new AddOrEditCastView().Show();
        }
    }
}
