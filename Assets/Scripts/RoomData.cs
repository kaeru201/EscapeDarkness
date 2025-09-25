using UnityEngine;
using UnityEngine.SceneManagement;

//
public enum DoorDirection
{
    up,
    down
}

public class RoomData : MonoBehaviour
{
    public string roomName; //出入り口の識別名
    public string nextRoomName; //シーン切り替え先での行先
    public string nextScene; //シーン切り替え先
    public bool openedDoor; //ドアの開閉状況フラグ
    public DoorDirection direction; //プレイヤーの配置位置
    public MessageData message; //トークデータ
    public GameObject door; //表示/非表示対象のドア情報

    public bool isSavePoint;//セーブポイントに使われるスクリプトにするかどうか

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")　&& !isSavePoint)
        {
            ChangeScene();
        }
    }

    public void ChangeScene()
    {
        //このRoomに触れたらどこに行くのかを変数nextRoomNameで決めておく
        //シーンが切り替わって情報がリセットされる前にstatic変数であるtoRoomNumberに行先情報を記録
        RoomManager.toRoomNumber = nextRoomName;

        SceneManager.LoadScene(nextScene);
    }

    public void DoorOpenCheck()
    {
        //もしも開錠されていたら子オブジェクトである変数doorは非表示
        if (openedDoor) door.SetActive(false);
    }

}
