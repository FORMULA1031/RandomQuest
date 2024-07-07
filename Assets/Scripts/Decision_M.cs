using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Decision_M : MonoBehaviour
{
    GameObject Player;
    Status Status;
    bool retry = false;
    bool b_push = false;
    public AudioClip sound;
    AudioSource audioSource;
    GameObject SE;
    SE_Control SE_Control;
    float Volume;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        Status = Player.GetComponent<Status>();
        GetComponent<Button>().interactable = false;
        audioSource = GetComponent<AudioSource>();
        SE = GameObject.Find("SE");
        SE_Control = SE.GetComponent<SE_Control>();
        Volume = SE_Control.Volume;
        audioSource.volume = Volume;
    }

    // Update is called once per frame
    void Update()
    {
        retry = Status.retry;
        if (retry)
        {
            GetComponent<Button>().interactable = true;
        }
    }

    public void Decision()
    {
        if (!b_push)
        {
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
                FadeManager.Instance.LoadScene("BattleScene", 0.5f);
                break;
            }
        }
    }
}
