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
using System.Windows.Shapes;

namespace Filmoteka.View
{
    /// <summary>
    /// Interaction logic for AddMovieView.xaml
    /// </summary>
    public partial class AddMovieView : Window
    {
        public AddMovieView()
        {
            InitializeComponent();
            List<string> countries = MovieDAO.GetGountries(), languages = MovieDAO.GetLanguages();
            List<Genre> genres = GenreDAO.GetGenres();
            countries.ForEach(country => comboCountrySelect.Items.Add(country));
            languages.ForEach(language => comboLanguageSelect.Items.Add(language));
            genres.ForEach(genre => comboGenreSelect.Items.Add(genre.ToString()));
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
