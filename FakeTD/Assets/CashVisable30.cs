using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CashVisable30 : MonoBehaviour {

    // Use this for initialization
    void Update()
    {
        if (MoneyAndScores.money < 30)
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
