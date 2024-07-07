using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Opening2Scene : MonoBehaviour
{
    string[] TextWindow = { "..........",
        "というんだな！",
        "では早速だが\nそなたには",
        "ステータスリセット\n装置を使って魔王を倒してほしいのだ！",
        "..........ん？",
        "ステータスリセット\n装置とは一体何かって？",
        "ステータスリセット\n装置とはその名の通り自分のステータスを",
        "ランダムにリセット\n出来る装置のことだ！",
        "だがこの装置を使えるのは一部の限られた人のみなんだ",
        "その限られた人が\nそなたというわけだ！",
        "だからそなたには\nステータスリセット\n装置を使って魔王を倒してもらう！",
        "では、行けー！\nよ！"};

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
        MakingMessageText1();
        MakingMessageText11();

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
                    FadeManager.Instance.LoadScene("MakeingScene", 0.5f);
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

    private void MakingMessageText1()
    {
        TextWindow[1] = PlayerName + "というんだな！";
    }

    private void MakingMessageText11()
    {
        TextWindow[11] = "では、行けー！\n" + PlayerName + "よ！";
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
            case 6:
            case 7:
            case 8:
            case 9:
            case 10:
            case 11:
            case 12:
                NameText.text = "王様";
                break;
        }
    }

    public void SkipButton()
    {
        if (!skipbutton)
        {
            audioSource.PlayOneShot(sound);
            FadeManager.Instance.LoadScene("MakeingScene", 0.5f);
            skipbutton = true;
        }
    }
}
