using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InnEvent_Control : MonoBehaviour
{
    string[] TextWindow = { "近くに宿屋がある", "宿屋に泊まりますか？\n(100コイン)", "", "" };
    public int EventNumber = 0;

    GameObject Player;
    Status Status;
    Belongings Belongings;
    string PlayerName;
    int Point;
    int money;

    GameObject MessageText;
    TextSpeed TextSpeed;

    public AudioClip sound;
    AudioSource audioSource;

    GameObject SE;
    SE_Control SE_Control;
    float Volume;

    [SerializeField] Text text;
    float novelSpeed;//一文字一文字の表示する速さ
    int novelListIndex = 0; //現在表示中の会話文の配列
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

        Player = GameObject.Find("Player");
        Status = Player.GetComponent<Status>();
        PlayerName = Status.Player;

        Belongings = Player.GetComponent<Belongings>();
        money = Belongings.Money;

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
        int messageCount = 0; //現在表示中の文字数
        if (novelListIndex != 0)
            text.text = ""; //テキストのリセット
        while (TextWindow[novelListIndex].Length > messageCount)//文字をすべて表示していない場合ループ
        {
            text.text += TextWindow[novelListIndex][messageCount];//一文字追加
            messageCount++;//現在の文字数
            yield return new WaitForSeconds(novelSpeed);//任意の時間待つ
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

        novelListIndex++; //次の会話文配列
        time = 0.0f;
        NextText = true;
    }

    private void MakingText0()
    {
        TextWindow[0] = PlayerName + "は\n近くに宿屋を見つけた！";
    }

    private void MakingText1()
    {
        TextWindow[1] = "宿屋に泊まりますか？\n(100コイン)";
    }

    private bool MakingText2()
    {
        if (reply == "yes")
        {
            TextWindow[2] = PlayerName + "は\n宿屋に泊った";
            return (true);
        }
        else if (reply == "no")
        {
            TextWindow[2] = PlayerName + "は\n宿屋に泊らずに先へ進んだ";
            CancelFlag = true;
            return (true);
        }
        else
            return (false);
    }

    private void MakingText3()
    {
        TextWindow[3] = PlayerName + "のステータスが全回復した！";
    }

    public void YesButton()
    {
        if (!selectbutton && money >= 100)
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
