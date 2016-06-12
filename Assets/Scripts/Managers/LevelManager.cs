using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelManager : MonoBehaviour {
    

    private static LevelManager levelManager;
    public int levelsUnlocked, currentLevel;
    public int[] MovesInLevel;
    public int totalMoves;

    // Use this for initialization
    void Start ()
    {
        DontDestroyOnLoad(gameObject);

        levelsUnlocked = PlayerPrefs.GetInt("Level");
        currentLevel = levelsUnlocked + 1;
    }

    void CheckMoves()
    {
        totalMoves = MovesInLevel[currentLevel];
    }


    public void GenerateNextLevel(bool isReload)
    {
        if(!isReload)
        {
            currentLevel = currentLevel + 1;
            Unlock(currentLevel);
            StartCoroutine(WaitForAnim(1.5f, false));
        }
        else
        {
            StartCoroutine(WaitForAnim(1.5f, true));
        }
    }

    

    void Unlock(int level)
    {
        levelsUnlocked = level - 1;
        PlayerPrefs.SetInt("Level",levelsUnlocked);
    }
    


    public void GenerateLevel(int level)
    {
        currentLevel = level;
        CheckMoves();
        MainManager.SwitchScene("GamePlay");
        GameManager.instance.SetGameState(GameManager.GameState.GamePlay);
    }   

    IEnumerator WaitForAnim(float waitTime, bool isReload)
    {
        yield return new WaitForSeconds(waitTime);
        if(isReload)
        {
            CheckMoves();
            MainManager.SwitchScene("GamePlay");
            GameManager.instance.SetGameState(GameManager.GameState.GamePlay);
        }
        else
        {
            MainManager.SwitchScene("GameOver");
        }
    }


    public static LevelManager instance
    {
        get
        {
            if (levelManager == null)
            {
                levelManager = GameObject.FindObjectOfType<LevelManager>();
            }
            return levelManager;
        }
    }
}
