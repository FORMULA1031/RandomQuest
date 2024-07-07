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
            case "�X���C��":
                image.sprite = Slime;
                break;
            case "�t�@�C�A�[�X���C��":
                image.sprite = FireSlime;
                break;
            case "���[�t�X���C��":
                image.sprite = LeafSlime;
                break;
            case "���C�g�j���O�X���C��":
                image.sprite = LightningSlime;
                break;
            case "�S�u����":
                image.sprite = Goblin;
                break;
            case "�ԃS�u����":
                image.sprite = RedGoblin;
                break;
            case "�S�u����":
                image.sprite = BlueGoblin;
                break;
            case "���S�u����":
                image.sprite = YellowGoblin;
                break;
            case "�I�[�N":
                image.sprite = Ork;
                break;
            case "�ԃI�[�N":
                image.sprite = RedOrk;
                break;
            case "�I�[�N":
                image.sprite = BlueOrk;
                break;
            case "���I�[�N":
                image.sprite = YellowOrk;
                break;
            case "�C���Z�N�g":
                image.sprite = Insect;
                break;
            case "�t�@�C�A�[�C���Z�N�g":
                image.sprite = FireInsect;
                break;
            case "�E�H�[�^�[�C���Z�N�g":
                image.sprite = WaterInsect;
                break;
            case "���C�g�j���O�C���Z�N�g":
                image.sprite = LightningInsect;
                break;
            case "�t�@�C�A�[�S�[����":
                image.sprite = FireGolem;
                break;
            case "�E�H�[�^�[�S�[����":
                image.sprite = WaterGolem;
                break;
            case "���[�t�S�[����":
                image.sprite = LeafGolem;
                break;
            case "�S�[����":
                image.sprite = Golem;
                break;
            case "�ԃX�P���g��":
                image.sprite = RedSkeleton;
                break;
            case "�X�P���g��":
                image.sprite = BlueSkeleton;
                break;
            case "�΃X�P���g��":
                image.sprite = GreenSkeleton;
                break;
            case "���X�P���g��":
                image.sprite = YellowSkeleton;
                break;
            case "�t�@�C�A�[�E���t":
                image.sprite = FireWolf;
                break;
            case "�E�H�[�^�[�E���t":
                image.sprite = WaterWolf;
                break;
            case "���[�t�E���t":
                image.sprite = LeafWolf;
                break;
            case "���C�g�j���O�E���t":
                image.sprite = LightningWolf;
                break;
            case "�t�@�C�A�[�N���R�_�C��":
                image.sprite = FireCrocodile;
                break;
            case "�E�H�[�^�[�N���R�_�C��":
                image.sprite = WaterCrocodile;
                break;
            case "���[�t�N���R�_�C��":
                image.sprite = LeafCrocodile;
                break;
            case "���C�g�j���O�N���R�_�C��":
                image.sprite = LightningCrocodile;
                break;
            case "�h���S��":
                image.sprite = Dragon;
                break;
            case "�E�H�[�^�[�h���S��":
                image.sprite = WaterDragon;
                break;
            case "���[�t�h���S��":
                image.sprite = LeafDragon;
                break;
            case "���C�g�j���O�h���S��":
                image.sprite = LightningDragon;
                break;
            case "�t�@�C�A�[�\�[�T���[":
                image.sprite = FireSorcerer;
                break;
            case "�E�H�[�^�[�\�[�T���[":
                image.sprite = WaterSorcerer;
                break;
            case "���[�t�\�[�T���[":
                image.sprite = LeafSorcerer;
                break;
            case "���C�g�j���O�\�[�T���[":
                image.sprite = LightningSorcerer;
                break;
            case "����":
                image.sprite = Devil;
                break;
        }
    }

    private void AttackTechniqueDiscrimination()
    {
        switch (Technique)
        {
            case "�t�@�C�A�[�p���`":
            case "�t�@�C�A�[�X���b�V��":
            case "�G�N�X�v���[�W����":
            case "�C���v�V����":
            case "�E�H�[�^�[�p���`":
            case "�E�H�[�^�[�X���b�V��":
            case "�A�V�b�h���C��":
            case "�u���C�j�N��":
            case "���[�t�p���`":
            case "���[�t�X���b�V��":
            case "�_�X�g�X�g�[��":
            case "�h���t�g�E�b�h":
            case "���C�g�j���O�p���`":
            case "���C�g�j���O�X���b�V��":
            case "�}�O�l�e�B�b�N�t�B�[���h":
            case "���[���K��":
            case "�v���~�l���X":
            case "�A�u�\�����[�g�[��":
            case "���[�J�X�g�v���C�O":
            case "�X�^�[�o�[�X�g":
            case "��������":
                AttackTechnique = true;
                break;

            case "�T�[�}��":
            case "�n�C�h��":
            case "�E�B���h":
            case "�`���[�W":
            case "�T�[�}���p���[":
            case "�n�C�h���p���[":
            case "�E�B���h�p���[":
            case "�G�N�V�[�h�`���[�W":
            case "�q�[�g�w�C�Y":
            case "�G�N�X�g���}�[":
            case "�X�P�A�N���E":
            case "�t���b�V��":
            case "�C�O�j�b�V����":
            case "�I�[�o�[�L���X�g":
            case "�z�g�V���Z�V�X":
            case "�V���C�j���O":
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