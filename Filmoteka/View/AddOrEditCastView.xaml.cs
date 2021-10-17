using Filmoteka.Model;
using Filmoteka.Repository;
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
            heading.Text = "Edit a cast";
            confirmButton.Content = "Update";
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
            button.Content = "Delete";
            button.Margin = new Thickness(100, 0, 0, 0);
            button.Click += Delete_Click;
            buttonPanel.Children.Add(button);

            SizeChanged += OnWindowSizeChanged;
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBox = MessageBox.Show("Are you sure you want to delete the cast?", "Warning", MessageBoxButton.YesNo);
            switch (messageBox)
            {
                case MessageBoxResult.Yes:
                    if (StarDAO.Delete(ID))
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

            if (confirmButton.Content.ToString().Contains("Update"))
            {
                star.ID = ID;
                if (StarDAO.Update(star))
                    MessageBox.Show("Update successful", "Info", MessageBoxButton.OK);
                else MessageBox.Show("Update failed", "Info", MessageBoxButton.OK);
            }
            else
            {
                if (StarDAO.Save(star) != null)
                    MessageBox.Show("Actor saved successfuly", "Info", MessageBoxButton.OK);
                else MessageBox.Show("Could not add an actor", "Info", MessageBoxButton.OK);
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
