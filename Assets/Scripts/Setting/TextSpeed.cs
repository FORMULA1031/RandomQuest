using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextSpeed : MonoBehaviour
{
    public Slider t_Slider;

    public static float speed = 0.0f;
    static float SpeedGauge = 0.5f;
    float OriginalSpeed = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        if (t_Slider != null)
            t_Slider.value = SpeedGauge;
    }

    // Update is called once per frame
    void Update()
    {
        if (t_Slider != null)
        {
            SpeedGauge = t_Slider.GetComponent<Slider>().normalizedValue;
            speed = 0.5f - SpeedGauge;
        }
    }

    public void SpeedReset()
    {
        t_Slider.value = OriginalSpeed;

    }
}
