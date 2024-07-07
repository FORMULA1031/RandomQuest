using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Endroll_Control : MonoBehaviour
{
    //　テキストのスクロールスピード
    private float textScrollSpeed = 80;
    //　テキストの制限位置
    private float limitPosition = 2600f;
    //　エンドロールが終了したかどうか
    private bool isStopEndRoll;
    //　シーン移動用コルーチン
    private Coroutine endRollCoroutine;

    private bool push = false;

    // Update is called once per frame
    void Update()
    {
        //　エンドロールが終了した時
        if (isStopEndRoll)
        {
            endRollCoroutine = StartCoroutine(GoToNextScene());
        }
        else
        {
            //　エンドロール用テキストがリミットを越えるまで動かす
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
        //　5秒間待つ
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
