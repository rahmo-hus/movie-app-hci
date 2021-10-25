using Filmoteka.Model;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Filmoteka.View
{
    /// <summary>
    /// Interaction logic for UserPersonView.xaml
    /// </summary>
    public partial class UserPersonView : Window
    {
        public UserPersonView(Star star)
        {
            InitializeComponent();
            SetComponents(star);
        }
        public UserPersonView(Producer producer)
        {
            InitializeComponent();
            SetComponents(producer);
        }

        private void SetComponents(Person person)
        {
            SizeChanged += OnWindowSizeChanged;
            txtPersonName.Text = person.FullName;
            txtBlockBio.Text = person.Bio;
            txtBlockDateOfBirth.Text += ": " + person.DateOfBirth;
            heading.Text = person is Producer ? FindResource("producer") as string: FindResource("cast") as string;

            if(person.Image != null)
            {
                imagePanel.Children.Clear();
                Image image = new();
                image.Source = person.Image;
                imagePanel.Children.Add(image);
            }
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        protected void OnWindowSizeChanged(object sender, SizeChangedEventArgs e)
        {
            border.Height = e.NewSize.Height * 0.87;
        }
    }
}
