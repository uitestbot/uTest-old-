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
            ManagePageData();
            SetInitialProperties();
        }

        #region data mapping
        private void ManagePageData()
        {
            ManageSetTestRailGroup();
            ManageExecutionGroup();
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

        private void UpdateSettingsModel()
        {
            SettingsManager.SettingsManager.Settings.TestRailUrl = TestRailHostTextBox.Text;
            SettingsManager.SettingsManager.Settings.TestRailUsername = TestRailUserNameTextBox.Text;
            SettingsManager.SettingsManager.Settings.TestRailPassword = TestRailPasswordBox.Password;
            SettingsManager.SettingsManager.Settings.IsTestRailReportEnabled = (bool)ReportCheckbox.IsChecked;
            SettingsManager.SettingsManager.Settings.RerunIfFailed = (int)RepeatTestSlider.Value;
            SettingsManager.SettingsManager.Settings.SpeedMultiplier = SpeedMultiplierSlider.Value;
            SettingsManager.SettingsManager.Settings.DefaultTimeout = Int32.Parse(DefaultTimeoutTextBox.Text);
        }
        #endregion
        #region init
        private void SetInitialProperties()
        {
            SaveButton.IsEnabled = false;
            ResetButton.IsEnabled = false;
            if (SettingsManager.SettingsManager.Settings.Id > 1)
            {
                ResetButton.IsEnabled = true;
            }
        }
        #endregion
        #region events
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateSettingsModel();

            if (SettingsManager.SettingsManager.Settings.Id == 1)
            {
                SettingsManager.SettingsManager.SaveSettings();
            }
            else
            {
                SettingsManager.SettingsManager.UpdateSettings();
            }

            SettingsManager.SettingsManager.DefaultSettings = SettingsManager.SettingsManager.GetDefaultSetting();
            SettingsManager.SettingsManager.Settings = SettingsManager.SettingsManager.GetCurrentSetting();

            SaveButton.IsEnabled = false;
            ResetButton.IsEnabled = true;
        }

        private void Page_KeyUp(object sender, KeyEventArgs e)
        {
            EnableSaveButton();
            EnableResetButton();
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            EnableSaveButton();
            EnableResetButton();
        }

        private void ReportCheckbox_Click(object sender, RoutedEventArgs e)
        {
            EnableSaveButton();
            EnableResetButton();
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            DeleteSettings();

            SettingsManager.SettingsManager.Settings = SettingsManager.SettingsManager.DefaultSettings;

            ManagePageData();
            DisabledButtons();
        }

        private void DeleteSettings()
        {
            if (SettingsManager.SettingsManager.Settings.Id > 1)
            {
                SettingsManager.SettingsManager.DeleteSettings();
            }
        }
        #endregion
        #region button enabling/disabling
        private void EnableSaveButton()
        {
            if (SaveButton == null) return;
            if (SaveButton.IsEnabled) return;

            SaveButton.IsEnabled = true;
        }

        private void EnableResetButton()
        {
            if (ResetButton == null) return;
            if (ResetButton.IsEnabled) return;

            ResetButton.IsEnabled = true;
        }

        private void DisabledButtons()
        {
            ResetButton.IsEnabled = false;
            SaveButton.IsEnabled = false;
        }
        #endregion
        #region validators
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        #endregion
    }

}
