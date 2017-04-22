using System;
using System.IO;
using System.Xml.Serialization;

// xsd.exe /c /l:cs /n:DoenaSoft.DVDProfiler.GetCustomDVDFieldDefinitions CustomPluginDataDefinitions.xsd

namespace DoenaSoft.DVDProfiler.GetCustomDVDFieldDefinitions
{
    public partial class CustomPluginDataDefinitions
    {
        private static XmlSerializer s_XmlSerializer;

        [XmlIgnore()]
        public static XmlSerializer XmlSerializer
        {
            get
            {
                if (s_XmlSerializer == null)
                {
                    s_XmlSerializer = new XmlSerializer(typeof(CustomPluginDataDefinitions));
                }
                return (s_XmlSerializer);
            }
        }

        public static CustomPluginDataDefinitions Deserialize(String xml)
        {
            using (StringReader sr = new StringReader(xml))
            {
                CustomPluginDataDefinitions instance;

                instance = (CustomPluginDataDefinitions)(XmlSerializer.Deserialize(sr));
                return (instance);
            }
        }
    }
}