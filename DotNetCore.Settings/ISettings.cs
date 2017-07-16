using System;
namespace DotNetCore.Settings
{
    public interface ISettings
    {
        string TestSetting
        {
            get;
        }

        DocumentDBSettings DocumentDBSettings
        {
            get;
        }


    }
}
