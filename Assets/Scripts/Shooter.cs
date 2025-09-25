using UnityEngine;

public class Shooter : MonoBehaviour
{
    PlayerController playerCnt;

    public GameObject billPrefab;//Instatiateを生成する対象オブジェクト
    public float shootSpeed;//
    public float shootDelay;//
    bool inAttack;//

    void Start()
    {
        playerCnt = GetComponent<PlayerController>();//コンポーネント取得
    }

    // Update is called once per frame
    void Update()
    {
        //スペースキーを押したらお札を投擲
        if (Input.GetButtonDown("Jump")) Shoot();
    }

    public void Shoot()
    {
        if (inAttack || GameManager.bill <= 0) return;

        SoundManager.instance.SEPlay(SEType.Shoot);//お札を投げる音
        GameManager.bill--;//お札の数を減らす
        inAttack = true;//攻撃中

        //プレイヤーの角度を入手
        float angleZ = playerCnt.angleZ;
        //Rotationが扱っているQuatenion型として準備
        Quaternion q = Quaternion.Euler(0, 0, angleZ);

        //生成※お札、プレイヤーの位置、プレイヤーと同じ角度 　生成とすると同時にその情報をobj変数に代入する
        GameObject obj = Instantiate(billPrefab, transform.position, q);

        //生成したオブジェクトのRigid2Dの情報を取得
        Rigidbody2D rbody = obj.GetComponent<Rigidbody2D>();

        //生成したオブジェクトが向くべき方角を入手
        float x = Mathf.Cos(angleZ * Mathf.Deg2Rad);//角度に対する底辺　X軸の方向
        float y = Mathf.Sin(angleZ * Mathf.Deg2Rad);//

        //角度を分解したxとyをもとに方向データを整理
        Vector2 v = (new Vector2(x, y)).normalized * shootSpeed;

        //AddForceで指定した方角に飛ばす
        rbody.AddForce(v, ForceMode2D.Impulse);

        //時間差で攻撃中フラグを解除
        Invoke("StopAttack", shootDelay);

    }

    void StopAttack()
    {
        inAttack = false;//攻撃中フラグをOFFにする
    }

}
