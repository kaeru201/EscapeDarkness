using UnityEngine;

public class CameraController : MonoBehaviour
{
    GameObject player;//ターゲットとなるプレイヤーの情報

    public float follwSpeed = 5.0f;//プレイヤーに追いつくスピード

    void Start()
    {
        //プレイヤー情報を取得
        player = GameObject.FindGameObjectWithTag("Player");

        //スタートした瞬間のカメラの現在地(カメラのZ軸は一歩引きたいから-10）
        transform.position = new Vector3(player.transform.position.x,  player.transform.position.y,  -10);
    }

    
    void  LateUpdate()
    {
        //==のところを!=にしていた(!=なら{}で本文を囲むべき）
        if (player == null) return;//ゲームオーバーの時のエラー回避

        //目指すべきポイント
        Vector3 nextPos = new Vector3(player.transform.position.x, player.transform.position.y, -10);

        //現在のポイント
        Vector3 nowPos = transform.position;

        //現在地　→　目指すべき地点まので補間
        transform.position = Vector3.Lerp(nowPos,nextPos,follwSpeed *  Time.deltaTime);

    }
}

