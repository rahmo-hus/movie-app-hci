using Filmoteka.Model;
using Filmoteka.Repository;
using Microsoft.Win32;
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
    /// Interaction logic for AddOrEditProducerView.xaml
    /// </summary>
    public partial class AddOrEditProducerView : Window
    {
        BitmapImage ProducerImage;
        private static int ID;
        public AddOrEditProducerView()
        {
            InitializeComponent();
            SizeChanged += OnWindowSizeChanged;
        }

        public AddOrEditProducerView(Producer producer)
        {
            ID = producer.ID;
            InitializeComponent();
            txtFirstName.Text = producer.FirstName;
            txtLastName.Text = producer.LastName;
            txtProducerBio.Text = producer.Bio;
            datePicker.Text = producer.DateOfBirth;
            heading.Text = "Edit a cast";
            confirmButton.Content = "Update";
            ProducerImage = producer.Image;

            foreach (Syncfusion.Windows.Tools.Controls.ComboBoxItemAdv item in comboGenderSelect.Items)
                if (item.Content as string == producer.Gender)
                    comboGenderSelect.SelectedItem = item;

            if (producer.Image != null)
            {
                imagePanel.Children.Clear();
                Image image = new();
                image.Source = ProducerImage;
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
            MessageBoxResult messageBox = MessageBox.Show("Are you sure you want to delete the producer?", "Warning", MessageBoxButton.YesNo);
            switch (messageBox)
            {
                case MessageBoxResult.Yes:
                    if (ProducerDAO.Delete(ID))
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

        private void Bttn_Click_SaveProducer(object sender, RoutedEventArgs e)
        {
            Producer producer = new();
            producer.FirstName = txtFirstName.Text;
            producer.LastName = txtLastName.Text;
            producer.DateOfBirth = datePicker.Text;
            producer.Gender = comboGenderSelect.SelectedItem.ToString()[comboGenderSelect.SelectedItem.ToString().Length - 1].ToString();
            producer.Bio = txtProducerBio.Text;
            producer.Image = ProducerImage;

            if (confirmButton.Content.ToString().Contains("Update"))
            {
                producer.ID = ID;
                if (ProducerDAO.Update(producer))
                    MessageBox.Show("Update successful", "Info", MessageBoxButton.OK);
                else MessageBox.Show("Update failed", "Info", MessageBoxButton.OK);
            }
            else
            {
                if (ProducerDAO.Save(producer) != null)
                    MessageBox.Show("Producer saved successfuly", "Info", MessageBoxButton.OK);
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
                ProducerImage = new BitmapImage(new Uri(op.FileName));
                imagePanel.Children.Clear();

                Image image = new();
                image.Source = ProducerImage;
                image.Height = 230;
                image.Margin = new Thickness(25, 5, 0, 5);

                imagePanel.Children.Add(image);


            }
        }

        protected void OnWindowSizeChanged(object sender, SizeChangedEventArgs e)
        {
            border.Height = e.NewSize.Height * 0.87;
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
