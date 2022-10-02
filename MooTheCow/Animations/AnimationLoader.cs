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

        private static char _colorEscapeBegin = '['; 
        private static char _colorEscapeEnd = ']';

        static AnimationLoader()
        {
            LoadAnimiations();
        }

        private static void LoadAnimiations()
        {
            var animationsDoc = GetAnimationsDoc();
            var animals = GetAnimalAnimationNode(animationsDoc);
            foreach (XmlNode animal in animals)
            {
                var animalAnimationBlueprint = CreateAnimalAnimationBlueprint(animal);
                Animations.Add(animalAnimationBlueprint.AnimalName, animalAnimationBlueprint);
            }
            AnimalAnimations = CreateAnimations();
        }
        // Get the XML Doc of animations
        private static XmlDocument GetAnimationsDoc()
        {
            XmlDocument configDoc = new XmlDocument();
            var AssemblyPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

            configDoc.Load($"{AssemblyPath}\\Animations\\AnimationsConfig.xml");
            return configDoc;
        }
        // Get the node where animal animations are stored
        private static XmlNode GetAnimalAnimationNode(XmlDocument animationsDoc)
        {
            XmlNode animalsNode = animationsDoc.SelectSingleNode("//animals");
            return animalsNode;
        }
        // Create the Animal Animation blueprint from the XML Node 
        private static AnimalAnimationBlueprints CreateAnimalAnimationBlueprint(XmlNode animal)
        {
            var animalAnimationBlueprint = new AnimalAnimationBlueprints()
            {
                AnimalName = animal.Name,
                Animations = new List<AnimationBlueprint>()
            };

            foreach (XmlNode animalAnimation in animal)
            {
                animalAnimationBlueprint.Animations.Add(CreateAnimationBlueprint(animalAnimation));
            }
            return animalAnimationBlueprint;
        }
        // Create the specific animation blueprint from XML Node 
        private static AnimationBlueprint CreateAnimationBlueprint(XmlNode animalAnimation)
        {
            var animationBlueprint = new AnimationBlueprint()
            {
                AnimationName = animalAnimation.Name,
                Delay = animalAnimation.Attributes["delay"].Value,
                FrameFileNames = new List<string>()
            };
            foreach (XmlNode frame in animalAnimation)
            {
                animationBlueprint.FrameFileNames.Add(frame.InnerText);
            }
            return animationBlueprint;
        }

        public static Dictionary<string, Dictionary<AnimationTypes, IAnimation>> CreateAnimations()
        {
            var createdAnimations = new Dictionary<string, Dictionary<AnimationTypes, IAnimation>>();
            foreach (KeyValuePair<string, AnimalAnimationBlueprints> animalAnimations in Animations)
            {
                var animalName = animalAnimations.Value.AnimalName;
                var animationList = CreateAnimalAnimations(animalAnimations.Value);
                createdAnimations.Add(animalName, animationList);
            }
            return createdAnimations;
        }

        public static Dictionary<AnimationTypes, IAnimation> CreateAnimalAnimations(AnimalAnimationBlueprints animalAnimationBlueprints)
        {
            var animationList = new Dictionary<AnimationTypes, IAnimation>();
            foreach (AnimationBlueprint animationBlueprint in animalAnimationBlueprints.Animations)
            {
                var frames = CreateFrames(animationBlueprint);
                var delay = int.Parse(animationBlueprint.Delay);
                IAnimation animation = new Animation(frames, delay);

                var animationType = Enum.Parse<AnimationTypes>(animationBlueprint.AnimationName);
                animationList.Add(animationType, animation);
            }
            return animationList;
        }

        private static List<IObjectTile[,]> CreateFrames(AnimationBlueprint animationBlueprint)
        {
            var frames = new List<IObjectTile[,]>();
            foreach (string fileName in animationBlueprint.FrameFileNames)
            {
                var AssemblyPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                var filePath = $"{AssemblyPath}\\Animations\\Sprites\\{fileName}.txt";
                string[] readText = File.ReadAllLines(filePath);
                var stringToObjectTile = new StringToObjectTile(GetMaxWidth(readText), readText.Length);
                foreach (string line in readText)
                {
                    stringToObjectTile.Write(line);
                    stringToObjectTile.nextLine();
                }
                frames.Add(stringToObjectTile.ObjectTiles);
            }
            return frames;
        }

        // Get the max width of our ASCII image 
        private static int GetMaxWidth(string[] readText)
        {
            int maxWidth = 0;
            foreach (string s in readText)
            {
                var charLineCount = 0;
                for (int i = 0; i < s.Length; i++)
                {
                    if (s[i] == _colorEscapeBegin)
                    {
                        i = s.IndexOf(_colorEscapeEnd, i);
                    }
                    else
                    {
                        charLineCount++;
                    }
                }
                maxWidth = charLineCount > maxWidth ? charLineCount : maxWidth;
            }
            return maxWidth;
        }
    }
}
