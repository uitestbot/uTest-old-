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

namespace ui_test_bot
{
    /// <summary>
    /// Interaction logic for ExitConfirmation.xaml
    /// </summary>
    public partial class ExitConfirmation : Window
    {
        public Dashboard OwningWindow { get; set; }

        public ExitConfirmation()
        {
            InitializeComponent();
        }

        private void YesButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            if (OwningWindow != null)
                OwningWindow.Close();
        }

        private void NoButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            OwningWindow.IsEnabled = true;
        }

        private void ExitConfirm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Y)
            {
                this.Close();
                if (OwningWindow != null)
                    OwningWindow.Close();
            }
            else if (e.Key == Key.N)
            {
                OwningWindow.IsEnabled = true;
                this.Close();
            }
        }

    }
}
