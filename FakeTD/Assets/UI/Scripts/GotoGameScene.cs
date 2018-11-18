using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GotoGameScene : MonoBehaviour {

	// Use this for initialization
	void Start () {


    }
  public  void GameScene()
    {
            SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
        
    }
	// Update is called once per frame
	void Update () {
		
	}
}
