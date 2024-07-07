using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Technique3_Text : MonoBehaviour
{
    GameObject Player;
    Status Status;
    public Text Text;
    string Technique;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        Status = Player.GetComponent<Status>();
    }

    // Update is called once per frame
    void Update()
    {
        Technique = Status.Technique3;
        Text.text = Technique;
        TextColor(Technique);
    }

    void TextColor(string Technique)
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
                Text.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
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
                Text.color = new Color(0.0f, 0.0f, 1.0f, 1.0f);
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
                Text.color = new Color(0.0f, 0.5f, 0.0f, 1.0f);
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
                Text.color = new Color(1.0f, 1.0f, 0.0f, 1.0f);
                break;
        }
    }
}
