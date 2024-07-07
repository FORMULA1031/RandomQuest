using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    float time;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(time >= 1.0f)
        {
            FadeManager.Instance.LoadScene("StartMenuScene", 0.5f);
            time = 0.0f;
        }
    }
}
