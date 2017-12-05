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

namespace ui_test_bot
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Dashboard : Window
    {
        public Dashboard()
        {
            InitializeComponent();
            versionLabel.Content = Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            this.IsEnabled = false;
            ExitConfirmation exitConfirmation = new ExitConfirmation();
            exitConfirmation.OwningWindow = this;
            exitConfirmation.Show();
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

    }
}
