using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyStatus : MonoBehaviour
{
    public int HP;   //�G�̗̑�
    public int AT;   //�G�̍U����
    public int DF;   //�G�̖h���
    public int SP;   //�G�̑f����
    string[] EnemyNames = { "�t�@�C�A�[�X���C��", "�X���C��", "���[�t�X���C��", "���C�g�j���O�X���C��",
        "�ԃS�u����", "�S�u����", "�S�u����", "���S�u����",
        "�ԃI�[�N", "�I�[�N", "�I�[�N", "���I�[�N",
        "�t�@�C�A�[�C���Z�N�g", "�E�H�[�^�[�C���Z�N�g", "�C���Z�N�g", "���C�g�j���O�C���Z�N�g",
        "�t�@�C�A�[�S�[����", "�E�H�[�^�[�S�[����", "���[�t�S�[����", "�S�[����",
        "�ԃX�P���g��", "�X�P���g��", "�΃X�P���g��", "���X�P���g��",
        "�t�@�C�A�[�E���t", "�E�H�[�^�[�E���t", "���[�t�E���t", "���C�g�j���O�E���t",
        "�t�@�C�A�[�N���R�_�C��", "�E�H�[�^�[�N���R�_�C��", "���[�t�N���R�_�C��", "���C�g�j���O�N���R�_�C��",
        "�h���S��", "�E�H�[�^�[�h���S��", "���[�t�h���S��", "���C�g�j���O�h���S��",
        "�t�@�C�A�[�\�[�T���[", "�E�H�[�^�[�\�[�T���[", "���[�t�\�[�T���[", "���C�g�j���O�\�[�T���["};

    string[] Techniques = { "�t�@�C�A�[�p���`", "�t�@�C�A�[�X���b�V��", "�q�[�g�w�C�Y", "�T�[�}���p���[", "�C�O�j�b�V����", "�G�N�X�v���[�W����", "�C���v�V����", "�v���~�l���X",  
         "�E�H�[�^�[�p���`", "�E�H�[�^�[�X���b�V��", "�G�N�X�g���}�[", "�n�C�h���p���[", "�I�[�o�[�L���X�g", "�A�V�b�h���C��", "�u���C�j�N��", "�A�u�\�����[�g�[��",
         "���[�t�p���`", "���[�t�X���b�V��", "�X�P�A�N���E", "�E�B���h�p���[", "�z�g�V���Z�V�X", "�_�X�g�X�g�[��", "�h���t�g�E�b�h", "���[�J�X�g�v���C�O", 
         "���C�g�j���O�p���`", "���C�g�j���O�X���b�V��", "�t���b�V��", "�G�N�V�[�h�`���[�W", "�V���C�j���O", "�}�O�l�e�B�b�N�t�B�[���h", "���[���K��", "�X�^�[�o�[�X�g"};

    public string Technique;    //�G��������Z
    public string EnemyName;    //�G�̖��O
    public string Attribute;

    GameObject Player;
    Status Status;
    int PlayerAT;   //�v���C���[�̍U����

    GameObject MessageWindow;
    BattleText BattleText;
    bool DefendFlag = false;
    bool InspirationFlag = false;

    GameObject EventSystem;
    Stage_Control Stage_Control;
    int StageNumber;

    public int CurrentHp;   //�G�̎c��̗�
    public int DamegeHp;    //�G�̃_���[�W��
    public int RecoveryAmount;
    public int coin;    //�l���}�l�[
    bool turn;  //true��������v���C���[�̃^�[���Afalse��������G�̃^�[��
    bool PlayerAttackExecution = false; //�v���C���[�̍U�������s���ꂽ��
    bool EnemyAttackExecution = false;  //�G�̍U�������s���ꂽ��
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
                EnemyName = "�S�u����";
            else if (StageNumber == 30)
                EnemyName = "�C���Z�N�g";
            else if (StageNumber == 50)
                EnemyName = "�S�[����";
            else if (StageNumber == 70)
                EnemyName = "�h���S��";
            else if (StageNumber == 90)
                EnemyName = "�E�H�[�^�[�\�[�T���[";
            else if (StageNumber == 100)
                EnemyName = "����";
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
            case "�t�@�C�A�[�X���C��":
                Attribute = "fire";
                coin = 10;
                EnemyStatusChange(1, 300, 1, 80, 1, 30, 1, 100);
                break;

            case "�X���C��":
                Attribute = "water";
                coin = 10;
                EnemyStatusChange(1, 300, 1, 80, 1, 30, 1, 100);
                break;

            case "���[�t�X���C��":
                Attribute = "leaf";
                coin = 10;
                EnemyStatusChange(1, 300, 1, 80, 1, 30, 1, 100);
                break;

            case "���C�g�j���O�X���C��":
                Attribute = "lightning";
                coin = 10;
                EnemyStatusChange(1, 300, 1, 80, 1, 30, 1, 100);
                break;

            case "�ԃS�u����":
                Attribute = "fire";
                coin = 30;
                EnemyStatusChange(50, 250, 20, 50, 10, 40, 10, 40);
                break;

            case "�S�u����":
                Attribute = "water";
                coin = 30;
                EnemyStatusChange(50, 250, 20, 50, 10, 40, 10, 40);
                break;

            case "�S�u����":
                Attribute = "leaf";
                coin = 30;
                EnemyStatusChange(50, 250, 20, 50, 10, 40, 10, 40);
                break;

            case "���S�u����":
                Attribute = "lightning";
                coin = 30;
                EnemyStatusChange(50, 250, 20, 50, 10, 40, 10, 40);
                break;

            case "�ԃI�[�N":
                Attribute = "fire";
                coin = 30;
                EnemyStatusChange(50, 250, 30, 60, 15, 40, 10, 25);
                break;

            case "�I�[�N":
                Attribute = "water";
                coin = 30;
                EnemyStatusChange(50, 250, 30, 60, 15, 40, 10, 25);
                break;

            case "�I�[�N":
                Attribute = "leaf";
                coin = 30;
                EnemyStatusChange(50, 250, 30, 60, 15, 40, 10, 25);
                break;

            case "���I�[�N":
                Attribute = "lightning";
                coin = 30;
                EnemyStatusChange(50, 250, 30, 60, 15, 40, 10, 25);
                break;

            case "�t�@�C�A�[�C���Z�N�g":
                Attribute = "fire";
                coin = 50;
                EnemyStatusChange(10, 200, 20, 50, 10, 30, 50, 90);
                break;

            case "�E�H�[�^�[�C���Z�N�g":
                Attribute = "water";
                coin = 50;
                EnemyStatusChange(10, 200, 20, 50, 10, 30, 50, 90);
                break;

            case "�C���Z�N�g":
                Attribute = "leaf";
                coin = 50;
                EnemyStatusChange(10, 200, 20, 50, 10, 30, 50, 90);
                break;

            case "���C�g�j���O�C���Z�N�g":
                Attribute = "lightning";
                coin = 50;
                EnemyStatusChange(10, 200, 20, 50, 10, 30, 50, 90);
                break;

            case "�t�@�C�A�[�S�[����":
                Attribute = "fire";
                coin = 100;
                EnemyStatusChange(200, 500, 40, 60, 40, 70, 1, 30);
                break;

            case "�E�H�[�^�[�S�[����":
                Attribute = "water";
                coin = 100;
                EnemyStatusChange(200, 500, 40, 60, 40, 70, 1, 30);
                break;

            case "���[�t�S�[����":
                Attribute = "leaf";
                coin = 100;
                EnemyStatusChange(200, 500, 40, 60, 40, 70, 1, 30);
                break;

            case "�S�[����":
                Attribute = "lightning";
                coin = 100;
                EnemyStatusChange(200, 500, 40, 60, 40, 70, 1, 30);
                break;

            case "�ԃX�P���g��":
                Attribute = "fire";
                coin = 50;
                EnemyStatusChange(1, 100, 20, 80, 1, 10, 50, 70);
                break;

            case "�X�P���g��":
                Attribute = "water";
                coin = 50;
                EnemyStatusChange(1, 100, 20, 80, 1, 10, 50, 70);
                break;

            case "�΃X�P���g��":
                Attribute = "leaf";
                coin = 50;
                EnemyStatusChange(1, 100, 20, 80, 1, 10, 50, 70);
                break;

            case "���X�P���g��":
                Attribute = "lightning";
                coin = 50;
                EnemyStatusChange(1, 100, 20, 80, 1, 10, 50, 70);
                break;

            case "�t�@�C�A�[�E���t":
                Attribute = "fire";
                coin = 120;
                EnemyStatusChange(100, 300, 50, 60, 20, 40, 40, 60);
                break;

            case "�E�H�[�^�[�E���t":
                Attribute = "water";
                coin = 120;
                EnemyStatusChange(100, 300, 50, 60, 20, 40, 40, 60);
                break;

            case "���[�t�E���t":
                Attribute = "leaf";
                coin = 120;
                EnemyStatusChange(100, 300, 50, 60, 20, 40, 40, 60);
                break;

            case "���C�g�j���O�E���t":
                Attribute = "lightning";
                coin = 120;
                EnemyStatusChange(100, 300, 50, 60, 20, 40, 40, 60);
                break;

            case "�t�@�C�A�[�N���R�_�C��":
                Attribute = "fire";
                coin = 120;
                EnemyStatusChange(200, 300, 40, 60, 30, 45, 40, 50);
                break;

            case "�E�H�[�^�[�N���R�_�C��":
                Attribute = "water";
                coin = 120;
                EnemyStatusChange(200, 300, 40, 60, 30, 45, 40, 50);
                break;

            case "���[�t�N���R�_�C��":
                Attribute = "leaf";
                coin = 120;
                EnemyStatusChange(200, 300, 40, 60, 30, 45, 40, 50);
                break;

            case "���C�g�j���O�N���R�_�C��":
                Attribute = "lightning";
                coin = 120;
                EnemyStatusChange(200, 300, 40, 60, 30, 45, 40, 50);
                break;

            case "�h���S��":
                Attribute = "fire";
                coin = 200;
                EnemyStatusChange(200, 300, 50, 70, 20, 50, 20, 60);
                break;

            case "�E�H�[�^�[�h���S��":
                Attribute = "water";
                coin = 200;
                EnemyStatusChange(200, 300, 50, 70, 20, 50, 20, 60);
                break;

            case "���[�t�h���S��":
                Attribute = "leaf";
                coin = 200;
                EnemyStatusChange(200, 300, 50, 70, 20, 50, 20, 60);
                break;

            case "���C�g�j���O�h���S��":
                Attribute = "lightning";
                coin = 200;
                EnemyStatusChange(200, 300, 50, 70, 20, 50, 20, 60);
                break;

            case "�t�@�C�A�[�\�[�T���[":
                Attribute = "fire";
                coin = 200;
                EnemyStatusChange(100, 300, 30, 80, 10, 50, 10, 80);
                break;

            case "�E�H�[�^�[�\�[�T���[":
                Attribute = "water";
                coin = 200;
                EnemyStatusChange(100, 300, 30, 80, 10, 50, 10, 80);
                break;

            case "���[�t�\�[�T���[":
                Attribute = "leaf";
                coin = 200;
                EnemyStatusChange(100, 300, 30, 80, 10, 50, 10, 80);
                break;

            case "���C�g�j���O�\�[�T���[":
                Attribute = "lightning";
                coin = 200;
                EnemyStatusChange(100, 300, 30, 80, 10, 50, 10, 80);
                break;

            case "����":
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
            case "�t�@�C�A�[�p���`":
            case "�E�H�[�^�[�p���`":
            case "���[�t�p���`":
            case "���C�g�j���O�p���`":
                DamegeHp = (PlayerAT * 1) - DF;
                DamageAmountCorrection();
                CurrentHp -= DamegeHp;
                break;

            case "�t�@�C�A�[�X���b�V��":
            case "�E�H�[�^�[�X���b�V��":
            case "���[�t�X���b�V��":
            case "���C�g�j���O�X���b�V��":
                DamegeHp = PlayerAT + (PlayerAT / 2) - DF;
                DamageAmountCorrection();
                CurrentHp -= DamegeHp;
                break;

            case "�T�[�}���p���[":
            case "�n�C�h���p���[":
            case "�E�B���h�p���[":
            case "�G�N�V�[�h�`���[�W":
                DamegeHp = (PlayerAT * 0) - DF;
                DamageAmountCorrection();
                CurrentHp -= DamegeHp;
                break;

            case "�C�O�j�b�V����":
            case "�I�[�o�[�L���X�g":
            case "�z�g�V���Z�V�X":
            case "�V���C�j���O":
                DamegeHp = (PlayerAT * 0) - DF;
                DamageAmountCorrection();
                CurrentHp -= DamegeHp;
                break;

            case "�G�N�X�v���[�W����":
            case "�_�X�g�X�g�[��":
            case "�A�V�b�h���C��":
            case "�}�O�l�e�B�b�N�t�B�[���h":
                DamegeHp = (PlayerAT * 2) - DF;
                DamageAmountCorrection();
                CurrentHp -= DamegeHp;
                break;

            case "�C���v�V����":
            case "�u���C�j�N��":
            case "�h���t�g�E�b�h":
            case "���[���K��":
                DamegeHp = (PlayerAT * 2) + (PlayerAT / 2) - DF;
                DamageAmountCorrection();
                CurrentHp -= DamegeHp;
                break;

            case "�v���~�l���X":
            case "�A�u�\�����[�g�[��":
            case "���[�J�X�g�v���C�O":
            case "�X�^�[�o�[�X�g":
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
            case "�t�@�C�A�[�p���`":
            case "�t�@�C�A�[�X���b�V��":
            case "�T�[�}���p���[":
            case "�G�N�X�v���[�W����":
            case "�C���v�V����":
            case "�v���~�l���X":
                if (Attribute == "leaf")
                    PlayerAT *= 2;
                if (Attribute == "water")
                    PlayerAT /= 2;
                break;

            case "�E�H�[�^�[�p���`":
            case "�E�H�[�^�[�X���b�V��":
            case "�n�C�h���p���[":
            case "�A�V�b�h���C��":
            case "�u���C�j�N��":
            case "�A�u�\�����[�g�[��":
                if (Attribute == "fire")
                    PlayerAT *= 2;
                if (Attribute == "leaf")
                    PlayerAT /= 2;
                break;

            case "���[�t�p���`":
            case "���[�t�X���b�V��":
            case "�E�B���h�p���[":
            case "�_�X�g�X�g�[��":
            case "�h���t�g�E�b�h":
            case "���[�J�X�g�v���C�O":
                if (Attribute == "water")
                    PlayerAT *= 2;
                if (Attribute == "fire")
                    PlayerAT /= 2;
                break;

            case "���C�g�j���O�p���`":
            case "���C�g�j���O�X���b�V��":
            case "�G�N�V�[�h�`���[�W":
            case "�}�O�l�e�B�b�N�t�B�[���h":
                break;

            default:
                break;
        }
    }

    void TechniqueTypeDetermination()
    {
        switch (Technique)
        {
            case "�t�@�C�A�[�p���`":
            case "�E�H�[�^�[�p���`":
            case "���[�t�p���`":
            case "���C�g�j���O�p���`":
                TechniqueType = "Blow";
                break;

            case "�t�@�C�A�[�X���b�V��":
            case "�E�H�[�^�[�X���b�V��":
            case "���[�t�X���b�V��":
            case "���C�g�j���O�X���b�V��":
                TechniqueType = "Rip";
                break;

            case "�q�[�g�w�C�Y":
            case "�G�N�X�g���}�[":
            case "�X�P�A�N���E":
            case "�t���b�V��":
                TechniqueType = "Defend";
                break;

            case "�C�O�j�b�V����":
            case "�I�[�o�[�L���X�g":
            case "�z�g�V���Z�V�X":
            case "�V���C�j���O":
                TechniqueType = "Inspiration";
                break;

            case "�T�[�}���p���[":
            case "�n�C�h���p���[":
            case "�E�B���h�p���[":
            case "�G�N�V�[�h�`���[�W":
                TechniqueType = "GreatRecovery";
                break;

            case "�G�N�X�v���[�W����":
            case "�_�X�g�X�g�[��":
            case "�A�V�b�h���C��":
            case "�}�O�l�e�B�b�N�t�B�[���h":
                TechniqueType = "SmallMagic";
                break;

            case "�C���v�V����":
            case "�u���C�j�N��":
            case "�h���t�g�E�b�h":
            case "���[���K��":
                TechniqueType = "GreatMagic";
                break;

            case "�v���~�l���X":
            case "�A�u�\�����[�g�[��":
            case "���[�J�X�g�v���C�O":
            case "�X�^�[�o�[�X�g":
                TechniqueType = "Special";
                break;
        }
    }
}
