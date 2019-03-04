using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControl : MonoBehaviour {

    public void toEnactmentScene() {
        SceneManager.LoadScene(1);
    }

    public void toTimelineScene()
    {
        SceneManager.LoadScene(0);
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
