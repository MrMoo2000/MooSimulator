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

        // Goal is to make something where we can pass in a string, and it will know what to create...

        public AnimalFactory()
        {
            XmlDocument animalDoc = new XmlDocument();
            animalDoc.Load($"{Environment.CurrentDirectory}\\Animals\\AnimalsConfig.xml");
            foreach (XmlNode animal in animalDoc.SelectSingleNode ("//animals").ChildNodes)
            {
                Dictionary<string, string> configAnimalProperties = new Dictionary<string, string>();
                foreach (XmlNode animalProperty in animal)
                {
                    //Console.WriteLine($"{animalProperty.Name} + {animalProperty.InnerText}");
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
