using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackGround_Control : MonoBehaviour
{
    public Image image;

    public Sprite Last;

    GameObject Stage;
    Stage_Control Stage_Control;
    int stagenumber;

    // Start is called before the first frame update
    void Start()
    {
        image = this.GetComponent<Image>();

        Stage = GameObject.Find("Stage");
        Stage_Control = Stage.GetComponent<Stage_Control>();
        stagenumber = Stage_Control.StageNumber;
        if (stagenumber == 100)
            image.sprite = Last;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
