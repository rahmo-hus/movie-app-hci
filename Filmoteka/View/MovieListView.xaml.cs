using Filmoteka.Model;
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
            var movies = GetMovies();
            if (movies.Count > 0)
            {
                ListViewMovies.ItemsSource = movies;
            }
        }

        private List<Movie> GetMovies()
        {
            return new List<Movie>()
             {
                 new Movie("Contra tiempo", "Someone", "/Assets/gr-stocks-q8P8YoR6erg-unsplash.jpg"),
                 new Movie("Some other movie", "Some else", "/Assets/jake-hills-23LET4Hxj_U-unsplash.jpg"),
                 new Movie("Just one more", "Alcos", "/Assets/lan-deng-eVqU1HTZL8E-unsplash.jpg"),
                 new Movie("Contra tiempo", "Someone", "/Assets/gr-stocks-q8P8YoR6erg-unsplash.jpg"),
                 new Movie("Some other movie", "Some else", "/Assets/jake-hills-23LET4Hxj_U-unsplash.jpg"),
                 new Movie("Just one more", "Alcos", "/Assets/lan-deng-eVqU1HTZL8E-unsplash.jpg"),
                 new Movie("Contra tiempo", "Someone", "/Assets/gr-stocks-q8P8YoR6erg-unsplash.jpg"),
                 new Movie("Some other movie", "Some else", "/Assets/jake-hills-23LET4Hxj_U-unsplash.jpg"),
                 new Movie("Just one more", "Alcos", "/Assets/lan-deng-eVqU1HTZL8E-unsplash.jpg")
             };
        }

        private void AddAMovie_Btn_Click(object sender, RoutedEventArgs e)
        {
            new AddMovieView().Show();
        }
    }
}
