using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Threading;
using System.IO;
using System.Linq;
using System;



public class SlideData : MonoBehaviour {

    /*IN PROGRESS - this is a script attached to each slide to record which index of each object type it holds, as well as its recording data*/

    private AudioClip slideClip;
    private int charaIndex=0;
    private int backdropIndex=0;
    private int itemIndex=0;
    private string sceneInfo = "";
    private GameObject currentObject;
    private GameObject currentChara;
    private int vidCounter = 1;
    

    //recording variables
    //private bool captureOptitrack;
    private Vector4[] charaOptiPositions;
    private Vector4[] objectOptiPositions;
    private int currentFrame = 0;
    private bool recordOptitrack = false;
    private AudioSource slideAudio;
    private string videoClipName;
    private string audioClipName;
    private bool playing = false;


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
    private DirectoryInfo mediaDirectory;
    //0=default, 1=charaposition, 2=objectposition

    // Use this for initialization
    void Start () {
        slideAudio = gameObject.AddComponent<AudioSource>();
        slideClip = new AudioClip();
        mediaDirectory = new DirectoryInfo(GameObject.Find("SlideSections").GetComponent<SlideNumbering>().getSavingAddress());
        
    }

    // Update is called once per frame
    void Update () {
        if (!playing && GameObject.Find("CanvasManager").GetComponent<CanvasManagerBottomUp>().getWhichCanvas() == 1 && recordOptitrack && this.GetComponent<SlideSelectSlide>().getSelected() && isFilled())
        {
            Debug.Log("we started recording");
            
            //Vector3 charaPos = GameObject.Find("EnactmentCharacter").transform.position;
            //Vector3 objectPos = itemposes.getItemPos(slidePose, false, charaPosition, objectPosition, slidePose);

            //if (GameObject.FindGameObjectWithTag("current_item") != null)
            //{
            //    objectPos = GameObject.FindGameObjectWithTag("current_item").GetComponent<Transform>().localPosition;
            //}

            //charaLeapPositions[currentFrame] = new Vector4(charaPos.x, charaPos.y, charaPos.z, Time.deltaTime);
            //objectLeapPositions[currentFrame] = new Vector4(objectPos.x, objectPos.y, objectPos.z, Time.deltaTime);
            //currentFrame++;

        }
        else if (!playing && GameObject.Find("CanvasManager").GetComponent<CanvasManagerBottomUp>().getWhichCanvas() == 1 && !recordOptitrack && this.GetComponent<SlideSelectSlide>().getSelected() && isFilled())
        {
           // Debug.Log("we are not recording");
        }
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

    private bool myCheck()
    {
        //string OBSTempPath = "C:\\Users\\n.zarei.3001\\Desktop\\captures\\";
        //var OBSVideos = new DirectoryInfo(OBSTempPath);
        //FileInfo[] files = OBSVideos.GetFiles().OrderByDescending(p => p.Length).ToArray();
        //while (files.First().Length != 0)
        //{
        //    Thread.Sleep(500);
        //    OBSVideos = new DirectoryInfo(OBSTempPath);
        //    files = OBSVideos.GetFiles().OrderByDescending(p => p.Length).ToArray();

        //}
        //return true;
        const string filename = "C:\\Users\\n.zarei.3001\\Desktop\\captures\\video.mp4";

        // Set timeout to the time you want to quit (one minute from now)
        var timeout = DateTime.Now.Add(TimeSpan.FromSeconds(10));

        while (!File.Exists(filename))
        {
            if (DateTime.Now > timeout)
            {
                Debug.Log("Application timeout; app_boxed could not be created; try again");
                Environment.Exit(0);
            }

            Thread.Sleep(TimeSpan.FromMilliseconds(100));
        }

        return true;
    }

    public IEnumerator StartRecordCoroutine()
    {
        var t1 = new Func<bool>(() => myCheck());
        GameObject.Find("SlideSections").GetComponent<ObsWrapper>().StartRecording();
        yield return new WaitUntil(t1);
        slideAudio.clip = Microphone.Start(null, true, 600, 44100);
        Debug.Log("We have started");
        isRecording = true;
    }

    public void startRecord()
    {
        GameObject.Find("SlideSections").GetComponent<SlideNumbering>().recordingFunctionRunning = true;
        StartCoroutine(StartRecordCoroutine());
        GameObject.Find("SlideSections").GetComponent<SlideNumbering>().recordingFunctionRunning = false;
    }

    public void setPlaying(bool set)
    {
        playing = set;
    }

    public bool getPlaying()
    {
        return playing;
    }

    public IEnumerator EndRecordCoroutine()
    {
        isRecording = false;
        DigitalSalmon.OpenBroadcastStudio.Stop();

        int timeCut = Microphone.GetPosition(null);
        Microphone.End(null);

        float[] samples = new float[timeCut];
        slideAudio.clip.GetData(samples, 0);
        int freq = slideAudio.clip.frequency;
        slideAudio.clip = AudioClip.Create("SlideSound", samples.Length, 1, freq, false);
        slideAudio.clip.SetData(samples, 0);
        audioTime = samples.Length / freq;
        string OBSTempPath = "C:\\Users\\n.zarei.3001\\Desktop\\captures\\";
        string savingAddress = GameObject.Find("SlideSections").GetComponent<SlideNumbering>().getSavingAddress();

        var OBSVideos = new DirectoryInfo(OBSTempPath);
        FileInfo[] files = OBSVideos.GetFiles().OrderByDescending(p => p.CreationTime).ToArray();
        var obsVideoClipName = files.First().Name;
        videoClipName = "video_" + vidCounter.ToString() + ".mp4";
        Debug.Log(videoClipName);

        //audioClipName = Path.GetFileNameWithoutExtension("C:\\Users\\n.zarei.3001\\Desktop\\captures\\" + videoClipName);
        audioClipName = "video_" + vidCounter.ToString();
        Debug.Log(audioClipName);
        Debug.Log("We have stopped");

        isRecord = true;
        if (slideAudio.clip == null) { Debug.Log("No Audio Saved :("); }

        var t2 = new Func<bool>(() => !IsFileLocked(files.First()));
        yield return new WaitUntil(t2);

        File.Move(Path.Combine(OBSTempPath, obsVideoClipName), Path.Combine(savingAddress, videoClipName));
        GameObject.Find("SlideSections").GetComponent<SavWav>().Save(Path.Combine(savingAddress, audioClipName), getAudio());
        vidCounter++;
    }


    public void endRecord()
    {
        GameObject.Find("SlideSections").GetComponent<SlideNumbering>().savingFunctionRunning = true;
        StartCoroutine(EndRecordCoroutine());
        GameObject.Find("SlideSections").GetComponent<SlideNumbering>().savingFunctionRunning = false;
    }


    //This is where to change 
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
        //GameObject.FindGameObjectWithTag("frame").GetComponent<RawImage>().enabled = true;
        var videoFile = "C:\\Users\\n.zarei.3001\\Desktop\\captures\\" + videoClipName;
        var audioFile = "C:\\Users\\n.zarei.3001\\Desktop\\captures\\" + audioClipName + ".wav";
        //var fileData = File.ReadAllBytes(audioFile);
        //var aud = new AudioClip();
        //aud.LoadAudioData()
          

        if (videoFile == null || audioFile == null)
        {
            // Handle the file not being found
            Debug.LogError("video or audio file not found");
        }
        else
        {

            GameObject.FindGameObjectWithTag("Player").GetComponent<RenderHeads.Media.AVProVideo.MediaPlayer>().OpenVideoFromFile(RenderHeads.Media.AVProVideo.MediaPlayer.FileLocation.AbsolutePathOrURL, videoFile, false);
            GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>().clip = slideAudio.clip;
            GameObject.FindGameObjectWithTag("Player").GetComponent<RenderHeads.Media.AVProVideo.MediaPlayer>().Control.Play();
        }

        


        //if (slideAudio.clip != null) {
        //    //Debug.Log("well something is playing");

        //    if (slideAudio.isPlaying==false) {
        //        slideAudio.Play();


        //    }
        //    else
        //    {
        //        slideAudio.Stop();

        //    }

        //}
        //GameObject.FindGameObjectWithTag("frame").GetComponent<RawImage>().enabled = false;
    }

