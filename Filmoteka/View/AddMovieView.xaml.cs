using Filmoteka.DAO;
using Filmoteka.Model;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

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
            heading.Text = FindResource("editAMovie") as string;
            SubmitMovie.Content = FindResource("update") as string;
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
            button.Content = FindResource("delete") as string;
            button.Click += new RoutedEventHandler(Btn_DeleteMovie);

            buttonPanel.Children.Add(button);

            SizeChanged += OnWindowSizeChanged;
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
            MessageBoxResult messageBox = MessageBox.Show(FindResource("areUSureToDeleteMovie") as string,FindResource("warning") as string, MessageBoxButton.YesNo);
            switch (messageBox)
            {
                case MessageBoxResult.Yes:
                    if (MovieDAO.Delete(ID))
                    {
                        MessageBox.Show(FindResource("successfulDelete") as string, "Info", MessageBoxButton.OK);
                        Close();
                    }
                    else MessageBox.Show(FindResource("errorDelete") as string, "Error", MessageBoxButton.OK);
                    break;
                default:
                    break;

            }  
        }
        private void SaveMovie(object sender, RoutedEventArgs e)
        {

            if(comboCastSelect.SelectedItems == null || comboCountrySelect.SelectedItems == null || comboGenreSelect.SelectedItems == null 
                || comboLanguageSelect.SelectedItems == null || comboProducerSelect.SelectedItems == null || txtMovieName.Text.Equals(string.Empty) 
                || txtMovieDescription.Text.Equals(string.Empty) || txtDuration.Text.Equals(string.Empty) || txtBudget.Text.Equals(string.Empty))
            {
                MessageBox.Show(FindResource("someFieldsBlank") as string, "Error", MessageBoxButton.OK);
                return;
            }  

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

            if (SubmitMovie.Content.ToString().Contains(FindResource("update") as string))
            {
                movie.ID = ID;
                if (MovieDAO.Update(movie) != null)
                {
                    MessageBox.Show(FindResource("updateSuccessful") as string, "Info", MessageBoxButton.OK);
                    Close();
                }
                else MessageBox.Show(FindResource("updateFailed") as string, "Error", MessageBoxButton.OK);
            }
            else
            {
                if (MovieDAO.Save(movie) != null)
                {
                    MessageBox.Show(FindResource("movieSavedSuccessfully") as string, "Info", MessageBoxButton.OK);
                    Close();
                }
                else MessageBox.Show(FindResource("couldNotSaveMovie") as string, "Error", MessageBoxButton.OK);
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
