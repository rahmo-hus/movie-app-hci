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
using System.Windows.Shapes;

namespace Filmoteka.View
{
    /// <summary>
    /// Interaction logic for AdminView.xaml
    /// </summary>
    public partial class AdminView : Window
    {
        public AdminView()
        {
            InitializeComponent();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Movie_BtnClick(object sender, RoutedEventArgs e)
        {
            MovieListView movieListView = new MovieListView();
            if (movieList.Children.Count != 0)
                movieList.Children.Clear();
            movieList.Children.Add(movieListView);
        }
    }
}
