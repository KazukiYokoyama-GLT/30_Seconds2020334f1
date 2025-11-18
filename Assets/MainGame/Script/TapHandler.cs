using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class TapHandler : MonoBehaviour
{
    public AudioClip genericTapSound; // 汎用タップSE
    public AudioClip startTapSound;   // スタート画面専用タップSE
    private Blinker blinker;
    private BlinkerClock blinkerClock;

    void Start()
    {
        blinker = FindObjectOfType<Blinker>(); // Blinkerコンポーネントを取得
        blinkerClock = FindObjectOfType<BlinkerClock>();
        SoundManager.instance.ChangeBGM(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name); // 現在のシーンに基づいてBGMを再生
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Start") // 左クリックまたはタップ
        {
            StartCoroutine(HandleTap());
        }
    }

    IEnumerator HandleTap()
    {
        blinker.StopBlinking(); // Blinkerの点滅を停止
        blinker.StartCoroutine(blinker.FadeOut()); // Blinkerのフェードアウトを開始
        blinkerClock.StartCoroutine(blinkerClock.FadeOut());
        yield return PlayTapSound(); // タップ時のSEを再生
        yield return ChangeScene();
    }

    IEnumerator PlayTapSound()
    {
        string currentSceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        AudioClip clipToPlay = null;

        if (currentSceneName == "Start") // スタート画面のシーン名を指定
        {
            clipToPlay = startTapSound;
        }
        else
        {
            clipToPlay = genericTapSound;
        }

        if (clipToPlay != null)
        {
            SoundManager.instance.PlaySound(clipToPlay);
            yield return new WaitForSeconds(clipToPlay.length); // サウンドの再生が終わるのを待つ
        }
    }

    IEnumerator ChangeScene()
    {
        SceneTransitions.SceneLaod(SceneTransitions.SceneName.TITLE);
        yield break;
    }
}
