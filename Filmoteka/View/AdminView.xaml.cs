using Filmoteka.Model;
using System.Windows;
using System.Windows.Input;

namespace Filmoteka.View
{
    /// <summary>
    /// Interaction logic for AdminView.xaml
    /// </summary>
    public partial class AdminView : Window
    {
        private readonly User CurrentUser;
        public AdminView(User currentUser)
        {
            InitializeComponent();
            CurrentUser = currentUser;
            SizeChanged += OnWindowSizeChanged;
            if (itemList.Children.Count != 0)
                itemList.Children.Clear();
            itemList.Children.Add(new MovieListView());
            moviesTextBlock.Text = moviesTextBlock.Text.ToUpper();
            castTextBlock.Text = castTextBlock.Text.ToUpper();
            logoutTextBlock.Text = logoutTextBlock.Text.ToUpper();
            producersTextBlock.Text = producersTextBlock.Text.ToUpper();
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

        private void Click_Logout(object sender, RoutedEventArgs e)
        {
            new LoginView().Show();
            Close();
        }
    }
}
