using System.Windows.Controls;
using System.Windows.Input;
using System.Text.RegularExpressions;
using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Documents;
using System.Windows.Media;

namespace uTest.Pages
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Settings : Page
    {
        public Settings()
        {
            if (!IsInitialized)
            {
                InitializeComponent();
            }
            ManageSetTestRailGroup();
            ManageExecutionGroup();
            SetInitialProperties();
        }

        private void ManageSetTestRailGroup()
        {
            TestRailHostTextBox.Text = SettingsManager.SettingsManager.Settings.TestRailUrl;
            TestRailUserNameTextBox.Text = SettingsManager.SettingsManager.Settings.TestRailUsername;
            TestRailPasswordBox.Password = SettingsManager.SettingsManager.Settings.TestRailPassword;
            ReportCheckbox.IsChecked = SettingsManager.SettingsManager.Settings.IsTestRailReportEnabled;
        }

        private void ManageExecutionGroup()
        {
            RepeatTestSlider.Value = SettingsManager.SettingsManager.Settings.RerunIfFailed;
            RepeatTestTextBox.Text= SettingsManager.SettingsManager.Settings.RerunIfFailed.ToString();
            SpeedMultiplierSlider.Value = SettingsManager.SettingsManager.Settings.SpeedMultiplier;
            SpeedMultiplierTextBox.Text = SettingsManager.SettingsManager.Settings.SpeedMultiplier.ToString();
            DefaultTimeoutTextBox.Text = SettingsManager.SettingsManager.Settings.DefaultTimeout.ToString();
        }

        private void SetInitialProperties()
        {
            SaveButton.IsEnabled = false;
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void SaveButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            UpdateSettingsModel();

            SettingsManager.SettingsManager.SaveSettings();
            SaveButton.IsEnabled = false;
        }

        private void UpdateSettingsModel()
        {
            SettingsManager.SettingsManager.Settings.TestRailUrl = TestRailHostTextBox.Text;
            SettingsManager.SettingsManager.Settings.TestRailUsername = TestRailUserNameTextBox.Text;
            SettingsManager.SettingsManager.Settings.TestRailPassword = TestRailPasswordBox.Password;
            SettingsManager.SettingsManager.Settings.IsTestRailReportEnabled = (bool) ReportCheckbox.IsChecked;
            SettingsManager.SettingsManager.Settings.RerunIfFailed = (int) RepeatTestSlider.Value;
            SettingsManager.SettingsManager.Settings.SpeedMultiplier = (int) SpeedMultiplierSlider.Value;
            SettingsManager.SettingsManager.Settings.DefaultTimeout = Int32.Parse(DefaultTimeoutTextBox.Text);
        }

        private void Page_KeyUp(object sender, KeyEventArgs e)
        {
            if (!IsInitialized)
            {
                InitializeComponent();
            }
            if (SaveButton == null) return;
            if (SaveButton.IsEnabled) return;

            SaveButton.IsEnabled = true;
        }

        private void Page_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            EnableSaveButton();
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            EnableSaveButton();
        }

        private void ReportCheckbox_Click(object sender, RoutedEventArgs e)
        {
            EnableSaveButton();
        }

        private void EnableSaveButton()
        {
            if (!IsInitialized)
            {
                InitializeComponent();
            }
            if (SaveButton == null) return;
            if (SaveButton.IsEnabled) return;

            SaveButton.IsEnabled = true;
        }

    }
}
