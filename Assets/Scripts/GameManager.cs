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

    public static bool[] doorsOpendState;
    public static int key1;
    public static int key2;
    public static int key3;
    public static bool[] keyPickedState;//

    public static int  bill;//
    public static bool[] itemPickedState;//
    public static bool[] itemSpotLight;//

    public static int playerHP = 3;//


    static public bool hasSpotLight;//�X�|�b�g���C�g�������Ă��邩�ǂ���

    
    void Start()
    {
        //�܂��̓Q�[���͊J�n��Ԃɂ���
        gameState = GameState.playing;
    }

   
}
