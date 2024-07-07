using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyImageControl : MonoBehaviour
{

    public Image image;

    public Sprite Slime;
    public Sprite FireSlime;
    public Sprite LeafSlime;
    public Sprite LightningSlime;
    public Sprite Goblin;
    public Sprite RedGoblin;
    public Sprite BlueGoblin;
    public Sprite YellowGoblin;
    public Sprite Ork;
    public Sprite RedOrk;
    public Sprite BlueOrk;
    public Sprite YellowOrk;
    public Sprite Insect;
    public Sprite FireInsect;
    public Sprite WaterInsect;
    public Sprite LightningInsect;
    public Sprite FireGolem;
    public Sprite WaterGolem;
    public Sprite LeafGolem;
    public Sprite Golem;
    public Sprite RedSkeleton;
    public Sprite BlueSkeleton;
    public Sprite GreenSkeleton;
    public Sprite YellowSkeleton;
    public Sprite FireWolf;
    public Sprite WaterWolf;
    public Sprite LeafWolf;
    public Sprite LightningWolf;
    public Sprite FireCrocodile;
    public Sprite WaterCrocodile;
    public Sprite LeafCrocodile;
    public Sprite LightningCrocodile;
    public Sprite Dragon;
    public Sprite WaterDragon;
    public Sprite LeafDragon;
    public Sprite LightningDragon;
    public Sprite FireSorcerer;
    public Sprite WaterSorcerer;
    public Sprite LeafSorcerer;
    public Sprite LightningSorcerer;
    public Sprite Devil;

    GameObject Enemy;
    EnemyStatus EnemyStatus;
    string EnemyName;
    bool EnemyDisplay = false;
    bool EnemyDefeat = false;

    GameObject MessageWindow;
    BattleText BattleText;
    string Technique;
    bool EffectTime = false;
    bool turn;

    bool AttackTechnique;
    bool EffectFlag = false;
    float Transparency = 255;
    float damegetime = 0.0f;
    public bool Disappear = false;

    // Start is called before the first frame update
    void Start()
    {
        Enemy = GameObject.Find("Enemy");
        EnemyStatus = Enemy.GetComponent<EnemyStatus>();

        MessageWindow = GameObject.Find("MessageText");
        BattleText = MessageWindow.GetComponent<BattleText>();
    }

    // Update is called once per frame
    void Update()
    {
        image = this.GetComponent<Image>();

        EnemyName = EnemyStatus.EnemyName;
        if (EnemyName != "")
        {
            if (!EnemyDisplay)
            {
                EnemyImageDisplay();
                EnemyDisplay = true;
            }
        }

        turn = BattleText.PlayerTurn;
        EffectTime = BattleText.nexteffect;
        Technique = BattleText.Technique;
        AttackTechniqueDiscrimination();

        if (turn)
        {
            if (AttackTechnique)
            {
                if(EffectTime && !EffectFlag)
                {
                    DamegeEffect();
                }
            }
        }

        if (!EffectTime)
        {
            EffectFlag = false;
        }

        EnemyDefeat = EnemyStatus.EnemyDefeat;
        if (EnemyDefeat && EffectFlag)
        {
            if (Transparency > 0)
            {
                Transparency -= 0.3f;
            }

            if (Transparency < 0)
                Transparency = 0;

            Disappear = true;
            image.color = new Color(255, 255, 255, 0);
        }
    }

    void EnemyImageDisplay()
    {
        switch (EnemyName)
        {
            case "スライム":
                image.sprite = Slime;
                break;
            case "ファイアースライム":
                image.sprite = FireSlime;
                break;
            case "リーフスライム":
                image.sprite = LeafSlime;
                break;
            case "ライトニングスライム":
                image.sprite = LightningSlime;
                break;
            case "ゴブリン":
                image.sprite = Goblin;
                break;
            case "赤ゴブリン":
                image.sprite = RedGoblin;
                break;
            case "青ゴブリン":
                image.sprite = BlueGoblin;
                break;
            case "黄ゴブリン":
                image.sprite = YellowGoblin;
                break;
            case "オーク":
                image.sprite = Ork;
                break;
            case "赤オーク":
                image.sprite = RedOrk;
                break;
            case "青オーク":
                image.sprite = BlueOrk;
                break;
            case "黄オーク":
                image.sprite = YellowOrk;
                break;
            case "インセクト":
                image.sprite = Insect;
                break;
            case "ファイアーインセクト":
                image.sprite = FireInsect;
                break;
            case "ウォーターインセクト":
                image.sprite = WaterInsect;
                break;
            case "ライトニングインセクト":
                image.sprite = LightningInsect;
                break;
            case "ファイアーゴーレム":
                image.sprite = FireGolem;
                break;
            case "ウォーターゴーレム":
                image.sprite = WaterGolem;
                break;
            case "リーフゴーレム":
                image.sprite = LeafGolem;
                break;
            case "ゴーレム":
                image.sprite = Golem;
                break;
            case "赤スケルトン":
                image.sprite = RedSkeleton;
                break;
            case "青スケルトン":
                image.sprite = BlueSkeleton;
                break;
            case "緑スケルトン":
                image.sprite = GreenSkeleton;
                break;
            case "黄スケルトン":
                image.sprite = YellowSkeleton;
                break;
            case "ファイアーウルフ":
                image.sprite = FireWolf;
                break;
            case "ウォーターウルフ":
                image.sprite = WaterWolf;
                break;
            case "リーフウルフ":
                image.sprite = LeafWolf;
                break;
            case "ライトニングウルフ":
                image.sprite = LightningWolf;
                break;
            case "ファイアークロコダイル":
                image.sprite = FireCrocodile;
                break;
            case "ウォータークロコダイル":
                image.sprite = WaterCrocodile;
                break;
            case "リーフクロコダイル":
                image.sprite = LeafCrocodile;
                break;
            case "ライトニングクロコダイル":
                image.sprite = LightningCrocodile;
                break;
            case "ドラゴン":
                image.sprite = Dragon;
                break;
            case "ウォータードラゴン":
                image.sprite = WaterDragon;
                break;
            case "リーフドラゴン":
                image.sprite = LeafDragon;
                break;
            case "ライトニングドラゴン":
                image.sprite = LightningDragon;
                break;
            case "ファイアーソーサラー":
                image.sprite = FireSorcerer;
                break;
            case "ウォーターソーサラー":
                image.sprite = WaterSorcerer;
                break;
            case "リーフソーサラー":
                image.sprite = LeafSorcerer;
                break;
            case "ライトニングソーサラー":
                image.sprite = LightningSorcerer;
                break;
            case "魔王":
                image.sprite = Devil;
                break;
        }
    }

    private void AttackTechniqueDiscrimination()
    {
        switch (Technique)
        {
            case "ファイアーパンチ":
            case "ファイアースラッシュ":
            case "エクスプロージョン":
            case "イラプション":
            case "ウォーターパンチ":
            case "ウォータースラッシュ":
            case "アシッドレイン":
            case "ブライニクル":
            case "リーフパンチ":
            case "リーフスラッシュ":
            case "ダストストーム":
            case "ドリフトウッド":
            case "ライトニングパンチ":
            case "ライトニングスラッシュ":
            case "マグネティックフィールド":
            case "レールガン":
            case "プロミネンス":
            case "アブソリュートゼロ":
            case "ローカストプレイグ":
            case "スターバースト":
            case "悪あがき":
                AttackTechnique = true;
                break;

            case "サーマル":
            case "ハイドロ":
            case "ウィンド":
            case "チャージ":
            case "サーマルパワー":
            case "ハイドロパワー":
            case "ウィンドパワー":
            case "エクシードチャージ":
            case "ヒートヘイズ":
            case "エクストラマー":
            case "スケアクロウ":
            case "フラッシュ":
            case "イグニッション":
            case "オーバーキャスト":
            case "ホトシンセシス":
            case "シャイニング":
                AttackTechnique = false;
                break;
        }
    }

    private void DamegeEffect()
    {
        damegetime += Time.deltaTime;
        if(damegetime >= 1.0f)
        {
            damegetime = 0.0f;
            EffectFlag = true;
        }
        else if (damegetime >= 0.6f)
        {
            image.color = new Color(255, 255, 255, 255);
        }
        else if (damegetime >= 0.4f)
            image.color = new Color(255, 255, 255, 0);
        else if (damegetime >= 0.2f)
            image.color = new Color(255, 255, 255, 255);
        else
            image.color = new Color(255, 255, 255, 0);
    }
}