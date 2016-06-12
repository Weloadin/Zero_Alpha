using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour {

    private static MainManager mainManager;
    private string currentSceneName;
    private string nextSceneName;
    private AsyncOperation resourceUnloadTask;
    private AsyncOperation sceneLoadTask;
    private enum SceneState { Reset, Preload, Load, Unload, Postload, Ready, Run, Count };
    private SceneState sceneState;
    private delegate void UpdateDelegate();
    private UpdateDelegate[] updateDelegates;
    public static void SwitchScene(string nextSceneName)
    {
        if (mainManager != null)
        {
            if (mainManager.currentSceneName != nextSceneName)
            {
                mainManager.nextSceneName = nextSceneName;
            }
            else
            {
                mainManager.sceneState = SceneState.Reset;
            }

        }
    }


    protected void Awake()
    {
        DontDestroyOnLoad(gameObject);
        mainManager = this;

        //PlayerPrefs.DeleteAll();
        updateDelegates = new UpdateDelegate[(int)SceneState.Count];

        //Set each updateDelegate
        updateDelegates[(int)SceneState.Reset] = UpdateSceneReset;
        updateDelegates[(int)SceneState.Preload] = UpdateScenePreload;
        updateDelegates[(int)SceneState.Load] = UpdateSceneLoad;
        updateDelegates[(int)SceneState.Unload] = UpdateSceneUnload;
        updateDelegates[(int)SceneState.Postload] = UpdateScenePostload;
        updateDelegates[(int)SceneState.Ready] = UpdateSceneReady;
        updateDelegates[(int)SceneState.Run] = UpdateSceneRun;

        QualitySettings.vSyncCount = 0;  // VSync must be disabled
        Application.targetFrameRate = 60;

        nextSceneName = "LevelSelection";
        sceneState = SceneState.Reset;

    }
    protected void OnDestroy()
    {
        if (updateDelegates != null)
        {
            for (int i = 0; i < (int)SceneState.Count; i++)
            {
                updateDelegates[i] = null;
            }
            updateDelegates = null;
        }

        if (mainManager != null)
        {
            mainManager = null;
        }
    }


    public static MainManager instance
    {
        get
        {
            if (mainManager == null)
            {

                mainManager = GameObject.FindObjectOfType<MainManager>();
            }
            return mainManager;
        }
    }



    protected void Update()
    {
        if (updateDelegates[(int)sceneState] != null)
        {
            updateDelegates[(int)sceneState]();
        }

    }

    private void UpdateSceneReset()
    {
        // run a gc pass
        System.GC.Collect();
        sceneState = SceneState.Preload;
        
    }

    private void UpdateScenePreload()
    {
        sceneLoadTask = SceneManager.LoadSceneAsync(nextSceneName);
        sceneState = SceneState.Load;
    }

    private void UpdateSceneLoad()
    {
        // done loading?
        if (sceneLoadTask.isDone == true)
        {
            sceneState = SceneState.Unload;

        }
        else
        {


        }
    }

    private void UpdateSceneUnload()
    {
        if (resourceUnloadTask == null)
        {
            resourceUnloadTask = Resources.UnloadUnusedAssets();
        }
        else
        {
            if (resourceUnloadTask.isDone == true)
            {
                resourceUnloadTask = null;
                sceneState = SceneState.Postload;
            }
        }
    }

    private void UpdateScenePostload()
    {
        currentSceneName = nextSceneName;
        sceneState = SceneState.Ready;
    }

    private void UpdateSceneReady()
    {

        System.GC.Collect();
        sceneState = SceneState.Run;
    }

    // wait for scene change
    private void UpdateSceneRun()
    {
        if (currentSceneName != nextSceneName)
        {
            sceneState = SceneState.Reset;
        }
    }

}
