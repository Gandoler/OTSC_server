using OTSC_ui.Tools.AppSettingJsonPhars.Temaplates;

namespace OTSC_ui.Tools.AppSettingJsonPhars.Reader
{
    internal interface IJsonReaderForConfig
    {
        T Read<T>(string filePath) where T : ITemplates;
    }
}
