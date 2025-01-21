using Newtonsoft.Json;

namespace OTSC_ui.Tools.AppSettingJsonPhars.Temaplates
{
    internal class RootConfig : ITemplates
    {
        [JsonProperty("serverSetings")] // Укажите правильное имя из JSON
        public ServerSettings ServerSettings { get; set; } = new ServerSettings();

        [JsonProperty("emailSetings")]
        public EmailSettings EmailSettings { get; set; } = new EmailSettings();
        
        [JsonProperty("botSettings")]
        public string BotSettings { get; set; } = string.Empty;


        public string GetConnectionString() => EmailSettings?.ToString() + ServerSettings?.ToString();
        
        public override string ToString() => EmailSettings?.ToString() + ServerSettings?.ToString();
        
        public string GetBotToken() => BotSettings;
        
        

    }
}
