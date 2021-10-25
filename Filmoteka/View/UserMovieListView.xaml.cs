using Filmoteka.DAO;
using Filmoteka.Model;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Filmoteka.View
{
    /// <summary>
    /// Interaction logic for UserMovieListView.xaml
    /// </summary>
    public partial class UserMovieListView : Window
    {
        private readonly User CurrentUser;
        public UserMovieListView(User user)
        {
            InitializeComponent();
            CurrentUser = user;
            textBlockHello.Text += ", " + CurrentUser.Username;
            SizeChanged += OnWindowSizeChanged;
            var movies = MovieDAO.GetAllMovies();
            if (movies.Count > 0)
            {
                ListViewMovies.ItemsSource = movies;
            }
        }



        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void MovieClicked(object sender, MouseButtonEventArgs e)
        {
            int movieId = ((Movie)(((StackPanel)sender).DataContext)).ID;
            new MovieDetailsView(MovieDAO.GetById(movieId), CurrentUser).Show();
        }


        private void Btn_Click_Filter(object sender, RoutedEventArgs e)
        {
            var movies = MovieDAO.GetAllMovies().FindAll(movie => movie.Name.ToUpper().Contains(searchTextBox.Text.ToUpper()));
            ListViewMovies.ItemsSource = movies.Count > 0 ? movies : new List<Movie>();

        }

        private void Btn_Click_Reset(object sender, RoutedEventArgs e)
        {
            searchTextBox.Text = "";
            var movies = MovieDAO.GetAllMovies();
            if (movies.Count > 0)
                ListViewMovies.ItemsSource = movies;
            
        }

        private void Btn_Click_Logout(object sender, RoutedEventArgs e)
        {
            new LoginView().Show();
            Close();
        }

        protected void OnWindowSizeChanged(object sender, SizeChangedEventArgs e)
        {
            border.Height = e.NewSize.Height * 0.87;
        }
    }
}
