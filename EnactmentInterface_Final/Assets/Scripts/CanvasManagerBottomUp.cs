using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManagerBottomUp : MonoBehaviour {

    public int enactmentCondition; // 0 = Cartoon enactment (transformed self) 1 = video enactment (non-transformed self)
    public Canvas enactmentCanvas;
    public Canvas timelineCanvas;
    public Canvas playCanvas;
    public Canvas endCanvas;
    public Canvas startCanvas;
    public Canvas videoTimelineCanvas;
    public Canvas videoEnactmentCanvas;
    public Canvas videoPlayCanvas;
    public Canvas videoSinglePlayCanvas;
    public Canvas cartoonSinglePlayCanvas;
    private int whichCanvas = 0; // 0 - timeline, 1 - enactment, 2 - play, 3-end, 4-start, 5- videoTimeline, 6- videoEnactment, 7- videoPlay 8- SinglePlayVid 9 - Single play cartoon 
    //canvas 5,6,7 are for non-transformed self codition

    public CanvasGroup saveStoryPopUp;
    public CanvasGroup planningPopUp;
    public CanvasGroup playStoryPopUp;
    public CanvasGroup recordScenePopUp;
    public CanvasGroup slideInputPopUp;
    public CanvasGroup planningIncompletePopUp;
    public CanvasGroup addSlidePopUp;
    public CanvasGroup addingIncompletePopUp;

    private int whichPopUp = 0; // 0 - saveStory, 1 - planning, 2 - playStory, 3- recordScene, 4- slideInput 5- planningIncomplete 6-addSlide 7-addingIncomplete


	// Use this for initialization
	void Start () {
        //foreach (var device in Microphone.devices)
        //{
        //    Debug.Log("Name: " + device);
        //}

        Debug.Log("Name: " + Microphone.devices[0]);

        toStart();

        disableCanvasGroup(saveStoryPopUp);
        disableCanvasGroup(planningPopUp);
        disableCanvasGroup(playStoryPopUp);
        disableCanvasGroup(recordScenePopUp);
        disableCanvasGroup(slideInputPopUp);
        disableCanvasGroup(planningIncompletePopUp);

    }

    // Update is called once per frame
    void Update () {
        if (whichCanvas == 4) {
            if (GameObject.Find("InputParticipantID").GetComponent<InputField>().text == "" )
            {
                GameObject.Find("StartApp").GetComponent<Button>().interactable = false;
            }
            else
            {
                
                GameObject.Find("StartApp").GetComponent<Button>().interactable = true;
            }
        }
	}

    //Go to enactment canvas
    public void toEnactment()
    {
        if (enactmentCondition != 0)
        {
            disableCanvas(videoTimelineCanvas);
            disableCanvas(playCanvas);
            disableCanvas(enactmentCanvas);
            disableCanvas(endCanvas);
            disableCanvas(startCanvas);
            disableCanvas(timelineCanvas);
            enableCanvas(videoEnactmentCanvas);
            disableCanvas(videoPlayCanvas);
            disableCanvas(videoSinglePlayCanvas);
            whichCanvas = 6;
        }
        else
        {
            disableCanvas(timelineCanvas);
            disableCanvas(playCanvas);
            enableCanvas(enactmentCanvas);
            disableCanvas(endCanvas);
            disableCanvas(startCanvas);
            disableCanvas(timelineCanvas);
            disableCanvas(videoEnactmentCanvas);
            disableCanvas(videoPlayCanvas);
            disableCanvas(cartoonSinglePlayCanvas);
            whichCanvas = 1;
            //GameObject.FindGameObjectWithTag("frame").GetComponent<RawImage>().enabled = false; 
        }

    }

    //Go to timeline canvas
    public void toTimeline()
    {
        if (enactmentCondition != 0)
        {
            enableCanvas(videoTimelineCanvas);
            disableCanvas(playCanvas);
            disableCanvas(enactmentCanvas);
            StopAllCoroutines();
            disableCanvas(endCanvas);
            disableCanvas(startCanvas);
            disableCanvas(timelineCanvas);
            disableCanvas(videoEnactmentCanvas);
            disableCanvas(videoPlayCanvas);
            disableCanvas(videoSinglePlayCanvas);
            whichCanvas = 5;
        }
        else
        {
            enableCanvas(timelineCanvas);
            disableCanvas(playCanvas);
            disableCanvas(enactmentCanvas);
            StopAllCoroutines();
            disableCanvas(endCanvas);
            disableCanvas(startCanvas);
            disableCanvas(videoTimelineCanvas);
            disableCanvas(videoEnactmentCanvas);
            disableCanvas(videoPlayCanvas);
            whichCanvas = 0;
        }

    }

    //Go to play canvas
    public void toPlay()
    {
        if (enactmentCondition != 0)
        {
            disableCanvas(videoTimelineCanvas);
            disableCanvas(playCanvas);
            disableCanvas(enactmentCanvas);
            disableCanvas(endCanvas);
            disableCanvas(startCanvas);
            disableCanvas(timelineCanvas);
            disableCanvas(videoEnactmentCanvas);
            disableCanvas(videoPlayCanvas);
            enableCanvas(videoSinglePlayCanvas);
            whichCanvas = 8;

            //GameObject.Find("AVProVideo").GetComponent<video>().playAgain();
        }
        else
        {
            disableCanvas(timelineCanvas);
            disableCanvas(playCanvas);
            disableCanvas(enactmentCanvas);
            disableCanvas(endCanvas);
            disableCanvas(startCanvas);
            disableCanvas(videoTimelineCanvas);
            disableCanvas(videoEnactmentCanvas);
            disableCanvas(videoPlayCanvas);
            enableCanvas(cartoonSinglePlayCanvas);
            whichCanvas = 9;
        }
  
    }


    public void toPlayAll()
    {
        if (enactmentCondition != 0)
        {
            disableCanvas(videoTimelineCanvas);
            enableCanvas(playCanvas);
            disableCanvas(enactmentCanvas);
            disableCanvas(endCanvas);
            disableCanvas(startCanvas);
            disableCanvas(timelineCanvas);
            disableCanvas(videoEnactmentCanvas);
            disableCanvas(videoPlayCanvas);
            disableCanvas(videoSinglePlayCanvas);
            //whichCanvas = 8;

            //GameObject.Find("AVProVideo").GetComponent<video>().playAgain();
        }
        else
        {
            disableCanvas(timelineCanvas);
            enableCanvas(playCanvas);
            disableCanvas(enactmentCanvas);
            disableCanvas(endCanvas);
            disableCanvas(startCanvas);
            disableCanvas(videoTimelineCanvas);
            disableCanvas(videoEnactmentCanvas);
            disableCanvas(videoPlayCanvas);
            disableCanvas(cartoonSinglePlayCanvas);
            //whichCanvas = 9;
        }

    }



    public void toStart()
    {
        //if (enactmentCondition != 0)
        //{
        //    enableCanvas(videoTimelineCanvas);
        //    disableCanvas(playCanvas);
        //    disableCanvas(enactmentCanvas);
        //    disableCanvas(endCanvas);
        //    disableCanvas(startCanvas);
        //    disableCanvas(timelineCanvas);
        //    disableCanvas(videoEnactmentCanvas);
        //    disableCanvas(videoPlayCanvas);
        //    whichCanvas = 5;
        //}
        //else
        //{
            disableCanvas(timelineCanvas);
            disableCanvas(playCanvas);
            disableCanvas(enactmentCanvas);
            disableCanvas(endCanvas);
            enableCanvas(startCanvas);
            disableCanvas(videoTimelineCanvas);
            disableCanvas(videoEnactmentCanvas);
            disableCanvas(videoPlayCanvas);
            disableCanvas(videoSinglePlayCanvas);
            whichCanvas = 4;
        //}
        
    }

    public void toEnd()
    {
        disableCanvas(timelineCanvas);
        disableCanvas(playCanvas);
        disableCanvas(enactmentCanvas);
        enableCanvas(endCanvas);
        disableCanvas(startCanvas);
        whichCanvas = 3;
    }

    void disableCanvas(Canvas can)
    {
        can.GetComponent<CanvasGroup>().alpha = 0;
        can.GetComponent<CanvasGroup>().interactable = false;
        can.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    void enableCanvas(Canvas can)
    {
        can.GetComponent<CanvasGroup>().alpha = 1;
        can.GetComponent<CanvasGroup>().interactable = true;
        can.GetComponent<CanvasGroup>().blocksRaycasts = true;
        can.transform.SetAsLastSibling();
    }

    void disableCanvasGroup(CanvasGroup can)
    {
        can.alpha = 0;
        can.interactable = false;
        can.blocksRaycasts = false;
    }

    void enableCanvasGroup(CanvasGroup can)
    {
        can.alpha = 1;
        can.interactable = true;
        can.blocksRaycasts = true;
        
    }

    public int getWhichCanvas()
    {
        return whichCanvas;
    }

    public void quitApp()
    {
        Application.Quit();
    }


    // 0 - saveStory, 1 - planning, 2 - playStory, 3- recordScene, 4- slideInput 5- planningIncomplete 6-addSlide 7-addingIncomplete

    public void saveStoryPop()
    {
        whichPopUp = 0;
        enableCanvasGroup(saveStoryPopUp);
        playCanvas.GetComponent<CanvasGroup>().interactable = false;
    }

    public void planningPop()
    {
        whichPopUp = 1;
        enableCanvasGroup(planningPopUp);
        timelineCanvas.GetComponent<CanvasGroup>().interactable = false;
    }

    public void playStoryPop()
    {
        whichPopUp = 2;
        enableCanvasGroup(playStoryPopUp);
        timelineCanvas.GetComponent<CanvasGroup>().interactable = false;
    }

    public void recordScenePop()
    {
        whichPopUp = 3;
        enableCanvasGroup(recordScenePopUp);
        timelineCanvas.GetComponent<CanvasGroup>().interactable = false;
    }

    public void slideInputPop()
    {
        whichPopUp = 4;
        enableCanvasGroup(slideInputPopUp);
        timelineCanvas.GetComponent<CanvasGroup>().interactable = false;
    }

    public void planningIncompletePop()
    {
        whichPopUp = 5;
        enableCanvasGroup(planningIncompletePopUp);
        timelineCanvas.GetComponent<CanvasGroup>().interactable = false;
    }

    public void planningIncompleteHide()
    {
        whichPopUp = 5;
        disableCanvasGroup(planningIncompletePopUp);
        timelineCanvas.GetComponent<CanvasGroup>().interactable = true;
    }

    public void addingIncompletePop()
    {
        whichPopUp = 7;
        enableCanvasGroup(addingIncompletePopUp);
        timelineCanvas.GetComponent<CanvasGroup>().interactable = false;
    }

    public void addingIncompleteHide()
    {
        whichPopUp = 7;
        disableCanvasGroup(addingIncompletePopUp);
        timelineCanvas.GetComponent<CanvasGroup>().interactable = true;
    }

    public void addSlidePop()
    {
        whichPopUp = 6;
        enableCanvasGroup(addSlidePopUp);
        timelineCanvas.GetComponent<CanvasGroup>().interactable = false;
    }

    public void addSlideHide()
    {
        whichPopUp = 6;
        disableCanvasGroup(addSlidePopUp);
        timelineCanvas.GetComponent<CanvasGroup>().interactable = true;
    }

    public void saveStoryHide()
    {
        whichPopUp = 0;
        disableCanvasGroup(saveStoryPopUp);
        playCanvas.GetComponent<CanvasGroup>().interactable = true;
    }

    public void planningHide()
    {
        whichPopUp = 1;
        disableCanvasGroup(planningPopUp);
        timelineCanvas.GetComponent<CanvasGroup>().interactable = true;
    }

    public void playStoryHide()
    {
        whichPopUp = 2;
        disableCanvasGroup(playStoryPopUp);
        timelineCanvas.GetComponent<CanvasGroup>().interactable = true;
    }

    public void recordSceneHide()
    {
        whichPopUp = 3;
        disableCanvasGroup(recordScenePopUp);
        timelineCanvas.GetComponent<CanvasGroup>().interactable = true;
    }

    public void slideInputHide()
    {
        whichPopUp = 4;
        disableCanvasGroup(slideInputPopUp);
        timelineCanvas.GetComponent<CanvasGroup>().interactable = true;
    }

    public void generalHide()
    {
        
        switch (whichPopUp)
        {
            case 0:
                saveStoryHide();
                break;
            case 1:
                planningHide();
                break;
            case 2:
                playStoryHide();
                break;
            case 3:
                recordSceneHide();
                break;
            case 4:
                slideInputHide();
                break;
            case 5:
                planningIncompleteHide();
                break;
            case 6:
                addSlideHide();
                break;
            case 7:
                addingIncompleteHide();
                break;
            default:
                saveStoryHide();
                break;
        }

    }

    public int getEnactmentCondition()
    {
        return enactmentCondition;
    }

}
