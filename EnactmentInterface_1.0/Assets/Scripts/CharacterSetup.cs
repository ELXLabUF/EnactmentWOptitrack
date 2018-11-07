using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character
{
    public Sprite[] Sprites;
}

public enum Direction
{
    Front = 0,
    Right,
    Back,
    Left
}
public enum BodyParts
{
    Torso = 0,
    Head,
    LeftLeg,
    RightLeg,
    LeftUpperArm,
    RightUpperArm,
    LeftLowerArm,
    RightLowerArm,
    TorsoRight,
    TorsoBack,
    TorsoLeft,
    HeadRight,
    HeadBack,
    HeadLeft
}

public class CharacterSetup : MonoBehaviour {

    public Character[] allSpriteSets;
    public int characterSelected;
    //	private GameObject[] bodyPartGameObject; 
    public bool testingOn;
    //public
    private Direction currentTorsoDirection;
    private GameObject[] bodyPartGameObject = new GameObject[14];


    // Use this for initialization
    void Start()
    {
        if (!testingOn)
            characterSelected = PlayerPrefs.GetInt("characterGrid");

        //Find the GameObjects in the character 
        bodyPartGameObject[(int)BodyParts.Torso] = GameObject.FindGameObjectWithTag("Torso");
        bodyPartGameObject[(int)BodyParts.Head] = GameObject.FindGameObjectWithTag("Head");

        bodyPartGameObject[(int)BodyParts.LeftLeg] = GameObject.FindGameObjectWithTag("LeftLeg");
        bodyPartGameObject[(int)BodyParts.RightLeg] = GameObject.FindGameObjectWithTag("RightLeg");

        bodyPartGameObject[(int)BodyParts.LeftUpperArm] = GameObject.FindGameObjectWithTag("LeftUpperArm");
        bodyPartGameObject[(int)BodyParts.RightUpperArm] = GameObject.FindGameObjectWithTag("RightUpperArm");
        bodyPartGameObject[(int)BodyParts.LeftLowerArm] = GameObject.FindGameObjectWithTag("LeftLowerArm");
        bodyPartGameObject[(int)BodyParts.RightLowerArm] = GameObject.FindGameObjectWithTag("RightLowerArm");


        //Assign New Sprites in the GameObject's SpriteRenderer Component 
        bodyPartGameObject[(int)BodyParts.Head].GetComponent<SpriteRenderer>().sprite = allSpriteSets[characterSelected].Sprites[(int)BodyParts.Head];
        bodyPartGameObject[(int)BodyParts.Torso].GetComponent<SpriteRenderer>().sprite = allSpriteSets[characterSelected].Sprites[(int)BodyParts.Torso];
        bodyPartGameObject[(int)BodyParts.LeftLeg].GetComponent<SpriteRenderer>().sprite = allSpriteSets[characterSelected].Sprites[(int)BodyParts.LeftLeg];
        bodyPartGameObject[(int)BodyParts.RightLeg].GetComponent<SpriteRenderer>().sprite = allSpriteSets[characterSelected].Sprites[(int)BodyParts.RightLeg];

        bodyPartGameObject[(int)BodyParts.LeftUpperArm].GetComponent<SpriteRenderer>().sprite = allSpriteSets[characterSelected].Sprites[(int)BodyParts.LeftUpperArm];
        bodyPartGameObject[(int)BodyParts.RightUpperArm].GetComponent<SpriteRenderer>().sprite = allSpriteSets[characterSelected].Sprites[(int)BodyParts.RightUpperArm];
        bodyPartGameObject[(int)BodyParts.LeftLowerArm].GetComponent<SpriteRenderer>().sprite = allSpriteSets[characterSelected].Sprites[(int)BodyParts.LeftLowerArm];
        bodyPartGameObject[(int)BodyParts.RightLowerArm].GetComponent<SpriteRenderer>().sprite = allSpriteSets[characterSelected].Sprites[(int)BodyParts.RightLowerArm];

    }

