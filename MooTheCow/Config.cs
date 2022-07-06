using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace MooTheCow
{
    static class Config
    {
        public static int HorizonOffset { get; private set; }
        static Config()
        {
            XmlDocument configDoc = new XmlDocument();
            configDoc.Load($"{Environment.CurrentDirectory}\\Config.xml");
            HorizonOffset = int.Parse(configDoc.SelectSingleNode("//horizonOffset").InnerText);
        }
    }
}
