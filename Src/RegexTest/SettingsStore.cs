using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Soap;

namespace RegexTest
{
    internal static class SettingsStore
    {
        public static void Save(string filename, Settings settings)
        {
            using (FileStream streamWrite = File.Create(filename))
            {
                SoapFormatter soapWrite = new SoapFormatter();

                soapWrite.Serialize(streamWrite, settings);
            }
        }

        public static Settings Load(string filename)
        {
            try
            {
                using (FileStream streamRead = File.OpenRead(filename))
                {
                    try
                    {
                        SoapFormatter soapRead = new SoapFormatter();
                        var settings = (Settings) soapRead.Deserialize(streamRead);
                        return settings;
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            catch (FileNotFoundException)
            {
            }
            return null;
        }
    }
}