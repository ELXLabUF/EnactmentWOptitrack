using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

[System.Serializable]
public class SaveData {

    public static SaveData current;

    // Basic Game Data
    public string mainTitle;
    public string begTitle;
    public string midTitle;
    public string endTitle;

    public int condition;
    public bool sceneNotesUsed;
    public bool isLeapAnimated;


    // Options Data, done through Asset Names which will have to be matched between versions
    public string[] charaBank;
    public string[] itemsBank;
    public string[] backdropBank;


    // Slide Data
    public int noBegSlides;
    public int noMidSlides;
    public int noEndSlides;

    public int[] begObjects = new int[5];
    public int[] midObjects = new int[5];
    public int[] endObjects = new int[5];

    public int[] begPoses = new int[5];
    public int[] midPoses = new int[5];
    public int[] endPoses = new int[5];

    public int[] begBackdrops = new int[5];
    public int[] midBackdrops = new int[5];
    public int[] endBackdrops = new int[5];

    public int[] begCharas = new int[5];
    public int[] midCharas = new int[5];
    public int[] endCharas = new int[5];

    public string[] begNotes = new string[5];
    public string[] midNotes = new string[5];
    public string[] endNotes = new string[5];

    public Vector4[] begCharaLeapPositions = new Vector4[5];
    public Vector4[] midCharaLeapPositions = new Vector4[5];
    public Vector4[] endCharaLeapPositions = new Vector4[5];

    public Vector4[] begObjLeapPositions = new Vector4[5];
    public Vector4[] midObjLeapPositions = new Vector4[5];
    public Vector4[] endObjLeapPositions = new Vector4[5];

    public string[] begVoiceClips = new string[5];
    public string[] midVoiceClips = new string[5];
    public string[] endVoiceClipss = new string[5];

    public float[] begTimes = new float[5];
    public float[] midTimes = new float[5];
    public float[] endTimes = new float[5];

    //ALSO INCLUDE IF ITS NOT LEAP POSITIONS HHHHHHHH

    public SaveData ()
    {

        charaBank = new string[GameObject.Find("ObjectType").GetComponent<ObjectArray>().getArrayLengths()[0]];
        itemsBank = new string[GameObject.Find("ObjectType").GetComponent<ObjectArray>().getArrayLengths()[1]];
        backdropBank = new string[GameObject.Find("ObjectType").GetComponent<ObjectArray>().getArrayLengths()[2]];

        noBegSlides = 5;
        noMidSlides = 5;
        noEndSlides = 5;


        begObjects = new int[5];
        midObjects = new int[5];
        endObjects = new int[5];

        begPoses = new int[5];
        midPoses = new int[5];
        endPoses = new int[5];

        begBackdrops = new int[5];
        midBackdrops = new int[5];
        endBackdrops = new int[5];

        begCharas = new int[5];
        midCharas = new int[5];
        endCharas = new int[5];

        begNotes = new string[5];
        midNotes = new string[5];
        endNotes = new string[5];

        begCharaLeapPositions = new Vector4[5];
        midCharaLeapPositions = new Vector4[5];
        endCharaLeapPositions = new Vector4[5];

        begObjLeapPositions = new Vector4[5];
        midObjLeapPositions = new Vector4[5];
        endObjLeapPositions = new Vector4[5];

        begVoiceClips = new string[5];
        midVoiceClips = new string[5];
        endVoiceClipss = new string[5];

        begTimes = new float[5];
        midTimes = new float[5];
        endTimes = new float[5];

}
}
