using System.Xml;
using System.Xml.Serialization;

namespace DrVegapunk.GPT.App.Utils;

public static class ConfigManager {
    public static T TryReadConfigFile<T>(string configFileName) {
        var configFileDir = $"{ProjectContext.ConfigDirPath}\\{configFileName}.config";
        XmlReader reader = XmlReader.Create(configFileDir);
        XmlSerializer serializer = new(typeof(T));
        object? deserializedConfig = serializer.Deserialize(reader);

        return deserializedConfig == null ?
            throw new XmlException($"Error deserializing {configFileName}.config") :
            (T)deserializedConfig;
    }
}
