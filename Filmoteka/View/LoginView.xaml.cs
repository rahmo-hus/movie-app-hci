using Filmoteka.DAO;
using Filmoteka.Model;
using System.Windows;
using System.Windows.Input;

namespace Filmoteka.View
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        public LoginView()
        {
            InitializeComponent();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (txtUsername.Text != "" && txtPassword.Password != "")
            {

                User user = UserDAO.Login(txtUsername.Text, txtPassword.Password);
                if (user != null && user.Role == ERole.ADMIN)
                {
                    new AdminView(user).Show();
                    Close();
                }
                else if (user != null && user.Role == ERole.VIEWER)
                {
                    new UserMovieListView(user).Show();
                    Close();
                }
                else
                {
                    MessageBox.Show(FindResource("invalidCredentials") as string, "Error", MessageBoxButton.OK);
                }
            }
            else MessageBox.Show(FindResource("someFieldsBlank") as string, "Error", MessageBoxButton.OK);

        }

        private void Click_Settings(object sender, RoutedEventArgs e)
        {
            new SettingsView().Show();
            Close();
        }
    }
}
