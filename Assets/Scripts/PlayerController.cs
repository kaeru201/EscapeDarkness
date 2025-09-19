using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("�v���C���[�̊�b�X�e�[�^�X")]
    public float playerSpeed = 3.0f;

    float axisH;//�������̓��͏��
    float axisV;//�c�����̓��͏��

    [Header("�v���C���[�̊p�x�v�Z�p")]
    public float angleZ = -90f;//�v���C���[�̊p�x�v�Z�p

    [Header("�I��/�I�t�̑ΏۃX�|�b�g���C�g")]
    public GameObject spotLight;//�Ώۂ̃X�|�b�g���C�g

    bool inDamage;//�_���[�W�����ǂ����̃t���O�Ǘ�

    //�R���|�[�l���g
    Rigidbody2D rbody;
    Animator anime;







    void Start()
    {
        //�R���|�[�l���g�̎擾
        rbody = GetComponent<Rigidbody2D>();
        anime = GetComponent<Animator>();

        //�X�|�b�g���C�g���������Ă���΃X�|�b�g���C�g��\��(static�̏ꍇ�̓X�v���C�g��.�ϐ���)
        if (GameManager.hasSpotLight)
        {
            spotLight.SetActive(true);
        }

    }


    void Update()
    {
        if (GameManager.gameState != GameState.playing) return;

        Move();//�㉺���E�̓��͒l�̎擾
        angleZ = GetAngle();//���̎��̊p�x��ϐ�angleZ�ɔ��f
        Animation();//angleZ�𗘗p���ăA�j���[�V����
    }

    private void FixedUpdate()
    {
        if (GameManager.gameState != GameState.playing) return;
        //�_���[�W�t���O�������Ă��邠����
        if (inDamage)
        {
            //�_�ŉ��o
            //Sin���\�b�h�̊p�x���ɃQ�[���J�n����̌o�ߎ��Ԃ�^����
            float val = Mathf.Sin(Time.time * 50);
            if (val > 0)
            {
                //
                gameObject.GetComponent<SpriteRenderer>().enabled = true;
            }
            else
            {
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
            }
                //
                return;
        }
        //���͏󋵂ɉ�����Player�𓮂���
        rbody.linearVelocity = (new Vector2(axisH, axisV)).normalized * playerSpeed;
    }

    //�㉺���E�̓��͒l�̎擾
    public void Move()
    {
        //axisH��axisV�ɓ��͏���������
        axisH = Input.GetAxisRaw("Horizontal");
        axisV = Input.GetAxisRaw("Vertical");
    }

    //���̎��̃v���C���[�̊p�x���擾
    public float GetAngle()
    {
        //���ݍ��W�̎擾
        Vector2 fromPos = transform.position;

        //���̏u�Ԃ̃L�[���͒l(axisH�AaxisV)�ɉ������\�����W�̎擾
        Vector2 toPos = new Vector2(fromPos.x + axisH, fromPos.y + axisV);

        float angle;//return�����l�̏���

        //��������������̓��͂�����΁A�V���Ɋp�x�Z�o
        if (axisH != 0 || axisV != 0)
        {
            float dirX = toPos.x - fromPos.x;
            float dirY = toPos.y - fromPos.y;

            //�������ɍ���Y,��������X��^����Ɗp�x�����W�A���`���ŎZ�o�i�~���̒����ŕ\���j
            float rad = Mathf.Atan2(dirY, dirX);

            //���W�A���l���I�C���[�l�i�f�O���[�j�ɕϊ�
            angle = rad * Mathf.Rad2Deg;

        }
        //�������͂���Ă��Ȃ���ΑO�t���[���p�x���𐘂��u��
        else
        {
            angle = angleZ;
        }

        return angle;
    }

    void Animation()
    {
        //�Ȃ�炩�̓��͂�����ꍇ
        if (axisH != 0 || axisV != 0)
        {

            //
            anime.SetBool("run", true);

            //angle�ŕ��p�����߂�@�p�����[�^direction int�^
            //int�^��direction ��:0�@��:1�@�E:2�@��:����ȊO

            if (angleZ > -135f && angleZ < -45f)//������
            {
                anime.SetInteger("direction", 0);
            }
            else if (angleZ >= -45 && angleZ <= 45f)//�E
            {
                anime.SetInteger("direction", 2);
                transform.localScale = new Vector2(1, 1);
            }
            else if (angleZ >= 45f && angleZ <= 135f)//��
            {
                anime.SetInteger("direction", 1);
            }
            else//��
            {
                anime.SetInteger("direction", 3);
                transform.localScale = new Vector2(-1, 1);
            }
        }
        else//�������͂��Ȃ��ꍇ
        {
            anime.SetBool("run", false);//����t���O��off
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //�Ԃ��������肪Enemy��������
        if (collision.gameObject.CompareTag("Enemy"))
        {
            GetDamage(collision.gameObject);//�_���[�W����
        }
    }

    void GetDamage(GameObject enemy)
    {
        if (GameManager.gameState != GameState.playing) return;

        GameManager.playerHP--;//�v���C���[HP��1���炷

        if (GameManager.playerHP > 0)
        {
            //
            rbody.linearVelocity = Vector2.zero; //new Vector2(0.0)
            //�v���C���[�ƓG�Ƃ̍����擾���A���������߂�
            Vector3 v = (transform.position - enemy.transform.position).normalized;
            //
            rbody.AddForce(v * 4, ForceMode2D.Impulse);

            //
            inDamage = true;

            Invoke("DamageEnd", 0.25f);
        }
        else
        {
            //
            GameOver();
        }
    }

    void DamageEnd()
    {
        inDamage = false;//�_�Ń_���[�W�t���O������
        gameObject.GetComponent<SpriteRenderer>().enabled = true;//�v���C���[���m���ɕ\��
    }

    void GameOver()
    {
        //�Q�[���̏�Ԃ�ς���
        GameManager.gameState = GameState.gameover;

        //
        GetComponent<CircleCollider2D>().enabled = false;//�����蔻��̖�����
        rbody.linearVelocity = Vector2.zero;//�������~�߂�
        rbody.gravityScale = 1.0f;//�d�͂̕���
        anime.SetTrigger("dead");//���S�̃A�j���N���b�v�̔���
        rbody.AddForce(new Vector2(0,5), ForceMode2D.Impulse);//��ɒ��ˏグ��
        Destroy(gameObject, 1.0f);//1�b��ɑ��݂�����

    }

}
