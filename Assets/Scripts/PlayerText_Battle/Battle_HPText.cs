using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Battle_HPText : MonoBehaviour
{
    GameObject Player;
    Status Status;
    public Text Text;
    int HP;
    int CurrentHp;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        Status = Player.GetComponent<Status>();
    }

    // Update is called once per frame
    void Update()
    {
        HP = Status.HP;
        CurrentHp = Status.CurrentHp;
        Text.text = CurrentHp + "/" + HP;
    }
}
