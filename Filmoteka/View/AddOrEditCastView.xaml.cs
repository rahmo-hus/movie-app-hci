using Filmoteka.Model;
using Filmoteka.DAO;
using Microsoft.Win32;
using Syncfusion.Windows.Tools.Controls;
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
    /// Interaction logic for AddACastView.xaml
    /// </summary>
    public partial class AddOrEditCastView : Window
    {
        BitmapImage CastImage;
        private static int ID;
        public AddOrEditCastView()
        {
            InitializeComponent();
            SizeChanged += OnWindowSizeChanged;
        }

        public AddOrEditCastView(Star star)
        {
            ID = star.ID;
            InitializeComponent();
            txtFirstName.Text = star.FirstName;
            txtLastName.Text = star.LastName;
            txtCastBio.Text = star.Bio;
            datePicker.Text = star.DateOfBirth;
            heading.Text = FindResource("editACast") as string;
            confirmButton.Content = FindResource("update") as string;
            CastImage = star.Image;

            foreach(ComboBoxItemAdv item in comboGenderSelect.Items)
                if (item.Content as string == star.Gender)
                    comboGenderSelect.SelectedItem = item;
            
            if(star.Image != null)
            {
                imagePanel.Children.Clear();
                Image image = new();
                image.Source = CastImage;
                image.Height = 230;
                imagePanel.Children.Add(image);
            }

            Button button = new();
            button.Width = 100;
            button.Content = FindResource("delete").ToString();
            button.Margin = new Thickness(100, 0, 0, 0);
            button.Click += Delete_Click;
            buttonPanel.Children.Add(button);

            SizeChanged += OnWindowSizeChanged;
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBox = MessageBox.Show(FindResource("areUSureToDeleteCast") as string, FindResource("warning") as string, MessageBoxButton.YesNo);
            switch (messageBox)
            {
                case MessageBoxResult.Yes:
                    if (StarDAO.Delete(ID))
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

        protected void OnWindowSizeChanged(object sender, SizeChangedEventArgs e)
        {
            border.Height = e.NewSize.Height * 0.87;
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Bttn_Click_SaveCast(object sender, RoutedEventArgs e)
        {
            Star star = new();
            star.FirstName = txtFirstName.Text;
            star.LastName = txtLastName.Text;
            star.DateOfBirth = datePicker.Text;
            star.Gender = comboGenderSelect.SelectedItem.ToString()[comboGenderSelect.SelectedItem.ToString().Length - 1].ToString();
            star.Bio = txtCastBio.Text;
            star.Image = CastImage;

            if (confirmButton.Content.ToString().Contains(FindResource("update") as string))
            {
                star.ID = ID;
                if (StarDAO.Update(star))
                    MessageBox.Show(FindResource("updateSuccessful") as string, "Info", MessageBoxButton.OK);
                else MessageBox.Show(FindResource("updateFailed") as string, "Error", MessageBoxButton.OK);
            }
            else
            {
                if (StarDAO.Save(star) != null)
                    MessageBox.Show(FindResource("castSavedSuccessfully") as string, "Info", MessageBoxButton.OK);
                else MessageBox.Show(FindResource("couldNotSaveCast") as string, "Error", MessageBoxButton.OK);
            }
        }

        private void OpenFileDialog(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new()
            {
                Title = "Select a picture",
                Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
                         "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                         "Portable Network Graphic (*.png)|*.png"
            };
            if (op.ShowDialog() == true)
            {
                CastImage = new BitmapImage(new Uri(op.FileName));
                imagePanel.Children.Clear();

                Image image = new();
                image.Source = CastImage;
                image.Height = 230;
                image.Margin = new Thickness(25, 5, 0, 5);

                imagePanel.Children.Add(image);
               

            }
        }
    }
}
