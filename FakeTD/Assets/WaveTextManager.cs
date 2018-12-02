using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveTextManager : MonoBehaviour {
    public static string Message = "";
    Text text;
    GameObject t;
	// Use this for initialization
	void Start () {
       // text = GetComponent<Text>();
        t = GameObject.FindGameObjectWithTag("WaveTextTag");
        text=t.GetComponent<Text>(); // children ? 
	}
	
	// Update is called once per frame
	void Update () {
        text.text = Message;
        
	}
}
