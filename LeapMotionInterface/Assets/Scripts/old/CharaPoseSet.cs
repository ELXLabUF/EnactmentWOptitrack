using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CharaPoseSet : MonoBehaviour
{

    //this class currently just stores the sprites for all the character poses for a character
    public Sprite[] poses;
    public int Test;
    //private Vector3[] itemPositions = new Vector3[5];
    private Vector3 pose0 = new Vector3(1651 - 1110+300, 989 - 715, 0);
    private Vector3 pose1 = new Vector3(1771 - 1110, 1028 - 715, 0);
    private Vector3 pose2 = new Vector3(1601 - 1110, 936 - 715, 0);
    private Vector3 pose3 = new Vector3(1595 - 1110, 917 - 715, 0);
    private Vector3 pose4 = new Vector3(1595 - 1110, 814 - 715, 0);

    // Use this for initialization
    void Start()
    {

        // itemPositions.SetValue(new Vector3(1772, 1036, 0), 0);
        /*   itemPositions[0] = new Vector3(1772, 1036, 0);
           itemPositions[1] = new Vector3(650, 320, 0);
           itemPositions[2] = new Vector3(570, 234, 0);
           itemPositions[3] = new Vector3(427, 129, 0);
           itemPositions[4] = new Vector3(515, 321, 0);*/
    }

    // Update is called once per frame
    void Update()
    {

    }

    public Vector3 getItemPos(int ind)
    {
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


}
