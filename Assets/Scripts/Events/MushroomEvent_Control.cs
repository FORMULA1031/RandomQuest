using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MushroomEvent_Control : MonoBehaviour
{
    string[] TextWindow = { "���[�ɃL�m�R�������Ă���", "�L�m�R��H�ׂ܂����H", "", "" };
    public int EventNumber = 0;

    GameObject Player;
    Status Status;
    string PlayerName;
    int Point;

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
    bool selectbutton = true;
    bool CancelFlag = false;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        SE = GameObject.Find("SE");
        SE_Control = SE.GetComponent<SE_Control>();
        Volume = SE_Control.Volume;
        audioSource.volume = Volume;

        EventNumber = Random.Range(1, 6);
        Player = GameObject.Find("Player");
        Status = Player.GetComponent<Status>();
        PlayerName = Status.Player;

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
        if(novelListIndex != 0)
            text.text = ""; //�e�L�X�g�̃��Z�b�g
        while (TextWindow[novelListIndex].Length > messageCount)//���������ׂĕ\�����Ă��Ȃ��ꍇ���[�v
        {
            text.text += TextWindow[novelListIndex][messageCount];//�ꕶ���ǉ�
            messageCount++;//���݂̕�����
            yield return new WaitForSeconds(novelSpeed);//�C�ӂ̎��ԑ҂�
        }

        if(novelListIndex == 1)
        {
            selectbutton = false;
        }

        if(novelListIndex >= 2 && CancelFlag)
        {
            FadeManager.Instance.LoadScene("PreparationScene", 0.5f);
        }

        if(novelListIndex >= 3)
        {
            FadeManager.Instance.LoadScene("PreparationScene", 0.5f);
        }

        novelListIndex++; //���̉�b���z��
        time = 0.0f;
        NextText = true;
    }

    private void MakingText0()
    {
        TextWindow[0] = "���[�ɃL�m�R�������Ă���I";
    }

    private void MakingText1()
    {
        TextWindow[1] = "�L�m�R��H�ׂ܂����H";
    }

    private bool MakingText2()
    {
        if (reply == "yes")
        {
            TextWindow[2] = PlayerName + "��\n���[�ɐ����Ă����L�m�R��H�ׂ��I";
            MakingText3();
            return (true);
        }
        else if (reply == "no")
        {
            TextWindow[2] = PlayerName + "��\n���[�ɐ����Ă����L�m�R��H�ׂ��ɐ�֐i�񂾁I";
            CancelFlag = true;
            return (true);
        }
        else
            return (false);
    }

    private void MakingText3()
    {
        Point = Status.RecoveryAmount;

        switch (EventNumber)
        {
            case 1:
                TextWindow[3] = PlayerName + "��HP" + Point + "�󂯂��I";
                break;

            case 2:
                TextWindow[3] = PlayerName + "��MP" + Point + "������I";
                break;

            case 3:
                TextWindow[3] = PlayerName + "��HP" + Point + "�񕜂����I";
                break;

            case 4:
                TextWindow[3] = PlayerName + "��MP" + Point + "�񕜂����I";
                break;

            case 5:
                TextWindow[3] = "������" + PlayerName + "�ɕω��͂Ȃ������I";
                break;
        }
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
