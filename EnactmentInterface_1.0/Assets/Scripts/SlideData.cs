using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class SlideData : MonoBehaviour {

    /*IN PROGRESS - this is a script attached to each slide to record which index of each object type it holds, as well as its recording data*/

    private AudioClip slideClip;
    private int charaIndex;
    private int backdropIndex;
    private int itemIndex;
    private AudioSource slideAudio;
    private int slidePose = 0;
    private bool isItem = false;
    private bool isChara = false;
    private bool isBackdrop = false;
    private bool useGround = false;
    private int groundPosition = 0;
    private int charaPosition = 0;
	// Use this for initialization
	void Start () {
        slideAudio = gameObject.AddComponent<AudioSource>();
        slideClip = new AudioClip();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void startRecord()
    {
        slideAudio.clip = Microphone.Start(null, true, 600, 44100);
        Debug.Log("We have started");
    }

    public void endRecord()
    {
        int timeCut = Microphone.GetPosition(null);
        Microphone.End(null);

        float[] samples = new float[timeCut];
        slideAudio.clip.GetData(samples, 0);


        int freq = slideAudio.clip.frequency;
        slideAudio.clip = AudioClip.Create("SlideSound", samples.Length, 1, freq, false);
        slideAudio.clip.SetData(samples, 0);

        Debug.Log("We have stopped");

        if (slideAudio.clip == null) { Debug.Log("uuhm"); }

    }

    public void playAudio()
    {
        if (slideAudio.clip != null) {
            Debug.Log("well something is playing");
            slideAudio.Play();
           
        }
    }

    public void setChara(int ind)
    {
        charaIndex = ind;
        isChara = true;
    }

    public void setBackdrop(int ind)
    {
        backdropIndex = ind;
        isBackdrop = true;
    }

    public void setItem(int ind)
    {
        itemIndex = ind;
        isItem = true;
    }

    public int getChara()
    {
        return charaIndex;
    }

    public int getBackdrop()
    {
        return backdropIndex;
    }

    public int getItem()
    {
        return itemIndex;
    }

    public void updateEnactmentScreen()
    {
        Sprite chara = GameObject.FindGameObjectWithTag("object_arrays").GetComponent<ObjectArray>().CharaPoseSets[charaIndex].GetComponent<CharaPoses>().poses[slidePose];
        Sprite backdrop = GameObject.FindGameObjectWithTag("object_arrays").GetComponent<ObjectArray>().Backdrops[backdropIndex].GetComponent<Backdrop>().backdrop;
        GameObject item = GameObject.FindGameObjectWithTag("object_arrays").GetComponent<ObjectArray>().Items[itemIndex];
        ItemPoses itempose = GameObject.FindGameObjectWithTag("object_arrays").GetComponent<ItemPoses>();

        if (item!= GameObject.FindGameObjectWithTag("current_item"))
        {
            Destroy(GameObject.FindGameObjectWithTag("current_item"));
            GameObject newItem = (GameObject)Instantiate(item, itempose.getItemPos(slidePose, useGround), item.transform.rotation);
            //newItem.transform.SetParent(GameObject.FindGameObjectWithTag("enactment_canvas").transform);
            //item.transform.position = GameObject.FindGameObjectWithTag("object_arrays").GetComponent<ObjectArray>().CharaPoseSets[charaIndex].GetComponent<CharaPoses>().getItemPos(slidePose);
            newItem.transform.localScale = new Vector3(newItem.GetComponent<Item>().getScale(), newItem.GetComponent<Item>().getScale(), newItem.GetComponent<Item>().getScale());
            newItem.tag = "current_item";
        }
        
        GameObject.Find("EnactmentBackdrop").GetComponent<SpriteRenderer>().sprite = backdrop;
        GameObject.Find("EnactmentCharacter").GetComponent<Image>().sprite = chara;

    }

    public void updateCharaPose()
    {
        Sprite chara = GameObject.FindGameObjectWithTag("object_arrays").GetComponent<ObjectArray>().CharaPoseSets[charaIndex].GetComponent<CharaPoses>().poses[slidePose];
        ItemPoses itempose = GameObject.FindGameObjectWithTag("object_arrays").GetComponent<ItemPoses>();
        GameObject item = GameObject.FindGameObjectWithTag("current_item");

        if (item != null)
        {
            item.transform.position = itempose.getItemPos(slidePose,useGround);
            //item.transform.SetPositionAndRotation(charapose.getItemPos(slidePose), item.transform.rotation);
            Debug.Log(useGround);
            Debug.Log("don't be mad at me I'm trying my bestttt");
        }
        GameObject.Find("EnactmentCharacter").GetComponent<Image>().sprite = chara;

    }

    public void setPose(int sp, bool ground)
    {
        if (!ground)
        {
            slidePose = sp;
            useGround = false;
        }
        else { slidePose = sp;
            useGround = true; }
       
    }

    public bool getGround()
    {
        return useGround;
    }

    public int getPose()
    {
        return slidePose;
    }
    public bool isFilled()
    {
        if (isChara==true && isItem == true && isBackdrop == true) { return true; }
        else { return false; }
    }

}
