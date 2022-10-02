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
            var AssemblyPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            XmlDocument configDoc = new XmlDocument();
            configDoc.Load($"{AssemblyPath}\\Config.xml");
            HorizonOffset = int.Parse(configDoc.SelectSingleNode("//horizonOffset").InnerText);
        }
    }
}
