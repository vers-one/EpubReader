using System.IO;
using System.Xml.Serialization;
using VersOne.Epub.WpfDemo.Entities;

namespace VersOne.Epub.WpfDemo
{
    internal class ApplicationContext
    {
        private const string SETTINGS_FILE_NAME = "settings.xml";

        static ApplicationContext()
        {
            Instance = new ApplicationContext();
        }

        private ApplicationContext()
        {
            CurrentDirectory = Directory.GetCurrentDirectory();
            Settings = LoadSettings();
        }

        public static ApplicationContext Instance { get; }
        public string CurrentDirectory { get; }
        public Settings Settings { get; }

        public void SaveSettings()
        {
            using (FileStream fileStream = new FileStream(SETTINGS_FILE_NAME, FileMode.Create))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(Settings));
                xmlSerializer.Serialize(fileStream, Settings);
            }
        }

        private Settings LoadSettings()
        {
            if (!File.Exists(SETTINGS_FILE_NAME))
            {
                return Settings.Empty;
            }
            using (FileStream fileStream = new FileStream(SETTINGS_FILE_NAME, FileMode.Open))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(Settings), typeof(Settings).GetNestedTypes());
                Settings result = (Settings)xmlSerializer.Deserialize(fileStream);
                return result;
            }
        }
    }
}
