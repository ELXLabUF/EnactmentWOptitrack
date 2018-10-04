using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnactmentSceneSetup : MonoBehaviour {

    private int objectIndex;
    private int characterIndex;
    private int backdropIndex;
    private GameObject current_object;
    private GameObject current_character;
    private GameObject current_backdrop;

    void setObject(int ind)
    {
        switch (ind)
        {
            case 0:
                current_object = Instantiate(Resources.Load("Optitrack-interface/Objects/Apple")) as GameObject;
                break;
            case 1:
                current_object = Instantiate(Resources.Load("Optitrack-interface/Objects/Crate")) as GameObject;
                break;
        }
    }


    void setCharacter(int ind)
    {
        switch (ind)
        {
            case 0:
                current_character = Instantiate(Resources.Load("Optitrack-interface/Characters/Character1_Rigged")) as GameObject;
                break;
            case 1:
                current_character = Instantiate(Resources.Load("Optitrack-interface/Characters/Character2_Rigged")) as GameObject;
                break;
        }
    }


    void setBackdrop(int ind)
    {
        switch (ind)
        {
            case 0:
                current_backdrop = Instantiate(Resources.Load("Optitrack-interface/Backdrops/Background1")) as GameObject;
                break;
            case 1:
                current_backdrop = Instantiate(Resources.Load("Optitrack-interface/Backdrops/Background2")) as GameObject;
                break;
        }
    }

    // Use this for initialization
    void Start () {
        objectIndex = PlayerPrefs.GetInt("objectID");
        characterIndex = PlayerPrefs.GetInt("characterID");
        backdropIndex = PlayerPrefs.GetInt("backdropID");
        Debug.Log(objectIndex);
        Debug.Log(characterIndex);
        Debug.Log(backdropIndex);

        setObject(objectIndex);
        setCharacter(characterIndex);
        setBackdrop(backdropIndex);
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