    public void playVideo()
    {
        //DirectoryInfo info = new DirectoryInfo("C:\\Users\\n.zarei.3001\\Desktop\\captures\\");
        if (!playing)
        {
            var file = "C:\\Users\\n.zarei.3001\\Desktop\\captures\\" + videoClipName;
            if (file == null)
            {
                // Handle the file not being found
                Debug.LogError("video file not found");
            }
            else
            {
                GameObject.Find("AVProVideo").GetComponent<RenderHeads.Media.AVProVideo.MediaPlayer>().OpenVideoFromFile(RenderHeads.Media.AVProVideo.MediaPlayer.FileLocation.AbsolutePathOrURL, file, false);
                //this.gameObject.GetComponent<RenderHeads.Media.AVProVideo.MediaPlayer>().OpenVideoFromFile(RenderHeads.Media.AVProVideo.MediaPlayer.FileLocation.AbsolutePathOrURL, path, false);
                //this.gameObject.GetComponent<RenderHeads.Media.AVProVideo.MediaPlayer>().Lo
                GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>().clip = slideAudio.clip;
                GameObject.Find("AVProVideo").GetComponent<RenderHeads.Media.AVProVideo.MediaPlayer>().Control.Play();
            }
            GameObject.FindGameObjectWithTag("play_singlePlay_btn").GetComponent<Image>().sprite = GameObject.Find("SlideSections").GetComponent<SlideNumbering>().recordStop;
            playing = true;
            //while (GameObject.Find("AVProVideo").GetComponent<RenderHeads.Media.AVProVideo.MediaPlayer>().)
        }

        else
        {
            GameObject.Find("AVProVideo").GetComponent<RenderHeads.Media.AVProVideo.MediaPlayer>().Control.Stop();
            playing = false;
            GameObject.FindGameObjectWithTag("play_singlePlay_btn").GetComponent<Image>().sprite = GameObject.Find("SlideSections").GetComponent<SlideNumbering>().play;

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
        GameObject.Find("SceneInfoCartoonPlay").GetComponent<Text>().text = sceneInfo;
    }

    public void deactiveElements()
    {
        currentObject = GameObject.FindGameObjectWithTag("current_item");
        currentChara = GameObject.FindGameObjectWithTag("current_chara");
        currentObject.SetActive(false);
        currentChara.SetActive(false);

    }

    public void activateElements()
    {
        currentObject.SetActive(true);
        currentChara.SetActive(true);

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

    protected virtual bool IsFileLocked(FileInfo file)
    {
        FileStream stream = null;

        try
        {
            stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.None);
        }
        catch (IOException)
        {
            //the file is unavailable because it is:
            //still being written to
            //or being processed by another thread
            //or does not exist (has already been processed)
            return true;
        }
        finally
        {
            if (stream != null)
                stream.Close();
        }

        //file is not locked
        return false;
    }

}
