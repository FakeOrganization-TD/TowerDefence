using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{
    public static string cashInfoMessage = "", cashErrorMessage= "",hpInfoMessage;
    Text cashInfoText,cashErrorText,hpInfoText;
    GameObject cashError,cashInfo,hpInfo;
    // Use this for initialization
    void Start()
    {
        // text = GetComponent<Text>();
        cashError = GameObject.FindGameObjectWithTag("CashErrorTag");
        cashErrorText = cashError.GetComponent<Text>(); // children ? 

        hpInfo = GameObject.FindGameObjectWithTag("HealthPointsTag");
        hpInfoText = hpInfo.GetComponent<Text>();

        cashInfo = GameObject.FindGameObjectWithTag("CashInfoTag");
        cashInfoText = cashInfo.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        cashErrorText.text = cashErrorMessage;
        cashInfoText.text = "Cash: "+ cashInfoMessage;
        hpInfoText.text = "Health Points: " + hpInfoMessage;
    }
}




