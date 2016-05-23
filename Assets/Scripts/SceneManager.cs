using UnityEngine;
using System.Collections;

public class SceneManager : MonoBehaviour {

   public void GoToLevelSelect()
    {
        Application.LoadLevel("LevelSelection");
    }
   public void GoToLevel1()
   {
       Application.LoadLevel("GamePlay");
   }
   public void GoToLevel2()
   {
       Application.LoadLevel("GameOver");
   }
   public void GoToBack()
   {
       Application.LoadLevel("MainMenu");
   }
 

}
