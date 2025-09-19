using UnityEngine;

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

    public static bool[] doorsOpendState = { false, false, false };
    public static int key1;
    public static int key2;
    public static int key3;
    public static bool[] keysPickedState = { false, false, false };//

    public static int bill = 10;//
    public static bool[] itemsPickedState = { false, false, false, false, false };//
    public static bool[] itemSpotLight;//

    public static int playerHP = 3;//


    static public bool hasSpotLight;//スポットライトを持っているかどうか


    void Start()
    {
        //まずはゲームは開始状態にする
        gameState = GameState.playing;
    }


}
