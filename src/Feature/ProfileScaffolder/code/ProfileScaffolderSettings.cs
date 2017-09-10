using Sitecore.Configuration;

namespace Sitecore.Feature.ProfileScaffolder
{
    public static class ProfileScaffolderSettings
    {
        public static string ProfileKeyTemplateId
        {
            get { return Settings.GetSetting("Sitecore.Feature.ProfileScaffolder.ProfileKey.TemplateId", "{44AB5107-3C73-42F0-A427-BEC549F944B9}"); }
        }

        public static string ProfileCardTemplateId
        {
            get { return Settings.GetSetting("Sitecore.Feature.ProfileScaffolder.ProfileCard.TemplateId", "{0FC09EA4-8D87-4B0E-A5C9-8076AE863D9C}"); }
        }

        public static string PatternCardTemplateId
        {
            get { return Settings.GetSetting("Sitecore.Feature.ProfileScaffolder.PatternCard.TemplateId", "{4A6A7E36-2481-438F-A9BA-0453ECC638FA}"); }
        }

        public static string ProfileKeyDefaultMinValue
        {
            get { return Settings.GetIntSetting("Sitecore.Feature.ProfileScaffolder.ProfileKey.DefaultMinValue", 0).ToString(); }
        }

        public static string ProfileKeyDefaultMaxValue
        {
            get { return Settings.GetIntSetting("Sitecore.Feature.ProfileScaffolder.ProfileKey.DefaultMaxValue", 10).ToString(); }
        }

        public static string ProfileCardFolderName
        {
            get { return Settings.GetSetting("Sitecore.Feature.ProfileScaffolder.ProfileCard.FolderName", "Profile Cards"); }
        }

        public static string PatternCardFolderName
        {
            get { return Settings.GetSetting("Sitecore.Feature.ProfileScaffolder.PatternCard.FolderName", "Pattern Cards"); }
        }

        public struct ProfileKeyFieldNames
        {
            public static string MinValue
            {
                get { return Settings.GetSetting("Sitecore.Feature.ProfileScaffolder.ProfileKey.FieldNames.MinValue", "MinValue"); }
            }

            public static string MaxValue
            {
                get { return Settings.GetSetting("Sitecore.Feature.ProfileScaffolder.ProfileKey.FieldNames.MaxValue", "MaxValue"); }
            }
        }

        public struct ProfileCardFieldNames
        {
            public static string RadarGraph
            {
                get { return Settings.GetSetting("Sitecore.Feature.ProfileScaffolder.ProfileCard.FieldNames.RadarGraph", "Profile Card Value"); }
            }
        }

        public struct PatternCardFieldNames
        {
            public static string RadarGraph
            {
                get { return Settings.GetSetting("Sitecore.Feature.ProfileScaffolder.PatternCard.FieldNames.RadarGraph", "Pattern"); }
            }
        }
    }
}