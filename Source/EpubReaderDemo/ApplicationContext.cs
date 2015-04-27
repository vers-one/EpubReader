using EpubReaderDemo.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EpubReaderDemo
{
    internal class ApplicationContext
    {
        private const string SETTINGS_FILE_NAME = "settings.xml";

        private static readonly ApplicationContext instance;
        private Settings settings;

        static ApplicationContext()
        {
            instance = new ApplicationContext();
        }

        private ApplicationContext()
        {
            settings = LoadSettings();
        }

        public static ApplicationContext Instance
        {
            get
            {
                return instance;
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
                return Settings.Empty;
            using (FileStream fileStream = new FileStream(SETTINGS_FILE_NAME, FileMode.Open))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(Settings));
                Settings result = (Settings)xmlSerializer.Deserialize(fileStream);
                return result;
            }
        }
    }
}
