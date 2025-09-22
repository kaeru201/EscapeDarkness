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
    public string roomName; //�o������̎��ʖ�
    public string nextRoomName; //�V�[���؂�ւ���ł̍s��
    public string nextScene; //�V�[���؂�ւ���
    public bool openedDoor; //�h�A�̊J�󋵃t���O
    public DoorDirection direction; //�v���C���[�̔z�u�ʒu
    public MessageData message; //�g�[�N�f�[�^
    public GameObject door; //�\��/��\���Ώۂ̃h�A���

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ChangeScene();
        }
    }

    public void ChangeScene()
    {
        //����Room�ɐG�ꂽ��ǂ��ɍs���̂���ϐ�nextRoomName�Ō��߂Ă���
        //�V�[�����؂�ւ���ď�񂪃��Z�b�g�����O��static�ϐ��ł���toRoomNumber�ɍs������L�^
        RoomManager.toRoomNumber = nextRoomName;

        SceneManager.LoadScene(nextScene);
    }

    public void DoorOpenCheck()
    {
        //�������J������Ă�����q�I�u�W�F�N�g�ł���ϐ�door�͔�\��
        if (openedDoor) door.SetActive(false);
    }

}
