using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BGM_Control : MonoBehaviour
{
    AudioSource m_AudioSource;
    public AudioClip BGM;

    public Slider m_Slider;

    GameObject Stage;
    Stage_Control Stage_Control;
    int stagenumber;

    public static float Volume = 0.5f;
    float OriginalVolume = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
        m_AudioSource.volume = Volume;
        if(m_Slider != null)
            m_Slider.value = Volume;

        if (SceneManager.GetActiveScene().name == "BattleScene")
        {
            Stage = GameObject.Find("Stage");
            if (Stage != null)
            {
                Stage_Control = Stage.GetComponent<Stage_Control>();
                stagenumber = Stage_Control.StageNumber;
                if(stagenumber == 100)
                {
                    m_AudioSource.clip = BGM;
                    m_AudioSource.Play();
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (m_Slider != null)
        {
            m_AudioSource.volume = m_Slider.GetComponent<Slider>().normalizedValue;
            Volume = m_AudioSource.volume;
        }
        
    }

    public void BgmReset()
    {
        m_Slider.value = OriginalVolume;
    }
}
