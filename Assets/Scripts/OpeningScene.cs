using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OpeningScene : MonoBehaviour
{
    string[] TextWindow = { "..........",
        "�����������1�N���߂�����...",
        "���������X�e�[�^�X\n���Z�b�g���u������\n�o�����̂͂�����",
        "�̐S�̃X�e�[�^�X\n���Z�b�g���u�̓K���҂����Ȃ��̂ł͘b�ɂȂ��",
        "���������ǂ������...",
        "���炵�܂��I",
        "�ǂ������̂��I",
        "�X�e�[�^�X���Z�b�g\n���u�̓K���҂𔭌�\n�������܂����I",
        "�Ȃ񂾂ƁI\n�����ɘA��Ă܂���I",
        "�͂��I",
        "�K���҂����A�ꂵ�܂����I",
        "�����[�I\n���Ȃ����K���҂��I\n���O�͂Ȃ�Ă����񂾁H"};

    GameObject Player;
    Status Status;
    string PlayerName;

    GameObject MessageText;
    TextSpeed TextSpeed;

    public AudioClip sound;
    AudioSource audioSource;

    GameObject SE;
    SE_Control SE_Control;
    float Volume;

    [SerializeField] Text text;
    [SerializeField] Text NameText;
    float novelSpeed;//�ꕶ���ꕶ���̕\�����鑬��
    int novelListIndex = 0; //���ݕ\�����̉�b���̔z��
    public bool selectbutton = true;
    bool PushButton = false;
    bool skipbutton = false;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        SE = GameObject.Find("SE");
        SE_Control = SE.GetComponent<SE_Control>();
        Volume = SE_Control.Volume;
        audioSource.volume = Volume;

        MessageText = GameObject.Find("MessageText");
        TextSpeed = MessageText.GetComponent<TextSpeed>();
        novelSpeed = TextSpeed.speed;

        text.text = "";
        StartCoroutine(OpeningText());
    }

    // Update is called once per frame
    void Update()
    {
        MakingNameText();

#if UNITY_EDITOR
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
#else
        if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId)) {
            return;
        }
#endif

        if (Input.GetMouseButtonDown(0) && !PushButton)
        {
            if (!skipbutton)
            {
                if (novelListIndex <= 11)
                {
                    audioSource.PlayOneShot(sound);
                    StartCoroutine(OpeningText());
                    PushButton = true;
                }
                if (novelListIndex >= 12)
                {
                    audioSource.PlayOneShot(sound);
                    FadeManager.Instance.LoadScene("EnterNameScene", 0.5f);
                    PushButton = true;
                }
            }
        }
    }

    private IEnumerator OpeningText()
    {
        int messageCount = 0; //���ݕ\�����̕�����
        if (novelListIndex != 0)
            text.text = ""; //�e�L�X�g�̃��Z�b�g
        while (TextWindow[novelListIndex].Length > messageCount)//���������ׂĕ\�����Ă��Ȃ��ꍇ���[�v
        {
            text.text += TextWindow[novelListIndex][messageCount];//�ꕶ���ǉ�
            messageCount++;//���݂̕�����
            yield return new WaitForSeconds(novelSpeed);//�C�ӂ̎��ԑ҂�
        }

        novelListIndex++; //���̉�b���z��
        PushButton = false;
    }

    private void MakingNameText()
    {
        switch (novelListIndex)
        {
            case 0:
            case 1:
            case 2:
            case 3:
            case 4:
            case 5:
            case 7:
            case 9:
            case 12:
                NameText.text = "���l";
                break;

            case 6:
            case 8:
            case 10:
            case 11:
                NameText.text = "���m";
                break;
        }
    }

    public void SkipButton()
    {
        if (!skipbutton)
        {
            audioSource.PlayOneShot(sound);
            FadeManager.Instance.LoadScene("EnterNameScene", 0.5f);
            skipbutton = true;
        }
    }
}
