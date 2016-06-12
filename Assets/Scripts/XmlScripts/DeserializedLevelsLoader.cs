using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;

public class DeserializedLevelsLoader
{
	// Levels deserialized
	private DeserializedLevels deserializedLevels;

	private const string prefabsFolder = "Prefab/";
	
	struct ItemStruct
	{
		public GameObject	prefab;
		public float		x;
		public float		y;
        public float        z;
        public float		rot;
		public float		scale_x;
		public float		scale_y;
        public int          givenNumber;
        public int          solutionNumber;
        public int          goalNumber;
        public float        startX;
        public float        endX;
	}
	
	// Cache prefabs in prefabDict
	Dictionary<string,GameObject> prefabPool;

	// Cache all items with locations
	List<ItemStruct> sceneItemsList;

	Transform parentOfXmlItems;
	
	public const string xmlItemsGOName = "XmlItems";

    public void generateItems (int startLevel)
	{
		prefabPool = new Dictionary<string, GameObject>();
		sceneItemsList = new List<ItemStruct>();

		// if the XmlItems gameobject folder remained in the Hierarcy, then delete it
		while (GameObject.Find (xmlItemsGOName) != null)
			MonoBehaviour.DestroyImmediate(GameObject.Find (xmlItemsGOName));
		
		parentOfXmlItems = new GameObject(xmlItemsGOName).transform;

		deserializedLevels = XmlIO.LoadXml<DeserializedLevels>("Levels");

		// if startlevel is in the XML i.e. <Developer StartLevel="3" /> then get level from there
		// otherwise start with level 1
		//int startLevel = int.Parse (deserializedLevels.developer.startLevel);

		DeserializedLevels.Level currentLevel = deserializedLevels.levels[startLevel-1];

		// <Item prefab="Chair" x="1" y="10" rot="90" />
		foreach (DeserializedLevels.Item deserializedItem in currentLevel.items)
		{
			// caching prefabString i.e. "phone"
			string prefabString = deserializedItem.prefab;
			
			// if the prefab in the item XmlNode has not been loaded then add it to the prefabsDict dictionary,
			if (!prefabPool.ContainsKey(prefabString))
			{
				// load prefab
				GameObject prefabObject = Resources.Load (prefabsFolder + prefabString, typeof(GameObject)) as GameObject;
				
				// if unsuccesful, error message and jump to next in the foreach loop
				if (prefabObject == null)
				{
					Debug.LogError ("Prefab \"" + prefabString + "\" does not exists.");
					continue;
				}
				
				// otherwise add to dictionary
				prefabPool.Add (prefabString, prefabObject);
			}

            ItemStruct item;
            item.prefab = prefabPool[prefabString];
			item.x 		= toFloatZeroIfNull(deserializedItem.x);
			item.y 		= toFloatZeroIfNull(deserializedItem.y);
            item.z = toFloatZeroIfNull(deserializedItem.z);
            item.rot 	= toFloatZeroIfNull(deserializedItem.rot);
			item.scale_x= toFloatOneIfNull(deserializedItem.scale_x);
			item.scale_y= toFloatOneIfNull(deserializedItem.scale_y);
            //if (item.prefab.name == "Orange Pad")
                item.givenNumber = toIntOneIfNull(deserializedItem.givenNumber);
            //if (item.prefab.name == "Solution Pad")
                item.solutionNumber = toIntOneIfNull(deserializedItem.solutionNumber);
            //if (item.prefab.name == "Goal Pad")
                item.goalNumber = toIntOneIfNull(deserializedItem.goalNumber);
            //if (item.prefab.name == "Line")
            //{
                item.startX = toFloatZeroIfNull(deserializedItem.startX);
                item.endX = toFloatZeroIfNull(deserializedItem.endX);
            //}
            sceneItemsList.Add(item);

        }

        // Finally instantiate all items
        foreach (ItemStruct item in sceneItemsList)
		{

			// TODO load height coordinate from a directory
			GameObject newGameObject = MonoBehaviour.Instantiate(item.prefab) as GameObject;

			// set position
			setPos3D (newGameObject, new Vector3(item.x, item.y, item.z));

			// set rotation
			setRot2D (newGameObject, item.rot);

            if (item.prefab.name == "Orange Pad")
                SetGivenNumber(newGameObject, item.givenNumber);
            if (item.prefab.name == "Solution Pad")
                SetSolutionNumber(newGameObject, item.solutionNumber);
            if (item.prefab.name == "Goal Pad")
                SetGoalNumber(newGameObject, item.goalNumber);
            if (item.prefab.name == "Line")
                SetLinePosition(newGameObject, item.startX, item.endX);

			// set scale
			newGameObject.transform.localScale = new Vector3 (item.scale_x, item.scale_y, 1);
			
			// set parent
			newGameObject.transform.parent = parentOfXmlItems;
		}
	}

	// DONE, these are only helper functions below

	// if no value then return zero or one, otherwise convert to float
	float toFloatZeroIfNull (string value) { return value == null ? 0 : float.Parse(value);	}
	float toFloatOneIfNull  (string value) { return value == null ? 1 : float.Parse(value);	}

    int toIntZeroIfNull(string value) { return value == null ? 0 : int.Parse(value); }
    int  toIntOneIfNull(string value) { return value == null ? 1 : int.Parse(value); }

    void setPos3D(GameObject g, Vector3 pos)
	{
		g.transform.position = new Vector3 (
			pos.x,
			pos.y,
			pos.z
		);
	}

	void setRot2D(GameObject g, float rot)
	{
		Quaternion rotation = Quaternion.identity;
		rotation.eulerAngles = new Vector3(0, 0, rot);
		g.transform.localRotation = rotation;
	}

    void SetGivenNumber(GameObject g , int number)
    {
        g.GetComponent<OrangePad>().givenNumber = number;
    }

    void SetSolutionNumber(GameObject g, int number)
    {
        g.GetComponent<SolutionPad>().solutionNumber = number;
    }

    void SetGoalNumber(GameObject g, int number)
    {
        g.GetComponent<GoalPad>().goalNumber = number;
    }

    void SetLinePosition(GameObject g, float Start, float End)
    {
        g.GetComponent<LineRenderer>().GetComponent<Line>().startX  =  Start;
        g.GetComponent<LineRenderer>().GetComponent<Line>().endX    =  End;
    }
}
