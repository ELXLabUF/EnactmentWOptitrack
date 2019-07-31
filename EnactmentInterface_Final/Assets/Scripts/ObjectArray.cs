using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ObjectArray : MonoBehaviour
{
    //the prefabs for the white circle behind the object and the object sprite itself
    public GameObject objectHolder;
    public GameObject dragObject;

    //main icon for each object
    public Sprite charaSprite;
    public Sprite itemSprite;
    public Sprite backdropSprite;

    //arrays for the sprites to represent the objects and their corresponding objects
    public GameObject[] CharaPoseSets;
    //public Sprite[] charaSprites;

    //public Sprite[] itemSprites;
    public GameObject[] Items;

    public GameObject[] Backdrops;
    //public Sprite[] backdropSprites;

    private int state = 1; //1-charas, 2-items, 3-backdropSprites

    // Use this for initialization
    void Start()
    {
        //set initial set of objects based upon state
        switch (state)
        {
            case 1:
                if (charaSprite == null) { Debug.Log("charaSprite is null - see ObjectArray script - SB"); }
                else { displaySet(CharaPoseSets, "chara", charaSprite); }
                break;
            case 2:
                if (itemSprite == null) { Debug.Log("itemSprite is null - see ObjectArray script - SB"); }
                else { displaySet(Items, "item", itemSprite); }
                break;
            case 3:
                if (backdropSprite == null) { Debug.Log("backdropSprite is null - see ObjectArray script - SB"); }
                else { displaySet(Backdrops, "backdrop", backdropSprite); }
                break;
            default:
                Debug.Log("private int state has been initialized to an out-of-bounds value - see ObjectArray script - SB");
                break;
        }

    }

    //move to next set
    public void nextSet()
    {
        destroyCurrentSet();
        switch (state)
        {
            case 1:
                state = 2;
                displaySet(Items, "item", itemSprite);
                break;
            case 2:
                state = 3;
                displaySet(Backdrops, "backdrop", backdropSprite);
                break;
            case 3:
                state = 1;
                displaySet(CharaPoseSets, "chara", charaSprite);
                break;
            default:
                Debug.Log("private int state is an out-of-bounds value - see ObjectArray script - SB");
                break;
        }
    }

    //move to previous set
    public void lastSet()
    {
        destroyCurrentSet();
        switch (state)
        {
            case 1:
                state = 3;
                displaySet(Backdrops, "backdrop", backdropSprite);
                break;
            case 2:
                state = 1;
                displaySet(CharaPoseSets, "chara", charaSprite);
                break;
            case 3:
                state = 2;
                displaySet(Items, "item", itemSprite);
                break;
            default:
                Debug.Log("private int state is an out-of-bounds value - see ObjectArray script - SB");
                break;
        }


    }

    //create selection of objects based on type
    void displaySet(GameObject[] objectArray, string objTag, Sprite sprite)
    {
        //set new main icon
        this.GetComponent<Image>().sprite = sprite;

        GameObject newObj;
        int newX;
        int newY;

        for (int i = 0; i < objectArray.Length; i++)
        {

            /*Place the object holder*/
            newObj = (GameObject)Instantiate(objectHolder, transform.position, transform.rotation);

            newObj.transform.SetParent(this.transform);

            //newX = 60 * (i % 3) - 60;
            //newY = -(i / 3) * 60 - 100;

            newX = 50 * (i % 3) - 50;
            newY = -(i / 3) * 50 - 90;
            newObj.transform.localPosition = new Vector3(newX, newY, 0);

            /*Place the object that will be dragged on top of the holder*/
            newObj = (GameObject)Instantiate(dragObject, transform.position, transform.rotation);

            newObj.GetComponent<DragMe>().assignID(i);
            newObj.GetComponent<Image>().sprite = objectArray[i].GetComponent<Icon>().iconSprite;
            newObj.GetComponent<ItemPreview>().PhysPreview = objectArray[i].GetComponent<Icon>().iconLarge;
            newObj.tag = objTag;
            newObj.transform.SetParent(this.transform);

            //newX = 60 * (i % 3) - 60;
            //newY = -(i / 3) * 60 - 100;

            newX = 50 * (i % 3) - 50;
            newY = -(i / 3) * 50 - 90;

            newObj.transform.localPosition = new Vector3(newX, newY, 0);

        }

    }

    //Destroys the current set to make way for the new one
    void destroyCurrentSet()
    {

        foreach (Transform child in this.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

    }
}
