using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Endroll_Control : MonoBehaviour
{
    //�@�e�L�X�g�̃X�N���[���X�s�[�h
    private float textScrollSpeed = 80;
    //�@�e�L�X�g�̐����ʒu
    private float limitPosition = 2600f;
    //�@�G���h���[�����I���������ǂ���
    private bool isStopEndRoll;
    //�@�V�[���ړ��p�R���[�`��
    private Coroutine endRollCoroutine;

    private bool push = false;

    // Update is called once per frame
    void Update()
    {
        //�@�G���h���[�����I��������
        if (isStopEndRoll)
        {
            endRollCoroutine = StartCoroutine(GoToNextScene());
        }
        else
        {
            //�@�G���h���[���p�e�L�X�g�����~�b�g���z����܂œ�����
            if (transform.position.y <= limitPosition)
            {
                transform.position = new Vector2(transform.position.x, transform.position.y + textScrollSpeed * Time.deltaTime);
                Debug.Log(transform.position.y);
            }
            else
            {
                isStopEndRoll = true;
            }
        }
    }

    IEnumerator GoToNextScene()
    {
        //�@5�b�ԑ҂�
        yield return new WaitForSeconds(3f);

        if (Input.GetMouseButton(0) && !push)
        {
            StopCoroutine(endRollCoroutine);
            FadeManager.Instance.LoadScene("StartMenuScene", 0.5f);
            push = true;
        }

        yield return null;
    }
}
