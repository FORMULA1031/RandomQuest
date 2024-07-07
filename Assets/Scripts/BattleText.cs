using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BattleText : MonoBehaviour
{
    GameObject Player;
    Status Status;
    string PlayerName;
    int PlayerSpeed;
    string Technique1;
    bool PlayerDefeat = false;
    int RecoveryAmount;
    int MP;

    GameObject Enemy;
    EnemyStatus EnemyStatus;
    int EnemySpeed;
    int DamegeHp;
    public bool EnemyDefeat = false;
    public int coin;

    GameObject MessageText;
    TextSpeed TextSpeed;

    GameObject Stage;
    Stage_Control Stage_Control;
    int stage;

    GameObject Technique1_obj;
    Technique1_Button Technique1_Button;
    bool Technique1Select = false;
    bool UnusableTechnique1 = false;

    GameObject Technique2_obj;
    Technique2_Button Technique2_Button;
    bool Technique2Select = false;
    bool UnusableTechnique2 = false;

    GameObject Technique3_obj;
    Technique3_Button Technique3_Button;
    bool Technique3Select = false;
    bool UnusableTechnique3 = false;

    GameObject Technique4_obj;
    Technique4_Button Technique4_Button;
    bool Technique4Select = false;
    bool UnusableTechnique4 = false;

    [SerializeField] List<int> messageList = new List<int>();//会話文リストの数
    string[] TextWindow = { "が現れた", "のターン", "を唱えた", "ダメージを受けた", "", ""};
    [SerializeField] Text text;
    float novelSpeed;//一文字一文字の表示する速さ

    int novelListIndex = 0; //現在表示中の会話文の配列
    string EnemyName = "";  //敵の名前
    public bool PlayerTurn = false; //プレイヤー、敵のターン
    public string Technique;   //テキストに表示するプレイヤーの技名
    public string EnemyTechnique;  //テキストに表示する敵の技名
    public string TechniqueType;   //技のタイプ
    public bool PlayerAttackExecution = false;  //プレイヤーの攻撃を実行してるかどうか
    public bool EnemyAttackExecution = false;   //敵の攻撃を実行してるかどうか
    public bool effect = false;
    public bool nexteffect = false;
    public bool UnusableTechnique = false;
    bool StartFlag = false; //TextWindow[0]が表示されたかどうかのフラグ
    bool NextText = true;   
    public bool SelectTechnique = false;    //プレイヤーの技決めをする為のフラグ
    public bool PlayerDefendFlag = false;
    public bool EnemyDefendFlag = false;
    public bool PlayerInspirationFlag = false;
    public bool EnemyInspirationFlag = false;
    float time = 0.0f;  //テキストの文字を全て表示した後の時間

    private void Awake()
    {
        Player = GameObject.Find("Player");
        Status = Player.GetComponent<Status>();
        Technique1 = Status.Technique1;

        Enemy = GameObject.Find("Enemy");
        EnemyStatus = Enemy.GetComponent<EnemyStatus>();

        MessageText = GameObject.Find("MessageText");
        TextSpeed = MessageText.GetComponent<TextSpeed>();

        Technique1_obj = GameObject.Find("Technique1");
        Technique1_Button = Technique1_obj.GetComponent<Technique1_Button>();
        Technique2_obj = GameObject.Find("Technique2");
        Technique2_Button = Technique2_obj.GetComponent<Technique2_Button>();
        Technique3_obj = GameObject.Find("Technique3");
        Technique3_Button = Technique3_obj.GetComponent<Technique3_Button>();
        Technique4_obj = GameObject.Find("Technique4");
        Technique4_Button = Technique4_obj.GetComponent<Technique4_Button>();
    }

    // Start is called before the first frame update
    void Start()
    {
        novelSpeed = TextSpeed.speed;
        PlayerName = Status.Player;

        Stage = GameObject.Find("Stage");
        Stage_Control = Stage.GetComponent<Stage_Control>();
        stage = Stage_Control.StageNumber;
    }

    // Update is called once per frame
    void Update()
    {
        EnemyDefeat = EnemyStatus.EnemyDefeat;
        PlayerDefeat = Status.PlayerDefeat;

        PlayerSpeed = Status.SP;
        EnemySpeed = EnemyStatus.SP;

        UnusableTechnique1 = Technique1_Button.UnusableTechnique;
        UnusableTechnique2 = Technique2_Button.UnusableTechnique;
        UnusableTechnique3 = Technique3_Button.UnusableTechnique;
        UnusableTechnique4 = Technique4_Button.UnusableTechnique;
        if (UnusableTechnique1 && UnusableTechnique2)
        {
            if (UnusableTechnique3 && UnusableTechnique4)
            {
                UnusableTechnique = true;
            }
        }

        if (EnemyName == "")
        {
            EnemyName = EnemyStatus.EnemyName;
            coin = EnemyStatus.coin;
            TextWindow[0] = MakeText0();
            PlayerTurn = Ordering();
            TextWindow[1] = MakeText1();

            if (EnemyName != "")
            {
                StartFlag = true;
                StartCoroutine(Novel());
            }
        }

        if (StartFlag)
        {
            time += Time.deltaTime;
            if (NextText && time >= 2.5f)
            {
                PlayerAttackExecution = false;
                EnemyAttackExecution = false;

                if (novelListIndex == 2 && !PlayerTurn)
                {
                    if (TechniqueSelect())
                    {
                        if (!PlayerTurn)
                        {
                            if (TechniqueType == "Defend")
                                EnemyDefendFlag = true;
                            else
                                EnemyDefendFlag = false;

                            if (TechniqueType != "")
                            {
                                StartFlag = false;
                                StartCoroutine(Novel());
                                time = 0.0f;
                            }
                        }
                    }
                }

                else if (novelListIndex == 3)
                {
                    if (PlayerTurn)
                    {
                        PlayerAttackExecution = true;

                        if (TechniqueType == "Inspiration")
                            PlayerInspirationFlag = true;
                        else
                            PlayerInspirationFlag = false;
                    }

                    else
                    {
                        EnemyAttackExecution = true;

                        if (TechniqueType == "Inspiration")
                            EnemyInspirationFlag = true;
                        else
                            EnemyInspirationFlag = false;
                    }

                    if (EnemyAttackExecution || PlayerAttackExecution)
                    {
                        TextWindow[3] = MakeText3();

                        StartFlag = false;
                        StartCoroutine(Novel());
                        time = 0.0f;
                    }
                }

                else if(novelListIndex != 2)
                {
                    TextWindow[1] = MakeText1();
                    TextWindow[4] = MakeText4();
                    TextWindow[5] = MakeText5();
                    if (novelListIndex < 5 || EnemyDefeat)
                    {
                        StartFlag = false;
                        StartCoroutine(Novel());
                    }
                    time = 0.0f;
                }
            }

            if (NextText && PlayerTurn)
            {
                if (novelListIndex == 2)
                {
                    PlayerAttackExecution = false;
                    EnemyAttackExecution = false;

                    SelectTechnique = true;
                    if (TechniqueSelect())
                    {
                        if (PlayerTurn)
                        {
                            if (TechniqueType == "Defend")
                                PlayerDefendFlag = true;
                            else
                                PlayerDefendFlag = false;
                        }

                        if (TechniqueType != "")
                        {
                            StartFlag = false;
                            StartCoroutine(Novel());
                            time = 0.0f;
                        }
                    }
                }
            }
        }
    }

    private bool Ordering()
    {
        if(PlayerSpeed > EnemySpeed)
        {
            return (true);
        }

        else if(PlayerSpeed < EnemySpeed)
        {
            PlayerTurn = false;
            return (false);
        }

        else
        {
            int RandomNumber = Random.Range(1, 2);
            if (RandomNumber == 1)
                return (true);
            else
                return (false);
        }
    }

    private bool ChangeTurn()
    {
        if (PlayerTurn)
            return (false);

        else
            return (true);
    }

    private bool TechniqueSelect()
    {
        Technique1Select = Technique1_Button.select;
        Technique2Select = Technique2_Button.select;
        Technique3Select = Technique3_Button.select;
        Technique4Select = Technique4_Button.select;

        if (PlayerTurn)
        {
            if (Technique1Select)
            {
                Technique = Status.Technique1;
                MP = Technique1_Button.MP;
                SelectTechnique = false;
                TextWindow[2] = MakeText2();
                TechniqueType = Technique1_Button.TechniqueType;
                return (true);
            }

            if (Technique2Select)
            {
                Technique = Status.Technique2;
                MP = Technique2_Button.MP;
                SelectTechnique = false;
                TextWindow[2] = MakeText2();
                TechniqueType = Technique2_Button.TechniqueType;
                return (true);
            }

            if (Technique3Select)
            {
                Technique = Status.Technique3;
                MP = Technique3_Button.MP;
                SelectTechnique = false;
                TextWindow[2] = MakeText2();
                TechniqueType = Technique3_Button.TechniqueType;
                return (true);
            }

            if (Technique4Select)
            {
                Technique = Status.Technique4;
                MP = Technique4_Button.MP;
                SelectTechnique = false;
                TextWindow[2] = MakeText2();
                TechniqueType = Technique4_Button.TechniqueType;
                return (true);
            }
        }

        else if (!PlayerTurn)
        {
            EnemyTechnique = EnemyStatus.Technique;
            if (EnemyStatus.Technique != null)
            {
                TextWindow[2] = MakeText2();
                TechniqueType = EnemyStatus.TechniqueType;
                return (true);
            }
        }

        if(UnusableTechnique)
        {
            TextWindow[2] = NoneMpText2();
            Technique = "悪あがき";
            TechniqueType = "none";
            return (true);
        }

        return (false);
    }

    private string MakeText0()
    {
        string textwindow = EnemyName + TextWindow[0];
        return (textwindow);
    }

    private string MakeText1()
    {
        string textwindow;
        TextWindow[1] = "";

        PlayerTurn = ChangeTurn();

        if (PlayerTurn)
            textwindow = PlayerName + "のターン";
        else
            textwindow = EnemyName + "のターン";
        return (textwindow);
    }

    private string MakeText2()
    {
        string textwindow;
        TextWindow[2] = "";

        if (PlayerTurn)
            textwindow = PlayerName + "はMP" + MP + "消費して" + Technique + "を唱えた！";
        else
            textwindow = EnemyName + "は" + EnemyTechnique + "を唱えた！";
        return (textwindow);
    }

    private string NoneMpText2()
    {
        string textwindow;
        TextWindow[2] = "";

        textwindow = PlayerName + "は悪あがきをした！";

        return (textwindow);
    }

    private string MakeText3()
    {
        string textwindow;
        TextWindow[3] = "";

        if (PlayerTurn)
        {
            DamegeHp = EnemyStatus.DamegeHp;
            RecoveryAmount = Status.RecoveryAmount;
        }
        else
        {
            DamegeHp = Status.DamegeHp;
            RecoveryAmount = EnemyStatus.RecoveryAmount;
        }

        if (PlayerTurn)
        {
            if (TechniqueType == "GreatRecovery")
                textwindow = PlayerName + "は" + RecoveryAmount + "回復した！";
            else if (TechniqueType == "Defend")
                textwindow = PlayerName + "はバリアを張った！";
            else if (TechniqueType == "Inspiration")
                textwindow = PlayerName + "は力が上がった！";
            else
                textwindow = EnemyName + "は" + DamegeHp + "ダメージを受けた！";
        }
        else
        {
            if (TechniqueType == "GreatRecovery")
                textwindow = EnemyName + "は" + RecoveryAmount + "回復した！";
            else if (TechniqueType == "Defend")
                textwindow = EnemyName + "はバリアを張った！";
            else if (TechniqueType == "Inspiration")
                textwindow = EnemyName + "は力が上がった！";
            else
                textwindow = PlayerName + "は" + DamegeHp + "ダメージを受けた！";
        }
        return (textwindow);
    }

    private string MakeText4()
    {
        string textwindow = "";
        TextWindow[4] = "";

        if (EnemyDefeat)
            textwindow = EnemyName + "を倒した！";
        else if (PlayerDefeat)
            textwindow = PlayerName + "は力尽きた！";

        return (textwindow);
    }

    private string MakeText5()
    {
        string textwindow = "";
        TextWindow[5] = "";

        textwindow = PlayerName + "は" + coin + "コイン手に入れた！";

        return (textwindow);
    }

    private IEnumerator Novel()
    {
        int messageCount = 0; //現在表示中の文字数
        text.text = ""; //テキストのリセット
        NextText = false;

        if (novelListIndex == 2)
            effect = true;
        else
            effect = false;

        if (novelListIndex == 3)
            nexteffect = true;
        else
            nexteffect = false;

        while (TextWindow[novelListIndex].Length > messageCount)//文字をすべて表示していない場合ループ
        {
            text.text += TextWindow[novelListIndex][messageCount];//一文字追加
            messageCount++;//現在の文字数
            yield return new WaitForSeconds(novelSpeed);//任意の時間待つ
        }

        novelListIndex++; //次の会話文配列

        if (novelListIndex > 3)
        {
            if (!EnemyDefeat && !PlayerDefeat)
            {
                novelListIndex = 1;
            }
        }

        if (novelListIndex == 3)
        {
            if (PlayerTurn)
            {
                PlayerAttackExecution = true;
            }
            else
            {
                EnemyAttackExecution = true;
            }
        }

        if (novelListIndex >= 5)
        {
            if (PlayerDefeat)
                FadeManager.Instance.LoadScene("DefeatScene", 0.5f);
        }

        if(novelListIndex > 5)
        {
            if (EnemyDefeat)
            {
                if(stage == 100)
                    FadeManager.Instance.LoadScene("EndingScene", 0.5f);
                else
                    FadeManager.Instance.LoadScene("PreparationScene", 0.5f);
            }
        }

        NextText = true;
        StartFlag = true;
    }
}
