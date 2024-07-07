using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpeningBGM : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        DontDestroyOnLoad(gameObject);

        if (SceneManager.GetActiveScene().name == "MakeingScene")
        {
            Destroy(gameObject);
        }
    }
}
