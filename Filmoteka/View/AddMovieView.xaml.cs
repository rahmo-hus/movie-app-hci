using Filmoteka.Model;
using Filmoteka.Repository;
using Microsoft.Win32;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
using System.Windows.Shapes;

namespace Filmoteka.View
{
    /// <summary>
    /// Interaction logic for AddMovieView.xaml
    /// </summary>
    public partial class AddMovieView : Window
    {
        BitmapImage MovieImage;
        private readonly int ID;
        public AddMovieView()
        {
            InitializeComponent();
            List<Country> countries = CountryDAO.GetGountries();
            List<Language> languages = LanguageDAO.GetLanguages();
            List<Genre> genres = GenreDAO.GetGenres();
            List<Star> stars = StarDAO.GetCast();
            List<Producer> producers = ProducerDAO.GetProducers();

            countries.ForEach(country => comboCountrySelect.Items.Add(country));
            languages.ForEach(language => comboLanguageSelect.Items.Add(language));
            genres.ForEach(genre => comboGenreSelect.Items.Add(genre));
            stars.ForEach(star => comboCastSelect.Items.Add(star));
            producers.ForEach(producer => comboProducerSelect.Items.Add(producer));
            
            SizeChanged += OnWindowSizeChanged;
        }

        public AddMovieView(Movie movie) : this()
        {
            ID = movie.ID;
            comboGenreSelect.SelectedItems = new ObservableCollection<Genre>(movie.Genres);
            comboCastSelect.SelectedItems = new ObservableCollection<Star>(movie.Stars);
            comboProducerSelect.SelectedItems = new ObservableCollection<Producer>(movie.Producers);
            comboCountrySelect.SelectedIndex = comboCountrySelect.Items.IndexOf(movie.OriginCountry);
            comboLanguageSelect.SelectedIndex = comboLanguageSelect.Items.IndexOf(movie.Language);
            txtMovieName.Text = movie.Name;
            txtMovieDescription.Text = movie.Description;
            txtBudget.Text = movie.Budget + "";
            txtDuration.Text = movie.Duration + "";
            heading.Text = "Edit a movie";
            SubmitMovie.Content = "Update movie";
            MovieImage = movie.Image;

            if (MovieImage != null)
            {
                imagePanel.Children.Clear();
                Image image = new();
                image.Source = MovieImage;
                image.Height = 230;
                imagePanel.Children.Add(image);
            }

            Button button = new();
            button.Margin = new Thickness(490, 0, 0, 0);
            button.Width = 120;
            button.Content = "Delete movie";
            button.Click += new RoutedEventHandler(Btn_DeleteMovie);

            buttonPanel.Children.Add(button);
        }
        protected void OnWindowSizeChanged(object sender, SizeChangedEventArgs e)
        {
            border.Height = e.NewSize.Height*0.87;
        }
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Btn_DeleteMovie(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBox = MessageBox.Show("Are you sure you want to delete movie?", "Warning", MessageBoxButton.YesNo);
            switch (messageBox)
            {
                case MessageBoxResult.Yes:
                    if (MovieDAO.Delete(ID))
                    {
                        MessageBox.Show("Successful deletion", "Info window", MessageBoxButton.OK);
                        Close();
                    }
                    else MessageBox.Show("Unable to delete", "Error", MessageBoxButton.OK);
                    break;
                default:
                    break;

            }  
        }
        private void SaveMovie(object sender, RoutedEventArgs e)
        {
            List<Star> stars = new();
            List<Genre> genres = new();
            List<Producer> producers = new();

            foreach (var item in comboCastSelect.SelectedItems)
                stars.Add((Star)item);

            foreach (var item in comboGenreSelect.SelectedItems)
                genres.Add((Genre)item);

            foreach (var item in comboProducerSelect.SelectedItems)
                producers.Add((Producer)item);

            Movie movie = new(txtMovieName.Text, txtMovieDescription.Text, (Language)comboLanguageSelect.SelectedItem,
                (Country)comboCountrySelect.SelectedItem, float.Parse(txtBudget.Text), genres, stars, producers, int.Parse(txtDuration.Text));

            if(MovieImage != null) movie.Image = MovieImage;

            if (SubmitMovie.Content.ToString().Contains("Update"))
            {
                movie.ID = ID;
                if (MovieDAO.Update(movie)!= null)
                    MessageBox.Show("Update successful", "Info", MessageBoxButton.OK);
                else MessageBox.Show("Update failed", "Info", MessageBoxButton.OK);
            }
            else
            {
                if (MovieDAO.Save(movie) != null)
                    MessageBox.Show("Movie saved successfuly", "Info", MessageBoxButton.OK);
                else MessageBox.Show("Could not add a movie", "Info", MessageBoxButton.OK);
            }
        }

        private void Btn_OpenFileDialog(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog
            {
                Title = "Select a picture",
                Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png"
            };
            if (op.ShowDialog() == true)
            {
                MovieImage = new BitmapImage(new Uri(op.FileName));

                imagePanel.Children.Clear();
                Image image = new();
                image.Source = MovieImage;
                image.Height = 230;

                imagePanel.Children.Add(image);
            }
        }
    }
}
