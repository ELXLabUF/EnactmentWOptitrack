using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class SlideData : MonoBehaviour {

    /*IN PROGRESS - this is a script attached to each slide to record which index of each object type it holds, as well as its recording data*/

    private AudioClip slideClip;
    private int charaIndex=0;
    private int backdropIndex=0;
    private int itemIndex=0;
    private AudioSource slideAudio;

    private string sceneInfo = "";
   

    private int slidePose = 0;
    private bool isItem = false;
    private bool isChara = false;
    private bool isBackdrop = false;
    private bool useGround = false;
    private bool isRecord = false;
    private bool isRecording = false;
    private int charaPosition = 3;
    private int objectPosition = 3;

    private bool isLocked = false;

    private float audioTime = 0.0f;

    private int poseMode = 0;

    public Sprite recordStop;
    public Sprite playSprite;
    //0=default, 1=charaposition, 2=objectposition

	// Use this for initialization
	void Start () {
        slideAudio = gameObject.AddComponent<AudioSource>();
        slideClip = new AudioClip();


    }

    // Update is called once per frame
    void Update () {
       


    }


    public void lockScene()
    {
        isLocked = true;
    }

    public bool getLock()
    {
        return isLocked;
    }

    public string getSceneInfo()
    {
        return sceneInfo;
    }

    public void setSceneInfo(string s)
    {
        sceneInfo = s;
    }

    public void UpdatePlayRecordButton(int beg, int mid, int end, int total, int sect, int id)
    {
        GameObject[] poseButtons = GameObject.FindGameObjectsWithTag("pose_button");
        GameObject[] charaPosButtons = GameObject.FindGameObjectsWithTag("chara_position");
        GameObject[] objPosButtons = GameObject.FindGameObjectsWithTag("obj_position");

        GameObject playButton = GameObject.FindGameObjectWithTag("play_slide_button");
        GameObject backButton = GameObject.FindGameObjectWithTag("back_button");
        GameObject recordButton = GameObject.FindGameObjectWithTag("record_button");

        int trueID=100;

        bool nextTrue;
        bool lastTrue;

        if (sect==0)
        {
            trueID = id;
        }
        else if (sect==1)
        {
            trueID = beg + id;
        }
        else if (sect==2)
        {
            trueID = beg + mid + id;
        }

        if (trueID==0)
        {
            nextTrue = true;
            lastTrue = false;
        }
        else if (trueID==total-1)
        {
            nextTrue = false;
            lastTrue = true;
        }
        else
        {
            nextTrue = true;
            lastTrue = true;
        }

        

        //if (GameObject.Find("NextSlide")) { GameObject.Find("NextSlide").GetComponent<Button>().interactable = true; }
        //if (GameObject.Find("LastSlide")) { GameObject.Find("LastSlide").GetComponent<Button>().interactable = true; }

        switch (poseMode)
        {
            case 1:
                
                playButton.GetComponent<Button>().interactable = false;
                backButton.GetComponent<Button>().interactable = false;
                recordButton.GetComponent<Button>().interactable = false;

                if (GameObject.Find("NextSlide") && nextTrue)
                {
                    GameObject.Find("NextSlide").GetComponent<Button>().interactable = false;
                    GameObject.Find("NextSlide").GetComponent<Image>().color = new Color(.235f, .788f, .4f, 1);
                    GameObject.Find("NextText").GetComponent<Text>().color = new Color(1, 1, 1, 1);
                }
                else if (GameObject.Find("NextSlide"))
                {
                    GameObject.Find("NextSlide").GetComponent<Button>().interactable = false;
                    GameObject.Find("NextSlide").GetComponent<Image>().color = new Color(1, 1, 1, 0);
                    GameObject.Find("NextText").GetComponent<Text>().color = new Color(1, 1, 1, 0);
                }
                if (GameObject.Find("LastSlide") && lastTrue)
                {
                    GameObject.Find("LastSlide").GetComponent<Button>().interactable = false;
                    GameObject.Find("LastSlide").GetComponent<Image>().color = new Color(.235f, .788f, .4f, 1);
                    GameObject.Find("LastText").GetComponent<Text>().color = new Color(1, 1, 1, 1);
                }
                else if (GameObject.Find("NextSlide"))
                {
                    GameObject.Find("LastSlide").GetComponent<Button>().interactable = false;
                    GameObject.Find("LastSlide").GetComponent<Image>().color = new Color(1, 1, 1, 0);
                    GameObject.Find("LastText").GetComponent<Text>().color = new Color(1, 1, 1, 0);
                }

                foreach (GameObject button in poseButtons)
                {
                    button.GetComponent<Button>().interactable = false;

                }
                foreach (GameObject button in charaPosButtons)
                {
                    button.GetComponent<Button>().interactable = true;
                    button.GetComponent<Image>().color = new Color(1, 1, 1, .75f);
                    button.transform.SetAsLastSibling();
                }
                foreach (GameObject button in objPosButtons)
                {
                    //button.transform.SetSiblingIndex(0);
                    button.GetComponent<Button>().interactable = false;
                    button.GetComponent<Image>().color = new Color(1, 1, 1, 0);

                }
                GameObject.FindGameObjectWithTag("enactment_check").GetComponent<Button>().interactable = true;
                GameObject.FindGameObjectWithTag("enactment_check").GetComponent<Image>().color = new Color(1, 1, 1, 1);
                GameObject.FindGameObjectWithTag("back_button").GetComponent<Button>().interactable = false;

                setCharaPos(charaPosition);
                break;
            case 2:
                
                foreach (GameObject button in charaPosButtons)
                {
                    button.GetComponent<Button>().interactable = false;
                    button.GetComponent<Image>().color = new Color(1, 1, 1, 0);
                    //button.transform.SetSiblingIndex(0);
                }
                foreach (GameObject button in objPosButtons)
                {
                    button.GetComponent<Button>().interactable = true;
                    button.GetComponent<Image>().color = new Color(1, 1, 1, .75f);
                    button.transform.SetAsLastSibling();
                }
                setObjectPos(objectPosition);
                break;
            case 0:
                foreach (GameObject button in objPosButtons)
                {
                    button.GetComponent<Button>().interactable = false;
                    button.GetComponent<Image>().color = new Color(1, 1, 1, 0);
                }
                foreach (GameObject button in charaPosButtons)
                {
                    button.GetComponent<Button>().interactable = false;
                    button.GetComponent<Image>().color = new Color(1, 1, 1, 0);
                    //button.transform.SetSiblingIndex(0);
                }
                GameObject.FindGameObjectWithTag("enactment_check").GetComponent<Button>().interactable = false;
                GameObject.FindGameObjectWithTag("enactment_check").GetComponent<Image>().color = new Color(1, 1, 1, 0);
                if (!isRecording && !slideAudio.isPlaying)
                {
                    if (GameObject.Find("NextSlide")&&nextTrue) {
                        GameObject.Find("NextSlide").GetComponent<Button>().interactable = true;
                        GameObject.Find("NextSlide").GetComponent<Image>().color = new Color(.235f, .788f, .4f, 1);
                        GameObject.Find("NextText").GetComponent<Text>().color = new Color(1, 1, 1, 1);
                    }
                    else if(GameObject.Find("NextSlide")) {
                        GameObject.Find("NextSlide").GetComponent<Button>().interactable = false;
                        GameObject.Find("NextSlide").GetComponent<Image>().color = new Color(1, 1, 1, 0);
                        GameObject.Find("NextText").GetComponent<Text>().color = new Color(1, 1, 1, 0);
                    }
                    if (GameObject.Find("LastSlide")&& lastTrue) {
                        GameObject.Find("LastSlide").GetComponent<Button>().interactable = true;
                        GameObject.Find("LastSlide").GetComponent<Image>().color = new Color(.235f, .788f, .4f, 1);
                        GameObject.Find("LastText").GetComponent<Text>().color = new Color(1, 1, 1, 1);
                    }
                    else if (GameObject.Find("NextSlide"))
                    {
                        GameObject.Find("LastSlide").GetComponent<Button>().interactable = false;
                        GameObject.Find("LastSlide").GetComponent<Image>().color = new Color(1, 1, 1, 0);
                        GameObject.Find("LastText").GetComponent<Text>().color = new Color(1, 1, 1, 0);
                    }

                    foreach (GameObject button in poseButtons)
                    {
                        button.GetComponent<Button>().interactable = true;
                    }
                    
                    GameObject.FindGameObjectWithTag("back_button").GetComponent<Button>().interactable = true;
                    
                    recordButton.GetComponent<Button>().interactable = true;

                    if (isRecord) { playButton.GetComponent<Button>().interactable = true;}
                    else { playButton.GetComponent<Button>().interactable = false; }
                    playButton.GetComponent<Image>().sprite = playSprite;
                    playButton.GetComponent<Image>().color = new Color(.235f, .788f, .4f, 1);
                }
                else if (isRecording)
                {
                    foreach (GameObject button in poseButtons)
                    {
                        button.GetComponent<Button>().interactable = false;
                    }
                    GameObject.FindGameObjectWithTag("back_button").GetComponent<Button>().interactable = false;
                    playButton.GetComponent<Button>().interactable = false;
                    recordButton.GetComponent<Button>().interactable = true;
                    playButton.GetComponent<Image>().sprite = playSprite;
                    playButton.GetComponent<Image>().color = new Color(.235f, .788f, .4f, 1);

                    if (GameObject.Find("NextSlide") && nextTrue)
                    {
                        GameObject.Find("NextSlide").GetComponent<Button>().interactable = false;
                        GameObject.Find("NextSlide").GetComponent<Image>().color = new Color(.235f, .788f, .4f, 1);
                        GameObject.Find("NextText").GetComponent<Text>().color = new Color(1, 1, 1, 1);
                    }
                    else if (GameObject.Find("NextSlide"))
                    {
                        GameObject.Find("NextSlide").GetComponent<Button>().interactable = false;
                        GameObject.Find("NextSlide").GetComponent<Image>().color = new Color(1, 1, 1, 0);
                        GameObject.Find("NextText").GetComponent<Text>().color = new Color(1, 1, 1, 0);
                    }
                    if (GameObject.Find("LastSlide") && lastTrue)
                    {
                        GameObject.Find("LastSlide").GetComponent<Button>().interactable = false;
                        GameObject.Find("LastSlide").GetComponent<Image>().color = new Color(.235f, .788f, .4f, 1);
                        GameObject.Find("LastText").GetComponent<Text>().color = new Color(1, 1, 1, 1);
                    }
                    else if (GameObject.Find("NextSlide"))
                    {
                        GameObject.Find("LastSlide").GetComponent<Button>().interactable = false;
                        GameObject.Find("LastSlide").GetComponent<Image>().color = new Color(1, 1, 1, 0);
                        GameObject.Find("LastText").GetComponent<Text>().color = new Color(1, 1, 1, 0);
                    }

                }
                else if (slideAudio.isPlaying)
                {
                    foreach (GameObject button in poseButtons)
                    {
                        button.GetComponent<Button>().interactable = false;
                    }
                    GameObject.FindGameObjectWithTag("back_button").GetComponent<Button>().interactable = false;
                    playButton.GetComponent<Button>().interactable = true;
                    recordButton.GetComponent<Button>().interactable = false;
                    playButton.GetComponent<Image>().sprite = recordStop;
                    playButton.GetComponent<Image>().color = new Color(1, 0, 0, 1);

                    if (GameObject.Find("NextSlide") && nextTrue)
                    {
                        GameObject.Find("NextSlide").GetComponent<Button>().interactable = false;
                        GameObject.Find("NextSlide").GetComponent<Image>().color = new Color(.235f, .788f, .4f, 1);
                        GameObject.Find("NextText").GetComponent<Text>().color = new Color(1, 1, 1, 1);
                    }
                    else if (GameObject.Find("NextSlide"))
                    {
                        GameObject.Find("NextSlide").GetComponent<Button>().interactable = false;
                        GameObject.Find("NextSlide").GetComponent<Image>().color = new Color(1, 1, 1, 0);
                        GameObject.Find("NextText").GetComponent<Text>().color = new Color(1, 1, 1, 0);
                    }
                    if (GameObject.Find("LastSlide") && lastTrue)
                    {
                        GameObject.Find("LastSlide").GetComponent<Button>().interactable = false;
                        GameObject.Find("LastSlide").GetComponent<Image>().color = new Color(.235f, .788f, .4f, 1);
                        GameObject.Find("LastText").GetComponent<Text>().color = new Color(1, 1, 1, 1);
                    }
                    else if (GameObject.Find("NextSlide"))
                    {
                        GameObject.Find("LastSlide").GetComponent<Button>().interactable = false;
                        GameObject.Find("LastSlide").GetComponent<Image>().color = new Color(1, 1, 1, 0);
                        GameObject.Find("LastText").GetComponent<Text>().color = new Color(1, 1, 1, 0);
                    }

                }

                break;

            default:
                break;


        }
    }

    public void startRecord()
    {
        
        slideAudio.clip = Microphone.Start(null, true, 600, 44100);
        Debug.Log("We have started");
        isRecording = true;
    }

    public void endRecord()
    {
        isRecording = false;
        int timeCut = Microphone.GetPosition(null);
        Microphone.End(null);

        float[] samples = new float[timeCut];
        slideAudio.clip.GetData(samples, 0);


        int freq = slideAudio.clip.frequency;
        slideAudio.clip = AudioClip.Create("SlideSound", samples.Length, 1, freq, false);
        slideAudio.clip.SetData(samples, 0);

        audioTime = samples.Length / freq;
        //Debug.Log("We have stopped");

        isRecord = true;
        if (slideAudio.clip == null) { }//Debug.Log("uuhm"); }

    }

    public bool isSlideEmpty()
    {
        if (isRecord == false || isItem == false || isBackdrop == false || isChara == false || sceneInfo == "")
        {
            return true;
        }

        return false;
    }

    public float getTime()
    {
        return slideAudio.clip.length + .5f ;
    }

    public bool getIsRecord()
    {
        return isRecord;
    }

    public void playAudio()
    {
        

        if (slideAudio.clip != null) {
            //Debug.Log("well something is playing");

            if (slideAudio.isPlaying==false) {
                slideAudio.Play();

                
            }
            else
            {
                slideAudio.Stop();
                
            }
           
        }
    }

    public AudioClip getAudio()
    {
        return slideAudio.clip;
    }

    public bool isSlideAudioPlaying()
    {
        if (slideAudio.clip != null)
        {
            if (slideAudio.isPlaying == true) {
                return true;
            }
            else { return false; }
        }
        return false;
    }

    public void updatePoseMode()
    {
       
        switch (poseMode)
        {
            case 0:
                poseMode = 1;
                
                break;
            case 1:
                poseMode = 2;
               
                break;
            case 2:
                poseMode = 0;
                
                break;

            default:
                break;


        }

    }

    public void setCharaPos(int ind)
    {
        charaPosition = ind;

        GameObject.Find("chara_left").GetComponent<Image>().color = new Color(1, 1, 1, .75f);
        GameObject.Find("chara_right").GetComponent<Image>().color = new Color(1, 1, 1, .75f);
        GameObject.Find("chara_up").GetComponent<Image>().color = new Color(1, 1, 1, .75f);
        GameObject.Find("chara_mid").GetComponent<Image>().color = new Color(1, 1, 1, .75f);

        switch (charaPosition)
        {
            case 0:
                GameObject.Find("chara_left").GetComponent<Image>().color = new Color(1, 1, 1, 0);

                break;
            case 1:
                GameObject.Find("chara_right").GetComponent<Image>().color = new Color(1, 1, 1, 0);

                break;
            case 2:
                GameObject.Find("chara_up").GetComponent<Image>().color = new Color(1, 1, 1, 0);

                break;
            case 3:
                GameObject.Find("chara_mid").GetComponent<Image>().color = new Color(1, 1, 1, 0);

                break;
            default:
                break;

        }
    }

    public void setObjectPos(int ind)
    {
        objectPosition = ind;

        GameObject.Find("obj_left").GetComponent<Image>().color = new Color(1, 1, 1, .75f);
        GameObject.Find("obj_right").GetComponent<Image>().color = new Color(1, 1, 1, .75f);
        GameObject.Find("obj_up").GetComponent<Image>().color = new Color(1, 1, 1, .75f);
        GameObject.Find("obj_mid").GetComponent<Image>().color = new Color(1, 1, 1, .75f);

        switch (objectPosition)
        {
            case 0:
                GameObject.Find("obj_left").GetComponent<Image>().color = new Color(1, 1, 1, 0);

                break;
            case 1:
                GameObject.Find("obj_right").GetComponent<Image>().color = new Color(1, 1, 1, 0);

                break;
            case 2:
                GameObject.Find("obj_up").GetComponent<Image>().color = new Color(1, 1, 1, 0);

                break;
            case 3:
                GameObject.Find("obj_mid").GetComponent<Image>().color = new Color(1, 1, 1, 0);

                break;
            default:
                break;

        }
    }

    public void setChara(int ind)
    {
        if (isLocked == false)
        {
            charaIndex = ind;
            isChara = true;
        }
    }

    public void setBackdrop(int ind)
    {
        if (isLocked == false)
        {
            backdropIndex = ind;
            isBackdrop = true;
        }
    }

    public void setItem(int ind)
    {
        if (isLocked == false)
        {
            itemIndex = ind;
            isItem = true;
        }
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

    public int getPoseMode()
    {
        return poseMode;
    }

    public void updateEnactmentScreen()
    {
       
        //Destroy(GameObject.FindGameObjectWithTag("current_item"));
        Sprite backdrop = GameObject.FindGameObjectWithTag("object_arrays").GetComponent<ObjectArray>().Backdrops[backdropIndex].GetComponent<Backdrop>().backdrop;
        GameObject.Find("EnactmentBackdrop").GetComponent<SpriteRenderer>().sprite = backdrop;

        //Niloofar: need to update these - create empty GameObjects and assign the relevant prefabs to them
        //updateCharaPose(false);
        ///updateCharaPos(true);

        updateItem();

        updateCharacter();

        GameObject.Find("SceneInfo").GetComponent<Text>().text = sceneInfo;
    }

    public void updateCharacter()
    {
        if(GameObject.FindGameObjectsWithTag("current_chara").Length != 0)
        {
            GameObject[] currents;

            currents = GameObject.FindGameObjectsWithTag("current_chara");

            foreach (GameObject current in currents)
            {
                Destroy(current);
            }
        }

        GameObject chara = GameObject.FindGameObjectWithTag("object_arrays").GetComponent<ObjectArray>().CharaPoseSets[charaIndex];
        GameObject newChara = Instantiate(chara, chara.transform.position, chara.transform.rotation);
        newChara.tag = "current_chara";
    }

    public void updateItem()
    {
        if (GameObject.FindGameObjectsWithTag("current_item").Length != 0)
        {
            GameObject[] currents;

            currents = GameObject.FindGameObjectsWithTag("current_item");

            foreach (GameObject current in currents)
            {
                Destroy(current);
            }
        }

        GameObject item = GameObject.FindGameObjectWithTag("object_arrays").GetComponent<ObjectArray>().Items[itemIndex];
        GameObject newItem = Instantiate(item, item.transform.position, item.transform.rotation);
        newItem.transform.localScale = new Vector3(newItem.GetComponent<Item>().getScale(), newItem.GetComponent<Item>().getScale(), newItem.GetComponent<Item>().getScale());
        newItem.tag = "current_item";
    }

    public void updateItemPos(bool repose)
    {
        GameObject item = GameObject.FindGameObjectWithTag("object_arrays").GetComponent<ObjectArray>().Items[itemIndex];
        ItemPoses itempose = GameObject.FindGameObjectWithTag("object_arrays").GetComponent<ItemPoses>();

        if (item != GameObject.FindGameObjectWithTag("current_item")&&repose==true)
        {
            GameObject[] currents;

            currents = GameObject.FindGameObjectsWithTag("current_item");

            foreach (GameObject current in currents)
            {
                Destroy(current);
            }
            //Destroy(GameObject.FindGameObjectWithTag("current_item"));
            GameObject newItem = (GameObject)Instantiate(item, itempose.getItemPos(slidePose, useGround, charaPosition, objectPosition, slidePose), item.transform.rotation);
            //newItem.transform.SetParent(GameObject.FindGameObjectWithTag("enactment_canvas").transform);
            //item.transform.position = GameObject.FindGameObjectWithTag("object_arrays").GetComponent<ObjectArray>().CharaPoseSets[charaIndex].GetComponent<CharaPoses>().getItemPos(slidePose);
            newItem.transform.localScale = new Vector3(newItem.GetComponent<Item>().getScale(), newItem.GetComponent<Item>().getScale(), newItem.GetComponent<Item>().getScale());
            newItem.tag = "current_item";
        }

    }

    public void updateCharaPose(bool repose)
    {
        //Sprite chara = GameObject.FindGameObjectWithTag("object_arrays").GetComponent<ObjectArray>().CharaPoseSets[charaIndex].GetComponent<CharaPoses>().poses[slidePose];
        //GameObject chara = GameObject.FindGameObjectWithTag("object_arrays").GetComponent<ObjectArray>().CharaPoseSets[charaIndex];
        //GameObject.Find("EnactmentCharacter").GetComponent<Image>().sprite = chara;
        //GameObject.Find("EnactmentCharacter").transform.position = itempose.getCharaPos(charaIndex);
        updateItemPos(repose);
    }

    public void updateCharaPos(bool repose)
    {
        //Sprite chara = GameObject.FindGameObjectWithTag("object_arrays").GetComponent<ObjectArray>().CharaPoseSets[charaIndex].GetComponent<CharaPoses>().poses[slidePose];
        //ItemPoses itempose = GameObject.FindGameObjectWithTag("object_arrays").GetComponent<ItemPoses>();
        

        ///GameObject.Find("EnactmentCharacter").transform.localPosition = itempose.getCharaPos(charaPosition);
        //updateItemPos(repose);
    }

    public void setPose(int sp)
    {
        slidePose = sp;
       
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
        if (isChara==true && isItem == true && isBackdrop == true && sceneInfo!="") { return true; }
        else { return false; }
    }

    public void updatePlayScreen()
    {
        Sprite chara = GameObject.FindGameObjectWithTag("object_arrays").GetComponent<ObjectArray>().CharaPoseSets[charaIndex].GetComponent<CharaPoses>().poses[slidePose];
        Sprite backdrop = GameObject.FindGameObjectWithTag("object_arrays").GetComponent<ObjectArray>().Backdrops[backdropIndex].GetComponent<Backdrop>().backdrop;
        GameObject item = GameObject.FindGameObjectWithTag("object_arrays").GetComponent<ObjectArray>().Items[itemIndex];
        ItemPoses itempose = GameObject.FindGameObjectWithTag("object_arrays").GetComponent<ItemPoses>();

        GameObject.Find("EnactmentBackdrop").GetComponent<SpriteRenderer>().sprite = backdrop;

        //Destroy(GameObject.FindGameObjectWithTag("current_item"));

        if (item != GameObject.FindGameObjectWithTag("current_item"))
        {
            //Destroy(GameObject.FindGameObjectWithTag("current_item"));
            GameObject[] currents;

            currents = GameObject.FindGameObjectsWithTag("current_item");

            foreach (GameObject current in currents)
            {
                Destroy(current);
            }
            GameObject newItem = (GameObject)Instantiate(item, itempose.getItemPos(slidePose, useGround, charaPosition, objectPosition, slidePose), item.transform.rotation);
            newItem.transform.localScale = new Vector3(newItem.GetComponent<Item>().getScale(), newItem.GetComponent<Item>().getScale(), newItem.GetComponent<Item>().getScale());
            newItem.tag = "current_item";
        }

        GameObject.Find("PlayCharacter").GetComponent<Image>().sprite = chara;
        GameObject.Find("PlayCharacter").transform.localPosition = itempose.getCharaPos(charaPosition);

    }

}
