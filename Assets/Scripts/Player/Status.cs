using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Status : MonoBehaviour
{
    public static string Player;    //�v���C���[�̖��O
    public static int HP = 1;   //�v���C���[�̗̑�
    public static int MP;   //�v���C���[�̃}�W�b�N�|�C���g
    public static int AT;   //�v���C���[�̍U����
    public static int DF;   //�v���C���[�̖h���
    public static int SP;   //�v���C���[�̑f����
    public static string Technique1;
    public static string Technique2;
    public static string Technique3;
    public static string Technique4;
    string [] Technique = {
        "�t�@�C�A�[�p���`", "�E�H�[�^�[�p���`", "���[�t�p���`", "���C�g�j���O�p���`", 
        "�t�@�C�A�[�X���b�V��", "�E�H�[�^�[�X���b�V��", "���[�t�X���b�V��", "���C�g�j���O�X���b�V��",
        "�q�[�g�w�C�Y", "�G�N�X�g���}�[", "�X�P�A�N���E", "�t���b�V��",
        "�T�[�}���p���[", "�n�C�h���p���[", "�E�B���h�p���[", "�G�N�V�[�h�`���[�W",
        "�C�O�j�b�V����", "�I�[�o�[�L���X�g", "�z�g�V���Z�V�X", "�V���C�j���O",
        "�G�N�X�v���[�W����", "�A�V�b�h���C��", "�_�X�g�X�g�[��", "�}�O�l�e�B�b�N�t�B�[���h",
        "�C���v�V����", "�u���C�j�N��", "�h���t�g�E�b�h", "���[���K��",
        "�v���~�l���X", "�A�u�\�����[�g�[��", "���[�J�X�g�v���C�O", "�X�^�[�o�[�X�g",};

    public AudioClip sound;
    AudioSource audioSource;

    Belongings Belongings;
    int NumberOfHerbs;
    int NumberOfHoney;
    int money;
    bool sword;
    bool armor;

    GameObject Enemy;
    EnemyStatus EnemyStatus;

    GameObject InputFieldManagerObject;
    InputFieldManager InputFieldManagerScript;

    GameObject Technique1_obj;
    Technique1_Button Technique1_Button;
    bool select1;

    GameObject Technique2_obj;
    Technique2_Button Technique2_Button;
    bool select2;

    GameObject Technique3_obj;
    Technique3_Button Technique3_Button;
    bool select3;

    GameObject Technique4_obj;
    Technique4_Button Technique4_Button;
    bool select4;

    GameObject MessageWindow;
    BattleText BattleText;
    bool DefendFlag = false;
    bool InspirationFlag = false;

    GameObject MushroomEventControl;
    MushroomEvent_Control MushroomEvent_Control;
    int EventNumber = 0;

    GameObject SE;
    SE_Control SE_Control;
    float Volume;

    public static int CurrentHp;
    public static int CurrentMp;
    public int CurrentAt;
    public int CurrentDf;
    public int CurrentSp;
    public int DamegeHp;
    public int RecoveryAmount;
    public int MpConsumption;
    int EnemyAT;
    public bool retry = false;
    bool PlayerAttackExecution = false; //�v���C���[�̍U�������s���ꂽ��
    bool EnemyAttackExecution = false;  //�G�̍U�������s���ꂽ��
    string PlayerTechnique;
    string EnemyTechnique;
    bool ExecutionOfMpConsumption = false;
    public bool AttackExecution = false;
    public bool PlayerDefeat = false;
    bool EventFlag = false;
    int EventRecoveryAmount;
    string TechniqueType;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        SE = GameObject.Find("SE");
        SE_Control = SE.GetComponent<SE_Control>();
        Volume = SE_Control.Volume;
        if(audioSource != null)
            audioSource.volume = Volume;

        Belongings = GetComponent<Belongings>();
        NumberOfHerbs = Belongings.NumberOfHerbs;
        NumberOfHoney = Belongings.NumberOfHoney;
        money = Belongings.Money;
        sword = Belongings.Sword;
        armor = Belongings.Armor;

        Enemy = GameObject.Find("Enemy");
        if (Enemy != null)
            EnemyStatus = Enemy.GetComponent<EnemyStatus>();

        MessageWindow = GameObject.Find("MessageText");
        if(MessageWindow != null)
            BattleText = MessageWindow.GetComponent<BattleText>();

        MushroomEventControl = GameObject.Find("EventControl");
        if (MushroomEventControl != null)
            MushroomEvent_Control = MushroomEventControl.GetComponent<MushroomEvent_Control>();
        EventRecoveryAmount = Random.Range(1, 4);

        InputFieldManagerObject = GameObject.Find("InputFieldManager");
        if (InputFieldManagerObject != null)
            InputFieldManagerScript = InputFieldManagerObject.GetComponent<InputFieldManager>();

        Technique1_obj = GameObject.Find("Technique1");
        if (Technique1_obj != null)
            Technique1_Button = Technique1_obj.GetComponent<Technique1_Button>();
        Technique2_obj = GameObject.Find("Technique2");
        if (Technique2_obj != null)
            Technique2_Button = Technique2_obj.GetComponent<Technique2_Button>();
        Technique3_obj = GameObject.Find("Technique3");
        if (Technique3_obj != null)
            Technique3_Button = Technique3_obj.GetComponent<Technique3_Button>();
        Technique4_obj = GameObject.Find("Technique4");
        if (Technique4_obj != null)
            Technique4_Button = Technique4_obj.GetComponent<Technique4_Button>();
    }

    // Update is called once per frame
    void Update()
    {
        if (InputFieldManagerObject != null)
            Player = InputFieldManagerScript.name;

        if (CurrentHp <= 0)
        {
            CurrentHp = 0;
            PlayerDefeat = true;
        }

        if (CurrentMp <= 0)
        {
            CurrentMp = 0;
        }

        if (CurrentHp > HP)
            CurrentHp = HP;

        if (CurrentMp > MP)
            CurrentMp = MP;


        if (BattleText != null)
        {
            PlayerAttackExecution = BattleText.PlayerAttackExecution;
            EnemyAttackExecution = BattleText.EnemyAttackExecution;
        }

        if (PlayerAttackExecution)
        {
            if (!ExecutionOfMpConsumption)
            {
                PlayerTechnique = BattleText.Technique;
                MPConsumption();
                TechniqueType = BattleText.TechniqueType;

                if (TechniqueType == "GreatRecovery")
                {
                    RecoveryAmount = HP / 3;
                    CurrentHp += RecoveryAmount;
                }

                else if (TechniqueType == "none")
                {
                    CurrentHp -= 100;
                }

                ExecutionOfMpConsumption = true;
            }
        }

        else
        {
            ExecutionOfMpConsumption = false;
        }

        if(EnemyStatus != null)
            EnemyAT = EnemyStatus.AT;

        if (EnemyAttackExecution)
        {
            if (!AttackExecution)
            {
                EnemyTechnique = EnemyStatus.Technique;
                DamageAmount();
                AttackExecution = true;
            }
        }

        else
        {
            AttackExecution = false;
        }

        if(MushroomEvent_Control != null)
            EventNumber = MushroomEvent_Control.EventNumber;

        if(EventNumber != 0 && !EventFlag)
        {
            switch (EventNumber)
            {
                case 1:
                    if(EventRecoveryAmount == 1)
                        RecoveryAmount = HP / 2;
                    if (EventRecoveryAmount == 2)
                        RecoveryAmount = HP / 4;
                    else
                        RecoveryAmount = HP / 10;
                    CurrentHp -= RecoveryAmount;
                    break;

                case 2:
                    if (EventRecoveryAmount == 1)
                        RecoveryAmount = MP / 2;
                    if (EventRecoveryAmount == 2)
                        RecoveryAmount = MP / 4;
                    else
                        RecoveryAmount = MP / 10;
                    CurrentMp -= RecoveryAmount;
                    break;

                case 3:
                    if (EventRecoveryAmount == 1)
                        RecoveryAmount = HP / 2;
                    if (EventRecoveryAmount == 2)
                        RecoveryAmount = HP / 4;
                    else
                        RecoveryAmount = HP / 10;
                    CurrentHp += RecoveryAmount;
                    break;

                case 4:
                    if (EventRecoveryAmount == 1)
                        RecoveryAmount = MP / 2;
                    if (EventRecoveryAmount == 2)
                        RecoveryAmount = MP / 4;
                    else
                        RecoveryAmount = MP / 10;
                    CurrentMp += RecoveryAmount;
                    break;

                default:
                    break;
            }

            if (CurrentHp <= 0)
            {
                CurrentHp = 1;
            }

            EventFlag = true;
        }

        if(Technique1_Button != null)
        {
            select1 = Technique1_Button.select;
            if (select1)
            {
                Technique1Change();
            }
        }

        if (Technique2_Button != null)
        {
            select2 = Technique2_Button.select;
            if (select2)
            {
                Technique2Change();
            }
        }

        if (Technique3_Button != null)
        {
            select3 = Technique3_Button.select;
            if (select3)
            {
                Technique3Change();
            }
        }

        if (Technique4_Button != null)
        {
            select4 = Technique4_Button.select;
            if (select4)
            {
                Technique4Change();
            }
        }
    }

    public void Retry()
    {
        retry = true;
        PlayerStatus();
        Techniques();
        audioSource.PlayOneShot(sound);
        Debug.Log(Technique1 + "," + Technique2 + "," + Technique3 + "," + Technique4);
    }

    void PlayerStatus()
    {
        HP = Random.Range(1, 500);
        MP = Random.Range(1, 500);
        if (sword)
            AT = Random.Range(20, 120);
        else
            AT = Random.Range(1, 100);
        if(armor)
            DF = Random.Range(20, 120);
        else
            DF = Random.Range(1, 100);
        SP = Random.Range(1, 100);
        CurrentHp = HP;
        CurrentMp = MP;
    }

    void Techniques()
    {
        var list = new List<string>();
        list.AddRange(Technique);

        Technique1 = Technique[Random.Range(0, 32)];
        list.Remove(Technique1);

        string[] new_src;
        new_src = list.ToArray();

        Technique2 = new_src[Random.Range(0, 31)];
        list.Remove(Technique2);
        new_src = list.ToArray();

        Technique3 = new_src[Random.Range(0, 30)];
        list.Remove(Technique3);
        new_src = list.ToArray();

        Technique4 = new_src[Random.Range(0, 29)];
    }

    private void Technique1Change()
    {
        while (true)
        {
            string TC = Technique[Random.Range(0, 32)];
            if (TC != Technique2)
            {
                if(TC != Technique3)
                {
                    if(TC != Technique4)
                    {
                        Technique1 = TC;
                        return;
                    }
                }
            }
        }
    }

    private void Technique2Change()
    {
        while (true)
        {
            string TC = Technique[Random.Range(0, 32)];
            if (TC != Technique1)
            {
                if (TC != Technique3)
                {
                    if (TC != Technique4)
                    {
                        Technique2 = TC;
                        return;
                    }
                }
            }
        }
    }

    private void Technique3Change()
    {
        while (true)
        {
            string TC = Technique[Random.Range(0, 32)];
            if (TC != Technique1)
            {
                if (TC != Technique2)
                {
                    if (TC != Technique4)
                    {
                        Technique3 = TC;
                        return;
                    }
                }
            }
        }
    }

    private void Technique4Change()
    {
        while (true)
        {
            string TC = Technique[Random.Range(0, 32)];
            if (TC != Technique1)
            {
                if (TC != Technique2)
                {
                    if (TC != Technique3)
                    {
                        Technique4 = TC;
                        return;
                    }
                }
            }
        }
    }

    void MPConsumption()
    {
        switch (PlayerTechnique)
        {
            case "�t�@�C�A�[�p���`":
            case "�E�H�[�^�[�p���`":
            case "���[�t�p���`":
            case "���C�g�j���O�p���`":
                MpConsumption = 5;
                CurrentMp -= 5;
                break;

            case "�t�@�C�A�[�X���b�V��":
            case "�E�H�[�^�[�X���b�V��":
            case "���[�t�X���b�V��":
            case "���C�g�j���O�X���b�V��":
                MpConsumption = 10;
                CurrentMp -= 10;
                break;

            case "�q�[�g�w�C�Y":
            case "�G�N�X�g���}�[":
            case "�X�P�A�N���E":
            case "�t���b�V��":
                MpConsumption = 10;
                CurrentMp -= 10;
                break;

            case "�T�[�}���p���[":
            case "�n�C�h���p���[":
            case "�E�B���h�p���[":
            case "�G�N�V�[�h�`���[�W":
                MpConsumption = 20;
                CurrentMp -= 20;
                break;

            case "�C�O�j�b�V����":
            case "�I�[�o�[�L���X�g":
            case "�z�g�V���Z�V�X":
            case "�V���C�j���O":
                MpConsumption = 5;
                CurrentMp -= 5;
                break;

            case "�G�N�X�v���[�W����":
            case "�_�X�g�X�g�[��":
            case "�A�V�b�h���C��":
            case "�}�O�l�e�B�b�N�t�B�[���h":
                MpConsumption = 15;
                CurrentMp -= 15;
                break;

            case "�C���v�V����":
            case "�u���C�j�N��":
            case "�h���t�g�E�b�h":
            case "���[���K��":
                MpConsumption = 20;
                CurrentMp -= 20;
                break;

            case "�v���~�l���X":
            case "�A�u�\�����[�g�[��":
            case "���[�J�X�g�v���C�O":
            case "�X�^�[�o�[�X�g":
                MpConsumption = 25;
                CurrentMp -= 25;
                break;
        }
    }

    void MPConsumption_Text()
    {
        switch (PlayerTechnique)
        {
            case "�t�@�C�A�[�p���`":
            case "�E�H�[�^�[�p���`":
            case "���[�t�p���`":
            case "���C�g�j���O�p���`":
                MpConsumption = 5;
                break;

            case "�t�@�C�A�[�X���b�V��":
            case "�E�H�[�^�[�X���b�V��":
            case "���[�t�X���b�V��":
            case "���C�g�j���O�X���b�V��":
                MpConsumption = 10;
                break;

            case "�q�[�g�w�C�Y":
            case "�G�N�X�g���}�[":
            case "�X�P�A�N���E":
            case "�t���b�V��":
                MpConsumption = 10;
                break;

            case "�T�[�}���p���[":
            case "�n�C�h���p���[":
            case "�E�B���h�p���[":
            case "�G�N�V�[�h�`���[�W":
                MpConsumption = 20;
                break;

            case "�C�O�j�b�V����":
            case "�I�[�o�[�L���X�g":
            case "�z�g�V���Z�V�X":
            case "�V���C�j���O":
                MpConsumption = 5;
                break;

            case "�G�N�X�v���[�W����":
            case "�_�X�g�X�g�[��":
            case "�A�V�b�h���C��":
            case "�}�O�l�e�B�b�N�t�B�[���h":
                MpConsumption = 15;
                break;

            case "�C���v�V����":
            case "�u���C�j�N��":
            case "�h���t�g�E�b�h":
            case "���[���K��":
                MpConsumption = 20;
                break;

            case "�v���~�l���X":
            case "�A�u�\�����[�g�[��":
            case "���[�J�X�g�v���C�O":
            case "�X�^�[�o�[�X�g":
                MpConsumption = 25;
                break;
        }
    }

    void DamageAmount()
    {
        switch (EnemyTechnique)
        {
            case "�t�@�C�A�[�p���`":
            case "�E�H�[�^�[�p���`":
            case "���[�t�p���`":
            case "���C�g�j���O�p���`":
                DamegeHp = (EnemyAT * 1) - DF;
                DamageAmountCorrection();
                CurrentHp -= DamegeHp;
                break;

            case "�t�@�C�A�[�X���b�V��":
            case "�E�H�[�^�[�X���b�V��":
            case "���[�t�X���b�V��":
            case "���C�g�j���O�X���b�V��":
                DamegeHp = EnemyAT + (EnemyAT / 2) - DF;
                DamageAmountCorrection();
                CurrentHp -= DamegeHp;
                break;

            case "�G�N�X�v���[�W����":
            case "�_�X�g�X�g�[��":
            case "�A�V�b�h���C��":
            case "�}�O�l�e�B�b�N�t�B�[���h":
                DamegeHp = (EnemyAT * 2) - DF;
                DamageAmountCorrection();
                CurrentHp -= DamegeHp;
                break;

            case "�C���v�V����":
            case "�u���C�j�N��":
            case "�h���t�g�E�b�h":
            case "���[���K��":
                DamegeHp = (EnemyAT * 2) + (EnemyAT / 2) - DF;
                DamageAmountCorrection();
                CurrentHp -= DamegeHp;
                break;

            case "�v���~�l���X":
            case "�A�u�\�����[�g�[��":
            case "���[�J�X�g�v���C�O":
            case "�X�^�[�o�[�X�g":
                DamegeHp = EnemyAT * 3 - DF;
                DamageAmountCorrection();
                CurrentHp -= DamegeHp;
                break;

            default:
                break;
        }
    }

    void DamageAmountCorrection()
    {
        if (DamegeHp < 0)
        {
            DamegeHp = 0;
        }

        DefendFlag = BattleText.PlayerDefendFlag;
        if (DefendFlag)
            DamegeHp = 0;

        InspirationFlag = BattleText.EnemyInspirationFlag;
        if (InspirationFlag)
            DamegeHp *= 2;
    }

    public void UseHerbs()
    {
        if (NumberOfHoney > 0 && CurrentHp != HP)
            CurrentHp += 50;
    }

    public void UseHoney()
    {
        if (NumberOfHoney > 0 || CurrentMp == MP)
            CurrentMp += 50;
    }

    public void EnterTheInn()
    {
        if (money >= 100 && !EventFlag)
        {
            CurrentHp = HP;
            CurrentMp = MP;
            EventFlag = true;
        }
    }

    public void CanselEnterTheInn()
    {
        EventFlag = true;
    }
}
