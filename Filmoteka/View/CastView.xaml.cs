using Filmoteka.DAO;
using Filmoteka.Model;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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
