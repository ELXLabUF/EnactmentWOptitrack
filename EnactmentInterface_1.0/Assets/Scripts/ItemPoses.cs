using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPoses : MonoBehaviour {


    public Vector3 pose0;
    public Vector3 pose1;
    public Vector3 pose2;
    public Vector3 pose3;
    public Vector3 pose4;

    public Vector3 groundPose;

    public Vector3 groundPoseLeft;
    public Vector3 groundPoseMid;
    public Vector3 groundPoseRight;

    public Vector3 charaPose0_Left;
    public Vector3 charaPose1_Left;
    public Vector3 charaPose2_Left;
    public Vector3 charaPose3_Left;
    public Vector3 charaPose4_Left;

    public Vector3 charaPose0_Mid;
    public Vector3 charaPose1_Mid;
    public Vector3 charaPose2_Mid;
    public Vector3 charaPose3_Mid;
    public Vector3 charaPose4_Mid;

    public Vector3 charaPose0_Right;
    public Vector3 charaPose1_Right;
    public Vector3 charaPose2_Right;
    public Vector3 charaPose3_Right;
    public Vector3 charaPose4_Right;

    public Vector3 charaPose0_Up;
    public Vector3 charaPose1_Up;
    public Vector3 charaPose2_Up;
    public Vector3 charaPose3_Up;
    public Vector3 charaPose4_Up;

    public Vector3 charaLeft;
    public Vector3 charaMid;
    public Vector3 charaRight;
    public Vector3 charaUp;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public Vector3 getItemPos(int ind, bool ground)
    {
        if (!ground) {
            switch (ind)
            {
                case 0:
                    return pose0;
                case 1:
                    return pose1;
                case 2:
                    return pose2;
                case 3:
                    return pose3;
                case 4:
                    return pose4;
                default:
                    return pose0;

            }
        }
        else { return groundPose; }
    }
}
