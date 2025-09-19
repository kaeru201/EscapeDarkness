using UnityEngine;

public class BillData : MonoBehaviour
{
    Rigidbody2D rbody;
    public int itemNum;//�A�C�e���̎��ʔԍ�

    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();//Rigidboy2D�R���|�[�l���g�̎擾
        rbody.bodyType = RigidbodyType2D.Static;//Rigidbody�̋�����Î~
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.bill++;//1���₷
            GameManager.itemsPickedState[itemNum] = true;

            //�A�C�e���擾���o
            //�@�R���C�_�[�𖳌���
            //GetComponent<CircleCollider2D>().enabled = false; �@�����̏ȗ��`
            this.gameObject.GetComponent<CircleCollider2D>().enabled = false;

            //�ARigidbody2D�̕���(Dynamic�ɂ���)
            rbody.bodyType = RigidbodyType2D.Dynamic;

            //�B��ɑł��グ(�����5�̗́j
            rbody.AddForce(new Vector2(0, 5), ForceMode2D.Impulse);

            //�C�������M�𖕏�(0.5�b��)
            Destroy(gameObject, 0.5f);


            //�@�R���C�_�[�𖳌���

            //�ARigidbody2D�̕���(Dynamic�ɂ���)

            //�B��ɑł��グ(�����5�̗́j


            //�C�������M�𖕏�(0.5�b��)
        }
    }

}
