using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


/*Script to be attached to each group of slides: Beginning, Middle, End*/
public class SlideArray : MonoBehaviour
{

    public GameObject slideBase;
    private int currentListID = 0;
    public string thisTag; //tag to assign each slide
    private float buffer = 10.0f; // space between slides
    private bool slideMoving = false; //true if this is the slide being dragged to swap positions
    private bool slideMovingHover = false; //true if this is the slide is the one being hovered over by the slide being dragged
    private int movingSlide; // id of slide being dragged
    private int movingHoverSlide; // id of slide being hovered over by the slide being dragged

    // Use this for initialization
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {

    }

    public int getCurrent()
    {
        return currentListID;
    }

    public void addSlideAlone()
    {
        if (slideBase != null && currentListID < 5)
        {

            //create new slide in scene
            GameObject newSlide = (GameObject)Instantiate(slideBase, transform.position, transform.rotation);


            newSlide.GetComponent<SlideSelectSlide>().assignID(currentListID); //assign it's id
            newSlide.tag = thisTag; //give it the tag relevant to this array
            newSlide.transform.SetParent(this.transform); //set parent to this array

            newSlide.GetComponent<SlideSelectSlide>().newPosition(buffer); //set slides position
            currentListID++; //add to this array's slide count

        }

        GameObject.FindGameObjectWithTag("all_canvases").GetComponent<CanvasManagerBottomUp>().addSlideHide();
    }

    public void addSlide()
    {
        
        if (GameObject.Find("SlideSections").GetComponent<SlideNumbering>().getCondition() == 2 || GameObject.Find("SlideSections").GetComponent<SlideNumbering>().getTotal() == 0)
        {
            if (slideBase != null && currentListID < 5)
            {

                //create new slide in scene
                GameObject newSlide = (GameObject)Instantiate(slideBase, transform.position, transform.rotation);


                newSlide.GetComponent<SlideSelectSlide>().assignID(currentListID); //assign it's id
                newSlide.tag = thisTag; //give it the tag relevant to this array
                newSlide.transform.SetParent(this.transform); //set parent to this array

                newSlide.GetComponent<SlideSelectSlide>().newPosition(buffer); //set slides position
                currentListID++; //add to this array's slide count

            }
        }
        else if(GameObject.Find("SlideSections").GetComponent<SlideNumbering>().getAddReady() == true)
        {
            

            switch (thisTag)
            {
                case "green":
                    GameObject.Find("SlideSections").GetComponent<SlideNumbering>().setAddWhich(0);
                    break;
                case "yellow":
                    GameObject.Find("SlideSections").GetComponent<SlideNumbering>().setAddWhich(1);
                    break;
                case "red":
                    GameObject.Find("SlideSections").GetComponent<SlideNumbering>().setAddWhich(2);
                    break;
                default:
                    break;

            }
            GameObject.FindGameObjectWithTag("all_canvases").GetComponent<CanvasManagerBottomUp>().addSlidePop();
        }
        else
        {
            GameObject.FindGameObjectWithTag("all_canvases").GetComponent<CanvasManagerBottomUp>().addingIncompletePop();
        }

    }


    /*Reposition all slides in front of deleted slide*/
    public void renumberSlidesDelete(int deletedID)
    {
        SlideSelectSlide[] slides = GetComponentsInChildren<SlideSelectSlide>(); //get all slides

        for (int i = deletedID + 1; i < currentListID; i++)
        {
            slides[i].GetComponent<SlideSelectSlide>().moveLeft(); //subtracts one from slide's id
            slides[i].GetComponent<SlideSelectSlide>().newPosition(buffer); //reset the slide's position   
        }
        currentListID--;
    }



    /*Get and set methods for:
        The slide being dragged: 
            State: slideMoving
            Slide: movingSlide
        The slide being hovered over by slide being dragged:
            State: slideMovingHover
            Slide: movingHoverSlide
    */
    public void setSlideMoving(bool ismoving, int id)
    {
        slideMoving = ismoving;
        movingSlide = id;
    }

    public void setSlideMovingHover(bool ishover, int id)
    {
        slideMovingHover = ishover;
        movingHoverSlide = id;
    }

    public bool getSlideMoving()
    {
        return slideMoving;
    }

    public bool getSlideMovingHover()
    {
        return slideMovingHover;
    }

    public int getWhichSlideMoving()
    {
        return movingSlide;

    }




    /*Function for swapping slides. Currently slides can only be moved to the right of the slide it is dropped ontop of within its own array*/
    public void renumberSlidesSwap()
    {
        SlideSelectSlide[] slides = GetComponentsInChildren<SlideSelectSlide>(); //get all slides in array


        int[] slideIDs = new int[currentListID]; //old slide ids
        int[] newIDs = new int[currentListID]; //new slide ids

        for (int i = 0; i < currentListID; i++)
        {
            slideIDs[i] = slides[i].getListID(); //fill old slide ids array with current slide id values

        }


        /*a list is used to easily remove and insert a slide id value as a copy of the old slide ids array*/
        List<int> IDList = new List<int>(slideIDs);

        int item = IDList[movingSlide];
        IDList.RemoveAt(movingSlide);
        if (movingHoverSlide < movingSlide) { movingHoverSlide++; }
        IDList.Insert(movingHoverSlide, item);


        //convert list back to array of new ids
        newIDs = IDList.ToArray();


        //assign new ids to slides in array and reposition them
        for (int i = 0; i < currentListID; i++)
        {
            slides[newIDs[i]].GetComponent<SlideSelectSlide>().assignID(slideIDs[i]);
            slides[newIDs[i]].GetComponent<SlideSelectSlide>().newPosition(buffer);
        }


        /*For the function in SlideNumbering to work that sets all the slide number values, the slides have to be in proper order in the heirarchy*/
        for (int i = 0; i < currentListID; i++)
        {
            for (int k = 0; k < currentListID; k++)
            {
                if (slides[k].getListID() == i) { slides[k].setSiblingIndex(i); }
            }
        }

    }

}
