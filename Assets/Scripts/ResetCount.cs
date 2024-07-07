using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetCount : MonoBehaviour
{
    public GameObject Interstitial;
    int count = 0;

    public void Count()
    {
        count++;
        if(count >= 5)
        {
            Instantiate(Interstitial, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
            count = 0;
        }
    }
}
