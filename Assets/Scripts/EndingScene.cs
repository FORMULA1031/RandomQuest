using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EndingScene : MonoBehaviour
{
    string[] TextWindow = { "おおー！\nよ！",
        "よくぞ魔王を倒してくれた！\nそなたには感謝する！",
        "感謝のしるしに私から\n1000ゴールドそなたにプレゼントしよう！",
        "本当にありがとう！\nよ！" };

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
    float novelSpeed;//一文字一文字の表示する速さ
    int novelListIndex = 0; //現在表示中の会話文の配列
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

        Player = GameObject.Find("Player");
        Status = Player.GetComponent<Status>();
        PlayerName = Status.Player;

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
        MakingMessageText0();
        MakingMessageText3();

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
                if (novelListIndex <= 3)
                {
                    audioSource.PlayOneShot(sound);
                    StartCoroutine(OpeningText());
                    PushButton = true;
                }
                if (novelListIndex >= 4)
                {
                    audioSource.PlayOneShot(sound);
                    FadeManager.Instance.LoadScene("EndRollScene", 0.5f);
                    PushButton = true;
                }
            }
        }
    }

    private IEnumerator OpeningText()
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

        novelListIndex++; //次の会話文配列
        PushButton = false;
    }

    private void MakingMessageText0()
    {
        TextWindow[0] = "おおー！\n" + PlayerName + "よ！";
    }

    private void MakingMessageText3()
    {
        TextWindow[3] = "本当にありがとう！\n" + PlayerName + "よ！";
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
                NameText.text = "王様";
                break;
        }
    }

    public void SkipButton()
    {
        if (!skipbutton)
        {
            audioSource.PlayOneShot(sound);
            FadeManager.Instance.LoadScene("EndRollScene", 0.5f);
            skipbutton = true;
        }
    }
}
