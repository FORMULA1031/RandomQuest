using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Battle_EnemyNameText : MonoBehaviour
{
    GameObject Enemy;
    EnemyStatus EnemyStatus;
    public Text Text;
    string EnemyName;

    // Start is called before the first frame update
    void Start()
    {
        Enemy = GameObject.Find("Enemy");
        EnemyStatus = Enemy.GetComponent<EnemyStatus>();
        EnemyName = EnemyStatus.EnemyName;
        Text.text = EnemyName;
    }

    // Update is called once per frame
    void Update()
    {
        EnemyName = EnemyStatus.EnemyName;
        Text.text = EnemyName;
    }
}
