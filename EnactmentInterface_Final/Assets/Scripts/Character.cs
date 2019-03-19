using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RootMotion.FinalIK;

public class Character : MonoBehaviour {
    /// This script is attached to the character and currently has two main roles:
    /// 1- Attach body effectors to their tracked targets (rigidbodies) in start()
    /// 2- When character is turning around, transfer body effector rotation to character root in update()
    private FullBodyBipedIK ik;
    //private Vector3 transformChara;

    // Use this for initialization
    void Start () {
        ik = GetComponent<FullBodyBipedIK>();
        ik.solver.bodyEffector.target = GameObject.Find("Torso").transform;
        ik.solver.bodyEffector.positionWeight = 1;
        ik.solver.rightHandEffector.target = GameObject.Find("RightHand").transform;
        ik.solver.rightHandEffector.positionWeight = 1;
        ik.solver.leftHandEffector.target = GameObject.Find("LeftHand").transform;
        ik.solver.leftHandEffector.positionWeight = 1;
        ik.solver.rightFootEffector.target = GameObject.Find("RightFoot").transform;
        ik.solver.rightFootEffector.positionWeight = 1;
        ik.solver.leftFootEffector.target = GameObject.Find("LeftFoot").transform;
        ik.solver.leftFootEffector.positionWeight = 1;
    }
	
	// Update is called once per frame
	void Update () {
        var rotationVector = this.gameObject.GetComponent<Transform>().rotation.eulerAngles;
        rotationVector.y = GameObject.Find("Torso").transform.rotation.eulerAngles.y;
        this.gameObject.GetComponent<Transform>().rotation = Quaternion.Euler(rotationVector);

    }
}
