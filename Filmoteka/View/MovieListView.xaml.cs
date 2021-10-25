using Filmoteka.DAO;
using Filmoteka.Model;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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
