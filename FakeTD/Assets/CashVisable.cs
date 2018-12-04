using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CashVisable : MonoBehaviour {


    // Update is called once per frame
    void Update () {
        if (MoneyAndScores.money < 15)
        {
            var colors = GetComponent<Button>().colors;
            colors.normalColor = Color.red;
            GetComponent<Button>().colors = colors;
        }
        else
        {
            var colors = GetComponent<Button>().colors;
            colors.normalColor = new Color32(83, 166, 255, 255);
            GetComponent<Button>().colors = colors;
        }
    }


 
}
