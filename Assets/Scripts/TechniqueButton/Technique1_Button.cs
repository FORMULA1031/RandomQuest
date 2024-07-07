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
            case "�t�@�C�A�[�p���`":
            case "�t�@�C�A�[�X���b�V��":
            case "�q�[�g�w�C�Y":
            case "�T�[�}��":
            case "�T�[�}���p���[":
            case "�C�O�j�b�V����":
            case "�G�N�X�v���[�W����":
            case "�C���v�V����":
            case "�v���~�l���X":
                button.image.color = new Color(1.0f, 0.3f, 0.0f, 1.0f);
                break;

            case "�E�H�[�^�[�p���`":
            case "�E�H�[�^�[�X���b�V��":
            case "�G�N�X�g���}�[":
            case "�n�C�h��":
            case "�n�C�h���p���[":
            case "�I�[�o�[�L���X�g":
            case "�A�V�b�h���C��":
            case "�u���C�j�N��":
            case "�A�u�\�����[�g�[��":
                button.image.color = new Color(0.0f, 0.5f, 1.0f, 1.0f);
                break;

            case "���[�t�p���`":
            case "���[�t�X���b�V��":
            case "�X�P�A�N���E":
            case "�E�B���h":
            case "�E�B���h�p���[":
            case "�z�g�V���Z�V�X":
            case "�_�X�g�X�g�[��":
            case "�h���t�g�E�b�h":
            case "���[�J�X�g�v���C�O":
                button.image.color = new Color(0.0f, 1.0f, 0.0f, 1.0f);
                break;

            case "���C�g�j���O�p���`":
            case "���C�g�j���O�X���b�V��":
            case "�t���b�V��":
            case "�`���[�W":
            case "�G�N�V�[�h�`���[�W":
            case "�V���C�j���O":
            case "�}�O�l�e�B�b�N�t�B�[���h":
            case "���[���K��":
            case "�X�^�[�o�[�X�g":
                button.image.color = new Color(1.0f, 1.0f, 0.0f, 1.0f);
                break;
        }
    }

    void MPConsumption()
    {
        switch (Technique)
        {
            case "�t�@�C�A�[�p���`":
            case "�E�H�[�^�[�p���`":
            case "���[�t�p���`":
            case "���C�g�j���O�p���`":
                MP = 5;
                TechniqueType = "Blow";
                break;

            case "�t�@�C�A�[�X���b�V��":
            case "�E�H�[�^�[�X���b�V��":
            case "���[�t�X���b�V��":
            case "���C�g�j���O�X���b�V��":
                MP = 10;
                TechniqueType = "Rip";
                break;

            case "�T�[�}��":
            case "�n�C�h��":
            case "�E�B���h":
            case "�`���[�W":
                MP = 10;
                TechniqueType = "SmallRecovery";
                break;

            case "�T�[�}���p���[":
            case "�n�C�h���p���[":
            case "�E�B���h�p���[":
            case "�G�N�V�[�h�`���[�W":
                MP = 20;
                TechniqueType = "GreatRecovery";
                break;

            case "�C�O�j�b�V����":
            case "�I�[�o�[�L���X�g":
            case "�z�g�V���Z�V�X":
            case "�V���C�j���O":
                MP = 5;
                TechniqueType = "Inspiration";
                break;

            case "�G�N�X�v���[�W����":
            case "�_�X�g�X�g�[��":
            case "�A�V�b�h���C��":
            case "�}�O�l�e�B�b�N�t�B�[���h":
                MP = 15;
                TechniqueType = "SmallMagic";
                break;

            case "�C���v�V����":
            case "�u���C�j�N��":
            case "�h���t�g�E�b�h":
            case "���[���K��":
                MP = 20;
                TechniqueType = "GreatMagic";
                break;

            case "�v���~�l���X":
            case "�A�u�\�����[�g�[��":
            case "���[�J�X�g�v���C�O":
            case "�X�^�[�o�[�X�g":
                MP = 25;
                TechniqueType = "Special";
                break;

            case "�q�[�g�w�C�Y":
            case "�G�N�X�g���}�[":
            case "�X�P�A�N���E":
            case "�t���b�V��":
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
