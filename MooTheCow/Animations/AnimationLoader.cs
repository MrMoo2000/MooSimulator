using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;

namespace MooTheCow
{
    public struct AnimalAnimationBlueprints
    {
        public string AnimalName;
        public List<AnimationBlueprint> Animations;
    }
    public struct AnimationBlueprint
    {
        public string AnimationName;
        public List<string> FrameFileNames;
        public string Delay;
    }

    class AnimationLoader
    {
        public static Dictionary<string, AnimalAnimationBlueprints> Animations = new Dictionary<string, AnimalAnimationBlueprints>();
        public static Dictionary<string, Dictionary<AnimationTypes, IAnimation>> AnimalAnimations = new Dictionary<string, Dictionary<AnimationTypes, IAnimation>>();
        // Next we need to take our blueprints, and turn them into actual animations that can be looked up by the factory. 

        public static void LoadAnimiations()
        {
            XmlDocument configDoc = new XmlDocument();
            configDoc.Load($"{Environment.CurrentDirectory}\\Animations.xml");
            XmlNode animalsNode = configDoc.SelectSingleNode("//animals");

            foreach(XmlNode animal in animalsNode)
            {
                var animalAnimationBlueprint = new AnimalAnimationBlueprints();
                animalAnimationBlueprint.AnimalName = animal.Name;
                animalAnimationBlueprint.Animations = new List<AnimationBlueprint>();

                foreach (XmlNode animalAnimation in animal)
                {
                    var animationBlueprint = new AnimationBlueprint();
                    animationBlueprint.AnimationName = animalAnimation.Name;
                    animationBlueprint.Delay = animalAnimation.Attributes["delay"].Value;
                    animationBlueprint.FrameFileNames = new List<string>();
                    foreach (XmlNode frame in animalAnimation)
                    {
                        Console.WriteLine(frame.InnerText);
                        animationBlueprint.FrameFileNames.Add(frame.InnerText);
                    }
                    animalAnimationBlueprint.Animations.Add(animationBlueprint);
                }
                Animations.Add(animalAnimationBlueprint.AnimalName, animalAnimationBlueprint);
            }
            CreateAnimations();
        }
        public static void CreateAnimations()
        {
            char colorEscapeBegin = '['; //TODO Change escape to config values 
            char colorEscapeEnd = ']';

            foreach (KeyValuePair<string, AnimalAnimationBlueprints> animalAnimations in Animations)
            {
                var animalName = animalAnimations.Value.AnimalName;
                var animationList = new Dictionary<AnimationTypes, IAnimation>();
                foreach (AnimationBlueprint animationBlueprint in animalAnimations.Value.Animations)
                {
                    var animationType = Enum.Parse<AnimationTypes>(animationBlueprint.AnimationName);
                    var delay = int.Parse(animationBlueprint.Delay);


                    var frames = new List<IObjectTile[,]>();
                    foreach (string fileName in animationBlueprint.FrameFileNames)
                    {
                        var filePath = $"{Environment.CurrentDirectory}\\Animations\\{fileName}.txt";
                        string[] readText = File.ReadAllLines(filePath);
                        var height = readText.Length;
                        var width = 0;
                        foreach (string s in readText)
                        {
                            var charLineCount = 0;
                            for (int i = 0; i < s.Length; i++)
                            {
                                if (s[i] == colorEscapeBegin)
                                {
                                    i = s.IndexOf(colorEscapeEnd, i);
                                }
                                else
                                {
                                    charLineCount++;
                                }
                            }
                            width = charLineCount > width ? charLineCount : width ;
                        }
                        var stringToObjectTile = new StringToObjectTile(width, height);
                        foreach (string s in readText)
                        {
                            stringToObjectTile.Write2(s);
                            stringToObjectTile.nextLine();
                        }
                        frames.Add(stringToObjectTile.ObjectTiles);
                    }
                    IAnimation animation = new Animation(frames, delay);
                    animationList.Add(animationType, animation);
                }
                AnimalAnimations.Add(animalName, animationList);
            }
        }
    }
}
