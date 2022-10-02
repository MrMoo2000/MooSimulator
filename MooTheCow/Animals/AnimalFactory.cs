using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Drawing;

namespace MooTheCow
{
    class AnimalFactory
    {
        private Dictionary<string, Dictionary<string,string>> _animalProperties = new Dictionary<string, Dictionary<string, string>>();

        public AnimalFactory()
        {
            XmlDocument animalDoc = new XmlDocument();
            var AssemblyPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

            animalDoc.Load($"{AssemblyPath}\\Animals\\AnimalsConfig.xml");
            foreach (XmlNode animal in animalDoc.SelectSingleNode ("//animals").ChildNodes)
            {
                Dictionary<string, string> configAnimalProperties = new Dictionary<string, string>();
                foreach (XmlNode animalProperty in animal)
                {
                    configAnimalProperties.Add(animalProperty.Name, animalProperty.InnerText);
                }
                if (configAnimalProperties.ContainsKey("name"))
                {
                    _animalProperties.Add(configAnimalProperties["name"], configAnimalProperties);
                }
            }
        }
        public Animal GetAnimal(string type)
        {
            return new Animal(_animalProperties[type]);
        }
    }
}
