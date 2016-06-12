using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelSelection : MonoBehaviour {


    public GameObject[] levels;
    int unlockLevels;


    void Start()
    {
        unlockLevels = PlayerPrefs.GetInt("Level");
        CheckLock();
    }

    void CheckLock()
    {
        for (int i = 0; i <= unlockLevels; i++)
        {
            levels[i].GetComponent<Button>().interactable = true;
            levels[i].transform.GetChild(1).gameObject.SetActive(false);
        }
    }

    public void PlayLevel(int level)
    {
        LevelManager.instance.GenerateLevel(level);
    }

    
	
}
