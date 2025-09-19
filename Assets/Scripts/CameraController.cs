using UnityEngine;

public class CameraController : MonoBehaviour
{
    GameObject player;//�^�[�Q�b�g�ƂȂ�v���C���[�̏��

    public float follwSpeed = 5.0f;//�v���C���[�ɒǂ����X�s�[�h

    void Start()
    {
        //�v���C���[�����擾
        player = GameObject.FindGameObjectWithTag("Player");

        //�X�^�[�g�����u�Ԃ̃J�����̌��ݒn(�J������Z���͈��������������-10�j
        transform.position = new Vector3(player.transform.position.x,  player.transform.position.y,  -10);
    }

    
    void  LateUpdate()
    {
        //==�̂Ƃ����!=�ɂ��Ă���(!=�Ȃ�{}�Ŗ{�����͂ނׂ��j
        if (player == null) return;//�Q�[���I�[�o�[�̎��̃G���[���

        //�ڎw���ׂ��|�C���g
        Vector3 nextPos = new Vector3(player.transform.position.x, player.transform.position.y, -10);

        //���݂̃|�C���g
        Vector3 nowPos = transform.position;

        //���ݒn�@���@�ڎw���ׂ��n�_�܂̂ŕ��
        transform.position = Vector3.Lerp(nowPos,nextPos,follwSpeed *  Time.deltaTime);

    }
}

