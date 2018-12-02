using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{
    public static string cashInfoMessage = "", cashErrorMessage= "";
    Text cashInfoText,cashErrorText;
    GameObject cashError,cashInfo;
    // Use this for initialization
    void Start()
    {
        // text = GetComponent<Text>();
        cashError = GameObject.FindGameObjectWithTag("CashErrorTag");
        cashErrorText = cashError.GetComponent<Text>(); // children ? 

        cashInfo = GameObject.FindGameObjectWithTag("CashInfoTag");
        cashInfoText = cashInfo.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        cashErrorText.text = cashErrorMessage;
        cashInfoText.text = "Cash: "+ cashInfoMessage;

    }
}




