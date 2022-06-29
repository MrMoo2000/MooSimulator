using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace MooTheCow
{
    class Config
    {
        public int HorizonOffset { get; }
        public Config()
        {
            XmlDocument configDoc = new XmlDocument();
            configDoc.Load($"{Environment.CurrentDirectory}\\Config.xml");
            HorizonOffset = int.Parse(configDoc.SelectSingleNode("//horizonOffset").InnerText);
        }
    }
}
