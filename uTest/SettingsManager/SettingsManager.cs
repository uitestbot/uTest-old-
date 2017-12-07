using System;
using NHibernate.Tool.hbm2ddl;
using SettingsManager.Dao;
using System.IO;
using SettingsManager.Model;

namespace SettingsManager
{
    public class SettingsManager
    {
        private static ISettingsRepository SettingsRepository;

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

        public static void InitSettings()
        {
            try
            {
                SetSettingsRepository();
                CreateDB();
                InitDefaultValues();
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

        private static void CreateDB()
        {
            if (File.Exists(@"profile.db")) return;

            var schemaUpdate = new SchemaUpdate(NHibernateHelper.Configuration);
            schemaUpdate.Execute(false, true);
        }

        private static void InitDefaultValues()
        {
            if (!File.Exists(@"profile.db")) return;

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

            SettingsRepository.Save(new Settings
            {
                TestRailUrl = @"http://10.0.33.57/",
                TestRailUsername = @"andrei.hreapca@comodo.com",
                TestRailPassword = @"test",
                IsTestRailReportEnabled = true,
                RerunIfFailed = 2,
                SpeedMultiplier = 1,
                DefaultTimeout = 30
            });
        }
    }

}
