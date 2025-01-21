using Newtonsoft.Json;
using OTSC_ui.Tools.AppSettingJsonPhars.Temaplates;
using Serilog;


namespace OTSC_ui.Tools.AppSettingJsonPhars.Reader
{
    internal class JsonReaderForConfig : IJsonReaderForConfig
    {
        public T Read<T>(string filePath) where T : ITemplates
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    Log.Information($"File path - {filePath}");
                    throw new FileNotFoundException($"File not found: {filePath}");
                }

                string jsonContent = File.ReadAllText(filePath);
                Log.Information("json content:" + jsonContent);

                var deserializedObject = JsonConvert.DeserializeObject<T>(jsonContent);
                if (deserializedObject == null)
                {
                    Log.Error("Не удалось десериализовать JSON в объект.");
                }
               
                else
                {
                    Log.Error($"Объект успешно десериализован: {deserializedObject}");
                }
                Log.Information("deserializedObject T type" + deserializedObject?.ToString());
                if (deserializedObject == null)
                {
                    Log.Error("JsonReaderForConfig:     Failed to deserialize JSON content.");
                    throw new JsonException("Empty or bad file");
                }

                Log.Information($"JsonReaderForConfig:      Deserialized object: {deserializedObject}");

                return deserializedObject;
            }
            catch (Exception ex)
            {
                Log.Error($"Error reading JSON file: {ex.Message}");
                throw;
            }
        }
    }
}
