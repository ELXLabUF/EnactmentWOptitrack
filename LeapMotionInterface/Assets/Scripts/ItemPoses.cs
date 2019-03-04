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

    public Vector3 groundPoseUp;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public Vector3 getCharaPos(int charapos)
    {
        switch (charapos)
        {
            case 0:
                return charaLeft;
            case 1:
                return charaRight;
            case 2:
                return charaUp;
            case 3:
                return charaMid;
            default:
                return charaLeft;

        }
    }

    public Vector3 getItemPos(int ind, bool ground, int charapos, int objpos, int pose)
    {
        /* if (!ground) {
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
         else { return groundPose; }*/


        if (charapos != objpos)
        {
            switch (objpos)
            {
                case 0:
                    return groundPoseLeft;
                case 1:
                    return groundPoseRight;
                case 2:
                    return groundPoseUp;
                case 3:
                    return groundPoseMid;
                default:
                    return groundPoseLeft;
            }
        }
        else
        {
            Vector3[] leftPoses = { charaPose0_Left, charaPose1_Left, charaPose2_Left, charaPose3_Left, charaPose4_Left };
            Vector3[] rightPoses = { charaPose0_Right, charaPose1_Right, charaPose2_Right, charaPose3_Right, charaPose4_Right };
            Vector3[] upPoses = { charaPose0_Up, charaPose1_Up, charaPose2_Up, charaPose3_Up, charaPose4_Up };
            Vector3[] midPoses = { charaPose0_Mid, charaPose1_Mid, charaPose2_Mid, charaPose3_Mid, charaPose4_Mid };

            switch (objpos)
            {
                case 0:
                    return leftPoses[pose];
                case 1:
                    return rightPoses[pose];
                case 2:
                    return upPoses[pose];
                case 3:
                    return midPoses[pose];
                default:
                    return leftPoses[pose];
            }
        
        }

    }


}
