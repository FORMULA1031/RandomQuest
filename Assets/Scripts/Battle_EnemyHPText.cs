using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Battle_EnemyHPText : MonoBehaviour
{
    GameObject Enemy;
    EnemyStatus EnemyStatus;
    public Text Text;
    int HP;
    int CurrenHP;

    // Start is called before the first frame update
    void Start()
    {
        Enemy = GameObject.Find("Enemy");
        EnemyStatus = Enemy.GetComponent<EnemyStatus>();
    }

    // Update is called once per frame
    void Update()
    {
        HP = EnemyStatus.HP;
        CurrenHP = EnemyStatus.CurrentHp;
        Text.text = CurrenHP + "/" + HP;
    }
}
