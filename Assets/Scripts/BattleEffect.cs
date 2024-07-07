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
            case "�t�@�C�A�[�p���`":
            case "�t�@�C�A�[�X���b�V��":
            case "�G�N�X�v���[�W����":
            case "�C���v�V����":
                TechniqueType = "Fire";
                break;

            case "�E�H�[�^�[�p���`":
            case "�E�H�[�^�[�X���b�V��":
            case "�A�V�b�h���C��":
            case "�u���C�j�N��":
                TechniqueType = "Water";
                break;

            case "���[�t�p���`":
            case "���[�t�X���b�V��":
            case "�_�X�g�X�g�[��":
            case "�h���t�g�E�b�h":
                TechniqueType = "Leaf";
                break;

            case "���C�g�j���O�p���`":
            case "���C�g�j���O�X���b�V��":
            case "�}�O�l�e�B�b�N�t�B�[���h":
            case "���[���K��":
                TechniqueType = "Lightning";
                break;

            case "�T�[�}���p���[":
            case "�n�C�h���p���[":
            case "�E�B���h�p���[":
            case "�G�N�V�[�h�`���[�W":
                TechniqueType = "Recovery";
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

            case "�v���~�l���X":
                TechniqueType = "SpecialFire";
                break;

            case "�A�u�\�����[�g�[��":
                TechniqueType = "SpecialWater";
                break;

            case "���[�J�X�g�v���C�O":
                TechniqueType = "SpecialLeaf";
                break;

            case "�X�^�[�o�[�X�g":
                TechniqueType = "SpecialLightning";
                break;

            case "��������":
                TechniqueType = "Waruagaki";
                break;
        }
    }
}
