using System.IO;
using System.Xml.Serialization;
using VersOne.Epub.WpfDemo.Entities;

namespace VersOne.Epub.WpfDemo
{
    internal class ApplicationContext
    {
        private const string SETTINGS_FILE_NAME = "settings.xml";

        private static readonly ApplicationContext instance;
        private readonly string currentDirectory;
        private readonly Settings settings;

        static ApplicationContext()
        {
            instance = new ApplicationContext();
        }

        private ApplicationContext()
        {
            currentDirectory = Directory.GetCurrentDirectory();
            settings = LoadSettings();
        }

        public static ApplicationContext Instance
        {
            get
            {
                return instance;
            }
        }

        public string CurrentDirectory
        {
            get
            {
                return currentDirectory;
            }
        }

        public Settings Settings
        {
            get
            {
                return settings;
            }
        }

        public void SaveSettings()
        {
            using (FileStream fileStream = new FileStream(SETTINGS_FILE_NAME, FileMode.Create))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(Settings));
                xmlSerializer.Serialize(fileStream, settings);
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
