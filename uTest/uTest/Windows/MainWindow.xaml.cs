using System;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using uTest.Pages;
using uTest.Resources;

namespace uTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static Dashboard dashboard = null;
        private static History history = null;
        private static Implement implement = null;
        private static Settings settings = null;

        public MainWindow()
        {
            ValidateResources();
            InitializeProfile();
            InitializeComponent();
            SetVersionLabel();
            SetStartingPageInMainFrame();

            var result = SettingsManager.SettingsManager.GetDefaultSetting();
            //MessageBox.Show(String.Format("Default Settings: \n\n username: {0}, password: {1}", result.TestRailUsername, result.TestRailPassword));

            var result2 = SettingsManager.SettingsManager.GetCurrentSetting();
            //MessageBox.Show(String.Format("Current Settings: \n\n username: {0}, password: {1}", result2.TestRailUsername, result2.TestRailPassword));

            dashboard.message_Label.Content = String.Format("Default Settings: \n username: {0}, password: {1}", result.TestRailUsername, result.TestRailPassword);
            dashboard.message_Label.Content += String.Format("\n\nCurrent Settings: \n username: {0}, password: {1}", result2.TestRailUsername, result2.TestRailPassword);
        }

        private void ValidateResources() {
            var assemblyLocation = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

            foreach(String dependency in Dependencies.dependencies) 
            {
                if (!FileOrDirectoryExists(String.Format("{0}/{1}", assemblyLocation, dependency)))
                {
                    MessageBox.Show(String.Format("Missing resource: {0}", dependency));
                    Application.Current.Shutdown();
                }
            }
        }

        private void InitializeProfile()
        {
            try
            {
                SettingsManager.SettingsManager.InitSettings();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void SetVersionLabel()
        {
            versionLabel.Content = String.Format("v.{0}", Assembly.GetExecutingAssembly().GetName().Version.ToString());
        }

        private void SetStartingPageInMainFrame()
        {
            if (dashboard == null)
            {
                dashboard = new Dashboard();
            }
            mainFrame.Content = dashboard;
        }

        private bool FileOrDirectoryExists(string name)
        {
            return (Directory.Exists(name) || File.Exists(name));
        }

        #region NAVIGATION EVENTS
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            this.IsEnabled = false;

            ExitConfirmation exitConfirmation = new ExitConfirmation();
            exitConfirmation.OwningWindow = this;
            exitConfirmation.Show();

            exitConfirmation.Left = this.Left + (this.Width - exitConfirmation.ActualWidth) / 2;
            exitConfirmation.Top = this.Top + (this.Height - exitConfirmation.ActualHeight) / 2;
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
        #endregion
        #region MENU EVENTS
        private void Menu1Title_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (dashboard == null)
            {
                dashboard = new Dashboard();
            }
            mainFrame.Content = dashboard;

            menu1Decoration.Fill = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF127A96"));
            menu2Decoration.Fill = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF222222"));
            menu3Decoration.Fill = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF222222"));
            menu4Decoration.Fill = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF222222"));
        }

        private void Menu2Title_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (history == null)
            {
                history = new History();
            }
            mainFrame.Content = history;

            menu1Decoration.Fill = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF222222"));
            menu2Decoration.Fill = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF127A96"));
            menu3Decoration.Fill = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF222222"));
            menu4Decoration.Fill = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF222222"));
        }

        private void Menu3Title_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (implement == null)
            {
                implement = new Implement();
            }
            mainFrame.Content = implement;

            menu1Decoration.Fill = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF222222"));
            menu2Decoration.Fill = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF222222"));
            menu3Decoration.Fill = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF127A96"));
            menu4Decoration.Fill = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF222222"));
        }

        private void Menu4Title_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (settings == null)
            {
                settings = new Settings();
            }
            mainFrame.Content = settings;

            menu1Decoration.Fill = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF222222"));
            menu2Decoration.Fill = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF222222"));
            menu3Decoration.Fill = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF222222"));
            menu4Decoration.Fill = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF127A96"));
        }
        #endregion
       
    }
}
