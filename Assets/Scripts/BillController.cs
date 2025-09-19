using UnityEngine;

public class BillController : MonoBehaviour
{
    public float deleteTime = 2.0f;//���������܂ł̎���
    public GameObject barrierPrefab;//���ȏ��łƈ��������ɐ�������v���n�u

    void Start()
    {
        //
        Invoke("FildExpansion", deleteTime);
    }
    //
    void FildExpansion()
    {
        Instantiate(barrierPrefab,transform.position, Quaternion.identity);//���D�Ɠ����ꏊ�Ƀo���A�𐶐�
        Destroy(gameObject);//���D�͏���
    }

    //�G�ƂԂ�������o���A����
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            FildExpansion();
        }
    }

}
