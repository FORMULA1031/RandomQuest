using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SP_Text : MonoBehaviour
{
    GameObject Player;
    Status Status;
    public Text Text;
    int SP;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        Status = Player.GetComponent<Status>();
    }

    // Update is called once per frame
    void Update()
    {
        SP = Status.SP;
        Text.text = "" + SP;
    }
}
