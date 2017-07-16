using System;
using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.Options;
namespace DotNetCore.Settings
{
    public class SettingsManager : ISettings
    {

        //public SettingsManager(IOptions<DocumentDBSetting> documentDBSettings)
        public SettingsManager(IConfiguration configuration, DocumentDBSettings documentDBSettings)
        {
            TestSetting = configuration.GetSection("TestSetting").Value;
            DocumentDBSettings = documentDBSettings;
        }

        public DocumentDBSettings DocumentDBSettings { get; private set; }

        public string TestSetting { get; private set; }


    }
}
