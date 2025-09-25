using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    playing,
    talk,
    gameover,
    gameclear,
    ending
}

public class GameManager : MonoBehaviour
{
    public static GameState gameState;//

    public static bool[] doorsOpenedState = { false, false, false };
    public static int key1;
    public static int key2;
    public static int key3;
    public static bool[] keysPickedState = { false, false, false };//

    public static int bill = 0;//
    public static bool[] itemsPickedState = { false, false, false, false, false };//
    public static bool[] itemSpotLight;//

    public static int playerHP = 3;//


    static public bool hasSpotLight;//スポットライトを持っているかどうか


    void Start()
    {
        //まずはゲームは開始状態にする
        gameState = GameState.playing;
        //シーン名の取得
        Scene currentScene = SceneManager.GetActiveScene();
        // シーンの名前を取得
        string sceneName = currentScene.name;

        switch (sceneName)
        {
            case "Title":
                SoundManager.instance.PlayBgm(BGMType.Title);
                break;
            case "Boss":
                SoundManager.instance.PlayBgm(BGMType.InBoss);
                break;
            case "Opening":
            case "Ending":
                SoundManager.instance.StopBgm();
                break;
            default:
                SoundManager.instance.PlayBgm(BGMType.InGame);
                break;
        }
    }

    public void Update()
    {
        if(gameState == GameState.gameover)
        {
            StartCoroutine(TitleBack());
        }
    }

    IEnumerator TitleBack()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("Title");
    }

}
