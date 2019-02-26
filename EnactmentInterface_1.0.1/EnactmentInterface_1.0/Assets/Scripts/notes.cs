using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class notes : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

/*
            ready = true;
            int num = 1;
            GameObject.FindGameObjectWithTag("record_screen").GetComponent<Button>().interactable = false;

            if (donePlanning == false) { GameObject.FindGameObjectWithTag("planning_button").GetComponent<Button>().interactable = true; }

            SlideArray[] arrays = GetComponentsInChildren<SlideArray>();


            foreach (Transform child in this.transform) //Gets all slide arrays set as children to the GameObject with this script
            {
                foreach (Transform grandchild in child.transform)
                {
                    if (grandchild.GetComponentInChildren<SlideSelectSlide>() != null) //Since the add slide buttons and slides are under the same parent, only objects with a Slide script is assigned a number to its Text component
                    {
                        if (grandchild.GetComponentInChildren<Text>() != null)
                        {
                            grandchild.GetComponentInChildren<Text>().text = num.ToString();
                            num++;
                        }

                        if (grandchild.GetComponentInChildren<SlideSelectSlide>().getSelected() && grandchild.GetComponentInChildren<SlideData>().isFilled())
                        {
                            GameObject.FindGameObjectWithTag("record_screen").GetComponent<Button>().interactable = true;
                        }

                        if (grandchild.GetComponentInChildren<SlideData>().isFilled() != true || (arrays[0].getCurrent() > 0 && arrays[1].getCurrent() > 0 && arrays[2].getCurrent() > 0 == false))
                        {
                            if (ready == false) { GameObject.FindGameObjectWithTag("planning_button").GetComponent<Button>().interactable = false; }
                            ready = false;
                        }
                    }
                }
            }
        }*/

/*
GameObject[] poseButtons = GameObject.FindGameObjectsWithTag("pose_button");

    GameObject playButton = GameObject.FindGameObjectWithTag("play_slide_button");
    GameObject backButton = GameObject.FindGameObjectWithTag("back_button");
    GameObject recordButton = GameObject.FindGameObjectWithTag("record_button");

    if (getSelectedStatus() == true && getSelectedData().isFilled() == true)
    {
        if (getSelectedData().getIsRecord())
        {
            playButton.transform.position = playButtonPosition;

        }
        else
        {
            playButton.transform.position = new Vector3(5000, 5000);
        }


        if (getSelectedData().isSlideAudioPlaying() == true)
        {
            playButton.GetComponent<Image>().sprite = recordStop;
            playButton.GetComponent<Image>().color = new Color(1, 0, 0, 1);

            foreach (GameObject button in poseButtons)
            {
                button.GetComponent<Button>().interactable = false;

            }
            backButton.GetComponent<Button>().interactable = false;
            recordButton.GetComponent<Button>().interactable = false;

        }
        else if (getSelectedData().isSlideAudioPlaying() == false && getSelectedData().getPoseMode() == 0)
        {
            playButton.GetComponent<Image>().sprite = playSprite;
            playButton.GetComponent<Image>().color = new Color(.235f, .788f, .4f, 1);

            foreach (GameObject button in poseButtons)
            {
                button.GetComponent<Button>().interactable = true;

            }
            backButton.GetComponent<Button>().interactable = true;
            recordButton.GetComponent<Button>().interactable = true;
        }
        else
        {
            playButton.GetComponent<Image>().sprite = playSprite;
            playButton.GetComponent<Image>().color = new Color(.235f, .788f, .4f, 1);
            playButton.GetComponent<Button>().interactable = false;

            backButton.GetComponent<Button>().interactable = false;
            recordButton.GetComponent<Button>().interactable = false;
        }

    }

    */