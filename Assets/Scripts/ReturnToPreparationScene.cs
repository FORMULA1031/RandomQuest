using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToPreparationScene : MonoBehaviour
{
    GameObject SE;
    SE_Control SE_Control;
    public AudioClip sound;
    AudioSource audioSource;
    bool GobackScene = false;
    float Volume;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        SE = GameObject.Find("SE");
        SE_Control = SE.GetComponent<SE_Control>();
    }

    // Update is called once per frame    
    void Update()
    {
        Volume = SE_Control.Volume;
        audioSource.volume = Volume;
    }

    public void GobackButton()
    {
        if (!GobackScene)
        {
            audioSource.PlayOneShot(sound);
            StartCoroutine("SceneChange", audioSource);
            GobackScene = true;
        }
    }

    private IEnumerator SceneChange(AudioSource audio)
    {
        while (true)
        {
            yield return new WaitForFixedUpdate();
            if (!audio.isPlaying)
            {
                FadeManager.Instance.LoadScene("PreparationScene", 0.5f);
                break;
            }
        }
    }
}
