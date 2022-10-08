using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Linq;
using System.Drawing;

namespace MooTheCow
{
    class AnimalFactory
    {
        private Dictionary<string, Dictionary<string,string>> _animalProperties = new Dictionary<string, Dictionary<string, string>>();

        public AnimalFactory()
        {
            var AssemblyPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

            XmlDocument animalDoc = new XmlDocument();

            animalDoc.Load($"{AssemblyPath}\\Animals\\AnimalsConfig.xml");
            
            foreach (XmlNode animal in animalDoc.SelectSingleNode ("//animals").ChildNodes)
            {
                var configAnimalProperties = animal
                    .Cast<XmlNode>()
                    .ToDictionary(animalProperty => animalProperty.Name, animalProperty => animalProperty.InnerText);
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
