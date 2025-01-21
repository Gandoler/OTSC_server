using OTSC_ui.Tools.AppSettingJsonPhars.Reader;
using OTSC_ui.Tools.AppSettingJsonPhars.Temaplates;
using Serilog;

namespace OTSC_ui.Tools.AppSettingJsonPhars.ConnectionStringManager
{
    public static class ConnectionStringManager
    {
        static RootConfig rootConfig = new();

        static ConnectionStringManager()
        {
            GoDeserialiseObject();
        }
        private static void GoDeserialiseObject()
        {
          
            JsonReaderForConfig jsonReader = new();
            try
            {
                rootConfig = jsonReader.Read<RootConfig>(@"C:\\Users\\glkru\\RiderProjects\\OTSC_server\\ClassLibrary1\\tools\\AppSettingJsonPhars\\RealSAppSettimgs.json");
                Log.Information($"ConnectionStringManager: settings:{rootConfig}");
            }
            catch (Exception ex)
            {
                Log.Error($"In ConnectionStringManager {ex.Message}");
            }
            
        }

        public static string GetConnectionString() => rootConfig.ServerSettings.GetConnectionString();


        public static EmailSettings GetEmailSettings() => rootConfig.EmailSettings;

        public static string GetTgBotToken() => rootConfig.GetBotToken();

    }
}
