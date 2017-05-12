using System;
using Microsoft.Extensions.Options;
namespace DotNetCore.Settings
{
    public class SettingsManager : ISettings
    {
        readonly DocumentDBSetting _documentDBSettings;

        public SettingsManager(IOptions<DocumentDBSetting> documentDBSettings)
        {
            this._documentDBSettings = documentDBSettings.Value;
        }


        public DocumentDBSetting DocumentDBSetting 
        {
            get => this._documentDBSettings;
        }


    }
}
