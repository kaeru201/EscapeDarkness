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
        
    
}
