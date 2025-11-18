using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitions : MonoBehaviour
{
    static public SceneTransitions instance;

    static string nowscenename;
    public static string NowSceneName
    {
        get
        {
            return nowscenename;
        }
    }
    static string oldscenename;
    public static string OldSceneName
    {
        get
        {
            return oldscenename;
        }
    }
    public enum SceneName
    {
        TITLE,
        INTERVAL,
        STORY,
        ENDING,
        MAINGAMEFIRST,
        MAINGAMELAST,
    }

    static readonly Dictionary<SceneName, string> SceneNameMap = new Dictionary<SceneName, string>
    {
        { SceneName.TITLE, "title" },
        { SceneName.INTERVAL, "interval" },
        { SceneName.STORY, "story" },
        { SceneName.ENDING, "ending" },
        { SceneName.MAINGAMEFIRST, "maingamefirst" },
        { SceneName.MAINGAMELAST, "maingamelast" },
    };


    void Awake()
    {
        if (instance == null)
        {

            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        oldscenename = nowscenename;
    }

    // Update is called once per frame
    void Update()
    {
        if (MainGameProgress.gameStaus != MainGameProgress.GameStaus.IntervalStart &&
            MainGameProgress.gameStaus != MainGameProgress.GameStaus.IntervalEnd)
            nowscenename = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        //Debug.Log("現在のシーン:" + nowscenename);
        // Debug.Log("過去のシーン:" + oldscenename);
    }

    /// <summary>
    /// シーン遷移
    /// </summary>
    /// <param name="scene">列挙型のシーン名</param>
    public static string GetSceneName(SceneName scene)
    {
        if (SceneNameMap.TryGetValue(scene, out var sceneName))
        {
            return sceneName;
        }

        Debug.LogError($"Scene name mapping for {scene} is not defined.");
        return string.Empty;
    }

    public static void SceneLaod(SceneName scene)
    {
        oldscenename = nowscenename;
        string sceneName = GetSceneName(scene);
        if (string.IsNullOrEmpty(sceneName))
        {
            return;
        }

        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
}
