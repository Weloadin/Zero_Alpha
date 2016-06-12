using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

[XmlRoot("Levels")]
public class DeserializedLevels
{
	[XmlElement ("Developer")]
    public Developer developer;
    public class Developer
    {
        [XmlAttribute("StartLevel")]
        public string startLevel;
    }

    [XmlElement ("Level")]
	public Level[] levels;
	public class Level
	{
		[XmlAttribute ("playerx")]
		public string playerx;
		
		[XmlAttribute ("playery")]
		public string playery;
		
		[XmlAttribute ("playerrot")]
		public string playerrot;
		
		[XmlElement("Item")]
		public Item[] items;
	}

	public class Item
	{
		[XmlAttribute ("prefab")]
		public string prefab;
		
		[XmlAttribute ("x")]
		public string x;
		
		[XmlAttribute ("y")]
		public string y;

        [XmlAttribute("z")]
        public string z;

        [XmlAttribute ("rot")]
		public string rot;
		
		[XmlAttribute ("scale_x")]
		public string scale_x;
		
		[XmlAttribute ("scale_y")]
		public string scale_y;

        [XmlAttribute("GivenNumber")]
        public string givenNumber;

        [XmlAttribute("SolutionNumber")]
        public string solutionNumber;

        [XmlAttribute("GoalNumber")]
        public string goalNumber;

        [XmlAttribute("StartX")]
        public string startX;

        [XmlAttribute("EndX")]
        public string endX;



    }
}

