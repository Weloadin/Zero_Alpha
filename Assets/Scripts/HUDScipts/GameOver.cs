using UnityEngine;
using System.Collections;

public class GameOver : HudManager
{
    

    public void Reload()
    {
        LevelManager.instance.GenerateLevel(LevelManager.instance.currentLevel - 1);
    }
    		
}
