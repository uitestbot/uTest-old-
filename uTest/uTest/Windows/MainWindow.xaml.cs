using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using uTest.Pages;

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
            InitializeComponent();
            versionLabel.Content = String.Format("v.{0}", Assembly.GetExecutingAssembly().GetName().Version.ToString());
            if (dashboard == null)
            {
                dashboard = new Dashboard();
            }
            mainFrame.Content = dashboard;
        }

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

    }
}
