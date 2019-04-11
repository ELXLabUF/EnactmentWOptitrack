using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlideSelectSlideNoChara : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{

    //slide's id for ordering purposes
    private int listID = 0;

    //prefabs for holding the sprites for each selected character,item, and backdrop
    //public GameObject chara;
    public GameObject item;
    public GameObject backdrop;

    //prefab for the number that sits beneath each slide
    public GameObject numberText;

    //border prefab object and related sprites
    public GameObject border;
    public Sprite addSlideRight;
    public Sprite selectBorder;

    public GameObject slideRecordIcon;
    //public GameObject slideWriteIcon;
    public GameObject slideLockIcon;
    public GameObject slideTitle;

    private Sprite borderSprite;
    private string borderName;

    //for spacing the slides, widths of the prefabs are already set to this value 
    private float slideWidth = 100;

    /*for keeping track of slides position of heirarchy - 
        When the slide is dragged out of the array, it is brought out of its parent so it doesn't get covered by other elements while being dragged*/
    private Vector3 oldPlace;
    private int oldIndex;
    private Transform oldParent;

    /*booleans for slide states*/
    private bool selected = false; //whether slide is selected or not
    private bool movingHover = false; //being hovered over while another slide from same array is being dragged
    private bool moving = false; //being dragged

    private bool draggingEnabled = true;

    // Use this for initialization
    void Start()
    {

        // create the holders for each object type
        //GameObject newChara = (GameObject)Instantiate(chara, GetComponentInParent<Canvas>().transform);
        GameObject newItem = (GameObject)Instantiate(item, GetComponentInParent<Canvas>().transform);
        GameObject newBackdrop = (GameObject)Instantiate(backdrop, GetComponentInParent<Canvas>().transform);

        //the order by which the transforms are set is important, so that the backdrop stays in the back
        newBackdrop.transform.SetParent(this.transform);
        newBackdrop.transform.localPosition = new Vector3(0, 0, 0);

        //newChara.transform.SetParent(this.transform);
        //newChara.transform.localPosition = new Vector3(40, 0, 0);

        newItem.transform.SetParent(this.transform);
        newItem.transform.localPosition = new Vector3(-40, 0, 0);


        //the border and number goes on top, and are instantiated/set last
        GameObject newBorder = (GameObject)Instantiate(border, GetComponentInParent<Canvas>().transform);
        newBorder.transform.SetParent(this.transform);
        newBorder.transform.localPosition = new Vector3(0, 0, 0);

        borderSprite = newBorder.GetComponent<Image>().sprite;

        GameObject newRecordIcon = (GameObject)Instantiate(slideRecordIcon, GetComponentInParent<Canvas>().transform);
        newRecordIcon.transform.SetParent(this.transform);
        newRecordIcon.transform.localPosition = new Vector3(-25, -75, 0);
        newRecordIcon.GetComponent<Image>().color = new Color(1, 1, 1, 0);

        GameObject newNumber = (GameObject)Instantiate(numberText, GetComponentInParent<Canvas>().transform);
        newNumber.transform.SetParent(this.transform);
        newNumber.transform.localPosition = new Vector3(0, 60, 0);

        //GameObject newWriteIcon = (GameObject)Instantiate(slideWriteIcon, GetComponentInParent<Canvas>().transform);
        //newWriteIcon.transform.SetParent(this.transform);
        //newWriteIcon.transform.localPosition = new Vector3(25, -75, 0);
        //newWriteIcon.GetComponent<Image>().color = new Color(1, 1, 1, 0);

        GameObject newSlideTitle = (GameObject)Instantiate(slideTitle, GetComponentInParent<Canvas>().transform);
        newSlideTitle.transform.SetParent(this.transform);
        newSlideTitle.transform.localPosition = new Vector3(0, -70, 0);
        newSlideTitle.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        newSlideTitle.tag = "scene_title";


        GameObject newLockIcon = (GameObject)Instantiate(slideLockIcon, GetComponentInParent<Canvas>().transform);
        newLockIcon.transform.SetParent(this.transform);
        newLockIcon.transform.localPosition = new Vector3(75, 50, 0);
        newLockIcon.GetComponent<Image>().color = new Color(1, 1, 1, 0);



        //the border prefab has no script, and is best found by its name, which varies by the tag
        switch (this.tag)
        {
            case "green":
                borderName = "BegBorder(Clone)";
                break;
            case "yellow":
                borderName = "MidBorder(Clone)";
                break;
            case "red":
                borderName = "EndBorder(Clone)";
                break;
            default:
                Debug.Log("The slide prefabs either have the incorrect tags, or don't exist.");
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<SlideDataNoChara>().getSceneInfo() != this.transform.Find("slideTitle(Clone)").GetComponent<InputField>().text || GetComponent<SlideDataNoChara>().getSceneInfo() == null)
        {
            GetComponent<SlideDataNoChara>().setSceneInfo(this.transform.Find("slideTitle(Clone)").GetComponent<InputField>().text);
        }

        //   if (GetComponent<SlideDataNoChara>().getSceneInfo() == "")
        //{
        //this.transform.Find("SlideWritten(Clone)").GetComponent<Image>().color = new Color(1, 1, 1, 0);
        //}
        //else
        //{
        //this.transform.Find("SlideWritten(Clone)").GetComponent<Image>().color = new Color(1, 1, 1, 1);
        //}

        //if (GetComponent<SlideDataNoChara>().getLock() == false)
        //{
        // this.transform.Find("SlideLocked(Clone)").GetComponent<Image>().color = new Color(1, 1, 1, 0);
        //}
        //else
        //{
        //this.transform.Find("SlideLocked(Clone)").GetComponent<Image>().color = new Color(1, 1, 1, 1);
        //}

    }

    //assign new slide id - see SlideArrayNoCharaNoChara
    public void assignID(int id)
    {
        listID = id;
    }


    //function for decreasing slide id when a slide ordered before it is deleted - see SlideArrayNoChara
    public void moveLeft()
    {
        listID--;
    }

    //sets slide index in heirarchy
    public void setSiblingIndex(int ind)
    {

        this.transform.SetSiblingIndex(ind);
    }


    //updates slide position in array based on slide ID
    public void newPosition(float buffer)
    {
        var newX = 100 + slideWidth * .5f + buffer * .5f + buffer * listID + slideWidth * listID;
        transform.localPosition = new Vector3(newX, transform.localPosition.y, 0);

    }

    //deletes this slide
    public void deleteMe()
    {

        this.GetComponentInParent<SlideArrayNoChara>().renumberSlidesDelete(listID); //called to reorder slides affected by this slide being deleted

        //destroy all of this slide's children - object holders, border, etc
        foreach (Transform child in this.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        //then destroy this slide
        Destroy(transform.gameObject);


    }


    //select this slide
    public void selectMe()
    {

        selected = true;

        //change to select border
        this.transform.Find(borderName).GetComponent<Image>().sprite = selectBorder;
        this.transform.Find(borderName).GetComponent<RectTransform>().sizeDelta = new Vector2(117.419f, 75.161f);

        int whichSection = 0;

        //determine which array this slide belongs to for renumbering
        switch (this.tag)
        {
            case "green":
                whichSection = 0;
                break;
            case "yellow":
                whichSection = 1;
                break;
            case "red":
                whichSection = 2;
                break;
            default:
                Debug.Log("The slide prefabs have the incorrect tags.");
                break;
        }


        //make sure all other slides are deselected in other slide arrays
        if (GetComponentInParent<SlideArrayNoChara>().GetComponentInParent<SlideNumberingNoChara>() != null)
        {
            GetComponentInParent<SlideArrayNoChara>().GetComponentInParent<SlideNumberingNoChara>().selectNew(whichSection, listID);
        }
        else
        {
            Debug.Log("The heirarchy may have been changed.");
        }
    }

    //deselects slide
    public void deselectMe()
    {

        selected = false;

        //change back to regular border
        this.transform.Find(borderName).GetComponent<Image>().sprite = borderSprite;
        this.transform.Find(borderName).GetComponent<RectTransform>().sizeDelta = new Vector2(100.00f, 56.25f);

    }


    //returns list id
    public int getListID()
    {
        return listID;
    }

    //returns selected state
    public bool getSelected()
    {
        return selected;
    }

    //selects this slide if its not being dragged
    public void OnPointerClick(PointerEventData eventData) // 3
    {
        if (moving == false)
        {
            selectMe();
        }

    }

    //begin dragging slide
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (draggingEnabled)
        {
            this.GetComponentInParent<SlideArrayNoChara>().setSlideMoving(true, listID);
            moving = true;

            //record current position in heirarchy so it can be returned to it later
            oldPlace = this.transform.position;
            oldIndex = transform.GetSiblingIndex();
            oldParent = this.GetComponentInParent<SlideArrayNoChara>().transform;

            //take slide out of heirarchy so it doesn't get covered by other objects while being dragged
            transform.SetParent(this.GetComponentInParent<SlideArrayNoChara>().GetComponentInParent<Canvas>().transform);

            //takes away slide number while its being dragged to avoid confusion
            this.GetComponentInChildren<Text>().text = "";

            //offsets slide from mouse so the slide it hovers over can detect when the mouse enters it
            transform.position = new Vector3(eventData.position.x, eventData.position.y - 65, transform.position.z);
        }
    }

    public void draggingOff()
    {
        draggingEnabled = false;
    }

    public void draggingOn()
    {
        draggingEnabled = true;
    }

    //update slide position
    public void OnDrag(PointerEventData eventData)
    {
        if (draggingEnabled)
        {
            this.transform.Translate(eventData.delta);
        }
    }


    //once mouse is released after dragging slide
    public void OnEndDrag(PointerEventData eventData)
    {
        if (draggingEnabled)
        {
            //put slide back in its place in the heirarchy
            this.transform.position = oldPlace;
            transform.SetParent(oldParent);
            transform.SetSiblingIndex(oldIndex);

            //if a slide was hovered over in the same array, call the slide swapping function
            if (GetComponentInParent<SlideArrayNoChara>().getSlideMoving() == true && GetComponentInParent<SlideArrayNoChara>().getSlideMovingHover() == true && movingHover == false)
            {
                this.GetComponentInParent<SlideArrayNoChara>().renumberSlidesSwap();
            }

            //reset booleans for slide moving and slide being hovered over for this slide's array
            this.GetComponentInParent<SlideArrayNoChara>().setSlideMoving(false, listID);
            this.GetComponentInParent<SlideArrayNoChara>().setSlideMovingHover(false, listID);

            moving = false;
        }
    }


    //once mouse enters slide
    public void OnPointerEnter(PointerEventData data)
    {
        // if a slide is being dragged, and it isn't this slide, set this as the slide being hovered over for swapping purposes
        if (GetComponentInParent<SlideArrayNoChara>().getSlideMoving() == true && moving == false)
        {
            this.GetComponentInParent<SlideArrayNoChara>().setSlideMovingHover(true, listID);
            this.movingHover = true;

            //change slide border to an indicator of where the new slide will be put upon release
            this.transform.Find(borderName).GetComponent<Image>().sprite = addSlideRight;

        }


        /*Saving these two lines of code as they can be useful for eventually figuring out a better way to drag and reorder slides within an array*/
        //Vector3 localPosition = transform.InverseTransformPoint(data.position);
        //Debug.Log(localPosition);
    }

    //once mouse exits slide
    public void OnPointerExit(PointerEventData data)
    {
        //if this slide is set as the hover slide for dragging purposes, change that value back to false and reupdate the slide border to its prior state
        if (movingHover == true)
        {
            if (selected) { this.transform.Find(borderName).GetComponent<Image>().sprite = selectBorder; }
            else { this.transform.Find(borderName).GetComponent<Image>().sprite = borderSprite; }
            this.movingHover = false;
        }


    }

    public void showRecordedStatus()
    {
        this.transform.Find("SlideRecorded(Clone)").GetComponent<Image>().color = new Color(1, 1, 1, 1);
    }
}