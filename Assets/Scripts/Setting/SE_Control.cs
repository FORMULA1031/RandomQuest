using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SE_Control : MonoBehaviour
{
    AudioSource m_AudioSource;
    public AudioClip sound;
    AudioSource audioSource;
    public Slider m_Slider;

    public static float Volume = 0.8f;
    float OriginalVolume = 0.8f;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        m_AudioSource = GetComponent<AudioSource>();
        if(m_AudioSource != null)
            m_AudioSource.volume = Volume;
        if (m_Slider != null)
            m_Slider.value = Volume;
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

    public void SeReset()
    {
        audioSource.PlayOneShot(sound);
        m_Slider.value = OriginalVolume;

    }
}
