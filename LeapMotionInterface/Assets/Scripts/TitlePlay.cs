using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitlePlay : MonoBehaviour {

    public float countdown;

	// Set countdown to 5 seconds for the title screen
	void Start () {
        countdown = 5.0f; 
	}
	
	// Load the next scene once countdown is up
	void Update () {
        countdown -= Time.deltaTime; 
        if (countdown <= 0) { SceneManager.LoadScene(1); } 
    }
}