    // Update is called once per frame
    void Update()
    {
        currentTorsoDirection = getTorsoDirection();
        //characterLeftLegInst.transform.rotation = Quaternion.Slerp(characterLeftLeg.startQuat, characterLeftLeg.targetQuat, Time.deltaTime);

        switch (currentTorsoDirection)
        {
            case Direction.Front:
                bodyPartGameObject[(int)BodyParts.Head].GetComponent<SpriteRenderer>().sprite = allSpriteSets[characterSelected].Sprites[(int)BodyParts.Head];
                bodyPartGameObject[(int)BodyParts.Torso].GetComponent<SpriteRenderer>().sprite = allSpriteSets[characterSelected].Sprites[(int)BodyParts.Torso];
                break;
            case Direction.Right:
                //Switching Sprites
                bodyPartGameObject[(int)BodyParts.Head].GetComponent<SpriteRenderer>().sprite = allSpriteSets[characterSelected].Sprites[(int)BodyParts.HeadRight];
                bodyPartGameObject[(int)BodyParts.Torso].GetComponent<SpriteRenderer>().sprite = allSpriteSets[characterSelected].Sprites[(int)BodyParts.TorsoRight];

                //Layer Ordering 
                bodyPartGameObject[(int)BodyParts.LeftLeg].GetComponent<SpriteRenderer>().sortingOrder = -1;
                bodyPartGameObject[(int)BodyParts.RightLeg].GetComponent<SpriteRenderer>().sortingOrder = 1;

                bodyPartGameObject[(int)BodyParts.LeftUpperArm].GetComponent<SpriteRenderer>().sortingOrder = -1;
                bodyPartGameObject[(int)BodyParts.RightUpperArm].GetComponent<SpriteRenderer>().sortingOrder = 1;
                bodyPartGameObject[(int)BodyParts.LeftLowerArm].GetComponent<SpriteRenderer>().sortingOrder = -1;
                bodyPartGameObject[(int)BodyParts.RightLowerArm].GetComponent<SpriteRenderer>().sortingOrder = 1;
                break;
            case Direction.Back:
                bodyPartGameObject[(int)BodyParts.Head].GetComponent<SpriteRenderer>().sprite = allSpriteSets[characterSelected].Sprites[(int)BodyParts.HeadBack];
                bodyPartGameObject[(int)BodyParts.Torso].GetComponent<SpriteRenderer>().sprite = allSpriteSets[characterSelected].Sprites[(int)BodyParts.TorsoBack];

                break;
            case Direction.Left:
                bodyPartGameObject[(int)BodyParts.Head].GetComponent<SpriteRenderer>().sprite = allSpriteSets[characterSelected].Sprites[(int)BodyParts.HeadLeft];
                bodyPartGameObject[(int)BodyParts.Torso].GetComponent<SpriteRenderer>().sprite = allSpriteSets[characterSelected].Sprites[(int)BodyParts.TorsoLeft];

                //Layer Ordering 
                bodyPartGameObject[(int)BodyParts.LeftLeg].GetComponent<SpriteRenderer>().sortingOrder = 1;
                bodyPartGameObject[(int)BodyParts.RightLeg].GetComponent<SpriteRenderer>().sortingOrder = -1;

                bodyPartGameObject[(int)BodyParts.LeftUpperArm].GetComponent<SpriteRenderer>().sortingOrder = 1;
                bodyPartGameObject[(int)BodyParts.RightUpperArm].GetComponent<SpriteRenderer>().sortingOrder = -1;
                bodyPartGameObject[(int)BodyParts.LeftLowerArm].GetComponent<SpriteRenderer>().sortingOrder = 1;
                bodyPartGameObject[(int)BodyParts.RightLowerArm].GetComponent<SpriteRenderer>().sortingOrder = -1;
                break;
        }
    }
    /// <summary>
    /// Checks the direction of Torso And Returns Front/Right/Back/Left	/// </summary>
    /// <returns>The torso direction.</returns>
    Direction getTorsoDirection()
    {
        if (bodyPartGameObject[(int)BodyParts.Torso].transform.localEulerAngles.y >= 225 && bodyPartGameObject[(int)BodyParts.Torso].transform.localEulerAngles.y < 315)
        {
            return Direction.Right;
        }
        else if (bodyPartGameObject[(int)BodyParts.Torso].transform.localEulerAngles.y >= 135 && bodyPartGameObject[(int)BodyParts.Torso].transform.localEulerAngles.y < 225)
        {
            return Direction.Back;
        }
        else if (bodyPartGameObject[(int)BodyParts.Torso].transform.localEulerAngles.y >= 45 && bodyPartGameObject[(int)BodyParts.Torso].transform.localEulerAngles.y < 135)
        {
            return Direction.Left;
        }
        else
        {
            return Direction.Front;
        }
        /*
		else if( bodyPartGameObject [(int)BodyParts.Torso].transform.localEulerAngles.y >= 0 &&  bodyPartGameObject [(int)BodyParts.Torso].transform.localEulerAngles.y <45)
			return Direction.Front; */

    }

    //   // use this for initialization
    //   void start () {

    //}

    //// update is called once per frame
    //void update () {

    //}
}
