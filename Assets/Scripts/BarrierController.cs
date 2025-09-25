using UnityEngine;

public class BarrierController : MonoBehaviour
{
    public float deleteTime = 5.0f;//消滅するまでの時間

    private void Start()
    {
        SoundManager.instance.SEPlay(SEType.Barrier);//バリアが発生した音

        //deleteTime秒後に消滅
        Destroy(gameObject,deleteTime);
    }
}
