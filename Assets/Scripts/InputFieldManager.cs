using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InputFieldManager : MonoBehaviour
{
    InputField inputField;
    public string name;

    public AudioClip sound;
    public AudioClip sound2;
    AudioSource audioSource;
    bool SelectScene = false;
    GameObject SE;
    SE_Control SE_Control;
    float Volume;

    // Start is called before the first frame update
    void Start()
    {
        inputField = GameObject.Find("InputField").GetComponent<InputField>();
        audioSource = GetComponent<AudioSource>();
        SE = GameObject.Find("SE");
        SE_Control = SE.GetComponent<SE_Control>();
        Volume = SE_Control.Volume;
        audioSource.volume = Volume;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetInputName()
    {
        //InputFieldからテキスト情報を取得する
        name = inputField.text;
        Debug.Log(name);
    }

    public void Decision()
    {
        if (!SelectScene && name != "")
        {
            audioSource.PlayOneShot(sound);
            StartCoroutine("SceneChange", audioSource);
            SelectScene = true;
        }

        if(!SelectScene && name == "")
        {
            audioSource.PlayOneShot(sound2);
        }
    }

    private IEnumerator SceneChange(AudioSource audio)
    {
        while (true)
        {
            yield return new WaitForFixedUpdate();
            if (!audio.isPlaying)
            {
                //SceneManager.LoadScene("MakeingScene");
                FadeManager.Instance.LoadScene("Opening2Scene", 0.5f);
                break;
            }
        }
    }
}
