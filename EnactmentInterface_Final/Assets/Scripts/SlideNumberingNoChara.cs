using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class SlideNumberingNoChara : MonoBehaviour
{

    /*This class updates the numbers for each slide according to their position in the heirarchy, working its way down from the Beginning slide array to the Ending slides*/
    /*For this reason, it's important to keep the slides in order in the heirarchy by their id in each array*/

    private string participantName;
    private string saveAddress;
    private string archivePath;


    private bool isRecording = false;
    private bool donePlanning = false;
    private bool ready = false;

    private int condition = 1; //0-bottom up, 1-hybrid, 2-top down
    private bool sceneNotesOn = false; //whether we want to use scene notes
    private bool OptitrackCaptureOn = true;
    // Use this for initialization

    public Sprite recordStop;
    public Sprite recordStart;
    public Sprite blankSprite;
    //public Sprite playStop;

    private Sprite playSprite;

    private GameObject playButton;
    private Vector3 playPosition;


    private GameObject selectionTrio;
    private Vector3 trioPosition;

    public Sprite titleCard;
    public Sprite subtitleCard;
    public Sprite endCard;

    private int begSlideCount;
    private int midSlideCount;
    private int endSlideCount;
    private int slideTotalCount;

    private int selectedID;
    private int selectedSection;

    private bool recordScreenReady = false;
    private bool planningReady = false;
    private bool addReady = true;
    private int addWhich = 0;
    private bool playReady = false;
    //Bottom Up
    //private bool emptySlide = false;


    //Top Down

    void Start()
    {

        //* Set up Instructions*//


        switch (condition)
        {
            case 0:
                GameObject.Find("PlayPopUpText").GetComponent<Text>().text = "You can't play your story just yet! Please make sure every scene has been filled up and recorded first. To record a scene, select it with your mouse and hit then red circle record button to go to the acting screen. Also don't forget you need your main title and titles for each section of your story, as well as at least one slide in each section!";
                break;
            case 2:
                GameObject.Find("PlayPopUpText").GetComponent<Text>().text = "You can't play your story just yet! Please make sure every scene has been recorded first. To record a scene, select it with your mouse and hit then red circle record button to go to the acting screen.";
                break;
            default:
                break;
        }

        //**********************//

        playSprite = GameObject.FindGameObjectWithTag("play_btn_noChara").GetComponent<Image>().sprite;

        playButton = GameObject.FindGameObjectWithTag("play_screen_NoChara");
        playPosition = playButton.transform.position;

        selectionTrio = GameObject.FindGameObjectWithTag("object_selection");
        trioPosition = selectionTrio.transform.position;

        //Set up Enactment Screen//
        GameObject[] charaPosButtons = GameObject.FindGameObjectsWithTag("chara_position");
        GameObject[] objPosButtons = GameObject.FindGameObjectsWithTag("obj_position");

        foreach (GameObject button in charaPosButtons)
        {
            button.GetComponent<Button>().interactable = false;
            button.GetComponent<Image>().color = new Color(1, 1, 1, 0);

        }

        foreach (GameObject button in objPosButtons)
        {
            button.GetComponent<Button>().interactable = false;
            button.GetComponent<Image>().color = new Color(1, 1, 1, 0);
        }

        GameObject.FindGameObjectWithTag("enactment_check_NoChara").GetComponent<Button>().interactable = false;
        GameObject.FindGameObjectWithTag("enactment_check_NoChara").GetComponent<Image>().color = new Color(1, 1, 1, 0);
        //end of setting up Enactment Screen//


        //Setting up Timeline for condition

        //function here to get condition

        switch (condition)
        {
            case 0:
                //Bottom up- record button is visible but not yet active, planning button goes away, play story button is visible but disabled
                Destroy(GameObject.Find("NextSlide"));
                Destroy(GameObject.Find("LastSlide"));
                //GameObject.FindGameObjectWithTag("record_screen").GetComponent<Button>().interactable = false;
                GameObject.FindGameObjectWithTag("record_screen").GetComponent<Image>().color = new Color(.784f, .784f, .784f, .314f);
                //GameObject.FindGameObjectWithTag("play_screen").GetComponent<Button>().interactable = false;
                GameObject.FindGameObjectWithTag("play_screen").GetComponent<Image>().color = new Color(.784f, .784f, .784f, .314f);
                GameObject.FindGameObjectWithTag("planning_button").GetComponent<Button>().interactable = false;
                GameObject.FindGameObjectWithTag("planning_button").GetComponent<Image>().color = new Color(1, 1, 1, 0);
                GameObject.FindGameObjectWithTag("planning_button").transform.SetAsFirstSibling();
                GameObject.FindGameObjectWithTag("instructions").GetComponent<Text>().text = "Create Your Story";

                break;
            case 1:
                //Bottom up- record button is visible but not yet active, planning button goes away, play story button is visible but disabled
                Destroy(GameObject.Find("NextSlide"));
                Destroy(GameObject.Find("LastSlide"));
                GameObject.FindGameObjectWithTag("record_screen_NoChara").GetComponent<Button>().interactable = false;
                GameObject.FindGameObjectWithTag("record_screen_NoChara").GetComponent<Image>().color = new Color(.784f, .784f, .784f, .314f);
                GameObject.FindGameObjectWithTag("play_screen_NoChara").GetComponent<Button>().interactable = false;
                GameObject.FindGameObjectWithTag("play_screen_NoChara").GetComponent<Image>().color = new Color(.784f, .784f, .784f, .314f);
                GameObject.FindGameObjectWithTag("planning_button_NoChara").GetComponent<Button>().interactable = false;
                GameObject.FindGameObjectWithTag("planning_button_NoChara").GetComponent<Image>().color = new Color(1, 1, 1, 0);
                GameObject.FindGameObjectWithTag("planning_button_NoChara").transform.SetAsFirstSibling();
                GameObject.FindGameObjectWithTag("instructions").GetComponent<Text>().text = "Create Your Story";

                break;
            case 2:
                //Top down - record button and play story button goes away, planning button is visible but disabled
                GameObject.FindGameObjectWithTag("planning_button").transform.SetAsLastSibling();
                //GameObject.FindGameObjectWithTag("planning_button").GetComponent<Button>().interactable = false;
                GameObject.FindGameObjectWithTag("planning_button").GetComponent<Image>().color = new Color(.784f, .784f, .784f, .314f);
                GameObject.FindGameObjectWithTag("record_screen").GetComponent<Button>().interactable = false;
                GameObject.FindGameObjectWithTag("record_screen").GetComponent<Image>().color = new Color(1, 0, 0, 0);
                //GameObject.FindGameObjectWithTag("play_screen").GetComponent<Button>().interactable = false;
                GameObject.FindGameObjectWithTag("play_screen").GetComponent<Image>().color = new Color(.784f, .784f, .784f, .314f);
                //GameObject.FindGameObjectWithTag("play_screen").GetComponent<Image>().color = new Color(1, 1, 1, 0);
                GameObject.FindGameObjectWithTag("play_screen").transform.position = new Vector3(5000, 5000);
                GameObject.FindGameObjectWithTag("instructions").GetComponent<Text>().text = "Plan Your Story";
                break;
            default:
                break;

        }
    }

    // Update is called once per frame
    void Update()
    {
        SlideArrayNoChara[] arrays = GetComponentsInChildren<SlideArrayNoChara>();

        begSlideCount = arrays[0].getCurrent();
        midSlideCount = arrays[1].getCurrent();
        endSlideCount = arrays[2].getCurrent();


        slideTotalCount = begSlideCount + midSlideCount + endSlideCount;



        if (getSelectedStatus() && getSelectedData().isFilled())
        {
            getSelectedData().UpdatePlayRecordButton(begSlideCount, midSlideCount, endSlideCount, slideTotalCount, selectedSection, selectedID);
        }



        int num = 1;


        foreach (Transform child in this.transform) //Gets all slide arrays set as children to the GameObject with this script
        {
            foreach (Transform grandchild in child.transform)
            {
                if (grandchild.GetComponentInChildren<SlideSelectSlideNoChara>() != null) //Since the add slide buttons and slides are under the same parent, only objects with a Slide script is assigned a number to its Text component
                {
                    if (grandchild.GetComponentInChildren<Text>() != null)
                    {
                        grandchild.GetComponentInChildren<Text>().text = num.ToString();
                        num++;
                    }
                }
            }
        }


        GameObject[] addButtons = GameObject.FindGameObjectsWithTag("add_slide");
        SlideArrayNoChara[] children = GetComponentsInChildren<SlideArrayNoChara>();


        SlideDataNoChara[] begSlides = children[0].GetComponentsInChildren<SlideDataNoChara>();
        SlideDataNoChara[] midSlides = children[1].GetComponentsInChildren<SlideDataNoChara>();
        SlideDataNoChara[] endSlides = children[2].GetComponentsInChildren<SlideDataNoChara>();
        if (getSelectedStatus() && !donePlanning)
        {

            if (getSelectedData().getLock())
            {
                //GameObject.Find("WriteSlide").GetComponent<Button>().interactable = false;
                //GameObject.Find("WriteSlide").GetComponent<Image>().color = new Color(1, 1, 1, 0);
            }
            else
            {
                //GameObject.Find("WriteSlide").GetComponent<Button>().interactable = true;
                //GameObject.Find("WriteSlide").GetComponent<Image>().color = new Color(.232f, .918f, .235f, 1);
            }
        }
        else
        {
            //GameObject.Find("WriteSlide").GetComponent<Button>().interactable = false;
            //GameObject.Find("WriteSlide").GetComponent<Image>().color = new Color(1,1,1,0);
        }

        switch (condition)
        {

            case 0:


                if (!EmptySlide()) //no slides are empty
                {
                    foreach (GameObject button in addButtons)
                    {
                        //button.GetComponent<Button>().interactable = true;
                        addReady = true;
                        button.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                    }



                    SlideArrayNoChara[] childrenSlides = GetComponentsInChildren<SlideArrayNoChara>();

                    for (int i = 0; i < childrenSlides.Length; i++)
                    {
                        SlideSelectSlideNoChara[] grandchildren = childrenSlides[i].GetComponentsInChildren<SlideSelectSlideNoChara>();

                        for (int k = 0; k < grandchildren.Length; k++)
                        {
                            grandchildren[k].draggingOn();

                        }
                    }
                }
                else
                {
                    foreach (GameObject button in addButtons)
                    {
                        //button.GetComponent<Button>().interactable = false;
                        addReady = false;
                        button.GetComponent<Image>().color = new Color(.784f, .784f, .784f, .314f);
                    }

                    SlideArrayNoChara[] childrenSlides = GetComponentsInChildren<SlideArrayNoChara>();

                    for (int i = 0; i < childrenSlides.Length; i++)
                    {
                        SlideSelectSlideNoChara[] grandchildren = childrenSlides[i].GetComponentsInChildren<SlideSelectSlideNoChara>();

                        for (int k = 0; k < grandchildren.Length; k++)
                        {
                            //grandchildren[k].draggingOff();

                        }
                    }


                }

                if (getSelectedStatus() && !getSelectedData().getLock())
                {
                    if (getSelectedData().isFilled())
                    {
                        recordScreenReady = true;
                        //GameObject.FindGameObjectWithTag("record_screen").GetComponent<Button>().interactable = true;
                        GameObject.FindGameObjectWithTag("record_screen").GetComponent<Image>().color = new Color(.925f, 0, 0, 1);

                    }
                    else
                    {
                        recordScreenReady = false;
                        GameObject.FindGameObjectWithTag("record_screen").GetComponent<Image>().color = new Color(.784f, .784f, .784f, .314f);
                    }
                }
                else
                {
                    //GameObject.FindGameObjectWithTag("record_screen").GetComponent<Button>().interactable = false;
                    recordScreenReady = false;
                    GameObject.FindGameObjectWithTag("record_screen").GetComponent<Image>().color = new Color(.784f, .784f, .784f, .314f);
                }


                if (begSlides.Length > 0 && midSlides.Length > 0 && endSlides.Length > 0 && !EmptySlide() && titlesFilled())
                {
                    //GameObject.FindGameObjectWithTag("play_screen").GetComponent<Button>().interactable = true;
                    GameObject.FindGameObjectWithTag("play_screen").GetComponent<Image>().color = new Color(.235f, .788f, .4f, 1);
                    playReady = true;
                }
                else
                {
                    //GameObject.FindGameObjectWithTag("play_screen").GetComponent<Button>().interactable = false;
                    playReady = false;
                    GameObject.FindGameObjectWithTag("play_screen").GetComponent<Image>().color = new Color(.784f, .784f, .784f, .314f);
                }

                if (begSlideCount >= 5) { GameObject.Find("AddBeg").GetComponent<Button>().interactable = false; }
                if (midSlideCount >= 5) { GameObject.Find("AddMid").GetComponent<Button>().interactable = false; }
                if (endSlideCount >= 5) { GameObject.Find("AddEnd").GetComponent<Button>().interactable = false; }

                break;

            case 1:


                if (!EmptySlide()) //no slides are empty
                {
                    SlideArrayNoChara[] childrenSlides = GetComponentsInChildren<SlideArrayNoChara>();

                    for (int i = 0; i < childrenSlides.Length; i++)
                    {
                        SlideSelectSlideNoChara[] grandchildren = childrenSlides[i].GetComponentsInChildren<SlideSelectSlideNoChara>();

                        for (int k = 0; k < grandchildren.Length; k++)
                        {
                            grandchildren[k].draggingOn();

                        }
                    }
                }
                else
                {

                    SlideArrayNoChara[] childrenSlides = GetComponentsInChildren<SlideArrayNoChara>();

                    for (int i = 0; i < childrenSlides.Length; i++)
                    {
                        SlideSelectSlideNoChara[] grandchildren = childrenSlides[i].GetComponentsInChildren<SlideSelectSlideNoChara>();

                        for (int k = 0; k < grandchildren.Length; k++)
                        {
                            //grandchildren[k].draggingOff();

                        }
                    }


                }

                if (getSelectedStatus() && !getSelectedData().getLock())
                {
                    if (getSelectedData().isFilled())
                    {
                        recordScreenReady = true;
                        GameObject.FindGameObjectWithTag("record_screen_NoChara").GetComponent<Button>().interactable = true;
                        GameObject.FindGameObjectWithTag("record_screen_NoChara").GetComponent<Image>().color = new Color(.925f, 0, 0, 1);

                    }
                    else
                    {
                        recordScreenReady = false;
                        GameObject.FindGameObjectWithTag("record_screen_NoChara").GetComponent<Image>().color = new Color(.784f, .784f, .784f, .314f);
                    }
                }
                else
                {
                    //GameObject.FindGameObjectWithTag("record_screen").GetComponent<Button>().interactable = false;
                    recordScreenReady = false;
                    GameObject.FindGameObjectWithTag("record_screen_NoChara").GetComponent<Image>().color = new Color(.784f, .784f, .784f, .314f);
                }


                if (begSlides.Length > 0 && midSlides.Length > 0 && endSlides.Length > 0 && !EmptySlide() && titlesFilled())
                {
                    //GameObject.FindGameObjectWithTag("play_screen").GetComponent<Button>().interactable = true;
                    GameObject.FindGameObjectWithTag("play_screen_NoChara").GetComponent<Image>().color = new Color(.235f, .788f, .4f, 1);
                    playReady = true;
                }
                else
                {
                    //GameObject.FindGameObjectWithTag("play_screen").GetComponent<Button>().interactable = false;
                    playReady = false;
                    GameObject.FindGameObjectWithTag("play_screen_NoChara").GetComponent<Image>().color = new Color(.784f, .784f, .784f, .314f);
                }

                if (begSlideCount >= 5) { GameObject.Find("AddBeg").GetComponent<Button>().interactable = false; }
                if (midSlideCount >= 5) { GameObject.Find("AddMid").GetComponent<Button>().interactable = false; }
                if (endSlideCount >= 5) { GameObject.Find("AddEnd").GetComponent<Button>().interactable = false; }


                break;
            case 2:
                if (donePlanning)
                {
                    GameObject.FindGameObjectWithTag("instructions").GetComponent<Text>().text = "Act Out Your Story";
                    GameObject[] allTitles = GameObject.FindGameObjectsWithTag("title");

                    foreach (GameObject title in allTitles)
                    {
                        title.GetComponent<InputField>().interactable = false;
                    }


                    foreach (GameObject button in addButtons)
                    {
                        button.GetComponent<Button>().interactable = false;
                    }

                    SlideArrayNoChara[] childrenSlides = GetComponentsInChildren<SlideArrayNoChara>();

                    for (int i = 0; i < childrenSlides.Length; i++)
                    {
                        SlideSelectSlideNoChara[] grandchildren = childrenSlides[i].GetComponentsInChildren<SlideSelectSlideNoChara>();

                        for (int k = 0; k < grandchildren.Length; k++)
                        {
                            grandchildren[k].draggingOff();

                        }
                    }

                    //record button & play button come to life
                    //planning button goes away
                    GameObject.FindGameObjectWithTag("planning_button").transform.SetAsFirstSibling();
                    GameObject.FindGameObjectWithTag("planning_button").GetComponent<Button>().interactable = false;
                    GameObject.FindGameObjectWithTag("planning_button").GetComponent<Image>().color = new Color(1, 1, 1, 0);
                    GameObject.FindGameObjectWithTag("record_screen").GetComponent<Image>().color = new Color(1, 0, 0, 1);
                    GameObject.FindGameObjectWithTag("play_screen").GetComponent<Image>().color = new Color(.235f, .788f, .4f, 1);
                    GameObject.FindGameObjectWithTag("play_screen").transform.position = playPosition;
                    selectionTrio.transform.position = new Vector3(5000, 5000);


                    if (getSelectedStatus())
                    {
                        GameObject.FindGameObjectWithTag("record_screen").GetComponent<Button>().interactable = true;
                    }
                    else
                    {
                        GameObject.FindGameObjectWithTag("record_screen").GetComponent<Button>().interactable = false;
                    }


                    if (begSlides.Length > 0 && midSlides.Length > 0 && endSlides.Length > 0 && !EmptySlide())
                    {
                        //GameObject.FindGameObjectWithTag("play_screen").GetComponent<Button>().interactable = true;
                        GameObject.FindGameObjectWithTag("play_screen").GetComponent<Image>().color = new Color(.235f, .788f, .4f, 1);
                        playReady = true;
                    }
                    else
                    {
                        //GameObject.FindGameObjectWithTag("play_screen").GetComponent<Button>().interactable = false;
                        GameObject.FindGameObjectWithTag("play_screen").GetComponent<Image>().color = new Color(.784f, .784f, .784f, .314f);
                        playReady = false;
                    }
                }
                else
                {
                    if (begSlideCount >= 5) { GameObject.Find("AddBeg").GetComponent<Button>().interactable = false; }
                    else { GameObject.Find("AddBeg").GetComponent<Button>().interactable = true; }
                    if (midSlideCount >= 5) { GameObject.Find("AddMid").GetComponent<Button>().interactable = false; }
                    else { GameObject.Find("AddMid").GetComponent<Button>().interactable = true; }
                    if (endSlideCount >= 5) { GameObject.Find("AddEnd").GetComponent<Button>().interactable = false; }
                    else { GameObject.Find("AddEnd").GetComponent<Button>().interactable = true; }


                }

                if (donePlanButtonReady())
                {
                    //GameObject.FindGameObjectWithTag("planning_button").GetComponent<Button>().interactable = true;
                    GameObject.FindGameObjectWithTag("planning_button").GetComponent<Image>().color = new Color(.235f, .788f, .4f, 1);

                    planningReady = true;
                }
                else
                {
                    //GameObject.FindGameObjectWithTag("planning_button").GetComponent<Button>().interactable = false;
                    GameObject.FindGameObjectWithTag("planning_button").GetComponent<Image>().color = new Color(.784f, .784f, .784f, .314f);

                    planningReady = false;
                }


                break;

            default:
                break;

        }



    }

    public void setParticipantName()
    {
        string condString = "";
        var cond = GameObject.Find("CanvasManager").GetComponent<CanvasManagerBottomUp>().getEnactmentCondition();
        if (cond == 0) { condString = "Cartoon"; }
        else { condString = "Video"; }
        participantName = "Participant" + GameObject.Find("InputParticipantID").GetComponent<InputField>().text + "_" + condString + "mode";

    }

    public string getParticipantName()
    {
        return participantName;
    }

    public bool setSavingAddress()
    {
        string append = participantName;
        string append2 = participantName + "/archive";
        saveAddress = Path.Combine(Application.persistentDataPath, append);
        archivePath = Path.Combine(Application.persistentDataPath, append2);
        Debug.Log(saveAddress);
        Debug.Log(archivePath);

        try
        {
            // Determine whether the directory exists.
            if (!Directory.Exists(saveAddress))
            {
                DirectoryInfo di1 = Directory.CreateDirectory(saveAddress);
                Debug.Log("The directory was created successfully at " + saveAddress);
            }
            else
            {
                Debug.Log("The directory exists at " + saveAddress);
            }

            if (!Directory.Exists(archivePath))
            {
                DirectoryInfo di2 = Directory.CreateDirectory(archivePath);
                Debug.Log("The directory was created successfully at " + archivePath);
            }
            else
            {
                Debug.Log("The directory exists at " + archivePath);
            }

            return true;
        }
        catch (Exception e)
        {
            Debug.LogError("The process of setting save address failed: " + e.ToString());
            return false;
        }
    }

    public string getSavingAddress()
    {
        return saveAddress;
    }

    public void createDirectories()
    {
        setParticipantName();
        setSavingAddress();

        Debug.Log("we are all set to save videos!");
    }

    //This is the function that checks for titles.
    //1- changed tag of the title gameobjects that I wanted to remove (beg title, mide title, etc.)
    //2- removed the gameobjects from scene.
    public bool titlesFilled()
    {
        GameObject[] allTitles = GameObject.FindGameObjectsWithTag("title");

        foreach (GameObject title in allTitles)
        {
            if (title.GetComponent<InputField>().text == "") { return false; }
        }

        return true;
    }

    public void donePlan()
    {
        donePlanning = true;
        GameObject.FindGameObjectWithTag("trash").GetComponent<Button>().interactable = false;
        GameObject.FindGameObjectWithTag("all_canvases").GetComponent<CanvasManagerBottomUp>().planningHide();
    }

    public bool donePlanButtonReady()
    {

        SlideArrayNoChara[] children = GetComponentsInChildren<SlideArrayNoChara>();

        for (int i = 0; i < children.Length; i++)
        {
            SlideDataNoChara[] grandchildrenData = children[i].GetComponentsInChildren<SlideDataNoChara>();

            if (grandchildrenData.Length == 0) { return false; }

            for (int k = 0; k < grandchildrenData.Length; k++)
            {
                if (grandchildrenData[k].isFilled() == false)
                {
                    return false;
                }

            }
        }

        if (titlesFilled() == false) { return false; }

        return true;

    }

    //deselects all slides across all three arrays so only one slide is selected
    //is called by SelectMe in SlideSelectSlideNoChara script, by the slide being selected
    public void selectNew(int section, int slide)
    {
        SlideArrayNoChara[] children = GetComponentsInChildren<SlideArrayNoChara>();

        for (int i = 0; i < children.Length; i++)
        {
            SlideSelectSlideNoChara[] grandchildren = children[i].GetComponentsInChildren<SlideSelectSlideNoChara>();
            SlideDataNoChara[] grandchildrenData = children[i].GetComponentsInChildren<SlideDataNoChara>();
            for (int k = 0; k < grandchildren.Length; k++)
            {
                if (i == section && grandchildren[k].getListID() == slide)
                {

                    //do nothing, the slide calling this function handles its own state
                }
                else { grandchildren[k].deselectMe(); }

            }
        }



        updateSelectedVars();

    }

    public void selectNewRemotely(int section, int slide)
    {
        SlideArrayNoChara[] children = GetComponentsInChildren<SlideArrayNoChara>();

        for (int i = 0; i < children.Length; i++)
        {
            SlideSelectSlideNoChara[] grandchildren = children[i].GetComponentsInChildren<SlideSelectSlideNoChara>();
            SlideDataNoChara[] grandchildrenData = children[i].GetComponentsInChildren<SlideDataNoChara>();
            for (int k = 0; k < grandchildren.Length; k++)
            {
                if (i == section && grandchildren[k].getListID() == slide)
                {
                    grandchildren[k].selectMe();
                    //do nothing, the slide calling this function handles its own state
                }


            }
        }

    }


    //goes through all slides when the delete button is pressed to find which one is selected for deletion
    public void deleteSlide()

    {
        getSelectedData().archiveVideo();
        SlideArrayNoChara[] children = GetComponentsInChildren<SlideArrayNoChara>();

        for (int i = 0; i < children.Length; i++)
        {
            SlideSelectSlideNoChara[] grandchildren = children[i].GetComponentsInChildren<SlideSelectSlideNoChara>();

            for (int k = 0; k < grandchildren.Length; k++)
            {
                if (grandchildren[k].getSelected() == true) { grandchildren[k].deleteMe(); }

            }
        }


    }


    public void recordSelectedSlide()
    {

        GameObject playButton = GameObject.FindGameObjectWithTag("play_btn_noChara");
        GameObject backButton = GameObject.FindGameObjectWithTag("back_btn_noChara");
        GameObject recordButton = GameObject.FindGameObjectWithTag("record_btn_noChara");
        //GameObject[] poseButtons = GameObject.FindGameObjectsWithTag("pose_button");

        if (isRecording == false)
        {
            getSelectedData().startRecord();
            isRecording = true;
            playButton.GetComponent<Button>().interactable = false;
            backButton.GetComponent<Button>().interactable = false;
            recordButton.GetComponent<Image>().sprite = recordStop;
            //foreach (GameObject button in poseButtons)
            //{
            //    button.GetComponent<Button>().interactable = false;

            //}
        }
        else
        {
            getSelectedData().endRecord();
            getSelectedSlide().showRecordedStatus();
            isRecording = false;
            playButton.GetComponent<Button>().interactable = true;
            backButton.GetComponent<Button>().interactable = true;
            recordButton.GetComponent<Image>().sprite = recordStart;
            //foreach (GameObject button in poseButtons)
            //{
            //    button.GetComponent<Button>().interactable = true;

            //}
        }

    }

    public void playSelectedSlide()
    {
        getSelectedData().playVideo();

    }

    public void redoEnactment()
    {
        getSelectedData().archiveVideo();
    }

    public void poseZero()
    {

        getSelectedData().setPose(0);
        getSelectedData().updatePoseMode();
    }

    public void poseOne()
    {
        getSelectedData().setPose(1);
        getSelectedData().updatePoseMode();
    }

    public void poseTwo()
    {
        getSelectedData().setPose(2);
        getSelectedData().updatePoseMode();
    }

    public void poseThree()
    {
        getSelectedData().setPose(3);
        getSelectedData().updatePoseMode();
    }

    public void poseFour()
    {
        getSelectedData().setPose(4);
        getSelectedData().updatePoseMode();
    }



    public void updateEnactmentPose()
    {
        //getSelectedData().updateCharaPose(true);

    }

    public void updateEnactmentObjects()
    {
        getSelectedData().updateEnactmentScreen();

    }

    public void updateNewEnactmentObjects(int sect, int id)
    {

        SlideArrayNoChara[] children = GetComponentsInChildren<SlideArrayNoChara>();

        for (int i = 0; i < children.Length; i++)
        {
            SlideSelectSlideNoChara[] grandchildren = children[i].GetComponentsInChildren<SlideSelectSlideNoChara>();
            SlideDataNoChara[] grandchildrenData = children[i].GetComponentsInChildren<SlideDataNoChara>();
            for (int k = 0; k < grandchildren.Length; k++)
            {
                if (sect == i && grandchildren[k].getListID() == id)
                {

                    grandchildrenData[k].updateEnactmentScreen();

                }

            }
        }

        // getSelectedData().updateEnactmentScreen();

    }

    public void enactmentCheck()
    {
        getSelectedData().updatePoseMode();
    }

    /*   public void handheldObjects()
       {
           getSelectedData().setPose(getSelectedData().getPose(), false);

       }
       */

    public void charaLeft()
    {
        getSelectedData().setCharaPos(0);
        getSelectedData().updateCharaPos(true);
        //getSelectedData().updatePoseMode();
    }

    public void charaRight()
    {
        getSelectedData().setCharaPos(1);
        getSelectedData().updateCharaPos(true);
        //getSelectedData().updatePoseMode();
    }

    public void charaUp()
    {
        getSelectedData().setCharaPos(2);
        getSelectedData().updateCharaPos(true);
        //getSelectedData().updatePoseMode();
    }

    public void charaDown()
    {
        getSelectedData().setCharaPos(3);
        getSelectedData().updateCharaPos(true);
        //getSelectedData().updatePoseMode();
    }

    public void objectLeft()
    {
        getSelectedData().setObjectPos(0);
        getSelectedData().updateItemPos(true);
        //getSelectedData().updatePoseMode();
    }

    public void objectRight()
    {
        getSelectedData().setObjectPos(1);
        getSelectedData().updateItemPos(true);
        //getSelectedData().updatePoseMode();
    }

    public void objectUp()
    {
        getSelectedData().setObjectPos(2);
        getSelectedData().updateItemPos(true);
        //getSelectedData().updatePoseMode();
    }

    public void objectDown()
    {
        getSelectedData().setObjectPos(3);
        getSelectedData().updateItemPos(true);
        //getSelectedData().updatePoseMode();
    }

    private SlideDataNoChara getSelectedData()
    {
        SlideArrayNoChara[] children = GetComponentsInChildren<SlideArrayNoChara>();

        for (int i = 0; i < children.Length; i++)
        {
            SlideSelectSlideNoChara[] grandchildren = children[i].GetComponentsInChildren<SlideSelectSlideNoChara>();
            SlideDataNoChara[] grandchildrenData = children[i].GetComponentsInChildren<SlideDataNoChara>();
            for (int k = 0; k < grandchildren.Length; k++)
            {
                if (grandchildren[k].getSelected() == true)
                {
                    return grandchildrenData[k];

                }

            }
        }
        return new SlideDataNoChara();
    }

    private SlideSelectSlideNoChara getSelectedSlide()
    {
        SlideArrayNoChara[] children = GetComponentsInChildren<SlideArrayNoChara>();

        for (int i = 0; i < children.Length; i++)
        {
            SlideSelectSlideNoChara[] grandchildren = children[i].GetComponentsInChildren<SlideSelectSlideNoChara>();
            //SlideDataNoChara[] grandchildrenData = children[i].GetComponentsInChildren<SlideDataNoChara>();
            for (int k = 0; k < grandchildren.Length; k++)
            {
                if (grandchildren[k].getSelected() == true)
                {

                    return grandchildren[k];

                }

            }
        }
        return new SlideSelectSlideNoChara();
    }


    private void updateSelectedVars()
    {
        SlideArrayNoChara[] children = GetComponentsInChildren<SlideArrayNoChara>();

        for (int i = 0; i < children.Length; i++)
        {
            SlideSelectSlideNoChara[] grandchildren = children[i].GetComponentsInChildren<SlideSelectSlideNoChara>();
            //SlideDataNoChara[] grandchildrenData = children[i].GetComponentsInChildren<SlideDataNoChara>();
            for (int k = 0; k < grandchildren.Length; k++)
            {
                if (grandchildren[k].getSelected() == true)
                {
                    selectedSection = i;
                    selectedID = grandchildren[k].getListID();

                }

            }
        }

    }


    private bool getSelectedStatus()
    {
        SlideArrayNoChara[] children = GetComponentsInChildren<SlideArrayNoChara>();

        for (int i = 0; i < children.Length; i++)
        {
            SlideSelectSlideNoChara[] grandchildren = children[i].GetComponentsInChildren<SlideSelectSlideNoChara>();

            for (int k = 0; k < grandchildren.Length; k++)
            {
                if (grandchildren[k].getSelected() == true)
                {

                    return true;

                }

            }
        }
        return false;
    }


    public void setTitle()
    {
        GameObject[] currents;

        currents = GameObject.FindGameObjectsWithTag("current_item");

        foreach (GameObject current in currents)
        {
            Destroy(current);
        }

        GameObject.Find("EnactmentBackdrop").GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);

        GameObject.Find("EnactmentBackdrop").GetComponent<SpriteRenderer>().sprite = titleCard;
        GameObject.Find("PlayCharacter").GetComponent<Image>().color = new Color(1, 1, 1, 0);
        GameObject.Find("PlayTitle").GetComponent<Text>().text = GameObject.FindGameObjectWithTag("main_title").GetComponent<Text>().text;
        GameObject.Find("PlayTitle").GetComponent<Text>().color = new Color(0, 0, 0, 1);

    }


    public IEnumerator SequenceTiming(bool saveFiles)
    {
        GameObject.FindGameObjectWithTag("all_canvases").GetComponent<CanvasManagerBottomUp>().saveStoryHide();

        SlideArrayNoChara[] sections = GetComponentsInChildren<SlideArrayNoChara>();

        SlideDataNoChara[] beginning = sections[0].GetComponentsInChildren<SlideDataNoChara>();
        SlideDataNoChara[] middle = sections[1].GetComponentsInChildren<SlideDataNoChara>();
        SlideDataNoChara[] end = sections[2].GetComponentsInChildren<SlideDataNoChara>();

        float wait = 3.0f;

        int fileNum = 0;
        //int whichClip = 1;
        //int iterator = 1;

        if (saveFiles == true)
        {
            participantName = "Participant" + GameObject.Find("InputParticipantID").GetComponent<InputField>().text + "_mode" + GameObject.Find("canvasManager").GetComponent<CanvasManagerBottomUp>().getEnactmentCondition();
            Directory.CreateDirectory(Application.dataPath + "/" + participantName);

        }
        bool doubleTroubleFrame = false;
        bool doubleTroubleAud = false;

        GameObject.Find("PlayWindow").GetComponent<Image>().color = new Color(1, 1, 1, 0);

        //GameObject.Find("PlayInstruction").GetComponent<Text>().color = new Color(0, 0, 0, 0);
        GameObject.FindGameObjectWithTag("play_story_button").GetComponent<Button>().interactable = false;
        GameObject.FindGameObjectWithTag("playscreen_back_button").GetComponent<Button>().interactable = false;
        GameObject.FindGameObjectWithTag("play_story_button").GetComponent<Image>().color = new Color(1, 1, 1, 0);
        GameObject.Find("PlayText").GetComponent<Text>().color = new Color(1, 1, 1, 0);
        GameObject.FindGameObjectWithTag("playscreen_back_button").GetComponent<Image>().color = new Color(1, 1, 1, 0);

        GameObject.Find("SaveStory").GetComponent<Image>().color = new Color(1, 1, 1, 0);
        GameObject.Find("SaveText").GetComponent<Text>().color = new Color(1, 1, 1, 0);
        GameObject.Find("SaveStory").GetComponent<Button>().interactable = false;

        //Start Recording
        //GameObject.Find("Recorder").GetComponent<Animator>().StartRecording(24*60*5);
        GameObject.Find("EnactmentBackdrop").GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        GameObject.Find("EnactmentBackdrop").GetComponent<SpriteRenderer>().sprite = titleCard;


        if (saveFiles)
        {
            if (System.IO.File.Exists(fileNum + "_" + participantName + "/mainTitle.png"))
            { ScreenCapture.CaptureScreenshot(fileNum + "_" + participantName + "/mainTitle2.png"); }
            else { ScreenCapture.CaptureScreenshot(fileNum + "_" + participantName + "/mainTitle.png"); }

        }

        fileNum += 1;

        yield return new WaitForSeconds(wait); //pause for title

        GameObject.Find("EnactmentBackdrop").GetComponent<SpriteRenderer>().sprite = subtitleCard;
        GameObject.Find("PlayTitle").GetComponent<Text>().text = GameObject.Find("BegTitle").GetComponent<InputField>().text;


        if (saveFiles)
        {
            if (System.IO.File.Exists(fileNum + "_" + participantName + "/begTitle.png"))
            { ScreenCapture.CaptureScreenshot(fileNum + "_" + participantName + "/begTitle2.png"); }
            else { ScreenCapture.CaptureScreenshot(fileNum + "_" + participantName + "/begTitle.png"); }

        }

        fileNum += 1;
        yield return new WaitForSeconds(wait);

        //GameObject.Find("EnactmentBackdrop").GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        GameObject.Find("PlayTitle").GetComponent<Text>().color = new Color(0, 0, 0, 0);
        GameObject.Find("PlayCharacter").GetComponent<Image>().color = new Color(1, 1, 1, 1);

        for (int i = 0; i < beginning.Length; i++)
        {
            int x = i + 1;

            wait = beginning[i].getTime();

            beginning[i].updatePlayScreen();
            beginning[i].playAudio();

            if (saveFiles)
            {
                string framename;
                if (System.IO.File.Exists(fileNum + "_" + participantName + "/begFrame_" + x + ".png") || doubleTroubleFrame == true)
                {
                    framename = fileNum + "_" + participantName + "/begFrame2_" + x + ".png";
                    doubleTroubleFrame = true;
                }
                else
                {
                    framename = fileNum + "_" + participantName + "/begFrame_" + x + ".png";
                }
                ScreenCapture.CaptureScreenshot(framename);
            }


            yield return new WaitForSeconds(wait);

            if (saveFiles)
            {

                if (System.IO.File.Exists(fileNum + "_" + participantName + "/begAud_" + x) || doubleTroubleAud == true)
                {
                    GetComponent<SavWav>().Save(fileNum + "_" + participantName + "/begAud2_" + x, beginning[i].getAudio());
                    doubleTroubleAud = true;
                    //whichClip = 2;
                }
                else
                {
                    GetComponent<SavWav>().Save(fileNum + "_" + participantName + "/begAud_" + x, beginning[i].getAudio());
                    // whichClip = 2;
                }

            }

            fileNum += 1;
        }

        doubleTroubleFrame = false;
        doubleTroubleAud = false;



        GameObject[] currents;

        currents = GameObject.FindGameObjectsWithTag("current_item");

        foreach (GameObject current in currents)
        {
            Destroy(current);
        }

        GameObject.Find("EnactmentBackdrop").GetComponent<SpriteRenderer>().sprite = subtitleCard;
        //GameObject.Find("EnactmentBackdrop").GetComponent<SpriteRenderer>().color = new Color(.596f, .824f, .894f, 1);

        GameObject.Find("PlayCharacter").GetComponent<Image>().color = new Color(1, 1, 1, 0);
        GameObject.Find("PlayTitle").GetComponent<Text>().text = GameObject.Find("MidTitle").GetComponent<InputField>().text;
        GameObject.Find("PlayTitle").GetComponent<Text>().color = new Color(0, 0, 0, 1);

        if (saveFiles)
        {
            if (System.IO.File.Exists(fileNum + "_" + participantName + "/midTitle.png"))
            { ScreenCapture.CaptureScreenshot(fileNum + "_" + participantName + "/midTitle2.png"); }
            else { ScreenCapture.CaptureScreenshot(fileNum + "_" + participantName + "/midTitle.png"); }

        }
        fileNum += 1;
        yield return new WaitForSeconds(3.0f);

        GameObject.Find("EnactmentBackdrop").GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        GameObject.Find("PlayTitle").GetComponent<Text>().color = new Color(0, 0, 0, 0);
        GameObject.Find("PlayCharacter").GetComponent<Image>().color = new Color(1, 1, 1, 1);

        for (int i = 0; i < middle.Length; i++)
        {
            int x = i + 1;

            wait = middle[i].getTime();

            middle[i].updatePlayScreen();
            middle[i].playAudio();

            if (saveFiles)
            {
                string framename;
                if (System.IO.File.Exists(fileNum + "_" + participantName + "/midFrame_" + x + ".png") || doubleTroubleFrame == true)
                {
                    framename = fileNum + "_" + participantName + "/midFrame2_" + x + ".png";
                    doubleTroubleFrame = true;
                }
                else
                {
                    framename = fileNum + "_" + participantName + "/midFrame_" + x + ".png";
                }
                ScreenCapture.CaptureScreenshot(framename);
            }


            yield return new WaitForSeconds(wait);

            if (saveFiles)
            {

                if (System.IO.File.Exists(fileNum + "_" + participantName + "/midAud_" + x) || doubleTroubleAud == true)
                {
                    GetComponent<SavWav>().Save(fileNum + "_" + participantName + "/midAud2_" + x, middle[i].getAudio());
                    //whichClip = 2;
                    doubleTroubleAud = true;
                }
                else
                {
                    GetComponent<SavWav>().Save(fileNum + "_" + participantName + "/midAud_" + x, middle[i].getAudio());
                    //whichClip = 2;
                }

            }

            fileNum += 1;
        }

        doubleTroubleFrame = false;
        doubleTroubleAud = false;

        currents = GameObject.FindGameObjectsWithTag("current_item");

        foreach (GameObject current in currents)
        {
            Destroy(current);
        }

        GameObject.Find("EnactmentBackdrop").GetComponent<SpriteRenderer>().sprite = subtitleCard;
        //GameObject.Find("EnactmentBackdrop").GetComponent<SpriteRenderer>().color = new Color(.596f, .824f, .894f, 1);

        GameObject.Find("PlayCharacter").GetComponent<Image>().color = new Color(1, 1, 1, 0);
        GameObject.Find("PlayTitle").GetComponent<Text>().text = GameObject.Find("EndTitle").GetComponent<InputField>().text;
        GameObject.Find("PlayTitle").GetComponent<Text>().color = new Color(0, 0, 0, 1);

        if (saveFiles)
        {
            if (System.IO.File.Exists(fileNum + "_" + participantName + "/endTitle.png"))
            { ScreenCapture.CaptureScreenshot(fileNum + "_" + participantName + "/endTitle2.png"); }
            else { ScreenCapture.CaptureScreenshot(fileNum + "_" + participantName + "/endTitle.png"); }

        }
        fileNum += 1;
        yield return new WaitForSeconds(3.0f);

        GameObject.Find("EnactmentBackdrop").GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        GameObject.Find("PlayTitle").GetComponent<Text>().color = new Color(0, 0, 0, 0);
        GameObject.Find("PlayCharacter").GetComponent<Image>().color = new Color(1, 1, 1, 1);

        for (int i = 0; i < end.Length; i++)
        {
            int x = i + 1;

            wait = end[i].getTime();

            end[i].updatePlayScreen();
            end[i].playAudio();

            if (saveFiles)
            {
                string framename;
                if (System.IO.File.Exists(fileNum + "_" + participantName + "/endFrame_" + x + ".png") || doubleTroubleFrame == true)
                {
                    framename = fileNum + "_" + participantName + "/endFrame2_" + x + ".png";
                    doubleTroubleFrame = true;
                }
                else
                {
                    framename = fileNum + "_" + participantName + "/endFrame_" + x + ".png";
                }
                ScreenCapture.CaptureScreenshot(framename);
            }


            yield return new WaitForSeconds(wait);

            if (saveFiles)
            {

                if (System.IO.File.Exists(fileNum + "_" + participantName + "/endAud_" + x) || doubleTroubleAud == true)
                {
                    GetComponent<SavWav>().Save(fileNum + "_" + participantName + "/endAud2_" + x, end[i].getAudio());
                    //whichClip = 2;
                    doubleTroubleAud = true;
                }
                else
                {
                    GetComponent<SavWav>().Save(fileNum + "_" + participantName + "/endAud_" + x, end[i].getAudio());
                    //whichClip = 2;
                }

            }
            fileNum += 1;
        }



        currents = GameObject.FindGameObjectsWithTag("current_item");

        foreach (GameObject current in currents)
        {
            Destroy(current);
        }

        GameObject.Find("EnactmentBackdrop").GetComponent<SpriteRenderer>().sprite = endCard;
        //GameObject.Find("EnactmentBackdrop").GetComponent<SpriteRenderer>().color = new Color(.596f, .824f, .894f, 1);

        GameObject.Find("PlayCharacter").GetComponent<Image>().color = new Color(1, 1, 1, 0);
        GameObject.Find("PlayTitle").GetComponent<Text>().text = "The End";
        GameObject.Find("PlayTitle").GetComponent<Text>().color = new Color(0, 0, 0, 1);

        if (saveFiles)
        {
            if (System.IO.File.Exists(fileNum + "_" + participantName + "/theEnd.png"))
            { ScreenCapture.CaptureScreenshot(fileNum + "_" + participantName + "/theEnd2.png"); }
            else { ScreenCapture.CaptureScreenshot(fileNum + "_" + participantName + "/theEnd.png"); }

        }
        fileNum += 1;
        yield return new WaitForSeconds(3.0f);


        //Stop Recording
        //GameObject.Find("Recorder").GetComponent<Animator>().StopRecording();


        GameObject.Find("PlayWindow").GetComponent<Image>().color = new Color(1, 1, 1, .7098f);
        setTitle();

        //GameObject.Find("PlayInstruction").GetComponent<Text>().color = new Color(0, 0, 0, 1);
        GameObject.FindGameObjectWithTag("play_story_button").GetComponent<Button>().interactable = true;
        GameObject.FindGameObjectWithTag("playscreen_back_button").GetComponent<Button>().interactable = true;
        GameObject.FindGameObjectWithTag("play_story_button").GetComponent<Image>().color = new Color(.235f, .788f, .4f, 1);
        GameObject.Find("PlayText").GetComponent<Text>().color = new Color(1, 1, 1, 1);
        GameObject.FindGameObjectWithTag("playscreen_back_button").GetComponent<Image>().color = new Color(1, 1, 1, 1);

        GameObject.Find("SaveStory").GetComponent<Image>().color = new Color(.235f, .788f, .4f, 1);
        GameObject.Find("SaveText").GetComponent<Text>().color = new Color(1, 1, 1, 1);
        GameObject.Find("SaveStory").GetComponent<Button>().interactable = true;

        if (saveFiles)
        {
            GameObject.Find("CanvasManager").GetComponent<CanvasManagerBottomUp>().toEnd();
        }
        fileNum += 1;
        if (saveFiles)
        {
            StreamWriter writer = new StreamWriter(fileNum + "_" + participantName + "/sceneNotes.text", true);
            writer.WriteLine("SceneNotes: " + GameObject.FindGameObjectWithTag("main_title").GetComponent<Text>().text);
            writer.WriteLine(" ");
            writer.WriteLine(" ");
            writer.WriteLine("Beginning Title: " + GameObject.Find("BegTitle").GetComponent<InputField>().text);
            for (int i = 0; i < beginning.Length; i++)
            {
                writer.WriteLine("Beginning " + i + ": ");


            }
            writer.WriteLine(" ");
            writer.WriteLine("Middle Title: " + GameObject.Find("MidTitle").GetComponent<InputField>().text);
            for (int i = 0; i < middle.Length; i++)
            {
                writer.WriteLine("Middle " + i + ": ");


            }
            writer.WriteLine(" ");
            writer.WriteLine("Ending Title: " + GameObject.Find("EndTitle").GetComponent<InputField>().text);
            for (int i = 0; i < end.Length; i++)
            {
                writer.WriteLine("Ending " + i + ": ");


            }
            writer.WriteLine(" ");
            writer.WriteLine("The End");
            writer.Close();
        }
    }


    /* public IEnumerator TestingCoRoutine()
     {
         yield return new WaitForSeconds(3.0f);
         Debug.Log("3 have passed");
         yield return new WaitForSeconds(4.0f);
         Debug.Log("Done");

     }*/

    public void PlayThrough()
    {
        SlideArrayNoChara[] children = GetComponentsInChildren<SlideArrayNoChara>();
        for (int i = 0; i < children.Length; i++)
        {
            SlideDataNoChara[] grandchildrenData = children[i].GetComponentsInChildren<SlideDataNoChara>();

            for (int k = 0; k < grandchildrenData.Length; k++)
            {
                grandchildrenData[k].playVideo();
                grandchildrenData[k].setPlaying(false);
             
            }
        }
    }

    public void SaveThrough()
    {
        StopAllCoroutines();
        StartCoroutine(SequenceTiming(true));
    }

    private bool EmptySlide()
    {
        SlideArrayNoChara[] children = GetComponentsInChildren<SlideArrayNoChara>();

        for (int i = 0; i < children.Length; i++)
        {
            SlideDataNoChara[] grandchildrenData = children[i].GetComponentsInChildren<SlideDataNoChara>();

            for (int k = 0; k < grandchildrenData.Length; k++)
            {
                if (grandchildrenData[k].isSlideEmpty() == true)
                {
                    return true;
                }

            }
        }

        return false;

    }


    public void nextSlide()
    {
        int newSection;
        int newID;
        int sectionLength;

        updateSelectedVars();
        Debug.Log(selectedSection);
        Debug.Log(selectedID);
        switch (selectedSection)
        {
            case 0:
                sectionLength = begSlideCount;
                break;
            case 1:
                sectionLength = midSlideCount;
                break;
            case 2:
                sectionLength = endSlideCount;
                break;
            default:
                sectionLength = 100;
                break;
        }


        if (selectedID + 1 == sectionLength)
        {
            newSection = selectedSection + 1;
            newID = 0;
            Debug.Log("Next");
        }
        else
        {
            newSection = selectedSection;
            newID = selectedID + 1;
            Debug.Log("NextElse");
        }

        Debug.Log(newSection);
        Debug.Log(newID);
        selectNewRemotely(newSection, newID);
        updateNewEnactmentObjects(newSection, newID);
    }



    public void lastSlide()
    {
        int newSection;
        int newID;
        int sectionLength;

        updateSelectedVars();

        Debug.Log(selectedSection);
        Debug.Log(selectedID);


        if (selectedID == 0)
        {
            newSection = selectedSection - 1;
            switch (newSection)
            {
                case 0:
                    sectionLength = begSlideCount;
                    break;
                case 1:
                    sectionLength = midSlideCount;
                    break;
                case 2:
                    sectionLength = endSlideCount;
                    break;
                default:
                    sectionLength = 100;
                    break;
            }
            newID = sectionLength - 1;
            Debug.Log("Last");

        }
        else
        {
            newSection = selectedSection;
            newID = selectedID - 1;
            Debug.Log("LastElse");
        }

        Debug.Log(newSection);
        Debug.Log(newID);
        selectNewRemotely(newSection, newID);
        updateNewEnactmentObjects(newSection, newID);
    }


    public void recordButton()
    {
        switch (condition)
        {
            case 0:
                if (recordScreenReady)
                {
                    GameObject.FindGameObjectWithTag("all_canvases").GetComponent<CanvasManagerBottomUp>().toEnactment();
                    updateEnactmentObjects();
                    updateEnactmentPose();
                }
                else { GameObject.FindGameObjectWithTag("all_canvases").GetComponent<CanvasManagerBottomUp>().recordScenePop(); }


                break;
            case 1:
                if (recordScreenReady)
                {
                    GameObject.FindGameObjectWithTag("all_canvases").GetComponent<CanvasManagerBottomUp>().toEnactment();
                    updateEnactmentObjects();
                    updateEnactmentPose();
                }
                else { GameObject.FindGameObjectWithTag("all_canvases").GetComponent<CanvasManagerBottomUp>().recordScenePop(); }

                break;
            case 2:
                GameObject.FindGameObjectWithTag("all_canvases").GetComponent<CanvasManagerBottomUp>().toEnactment();
                updateEnactmentObjects();
                updateEnactmentPose();
                break;
            default:
                break;
        }
    }

    public void planningButton()
    {
        switch (condition)
        {
            case 0:

                break;
            case 1:
                break;
            case 2:
                if (planningReady)
                {
                    GameObject.FindGameObjectWithTag("all_canvases").GetComponent<CanvasManagerBottomUp>().planningPop();
                }
                else
                {
                    GameObject.FindGameObjectWithTag("all_canvases").GetComponent<CanvasManagerBottomUp>().planningIncompletePop();
                }

                break;
            default:
                break;
        }

    }

    public int getCondition()
    {
        return condition;
    }

    public bool getAddReady()
    {
        return addReady;
    }

    public void setAddWhich(int w)
    {
        addWhich = w;
    }

    public void bottomAddSlide()
    {
        SlideArrayNoChara[] children = GetComponentsInChildren<SlideArrayNoChara>();

        children[addWhich].addSlideAlone();

        //for each slide unfilled (all previous slides), lock them

        for (int i = 0; i < children.Length; i++)
        {
            SlideDataNoChara[] grandchildrenData = children[i].GetComponentsInChildren<SlideDataNoChara>();

            for (int k = 0; k < grandchildrenData.Length; k++)
            {
                if (grandchildrenData[k].isFilled() == true)
                {
                    grandchildrenData[k].lockScene();
                }

            }
        }

    }

    public int getTotal()
    {
        return slideTotalCount;
    }

    public void writeScene()
    {
        //GameObject.Find("InputSlideText").GetComponent<InputField>().text = getSelectedData().getSceneInfo();
        //GameObject.FindGameObjectWithTag("all_canvases").GetComponent<CanvasManagerBottomUp>().slideInputPop();
        //getSelectedData().setSceneInfo(GameObject.Find());
    }

    public void finishWriteScene()
    {
        getSelectedData().setSceneInfo(GameObject.Find("InputSlideText").GetComponent<InputField>().text);
        GameObject.FindGameObjectWithTag("all_canvases").GetComponent<CanvasManagerBottomUp>().slideInputHide();
    }

    public void setSceneTitle(string s)
    {
        getSelectedData().setSceneInfo(s);
    }

    public void toPlayScreenButton()
    {
        if (playReady)
        {
            GameObject.FindGameObjectWithTag("all_canvases").GetComponent<CanvasManagerBottomUp>().toPlay();
            setTitle();
        }
        else
        {
            GameObject.FindGameObjectWithTag("all_canvases").GetComponent<CanvasManagerBottomUp>().playStoryPop();

        }
    }

    public bool getOptitrackCapture()
    {
        return OptitrackCaptureOn;
    }
}
