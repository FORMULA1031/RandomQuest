using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OpeningScene : MonoBehaviour
{
    string[] TextWindow = { "..........",
        "魔王が現れて1年が過ぎたか...",
        "せっかくステータス\nリセット装置が完成\n出来たのはいいが",
        "肝心のステータス\nリセット装置の適正者がいないのでは話にならん",
        "いったいどうすれば...",
        "失礼します！",
        "どうしたのだ！",
        "ステータスリセット\n装置の適正者を発見\nいたしました！",
        "なんだと！\n直ぐに連れてまいれ！",
        "はっ！",
        "適正者をお連れしました！",
        "おぉー！\nそなたが適正者か！\n名前はなんていうんだ？"};

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
                NameText.text = "王様";
                break;

            case 6:
            case 8:
            case 10:
            case 11:
                NameText.text = "兵士";
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
