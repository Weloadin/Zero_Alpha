using UnityEngine;
using System.Collections;
using System.Xml;

public class DeserializedLevelsSaver
{
	public const string xmlItemsToExportGOName = "XmlItemsToExport";

	public void saveExportItems ()
	{
		
		// Create XmlItemsToExport if does not exist yet
		if (GameObject.Find (xmlItemsToExportGOName) == null)
			new GameObject(xmlItemsToExportGOName);
		
		GameObject xmlItemsToExportGO = GameObject.Find (xmlItemsToExportGOName);
		var xmlItemsToExportGOchildren = xmlItemsToExportGO.GetComponentsInChildren<Transform>();
		
		// Check if any children exist
		if (xmlItemsToExportGOchildren.Length == 0)
			Debug.LogError ("Add the prefabs to " + xmlItemsToExportGOName);
		
		DeserializedLevels.Level levelXml = new DeserializedLevels.Level();

		int n = 0;
		// count number of children skipping sub-items
		foreach (Transform item in xmlItemsToExportGOchildren)
			if (item.parent == xmlItemsToExportGO.transform) n++;

		// the items array should have that many elements
		levelXml.items = new DeserializedLevels.Item[n];

		// use i for counting items, i would be equal (one more to be precise) to n at the end of the cycle
		int i = 0;

		// cycle through the children again and add them to items
		foreach (Transform item in xmlItemsToExportGOchildren)
		{
			// skip sub-items
			if (item.parent != xmlItemsToExportGO.transform) continue;

			levelXml.items[i] = new DeserializedLevels.Item();

			levelXml.items[i].prefab = 	item.name;
			levelXml.items[i].x = 		toStringNullIfZero (item.transform.position.x);
			levelXml.items[i].y = 		toStringNullIfZero (item.transform.position.y);
            levelXml.items[i].z =       toStringNullIfZero(item.transform.position.z);
            levelXml.items[i].rot = 	toStringNullIfZero (item.localRotation.eulerAngles.z);
			levelXml.items[i].scale_x = toStringNullIfOne  (item.localScale.x);
			levelXml.items[i].scale_y = toStringNullIfOne  (item.localScale.y);
            if(item.name == "Orange Pad")
                levelXml.items[i].givenNumber = toStringNullIfZero(item.GetComponent<OrangePad>().givenNumber);
            if (item.name == "Solution Pad")
                levelXml.items[i].solutionNumber = toStringNullIfZero(item.GetComponent<SolutionPad>().solutionNumber);
            if (item.name == "Goal Pad")
                levelXml.items[i].goalNumber = toStringNullIfZero(item.GetComponent<GoalPad>().goalNumber);
            if (item.name == "Line")
            {
                levelXml.items[i].startX = toStringNullIfZero(item.GetComponent<Line>().startX);
                levelXml.items[i].endX = toStringNullIfZero(item.GetComponent<Line>().endX);
            }


            // increase i for the next cycle
            i++;
		}


		// Export just one level
		DeserializedLevels levelsXmlToExport = new DeserializedLevels();
		levelsXmlToExport.levels = new DeserializedLevels.Level[1];
		levelsXmlToExport.levels[0] = levelXml;
		XmlIO.SaveXml<DeserializedLevels>(levelsXmlToExport, "./Assets/Resources/" + xmlItemsToExportGOName + ".xml");
	}

	string toStringNullIfZero (float num) {	return num == 0 ? null : mathRound(num,2).ToString(); }
	string toStringNullIfOne (float num)  {	return num == 1 ? null : mathRound(num,2).ToString(); }

    string toStringNullIfZero(int num) { return num == 0 ? null : mathRound(num, 2).ToString(); }
    string toStringNullIfOne(int num) { return num == 1 ? null : mathRound(num, 2).ToString(); }


    float mathRound (float round, int decimals)
	{
		return Mathf.Round(round * Mathf.Pow(10,decimals)) / Mathf.Pow(10,decimals);
	}
}
