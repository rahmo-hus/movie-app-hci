using Syncfusion.Windows.Tools.Controls;
using System;
using System.Windows;
using System.Windows.Input;

namespace Filmoteka.View
{
    /// <summary>
    /// Interaction logic for SettingsView.xaml
    /// </summary>
    public partial class SettingsView : Window
    {
        public static ResourceDictionary ThemeDictionary
        {
            get { return Application.Current.Resources.MergedDictionaries[4]; }
        }

        public static ResourceDictionary LanguageDictionary
        {
            get { return Application.Current.Resources.MergedDictionaries[5]; }
        }
        public SettingsView()
        {
            InitializeComponent();
            SizeChanged += OnWindowSizeChanged;


            foreach (ComboBoxItemAdv item in comboThemeSelect.Items)
            {
                if ((ThemeDictionary.MergedDictionaries[0].Source.AbsolutePath.Contains("ThemeDark") && item.Content.Equals(FindResource("darkTheme") as string)))
                {
                    comboThemeSelect.SelectedItem = item;
                    break;
                }
                else if (ThemeDictionary.MergedDictionaries[0].Source.AbsolutePath.Contains("ThemeLight") && item.Content.Equals(FindResource("lightTheme") as string))
                {
                    comboThemeSelect.SelectedItem = item;
                    break;
                }
            }
            foreach(ComboBoxItemAdv item in comboLanguageSelect.Items)
            {
                if ((LanguageDictionary.MergedDictionaries[0].Source.AbsolutePath.Contains("English") && item.Content.Equals("English")))
                {
                    comboLanguageSelect.SelectedItem = item;
                    break;
                }
                else if ((LanguageDictionary.MergedDictionaries[0].Source.AbsolutePath.Contains("Bosnian") && item.Content.Equals("Bosanski")))
                {
                    comboLanguageSelect.SelectedItem = item;
                    break;
                }
            }
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        protected void OnWindowSizeChanged(object sender, SizeChangedEventArgs e)
        {
            border.Height = e.NewSize.Height * 0.87;
        }

        private void Click_Apply(object sender, RoutedEventArgs e)
        {
            string selectedTheme = ((ComboBoxItemAdv)comboThemeSelect.SelectedItem).Content.ToString();
            string selectedLanguage = ((ComboBoxItemAdv)comboLanguageSelect.SelectedItem).Content.ToString();
            ThemeDictionary.MergedDictionaries.Clear();
            LanguageDictionary.MergedDictionaries.Clear();
            ThemeDictionary.MergedDictionaries.Add(new ResourceDictionary
            {
                Source = new Uri("pack://application:,,,/Filmoteka;component/Dictionaries/" +( selectedTheme.Equals("Dark theme") || selectedTheme.Equals("Tamna tema") ? "ThemeDark.xaml" : "ThemeLight.xaml"))
            });
            LanguageDictionary.MergedDictionaries.Add(new ResourceDictionary
            {
                Source = new Uri("pack://application:,,,/Filmoteka;component/Dictionaries/" + (selectedLanguage.Equals("English") ? "English.xaml" : "Bosnian.xaml"))
            });
            new LoginView().Show();
            Close();
        }

        private void Click_Exit(object sender, RoutedEventArgs e)
        {
            new LoginView().Show();
            Close();
        }
    }
}
