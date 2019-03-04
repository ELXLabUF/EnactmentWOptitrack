using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {


    //This script currently just stores the item GameObject's custom scale values for placement in the scene
    public float scale;
    

    // Use this for initialization
    void Start()
    {
        
    }


    public float getScale()
    {

        return scale;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
