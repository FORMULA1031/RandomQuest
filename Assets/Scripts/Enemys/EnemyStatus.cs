using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyStatus : MonoBehaviour
{
    public int HP;   //敵の体力
    public int AT;   //敵の攻撃力
    public int DF;   //敵の防御力
    public int SP;   //敵の素早さ
    string[] EnemyNames = { "ファイアースライム", "スライム", "リーフスライム", "ライトニングスライム",
        "赤ゴブリン", "青ゴブリン", "ゴブリン", "黄ゴブリン",
        "赤オーク", "青オーク", "オーク", "黄オーク",
        "ファイアーインセクト", "ウォーターインセクト", "インセクト", "ライトニングインセクト",
        "ファイアーゴーレム", "ウォーターゴーレム", "リーフゴーレム", "ゴーレム",
        "赤スケルトン", "青スケルトン", "緑スケルトン", "黄スケルトン",
        "ファイアーウルフ", "ウォーターウルフ", "リーフウルフ", "ライトニングウルフ",
        "ファイアークロコダイル", "ウォータークロコダイル", "リーフクロコダイル", "ライトニングクロコダイル",
        "ドラゴン", "ウォータードラゴン", "リーフドラゴン", "ライトニングドラゴン",
        "ファイアーソーサラー", "ウォーターソーサラー", "リーフソーサラー", "ライトニングソーサラー"};

    string[] Techniques = { "ファイアーパンチ", "ファイアースラッシュ", "ヒートヘイズ", "サーマルパワー", "イグニッション", "エクスプロージョン", "イラプション", "プロミネンス",  
         "ウォーターパンチ", "ウォータースラッシュ", "エクストラマー", "ハイドロパワー", "オーバーキャスト", "アシッドレイン", "ブライニクル", "アブソリュートゼロ",
         "リーフパンチ", "リーフスラッシュ", "スケアクロウ", "ウィンドパワー", "ホトシンセシス", "ダストストーム", "ドリフトウッド", "ローカストプレイグ", 
         "ライトニングパンチ", "ライトニングスラッシュ", "フラッシュ", "エクシードチャージ", "シャイニング", "マグネティックフィールド", "レールガン", "スターバースト"};

    public string Technique;    //敵が唱える技
    public string EnemyName;    //敵の名前
    public string Attribute;

    GameObject Player;
    Status Status;
    int PlayerAT;   //プレイヤーの攻撃力

    GameObject MessageWindow;
    BattleText BattleText;
    bool DefendFlag = false;
    bool InspirationFlag = false;

    GameObject EventSystem;
    Stage_Control Stage_Control;
    int StageNumber;

    public int CurrentHp;   //敵の残り体力
    public int DamegeHp;    //敵のダメージ量
    public int RecoveryAmount;
    public int coin;    //獲得マネー
    bool turn;  //trueだったらプレイヤーのターン、falseだったら敵のターン
    bool PlayerAttackExecution = false; //プレイヤーの攻撃が実行されたか
    bool EnemyAttackExecution = false;  //敵の攻撃が実行されたか
    string PlayerTechnique;
    bool AttackExecution = false;
    bool PerformingHpRecovery = false;
    public bool EnemyDefeat = false;
    public string TechniqueType;

    // Start is called before the first frame update
    void Start()
    {
        EventSystem = GameObject.Find("EventSystem");
        if (EventSystem != null)
        {
            Stage_Control = EventSystem.GetComponent<Stage_Control>();
            StageNumber = Stage_Control.StageNumber;
            if(StageNumber == 10)
                EnemyName = "ゴブリン";
            else if (StageNumber == 30)
                EnemyName = "インセクト";
            else if (StageNumber == 50)
                EnemyName = "ゴーレム";
            else if (StageNumber == 70)
                EnemyName = "ドラゴン";
            else if (StageNumber == 90)
                EnemyName = "ウォーターソーサラー";
            else if (StageNumber == 100)
                EnemyName = "魔王";
            else
                EnemyName = EnemyNames[Random.Range(0, 40)];
        }
        ConfirmationOfEnemyName();
        TechniqueSelection();
        TechniqueTypeDetermination();
        Player = GameObject.Find("Player");
        Status = Player.GetComponent<Status>();
        MessageWindow = GameObject.Find("MessageText");
        BattleText = MessageWindow.GetComponent<BattleText>();
        CurrentHp = HP;
    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentHp <= 0)
        {
            CurrentHp = 0;
            EnemyDefeat = true;
        }

        if(CurrentHp > HP)
        {
            CurrentHp = HP;
        }


        EnemyAttackExecution = BattleText.EnemyAttackExecution;
        if (EnemyAttackExecution)
        {
            if (!PerformingHpRecovery)
            {
                if (TechniqueType == "GreatRecovery")
                {
                    RecoveryAmount = HP / 3;
                    CurrentHp += RecoveryAmount;
                }
                PerformingHpRecovery = true;
            }
        }

        else
        {
            PerformingHpRecovery = false;
        }

        PlayerAT = Status.AT;
        PlayerAttackExecution = BattleText.PlayerAttackExecution;
        if (PlayerAttackExecution)
        {
            if (!AttackExecution)
            {
                PlayerTechnique = BattleText.Technique;
                TypeCompatibility();
                DamageAmount();
                AttackExecution = true;
            }
        }

        else
        {
            AttackExecution = false;
        }

        turn = BattleText.PlayerTurn;
        if (turn)
        {
            TechniqueSelection();
            TechniqueTypeDetermination();
        }
    }

    void ConfirmationOfEnemyName()
    {
        switch (EnemyName)
        {
            case "ファイアースライム":
                Attribute = "fire";
                coin = 10;
                EnemyStatusChange(1, 300, 1, 80, 1, 30, 1, 100);
                break;

            case "スライム":
                Attribute = "water";
                coin = 10;
                EnemyStatusChange(1, 300, 1, 80, 1, 30, 1, 100);
                break;

            case "リーフスライム":
                Attribute = "leaf";
                coin = 10;
                EnemyStatusChange(1, 300, 1, 80, 1, 30, 1, 100);
                break;

            case "ライトニングスライム":
                Attribute = "lightning";
                coin = 10;
                EnemyStatusChange(1, 300, 1, 80, 1, 30, 1, 100);
                break;

            case "赤ゴブリン":
                Attribute = "fire";
                coin = 30;
                EnemyStatusChange(50, 250, 20, 50, 10, 40, 10, 40);
                break;

            case "青ゴブリン":
                Attribute = "water";
                coin = 30;
                EnemyStatusChange(50, 250, 20, 50, 10, 40, 10, 40);
                break;

            case "ゴブリン":
                Attribute = "leaf";
                coin = 30;
                EnemyStatusChange(50, 250, 20, 50, 10, 40, 10, 40);
                break;

            case "黄ゴブリン":
                Attribute = "lightning";
                coin = 30;
                EnemyStatusChange(50, 250, 20, 50, 10, 40, 10, 40);
                break;

            case "赤オーク":
                Attribute = "fire";
                coin = 30;
                EnemyStatusChange(50, 250, 30, 60, 15, 40, 10, 25);
                break;

            case "青オーク":
                Attribute = "water";
                coin = 30;
                EnemyStatusChange(50, 250, 30, 60, 15, 40, 10, 25);
                break;

            case "オーク":
                Attribute = "leaf";
                coin = 30;
                EnemyStatusChange(50, 250, 30, 60, 15, 40, 10, 25);
                break;

            case "黄オーク":
                Attribute = "lightning";
                coin = 30;
                EnemyStatusChange(50, 250, 30, 60, 15, 40, 10, 25);
                break;

            case "ファイアーインセクト":
                Attribute = "fire";
                coin = 50;
                EnemyStatusChange(10, 200, 20, 50, 10, 30, 50, 90);
                break;

            case "ウォーターインセクト":
                Attribute = "water";
                coin = 50;
                EnemyStatusChange(10, 200, 20, 50, 10, 30, 50, 90);
                break;

            case "インセクト":
                Attribute = "leaf";
                coin = 50;
                EnemyStatusChange(10, 200, 20, 50, 10, 30, 50, 90);
                break;

            case "ライトニングインセクト":
                Attribute = "lightning";
                coin = 50;
                EnemyStatusChange(10, 200, 20, 50, 10, 30, 50, 90);
                break;

            case "ファイアーゴーレム":
                Attribute = "fire";
                coin = 100;
                EnemyStatusChange(200, 500, 40, 60, 40, 70, 1, 30);
                break;

            case "ウォーターゴーレム":
                Attribute = "water";
                coin = 100;
                EnemyStatusChange(200, 500, 40, 60, 40, 70, 1, 30);
                break;

            case "リーフゴーレム":
                Attribute = "leaf";
                coin = 100;
                EnemyStatusChange(200, 500, 40, 60, 40, 70, 1, 30);
                break;

            case "ゴーレム":
                Attribute = "lightning";
                coin = 100;
                EnemyStatusChange(200, 500, 40, 60, 40, 70, 1, 30);
                break;

            case "赤スケルトン":
                Attribute = "fire";
                coin = 50;
                EnemyStatusChange(1, 100, 20, 80, 1, 10, 50, 70);
                break;

            case "青スケルトン":
                Attribute = "water";
                coin = 50;
                EnemyStatusChange(1, 100, 20, 80, 1, 10, 50, 70);
                break;

            case "緑スケルトン":
                Attribute = "leaf";
                coin = 50;
                EnemyStatusChange(1, 100, 20, 80, 1, 10, 50, 70);
                break;

            case "黄スケルトン":
                Attribute = "lightning";
                coin = 50;
                EnemyStatusChange(1, 100, 20, 80, 1, 10, 50, 70);
                break;

            case "ファイアーウルフ":
                Attribute = "fire";
                coin = 120;
                EnemyStatusChange(100, 300, 50, 60, 20, 40, 40, 60);
                break;

            case "ウォーターウルフ":
                Attribute = "water";
                coin = 120;
                EnemyStatusChange(100, 300, 50, 60, 20, 40, 40, 60);
                break;

            case "リーフウルフ":
                Attribute = "leaf";
                coin = 120;
                EnemyStatusChange(100, 300, 50, 60, 20, 40, 40, 60);
                break;

            case "ライトニングウルフ":
                Attribute = "lightning";
                coin = 120;
                EnemyStatusChange(100, 300, 50, 60, 20, 40, 40, 60);
                break;

            case "ファイアークロコダイル":
                Attribute = "fire";
                coin = 120;
                EnemyStatusChange(200, 300, 40, 60, 30, 45, 40, 50);
                break;

            case "ウォータークロコダイル":
                Attribute = "water";
                coin = 120;
                EnemyStatusChange(200, 300, 40, 60, 30, 45, 40, 50);
                break;

            case "リーフクロコダイル":
                Attribute = "leaf";
                coin = 120;
                EnemyStatusChange(200, 300, 40, 60, 30, 45, 40, 50);
                break;

            case "ライトニングクロコダイル":
                Attribute = "lightning";
                coin = 120;
                EnemyStatusChange(200, 300, 40, 60, 30, 45, 40, 50);
                break;

            case "ドラゴン":
                Attribute = "fire";
                coin = 200;
                EnemyStatusChange(200, 300, 50, 70, 20, 50, 20, 60);
                break;

            case "ウォータードラゴン":
                Attribute = "water";
                coin = 200;
                EnemyStatusChange(200, 300, 50, 70, 20, 50, 20, 60);
                break;

            case "リーフドラゴン":
                Attribute = "leaf";
                coin = 200;
                EnemyStatusChange(200, 300, 50, 70, 20, 50, 20, 60);
                break;

            case "ライトニングドラゴン":
                Attribute = "lightning";
                coin = 200;
                EnemyStatusChange(200, 300, 50, 70, 20, 50, 20, 60);
                break;

            case "ファイアーソーサラー":
                Attribute = "fire";
                coin = 200;
                EnemyStatusChange(100, 300, 30, 80, 10, 50, 10, 80);
                break;

            case "ウォーターソーサラー":
                Attribute = "water";
                coin = 200;
                EnemyStatusChange(100, 300, 30, 80, 10, 50, 10, 80);
                break;

            case "リーフソーサラー":
                Attribute = "leaf";
                coin = 200;
                EnemyStatusChange(100, 300, 30, 80, 10, 50, 10, 80);
                break;

            case "ライトニングソーサラー":
                Attribute = "lightning";
                coin = 200;
                EnemyStatusChange(100, 300, 30, 80, 10, 50, 10, 80);
                break;

            case "魔王":
                Attribute = "none";
                coin = 1000;
                EnemyStatusChange(100, 500, 30, 100, 10, 90, 50, 100);
                break;
        }
    }

    void EnemyStatusChange(int MinHp, int MaxHp, int MinAt, int MaxAt, int MinDf, int MaxDf, int MinSp, int MaxSp)
    {
        HP = Random.Range(MinHp, MaxHp);
        AT = Random.Range(MinAt, MaxAt);
        DF = Random.Range(MinDf, MaxDf);
        SP = Random.Range(MinSp, MaxSp);
    }

    void TechniqueSelection()
    {
        if (Attribute == "fire")
            Technique = Techniques[Random.Range(0, 8)];
        if (Attribute == "water")
            Technique = Techniques[Random.Range(8, 16)];
        if (Attribute == "leaf")
            Technique = Techniques[Random.Range(16, 24)];
        if (Attribute == "lightning")
            Technique = Techniques[Random.Range(24, 32)];
        if (Attribute == "none")
            Technique = Techniques[Random.Range(0, 32)];

    }

    void DamageAmount()
    {
        switch (PlayerTechnique)
        {
            case "ファイアーパンチ":
            case "ウォーターパンチ":
            case "リーフパンチ":
            case "ライトニングパンチ":
                DamegeHp = (PlayerAT * 1) - DF;
                DamageAmountCorrection();
                CurrentHp -= DamegeHp;
                break;

            case "ファイアースラッシュ":
            case "ウォータースラッシュ":
            case "リーフスラッシュ":
            case "ライトニングスラッシュ":
                DamegeHp = PlayerAT + (PlayerAT / 2) - DF;
                DamageAmountCorrection();
                CurrentHp -= DamegeHp;
                break;

            case "サーマルパワー":
            case "ハイドロパワー":
            case "ウィンドパワー":
            case "エクシードチャージ":
                DamegeHp = (PlayerAT * 0) - DF;
                DamageAmountCorrection();
                CurrentHp -= DamegeHp;
                break;

            case "イグニッション":
            case "オーバーキャスト":
            case "ホトシンセシス":
            case "シャイニング":
                DamegeHp = (PlayerAT * 0) - DF;
                DamageAmountCorrection();
                CurrentHp -= DamegeHp;
                break;

            case "エクスプロージョン":
            case "ダストストーム":
            case "アシッドレイン":
            case "マグネティックフィールド":
                DamegeHp = (PlayerAT * 2) - DF;
                DamageAmountCorrection();
                CurrentHp -= DamegeHp;
                break;

            case "イラプション":
            case "ブライニクル":
            case "ドリフトウッド":
            case "レールガン":
                DamegeHp = (PlayerAT * 2) + (PlayerAT / 2) - DF;
                DamageAmountCorrection();
                CurrentHp -= DamegeHp;
                break;

            case "プロミネンス":
            case "アブソリュートゼロ":
            case "ローカストプレイグ":
            case "スターバースト":
                DamegeHp = PlayerAT * 3 - DF;
                DamageAmountCorrection();
                CurrentHp -= DamegeHp;
                break;

            default :
                DamegeHp = 10 - DF;
                DamageAmountCorrection();
                CurrentHp -= DamegeHp;
                break;
        }
    }

    void DamageAmountCorrection()
    {
        if (DamegeHp < 0)
        {
            DamegeHp = 0;
        }

        DefendFlag = BattleText.EnemyDefendFlag;
        if (DefendFlag)
            DamegeHp = 0;

        InspirationFlag = BattleText.PlayerInspirationFlag;
        if (InspirationFlag)
            DamegeHp *= 2;
    }

    void TypeCompatibility()
    {
        switch (PlayerTechnique)
        {
            case "ファイアーパンチ":
            case "ファイアースラッシュ":
            case "サーマルパワー":
            case "エクスプロージョン":
            case "イラプション":
            case "プロミネンス":
                if (Attribute == "leaf")
                    PlayerAT *= 2;
                if (Attribute == "water")
                    PlayerAT /= 2;
                break;

            case "ウォーターパンチ":
            case "ウォータースラッシュ":
            case "ハイドロパワー":
            case "アシッドレイン":
            case "ブライニクル":
            case "アブソリュートゼロ":
                if (Attribute == "fire")
                    PlayerAT *= 2;
                if (Attribute == "leaf")
                    PlayerAT /= 2;
                break;

            case "リーフパンチ":
            case "リーフスラッシュ":
            case "ウィンドパワー":
            case "ダストストーム":
            case "ドリフトウッド":
            case "ローカストプレイグ":
                if (Attribute == "water")
                    PlayerAT *= 2;
                if (Attribute == "fire")
                    PlayerAT /= 2;
                break;

            case "ライトニングパンチ":
            case "ライトニングスラッシュ":
            case "エクシードチャージ":
            case "マグネティックフィールド":
                break;

            default:
                break;
        }
    }

    void TechniqueTypeDetermination()
    {
        switch (Technique)
        {
            case "ファイアーパンチ":
            case "ウォーターパンチ":
            case "リーフパンチ":
            case "ライトニングパンチ":
                TechniqueType = "Blow";
                break;

            case "ファイアースラッシュ":
            case "ウォータースラッシュ":
            case "リーフスラッシュ":
            case "ライトニングスラッシュ":
                TechniqueType = "Rip";
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

            case "サーマルパワー":
            case "ハイドロパワー":
            case "ウィンドパワー":
            case "エクシードチャージ":
                TechniqueType = "GreatRecovery";
                break;

            case "エクスプロージョン":
            case "ダストストーム":
            case "アシッドレイン":
            case "マグネティックフィールド":
                TechniqueType = "SmallMagic";
                break;

            case "イラプション":
            case "ブライニクル":
            case "ドリフトウッド":
            case "レールガン":
                TechniqueType = "GreatMagic";
                break;

            case "プロミネンス":
            case "アブソリュートゼロ":
            case "ローカストプレイグ":
            case "スターバースト":
                TechniqueType = "Special";
                break;
        }
    }
}
