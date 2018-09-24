using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManagerBottomUp : MonoBehaviour {

    public Canvas enactmentCanvas;
    public Canvas timelineCanvas;
    public Canvas playCanvas;
    private int whichCanvas = 0; // 0 - timeline, 1 - enactment, 2 - play

	// Use this for initialization
	void Start () {
        enableCanvas(timelineCanvas);
        disableCanvas(playCanvas);
        disableCanvas(enactmentCanvas);
        //SlideArray.addSlide();
        whichCanvas = 0;
    }


    // Update is called once per frame
    void Update () {
		
	}

    //Go to enactment canvas
    public void toEnactment()
    {
       
        disableCanvas(timelineCanvas);
        disableCanvas(playCanvas);
        enableCanvas(enactmentCanvas);

        whichCanvas = 1;
    }

    //Go to timeline canvas
    public void toTimeline()
    {
        enableCanvas(timelineCanvas);
        disableCanvas(playCanvas);
        disableCanvas(enactmentCanvas);

        whichCanvas = 0;
    }

    //Go to play canvas
    public void toPlay()
    {
        disableCanvas(timelineCanvas);
        enableCanvas(playCanvas);
        disableCanvas(enactmentCanvas);

        whichCanvas = 0;
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
}
