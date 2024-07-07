using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Technique2_Black : MonoBehaviour
{
    GameObject Player;
    Status Status;
    public Text Text;
    string Technique;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        Status = Player.GetComponent<Status>();
    }

    // Update is called once per frame
    void Update()
    {
        Technique = Status.Technique2;
        Text.text = Technique;
    }
}
