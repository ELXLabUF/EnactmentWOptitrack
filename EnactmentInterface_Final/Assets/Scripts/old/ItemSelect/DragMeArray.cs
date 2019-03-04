using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DragMeArray : MonoBehaviour {

    public Sprite[] dragIcons;
    public Sprite[] dragFullImages;

	// Use this for initialization
	void Start () {
		for(int i=0; i<dragIcons.Length; i++)
        {
            var obj = new GameObject();
            obj.AddComponent<Image>();
            obj.GetComponent<Image>().sprite = dragIcons[i];
            obj.AddComponent<DragMe>();

        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
