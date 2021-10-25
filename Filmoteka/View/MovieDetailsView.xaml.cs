using Filmoteka.DAO;
using Filmoteka.Model;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Filmoteka.View
{
    /// <summary>
    /// Interaction logic for MovieDetailsView.xaml
    /// </summary>
    public partial class MovieDetailsView : Window
    {
        private readonly User CurrentUser;
        private readonly int MovieId;
        public MovieDetailsView()
        {
            InitializeComponent();
        }

        public MovieDetailsView(Movie movie, User currentUser)
        {
            InitializeComponent();
            CurrentUser = currentUser;
            MovieId = movie.ID;
            string tempGenres = ": ";
            SizeChanged += OnWindowSizeChanged;
            ListViewCast.ItemsSource = movie.Stars;
            ListViewProducers.ItemsSource = movie.Producers;
            textBlockTitle.Text = movie.Name;
            movie.Genres.ForEach(genre => tempGenres += genre + ", ");
            textBlockGenres.Text = textBlockGenres.Text + tempGenres[0..^2];
            textBlockDuration.Text = movie.Duration + " min";
            textBlockCountryOfOrigin.Text = textBlockCountryOfOrigin.Text+": "+  movie.OriginCountry;
            textBlockLanguage.Text = textBlockLanguage.Text +": "+ movie.Language;
            textBlockStoryLine.Text = movie.Description;
            
            if(movie.Ratings != null && movie.Ratings.Count != 0)
            {
                ratingTextBlock.Text = movie.Ratings.Average(rating => rating.RatingScore) + "/5.0";
                Model.Rating r = movie.Ratings.Find(rating => rating.UserId == CurrentUser.ID);
                if (r != null)
                    rating.Value = r.RatingScore / 5.0;
            }

            if(movie.Image != null)
            {
                imagePanel.Children.Clear();
                Image image = new();
                image.Source = movie.Image;
                imagePanel.Children.Add(image);
            }
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void CastClicked(object sender, MouseButtonEventArgs e)
        {
            new UserPersonView((Star)(((StackPanel)sender).DataContext)).Show();
        }

        private void ProducerClicked(object sender, MouseButtonEventArgs e)
        {
            new UserPersonView((Producer)(((StackPanel)sender).DataContext)).Show();
        }

        private void RatingClick(object sender, RoutedEventArgs e)
        {
            bool success = MovieDAO.RateMovie(MovieId, Convert.ToInt32(rating.Value * 5), CurrentUser.ID);
            if (success)
                MessageBox.Show(FindResource("movieRatedSuccessfully") as string, "Info");
            else
                MessageBox.Show("Couldn't rate movie! ", "Info");
        }

        protected void OnWindowSizeChanged(object sender, SizeChangedEventArgs e)
        {
            border.Height = e.NewSize.Height * 0.87;
        }
    }
}
