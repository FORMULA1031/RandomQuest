using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Battle_MPText : MonoBehaviour
{
    GameObject Player;
    Status Status;
    public Text Text;
    int MP;
    int CurrentMp;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        Status = Player.GetComponent<Status>();
    }

    // Update is called once per frame
    void Update()
    {
        MP = Status.MP;
        CurrentMp = Status.CurrentMp;
        Text.text = CurrentMp + "/" + MP;
    }
}
