using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// タイマークラス
/// </summary>

public class Timer :MonoBehaviour
{
    static Timer instance;
    // UI Text指定用
    public Text UIText;

    //現在のカウント
    static float count;

    //カウント最大値
    [SerializeField] static float countmax = 30.0f;

    //カウントストップ用
    static public bool countstop;

    [SerializeField] Image timerImage;
    [SerializeField] GameObject timeupBgObj;
    Color defaultTextColor = Color.white;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }

    void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }
    }

    void Start()
    {
        count = countmax;
        countstop = false;
        if (UIText != null)
        {
            defaultTextColor = UIText.color;
        }
        if (timeupBgObj != null)
        {
            timeupBgObj.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        UIText.text = count.ToString("f0");
        //タイム残り5秒表示
        if ((count <= 5) && (count > 0))
        {
            UIText.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
            if (timeupBgObj != null)
            {
                timeupBgObj.SetActive(true);
            }
        }
        else if (count < 0)
        {
            TurnEnd();
        }
    }
    public static void TurnEnd()
    {
        MainGameProgress.gameStaus = MainGameProgress.GameStaus.ClearCheckNow;
        CountReset();
    }

    //カウントダウン
    public static void CountDown()
    {
        if (countstop == true)
        {
            //Debug.Log("Timer停止");
        }
        else
        {
            count -= Time.deltaTime;
        }
        //Debug.Log(count);
    }

    void ResetWarningVisuals()
    {
        if (UIText != null)
        {
            UIText.color = defaultTextColor;
        }

        if (timeupBgObj != null)
        {
            timeupBgObj.SetActive(false);
        }
    }

    public static void CountReset()
    {
        count = countmax;
        if (instance != null)
        {
            instance.ResetWarningVisuals();
        }
    }
}
