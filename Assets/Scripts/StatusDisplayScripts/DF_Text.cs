using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DF_Text : MonoBehaviour
{
    GameObject Player;
    Status Status;
    public Text Text;
    int DF;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        Status = Player.GetComponent<Status>();
    }

    // Update is called once per frame
    void Update()
    {
        DF = Status.DF;
        Text.text = "" + DF;
    }
}
