using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage_Control : MonoBehaviour
{
    public static int StageNumber = 1;
    GameObject Enemy;
    EnemyStatus EnemyStatus;
    
    GameObject SE;
    SE_Control SE_Control;
    float Volume;

    GameObject EventControl;
    FastWayEvent_Control FastWayEvent_Control;
    bool selectbutton = false;

    public Text Text;

    public AudioClip sound;
    AudioSource audioSource;

    int RandomNumber;
    public int NumberToGo;
    int NumberToGo_C;
    bool b_push = false;
    bool FastWayFlag = false;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        SE = GameObject.Find("SE");
        SE_Control = SE.GetComponent<SE_Control>();
        Volume = SE_Control.Volume;
        if (audioSource != null)
            audioSource.volume = Volume;

        Enemy = GameObject.Find("Enemy");
        if (Enemy != null)
            EnemyStatus = Enemy.GetComponent<EnemyStatus>();

        EventControl = GameObject.Find("EventControl");
        if (EventControl != null)
            FastWayEvent_Control = EventControl.GetComponent<FastWayEvent_Control>();

        RandomNumber = Random.Range(1, 9);
        NumberToGo_C = Random.Range(1,4);
        FastWayNumber();
    }

    // Update is called once per frame
    void Update()
    {
        if(Text != null)
            Text.text = "" + StageNumber;

        if(FastWayEvent_Control != null)
        {
            selectbutton = FastWayEvent_Control.selectbutton;
        }
    }

    public void CountReset()
    {
        StageNumber = 1;
    }

    private void FastWayNumber()
    {
        switch (NumberToGo_C)
        {
            case 1:
                NumberToGo = 5;
                break;

            case 2:
                NumberToGo = 8;
                break;

            case 3:
                NumberToGo = 10;
                break;
        }
    }

    public void FastWay()
    {
        if (!selectbutton)
        {
            StageNumber += NumberToGo;
        }
    }

    public void SortieButton()
    {
        if (!b_push)
        {
            StageNumber++;
            audioSource.PlayOneShot(sound);
            b_push = true;
            StartCoroutine("SceneChange", audioSource);
        }
    }

    private IEnumerator SceneChange(AudioSource audio)
    {
        while (true)
        {
            yield return new WaitForFixedUpdate();
            if (!audio.isPlaying)
            {
                if(StageNumber == 100)
                    FadeManager.Instance.LoadScene("BattleScene", 0.5f);
                else if (StageNumber == 90)
                    FadeManager.Instance.LoadScene("BattleScene", 0.5f);
                else if (StageNumber == 70)
                    FadeManager.Instance.LoadScene("BattleScene", 0.5f);
                else if (StageNumber == 50)
                    FadeManager.Instance.LoadScene("BattleScene", 0.5f);
                else if (StageNumber == 30)
                    FadeManager.Instance.LoadScene("BattleScene", 0.5f);
                else if (StageNumber == 10)
                    FadeManager.Instance.LoadScene("BattleScene", 0.5f);
                else if (RandomNumber == 1)
                    FadeManager.Instance.LoadScene("MushroomEventScene", 0.5f);
                else if (RandomNumber == 2)
                    FadeManager.Instance.LoadScene("InnEventScene", 0.5f);
                else if (RandomNumber == 3 && StageNumber < 80)
                    FadeManager.Instance.LoadScene("FastWayEventScene", 0.5f);
                else if (RandomNumber == 4)
                    FadeManager.Instance.LoadScene("ShoppingEventScene", 0.5f);
                else
                    FadeManager.Instance.LoadScene("BattleScene", 0.5f);
                break;
            }
        }
    }
}
