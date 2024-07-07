using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleEffect : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip Fire;
    public AudioClip Water;
    public AudioClip Leaf;
    public AudioClip Lightning;
    public AudioClip Recovery;
    public AudioClip Defend;
    public AudioClip Inspiration;
    public AudioClip SpecialFire;
    public AudioClip SpecialWater;
    public AudioClip SpecialLeaf;
    public AudioClip SpecialLightning;
    public AudioClip Waruagaki;
    public AudioClip FallDown;
    GameObject SE;
    SE_Control SE_Control;
    float Volume;

    GameObject MessageWindow;
    BattleText BattleText;
    string Technique;
    bool EffectTime = false;
    bool turn;

    GameObject EnemyImage;
    EnemyImageControl EnemyImageControl;
    bool Disappear = false;
    bool FallDownFlag = false;

    string TechniqueType;
    bool EffectFlag = false;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        SE = GameObject.Find("SE");
        SE_Control = SE.GetComponent<SE_Control>();
        Volume = SE_Control.Volume;
        audioSource.volume = Volume;
        MessageWindow = GameObject.Find("MessageText");
        BattleText = MessageWindow.GetComponent<BattleText>();
        EnemyImage = GameObject.Find("EnemyImage");
        EnemyImageControl = EnemyImage.GetComponent<EnemyImageControl>();
    }

    // Update is called once per frame
    void Update()
    {
        SE = GameObject.Find("SE");
        SE_Control = SE.GetComponent<SE_Control>();
        Volume = SE_Control.Volume;
        audioSource.volume = Volume;

        MessageWindow = GameObject.Find("MessageText");
        BattleText = MessageWindow.GetComponent<BattleText>();
        turn = BattleText.PlayerTurn;

        if (turn)
            Technique = BattleText.Technique;
        else
            Technique = BattleText.EnemyTechnique;
        SortingTechniqueTypes();
        EffectTime = BattleText.effect;

        if (EffectTime && !EffectFlag)
        {
            switch (TechniqueType)
            {
                case "Fire":
                    audioSource.PlayOneShot(Fire);
                    break;

                case "Water":
                    audioSource.PlayOneShot(Water);
                    break;

                case "Leaf":
                    audioSource.PlayOneShot(Leaf);
                    break;

                case "Lightning":
                    audioSource.PlayOneShot(Lightning);
                    break;

                case "Recovery":
                    audioSource.PlayOneShot(Recovery);
                    break;

                case "Defend":
                    audioSource.PlayOneShot(Defend);
                    break;

                case "Inspiration":
                    audioSource.PlayOneShot(Inspiration);
                    break;

                case "SpecialFire":
                    audioSource.PlayOneShot(SpecialFire);
                    break;

                case "SpecialWater":
                    audioSource.PlayOneShot(SpecialWater);
                    break;

                case "SpecialLeaf":
                    audioSource.PlayOneShot(SpecialLeaf);
                    break;

                case "SpecialLightning":
                    audioSource.PlayOneShot(SpecialLightning);
                    break;

                case "Waruagaki":
                    audioSource.PlayOneShot(Waruagaki);
                    break;
            }

            EffectFlag = true;
        }

        if (!EffectTime)
        {
            EffectFlag = false;
        }

        Disappear = EnemyImageControl.Disappear;
        if (Disappear && !FallDownFlag)
        {
            audioSource.PlayOneShot(FallDown);
            FallDownFlag = true;
        }
    }

    void SortingTechniqueTypes()
    {
        switch (Technique)
        {
            case "ファイアーパンチ":
            case "ファイアースラッシュ":
            case "エクスプロージョン":
            case "イラプション":
                TechniqueType = "Fire";
                break;

            case "ウォーターパンチ":
            case "ウォータースラッシュ":
            case "アシッドレイン":
            case "ブライニクル":
                TechniqueType = "Water";
                break;

            case "リーフパンチ":
            case "リーフスラッシュ":
            case "ダストストーム":
            case "ドリフトウッド":
                TechniqueType = "Leaf";
                break;

            case "ライトニングパンチ":
            case "ライトニングスラッシュ":
            case "マグネティックフィールド":
            case "レールガン":
                TechniqueType = "Lightning";
                break;

            case "サーマルパワー":
            case "ハイドロパワー":
            case "ウィンドパワー":
            case "エクシードチャージ":
                TechniqueType = "Recovery";
                break;

            case "ヒートヘイズ":
            case "エクストラマー":
            case "スケアクロウ":
            case "フラッシュ":
                TechniqueType = "Defend";
                break;

            case "イグニッション":
            case "オーバーキャスト":
            case "ホトシンセシス":
            case "シャイニング":
                TechniqueType = "Inspiration";
                break;

            case "プロミネンス":
                TechniqueType = "SpecialFire";
                break;

            case "アブソリュートゼロ":
                TechniqueType = "SpecialWater";
                break;

            case "ローカストプレイグ":
                TechniqueType = "SpecialLeaf";
                break;

            case "スターバースト":
                TechniqueType = "SpecialLightning";
                break;

            case "悪あがき":
                TechniqueType = "Waruagaki";
                break;
        }
    }
}
