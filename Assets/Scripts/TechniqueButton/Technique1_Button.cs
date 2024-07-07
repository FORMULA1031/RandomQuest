using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Technique1_Button : MonoBehaviour
{
    GameObject Player;
    Status Status;
    GameObject MessageWindow;
    BattleText BattleText;
    bool turn = false;
    string Technique;
    public Button button;
    public bool select = false;
    bool SelectTechnique = false;
    int CurrentMp;
    public int MP;
    public string TechniqueType;
    public bool UnusableTechnique = false;
    float time = 0.0f;

    public AudioClip FailureSound;
    public AudioClip sound;
    AudioSource audioSource;

    GameObject SE;
    SE_Control SE_Control;
    float Volume;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        SE = GameObject.Find("SE");
        SE_Control = SE.GetComponent<SE_Control>();
        Volume = SE_Control.Volume;

        Player = GameObject.Find("Player");
        Status = Player.GetComponent<Status>();
        MessageWindow = GameObject.Find("MessageText");
        BattleText = MessageWindow.GetComponent<BattleText>();
    }

    // Update is called once per frame
    void Update()
    {
        Technique = Status.Technique1;
        CurrentMp = Status.CurrentMp;
        MPConsumption();
        ButtonColor(Technique);
        SelectTechnique = BattleText.SelectTechnique;
        if (select)
        {
            time += Time.deltaTime;
            if (time > 1.0f)
            {
                select = false;
                time = 0.0f;
            }
        }

        if (CurrentMp < MP)
            UnusableTechnique = true;
    }

    void ButtonColor(string Technique)
    {
        switch (Technique)
        {
            case "ファイアーパンチ":
            case "ファイアースラッシュ":
            case "ヒートヘイズ":
            case "サーマル":
            case "サーマルパワー":
            case "イグニッション":
            case "エクスプロージョン":
            case "イラプション":
            case "プロミネンス":
                button.image.color = new Color(1.0f, 0.3f, 0.0f, 1.0f);
                break;

            case "ウォーターパンチ":
            case "ウォータースラッシュ":
            case "エクストラマー":
            case "ハイドロ":
            case "ハイドロパワー":
            case "オーバーキャスト":
            case "アシッドレイン":
            case "ブライニクル":
            case "アブソリュートゼロ":
                button.image.color = new Color(0.0f, 0.5f, 1.0f, 1.0f);
                break;

            case "リーフパンチ":
            case "リーフスラッシュ":
            case "スケアクロウ":
            case "ウィンド":
            case "ウィンドパワー":
            case "ホトシンセシス":
            case "ダストストーム":
            case "ドリフトウッド":
            case "ローカストプレイグ":
                button.image.color = new Color(0.0f, 1.0f, 0.0f, 1.0f);
                break;

            case "ライトニングパンチ":
            case "ライトニングスラッシュ":
            case "フラッシュ":
            case "チャージ":
            case "エクシードチャージ":
            case "シャイニング":
            case "マグネティックフィールド":
            case "レールガン":
            case "スターバースト":
                button.image.color = new Color(1.0f, 1.0f, 0.0f, 1.0f);
                break;
        }
    }

    void MPConsumption()
    {
        switch (Technique)
        {
            case "ファイアーパンチ":
            case "ウォーターパンチ":
            case "リーフパンチ":
            case "ライトニングパンチ":
                MP = 5;
                TechniqueType = "Blow";
                break;

            case "ファイアースラッシュ":
            case "ウォータースラッシュ":
            case "リーフスラッシュ":
            case "ライトニングスラッシュ":
                MP = 10;
                TechniqueType = "Rip";
                break;

            case "サーマル":
            case "ハイドロ":
            case "ウィンド":
            case "チャージ":
                MP = 10;
                TechniqueType = "SmallRecovery";
                break;

            case "サーマルパワー":
            case "ハイドロパワー":
            case "ウィンドパワー":
            case "エクシードチャージ":
                MP = 20;
                TechniqueType = "GreatRecovery";
                break;

            case "イグニッション":
            case "オーバーキャスト":
            case "ホトシンセシス":
            case "シャイニング":
                MP = 5;
                TechniqueType = "Inspiration";
                break;

            case "エクスプロージョン":
            case "ダストストーム":
            case "アシッドレイン":
            case "マグネティックフィールド":
                MP = 15;
                TechniqueType = "SmallMagic";
                break;

            case "イラプション":
            case "ブライニクル":
            case "ドリフトウッド":
            case "レールガン":
                MP = 20;
                TechniqueType = "GreatMagic";
                break;

            case "プロミネンス":
            case "アブソリュートゼロ":
            case "ローカストプレイグ":
            case "スターバースト":
                MP = 25;
                TechniqueType = "Special";
                break;

            case "ヒートヘイズ":
            case "エクストラマー":
            case "スケアクロウ":
            case "フラッシュ":
                MP = 10;
                TechniqueType = "Defend";
                break;
        }
    }

    public void OnClick()
    {
        turn = BattleText.PlayerTurn;
        if (turn)
        {
            if (SelectTechnique && CurrentMp >= MP)
            {
                select = true;
                audioSource.PlayOneShot(sound);
            }

            if (SelectTechnique && CurrentMp < MP)
                audioSource.PlayOneShot(FailureSound);
        }
    }
}
