using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FastWayEvent_Control : MonoBehaviour
{
    string[] TextWindow = { "�B���ʘH��������", "�B���ʘH��ʂ�܂����H", "", "" };

    GameObject Player;
    Status Status;
    string PlayerName;

    GameObject Stage;
    Stage_Control Stage_Control;
    int StageNumber;

    GameObject MessageText;
    TextSpeed TextSpeed;

    public AudioClip sound;
    AudioSource audioSource;

    GameObject SE;
    SE_Control SE_Control;
    float Volume;

    [SerializeField] Text text;
    float novelSpeed;//�ꕶ���ꕶ���̕\�����鑬��
    int novelListIndex = 0; //���ݕ\�����̉�b���̔z��
    float time = 0.0f;
    bool NextText = true;
    string reply = "";
    public bool selectbutton = true;
    bool CancelFlag = false;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        SE = GameObject.Find("SE");
        SE_Control = SE.GetComponent<SE_Control>();
        Volume = SE_Control.Volume;
        audioSource.volume = Volume;

        Player = GameObject.Find("Player");
        Status = Player.GetComponent<Status>();
        PlayerName = Status.Player;

        Stage = GameObject.Find("Stage");
        Stage_Control = Stage.GetComponent<Stage_Control>();
        StageNumber = Stage_Control.NumberToGo;

        MessageText = GameObject.Find("MessageText");
        TextSpeed = MessageText.GetComponent<TextSpeed>();
        novelSpeed = TextSpeed.speed;

        text.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        MakingText0();
        MakingText1();
        MakingText3();

        if (PlayerName != "")
        {
            time += Time.deltaTime;
            if (time >= 2f)
            {
                if (novelListIndex == 2)
                {
                    if (MakingText2())
                    {
                        if (NextText)
                        {
                            NextText = false;
                            StartCoroutine(EventText());
                        }
                    }
                }

                else if (novelListIndex <= 3)
                {
                    if (CancelFlag && novelListIndex >= 3)
                    {

                    }

                    else
                    {
                        if (NextText)
                        {
                            NextText = false;
                            StartCoroutine(EventText());
                        }
                    }
                }
            }
        }
    }

    private IEnumerator EventText()
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

        if (novelListIndex == 1)
        {
            selectbutton = false;
        }

        if (novelListIndex >= 2 && CancelFlag)
        {
            FadeManager.Instance.LoadScene("PreparationScene", 0.5f);
        }

        if (novelListIndex >= 3)
        {
            FadeManager.Instance.LoadScene("PreparationScene", 0.5f);
        }

        novelListIndex++; //���̉�b���z��
        time = 0.0f;
        NextText = true;
    }

    private void MakingText0()
    {
        TextWindow[0] = PlayerName + "��\n�B���ʘH���������I";
    }

    private void MakingText1()
    {
        TextWindow[1] = "�B���ʘH��ʂ�܂����H";
    }

    private bool MakingText2()
    {
        if (reply == "yes")
        {
            TextWindow[2] = PlayerName + "��\n�B���ʘH��ʂ����I";
            return (true);
        }
        else if (reply == "no")
        {
            TextWindow[2] = PlayerName + "��\n�B���ʘH��ʂ炸�ɐ�֐i�񂾁I";
            CancelFlag = true;
            return (true);
        }
        else
            return (false);
    }

    private void MakingText3()
    {
        TextWindow[3] = PlayerName + "��" + StageNumber + "�X�e�[�W��֐i�񂾁I";
    }

    public void YesButton()
    {
        if (!selectbutton)
        {
            reply = "yes";
            audioSource.PlayOneShot(sound);
            selectbutton = true;
        }
    }

    public void NoButton()
    {
        if (!selectbutton)
        {
            reply = "no";
            audioSource.PlayOneShot(sound);
            selectbutton = true;
        }
    }
}
