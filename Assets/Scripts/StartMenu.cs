using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public AudioClip sound;
    AudioSource audioSource;
    bool SelectScene = false;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartButton()
    {
        if (!SelectScene)
        {
            audioSource.PlayOneShot(sound);
            StartCoroutine("SceneChange", audioSource);
            SelectScene = true;
        }
    }

    public void ShoppingButton()
    {
        if (!SelectScene)
        {
            audioSource.PlayOneShot(sound);
            StartCoroutine("ShoppingSceneChange", audioSource);
            SelectScene = true;
        }
    }

    public void SettingButton()
    {
        if (!SelectScene)
        {
            audioSource.PlayOneShot(sound);
            StartCoroutine("SettingSceneChange", audioSource);
            SelectScene = true;
        }
    }

    private IEnumerator SceneChange(AudioSource audio)
    {
        while (true)
        {
            yield return new WaitForFixedUpdate();
            if (!audio.isPlaying)
            {
                FadeManager.Instance.LoadScene("OpeningScene", 0.5f);
                break;
            }
        }
    }

    private IEnumerator SettingSceneChange(AudioSource audio)
    {
        while (true)
        {
            yield return new WaitForFixedUpdate();
            if (!audio.isPlaying)
            {
                FadeManager.Instance.LoadScene("SettingScene", 0.5f);
                break;
            }
        }
    }

    private IEnumerator ShoppingSceneChange(AudioSource audio)
    {
        while (true)
        {
            yield return new WaitForFixedUpdate();
            if (!audio.isPlaying)
            {
                FadeManager.Instance.LoadScene("ShoppingScene", 0.5f);
                break;
            }
        }
    }
}
