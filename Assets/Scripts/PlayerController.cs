using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("プレイヤーの基礎ステータス")]
    public float playerSpeed = 3.0f;

    float axisH;//横方向の入力情報
    float axisV;//縦方向の入力情報

    [Header("プレイヤーの角度計算用")]
    public float angleZ = -90f;//プレイヤーの角度計算用

    [Header("オン/オフの対象スポットライト")]
    public GameObject spotLight;//対象のスポットライト

    bool inDamage;//ダメージ中かどうかのフラグ管理

    //コンポーネント
    Rigidbody2D rbody;
    Animator anime;

    

    


    
    void Start()
    {
        //コンポーネントの取得
        rbody = GetComponent<Rigidbody2D>();
        anime = GetComponent<Animator>();

        //スポットライトを所持していればスポットライトを表示(staticの場合はスプライト名.変数名)
        if (GameManager.hasSpotLight)
        {
            spotLight.SetActive(true);
        }
        
    }

    
    void Update()
    {
        Move();//上下左右の入力値の取得
    }

    private void FixedUpdate()
    {
        //入力状況に応じてPlayerを動かす
        rbody.linearVelocity = (new Vector2(axisH, axisV)).normalized * playerSpeed;
    }

    //上下左右の入力値の取得
    public void Move()
    {
        //axisHとaxisVに入力情報を代入する
        axisH = Input.GetAxisRaw("Horizontal");
        axisV = Input.GetAxisRaw("Vertical"); 
    }
        
    
}
