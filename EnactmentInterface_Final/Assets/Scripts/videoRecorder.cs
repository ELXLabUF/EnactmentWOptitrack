using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class videoRecorder : MonoBehaviour {

    public float targetTime = 10.0f;

    // Use this for initialization
    void Start()
    {
        this.gameObject.GetComponent<ObsWrapper>().StartRecording();
    }

    // Update is called once per frame
    void Update()
    {
        targetTime -= Time.deltaTime;
        Debug.Log(targetTime);

        if (targetTime <= 0.0f)
        {
            timerEnded();
            Debug.Log("stopping record");
        }
    }

    void timerEnded()
    {
        this.gameObject.GetComponent<ObsWrapper>().StopRecording();
    }
}
