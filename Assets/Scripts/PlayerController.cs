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
        angleZ = GetAngle();//その時の角度を変数angleZに反映
        Animation();//angleZを利用してアニメーション
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

    //その時のプレイヤーの角度を取得
    public float GetAngle()
    {
        //現在座標の取得
        Vector2 fromPos = transform.position;

        //その瞬間のキー入力値(axisH、axisV)に応じた予測座標の取得
        Vector2 toPos = new Vector2(fromPos.x + axisH, fromPos.y + axisV);

        float angle;//returnされる値の準備

        //もしも何かしらの入力があれば、新たに角度算出
        if (axisH != 0 || axisV != 0)
        {
            float dirX = toPos.x - fromPos.x;
            float dirY = toPos.y - fromPos.y;

            //第一引数に高さY,第二引数にXを与えると角度をラジアン形式で算出（円周の長さで表現）
            float rad = Mathf.Atan2(dirY, dirX);

            //ラジアン値をオイラー値（デグリー）に変換
            angle = rad * Mathf.Rad2Deg;

        }
        //何も入力されていなければ前フレーム角度情報を据え置き
        else
        {
            angle = angleZ;
        }

        return angle;
    }

    void Animation()
    {
        //なんらかの入力がある場合
        if (axisH != 0 || axisV != 0)
        {

            //
            anime.SetBool("run", true);

            //angleで方角を決める　パラメータdirection int型
            //int型のdirection 下:0　上:1　右:2　左:それ以外

            if (angleZ > -135f && angleZ < -45f)//下方向
            {
                anime.SetInteger("direction", 0);
            }
            else if (angleZ >= -45 && angleZ <= 45f)//右
            {
                anime.SetInteger("direction", 2);
                transform.localScale = new Vector2(1, 1);
            }
            else if (angleZ >= 45f && angleZ <= 135f)//上
            {
                anime.SetInteger("direction", 1);
            }
            else//左
            {
                anime.SetInteger("direction", 3);
                transform.localScale = new Vector2(-1, 1);
            }
        }
        else//何も入力がない場合
        {
            anime.SetBool("run", false);//走るフラグをoff
        }
    }

}
