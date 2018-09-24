using UnityEngine;
using System.Collections;

public class changeObjectImage : MonoBehaviour {

    public Sprite[] allSprites; 
	// Use this for initialization
	void Start ()
    {
	    
	}
	
	// Update is called once per frame
	void Update ()
    {
        this.GetComponent<SpriteRenderer>().sprite = allSprites[GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Scene0>().selGridObjectInt];
	}
}
