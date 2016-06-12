using UnityEngine;
using System.Collections;

public class GamePlay : HudManager {

    // Use this for initialization
    void Start()
    {
        DeserializedLevelsLoader load = new DeserializedLevelsLoader();
        load.generateItems(LevelManager.instance.currentLevel);
    }


    public void Reload()
    {
        LevelManager.instance.GenerateLevel(LevelManager.instance.currentLevel);
    }
}
