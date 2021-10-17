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
            SizeChanged += OnWindowSizeChanged;
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }
        protected void OnWindowSizeChanged(object sender, SizeChangedEventArgs e)
        {
            border.Height = e.NewSize.Height * 0.87;
        }

        private void Movie_ListViewClick(object sender, RoutedEventArgs e)
        {
            MovieListView movieListView = new();
            if (itemList.Children.Count != 0)
                itemList.Children.Clear();
            itemList.Children.Add(movieListView);
        }


        private void Movie_CastClick(object sender, RoutedEventArgs e)
        {
            CastView castView = new();
            if (itemList.Children.Count != 0)
                itemList.Children.Clear();
            itemList.Children.Add(castView);
        }

        private void Movie_ProducersClick(object sender, RoutedEventArgs e)
        {
            ProducersView producersView = new();
            if (itemList.Children.Count != 0)
                itemList.Children.Clear();
            itemList.Children.Add(producersView);
        }
    }
}
