using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class video : MonoBehaviour {

    private bool first = true;

	// Use this for initialization
	void Start () {
        //var path = "C:\\Users\\n.zarei.3001\\Desktop\\captures\\video.mp4";
        ////this.gameObject.GetComponent<RenderHeads.Media.AVProVideo.MediaPlayer>().m_VideoPath = "C:\\Users\\n.zarei.3001\\Desktop\\captures\video.flv";
        //this.gameObject.GetComponent<RenderHeads.Media.AVProVideo.MediaPlayer>().OpenVideoFromFile(RenderHeads.Media.AVProVideo.MediaPlayer.FileLocation.AbsolutePathOrURL, path, false);
        //this.gameObject.GetComponent<RenderHeads.Media.AVProVideo.MediaPlayer>().Control.Play();
    }
	
	// Update is called once per frame
	void Update () {
        //if (first)
        //{
        //    var path = "C:\\Users\\n.zarei.3001\\Desktop\\captures\\video.mp4";
        //    //this.gameObject.GetComponent<RenderHeads.Media.AVProVideo.MediaPlayer>().m_VideoPath = "C:\\Users\\n.zarei.3001\\Desktop\\captures\video.flv";
        //    this.gameObject.GetComponent<RenderHeads.Media.AVProVideo.MediaPlayer>().OpenVideoFromFile(RenderHeads.Media.AVProVideo.MediaPlayer.FileLocation.AbsolutePathOrURL, path, false);
        //    //this.gameObject.GetComponent<RenderHeads.Media.AVProVideo.MediaPlayer>().Lo
        //    this.gameObject.GetComponent<RenderHeads.Media.AVProVideo.MediaPlayer>().Control.Play();
        //    first = false;
        //}
        //if (this.gameObject.GetComponent<RenderHeads.Media.AVProVideo.MediaPlayer>().Control.IsPlaying())
        //{
        //    GameObject.FindGameObjectWithTag("play_singlePlay_btn").GetComponent<Button>().interactable = false;
            
        //}
        if (this.gameObject.GetComponent<RenderHeads.Media.AVProVideo.MediaPlayer>().Control.IsFinished())
        {
            GameObject.FindGameObjectWithTag("play_singlePlay_btn").GetComponent<Button>().interactable = true;
        }
		
	}


    public void playAgain()
    {
        var path = "C:\\Users\\Niloofar Zarei\\Desktop\\captures\\vid.mov";
        //this.gameObject.GetComponent<RenderHeads.Media.AVProVideo.MediaPlayer>().m_VideoPath = "C:\\Users\\n.zarei.3001\\Desktop\\captures\video.flv";
        this.gameObject.GetComponent<RenderHeads.Media.AVProVideo.MediaPlayer>().OpenVideoFromFile(RenderHeads.Media.AVProVideo.MediaPlayer.FileLocation.AbsolutePathOrURL, path, false);
        //this.gameObject.GetComponent<RenderHeads.Media.AVProVideo.MediaPlayer>().Lo
        this.gameObject.GetComponent<RenderHeads.Media.AVProVideo.MediaPlayer>().Control.Play();
    }
}
