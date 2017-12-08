using System;

namespace SettingsManager.Model
{
    public class Settings
    {
        public virtual int Id { get; set; }
        public virtual string TestRailUrl { get; set; }
        public virtual string TestRailPassword { get; set; }
        public virtual string TestRailUsername { get; set; }
        public virtual bool IsTestRailReportEnabled { get; set; }
        public virtual int RerunIfFailed { get; set; }
        public virtual double SpeedMultiplier { get; set; }
        public virtual int DefaultTimeout { get; set; }
    }

}
