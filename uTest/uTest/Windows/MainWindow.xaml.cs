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
        }

        private void ValidateResources() {
            try
            {
                var assemblyLocation = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

                foreach (String dependency in Dependencies.dependencies)
                {
                    if (!FileOrDirectoryExists(String.Format("{0}/{1}", assemblyLocation, dependency)))
                    {
                        MessageBox.Show(String.Format("Missing resource: {0}", dependency));
                        Application.Current.Shutdown();
                    }
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
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
                HandleException(ex);
            }
        }

        private void SetVersionLabel()
        {
            try
            {
                versionLabel.Content = String.Format("v.{0}", Assembly.GetExecutingAssembly().GetName().Version.ToString());
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
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

            //exitConfirmation.Left = this.Left + (this.Width - exitConfirmation.ActualWidth) / 2;
            //exitConfirmation.Top = this.Top + (this.Height - exitConfirmation.ActualHeight) / 2;

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
            SetActiveMenuDecoration("dashboard");
        }

        private void Menu2Title_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (history == null)
            {
                history = new History();
            }
            mainFrame.Content = history;
            SetActiveMenuDecoration("history");
        }

        private void Menu3Title_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (implement == null)
            {
                implement = new Implement();
            }
            mainFrame.Content = implement;
            SetActiveMenuDecoration("implement");
        }

        private void Menu4Title_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (settings == null)
            {
                settings = new Settings();
            }
            mainFrame.Content = settings;
            SetActiveMenuDecoration("settings");
        }

        public void SetActiveMenuDecoration(string menuItem)
        {
            menu1Decoration.Fill = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF222222"));
            menu2Decoration.Fill = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF222222"));
            menu3Decoration.Fill = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF222222"));
            menu4Decoration.Fill = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF222222"));

            switch (menuItem)
            {
                case "dashboard": menu1Decoration.Fill = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF127A96")); break;
                case "history": menu2Decoration.Fill = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF127A96")); break;
                case "implement": menu3Decoration.Fill = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF127A96")); break;
                case "settings": menu4Decoration.Fill = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF127A96")); break;
            }
        }
        #endregion
        #region GET PAGES
        public Dashboard GetDashboard()
        {
            if (dashboard == null)
            {
                dashboard = new Dashboard();
            }

            return dashboard;
        }

        public History GetHistory()
        {
            if (history == null)
            {
                history = new History();
            }

            return history;
        }

        public Implement GetImplement()
        {
            if (implement == null)
            {
                implement = new Implement();
            }

            return implement;
        }

        public Settings GetSettings()
        {
            if (settings == null)
            {
                settings = new Settings();
            }

            return settings;
        }
        #endregion
        #region EXCEPTION HANDLING
        private void HandleException(Exception ex)
        {
            ShowErrorNotificationWindow(this);
        }
    
        public void ShowErrorNotificationWindow(Window window)
        {
            this.IsEnabled = false;

            ErrorNotification errorNotification = new ErrorNotification();
            errorNotification.OwningWindow = this;

            //errorNotification.Left = this.Left + (this.Width - errorNotification.ActualWidth) / 2;
            //errorNotification.Top = this.Top + (this.Height - errorNotification.ActualHeight) / 2;

            errorNotification.Show();
        }
        #endregion


    }
}
