using Filmoteka.DAO;
using Filmoteka.Model;
using Filmoteka.DAO;
using Syncfusion.SfSkinManager;
using Syncfusion.Themes.MaterialDarkBlue.WPF;
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

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if(txtUsername.Text != "" && txtPassword.Password != "")
            {

                User user = UserDAO.Login(txtUsername.Text, txtPassword.Password);
                if(user != null && user.Role == ERole.ADMIN)
                {
                    new AdminView(user).Show();
                    Close();
                }
                else if(user != null && user.Role == ERole.VIEWER)
                {
                    new UserMovieListView(user).Show();
                    Close();
                }

            }

        }

        private void Click_Settings(object sender, RoutedEventArgs e)
        {
            new SettingsView().Show();
            Close();
        }
    }
}
