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
        Move();//�㉺���E�̓��͒l�̎擾
        angleZ = GetAngle();//���̎��̊p�x��ϐ�angleZ�ɔ��f
        Animation();//angleZ�𗘗p���ăA�j���[�V����
    }

    private void FixedUpdate()
    {
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

}
