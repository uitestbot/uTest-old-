using System;
using NHibernate.Tool.hbm2ddl;
using SettingsManager.Dao;
using System.IO;
using SettingsManager.Model;
using System.Collections.Generic;

namespace SettingsManager
{
    public class SettingsManager
    {
        private static ISettingsRepository SettingsRepository;
        private static IApplicationRepository ApplicationRepository;
        public static Settings DefaultSettings;
        public static Settings Settings;
        public static List<Application> ApplicationList;

        public static Settings GetCurrentSetting()
        {
            try
            {
                var currentSetting = SettingsRepository.GetLast();

                return currentSetting;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static Settings GetDefaultSetting()
        {
            try
            {
                var currentSetting = SettingsRepository.Get(1);

                return currentSetting;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<Application> GetApplicationList()
        {
            try
            {
                var applicationList = ApplicationRepository.GetAll();

                return applicationList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void InitSettings()
        {
            try
            {
                SetSettingsRepository();
                SetApplicationRepository();
                CreateDB();
                InitDefaultValues();
                SetDefaultSettings();
                SetSettings();
                SetApplicationList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static void SetSettingsRepository()
        {
            SettingsRepository = new NHibernateSettingsRepository();
        }

        private static void SetApplicationRepository()
        {
            ApplicationRepository = new NHibernateApplicationRepository();
        }

        private static void CreateDB()
        {
            if (File.Exists(@"profile.db")) return;

            var schemaUpdate = new SchemaUpdate(NHibernateHelper.Configuration);
            schemaUpdate.Execute(false, true);
        }

        private static void InitDefaultValues()
        {
            if (!File.Exists(@"profile.db")) return;
            if (SettingsRepository.RowCount() > 1) return;

            SettingsRepository.Save(new Settings
            {
                TestRailUrl = @"http://10.0.33.57/",
                TestRailUsername = @"sevim.ataseven@comodo.com",
                TestRailPassword = @"browser",
                IsTestRailReportEnabled = true,
                RerunIfFailed = 2,
                SpeedMultiplier = 1,
                DefaultTimeout = 30
            });
        }

        private static void SetDefaultSettings()
        {
            DefaultSettings = GetDefaultSetting();
        }

        private static void SetApplicationList()
        {
            ApplicationList = GetApplicationList();
        }

        private static void SetSettings()
        {
            Settings = GetCurrentSetting();
        }

        public static void SaveSettings()
        {
            SettingsRepository.Save(Settings);
        }

        public static void AddTestValues()
        {
            if (!File.Exists(@"profile.db")) return;
            if (SettingsRepository.RowCount() > 1) return;

            SettingsRepository.Save(new Settings
            {
                TestRailUrl = @"http://10.0.33.57/",
                TestRailUsername = @"andrei.hreapca@comodo.com",
                TestRailPassword = @"test4",
                IsTestRailReportEnabled = true,
                RerunIfFailed = 2,
                SpeedMultiplier = 1,
                DefaultTimeout = 30
            });

            if (ApplicationRepository.RowCount() > 0) return;
            ApplicationRepository.Save(new Application
            {
                Version = @"60.0.3112.114",
                Parameters = @"ProductId=19;ChannelId=25050030001"
            });
        }

    }

}
