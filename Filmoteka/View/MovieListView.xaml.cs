using Filmoteka.Model;
using Filmoteka.DAO;
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
    /// Interaction logic for MovieListView.xaml
    /// </summary>
    public partial class MovieListView : UserControl
    {
        public MovieListView()
        {
            InitializeComponent();
            var movies = MovieDAO.GetAllMovies();
            if (movies.Count > 0)
            {
                ListViewMovies.ItemsSource = movies;
            }
        }


        private void AddAMovie_Btn_Click(object sender, RoutedEventArgs e)
        {
            new AddMovieView().Show();
        }

        private void MovieClicked(object sender, MouseButtonEventArgs e)
        {
            new AddMovieView((Movie)(((StackPanel)sender).DataContext)).Show();
        }
    }
}
