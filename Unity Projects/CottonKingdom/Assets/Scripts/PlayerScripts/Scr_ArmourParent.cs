using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_ArmourParent : MonoBehaviour
{
    [Header("Joints")]
    public Transform torso;
    public Transform leftUpperLeg;
    public Transform leftLowerLeg;
    public Transform rightUpperLeg;
    public Transform rightLowerLeg;
    public Transform leftUpperArm;
    public Transform leftForearm;
    public Transform rightUpperArm;
    public Transform rightForearm;
    public Transform head;

    [Space(10)]
    [Header("Armour Pieces")]
    public Transform torso_armour;
    public Transform leftUpperLeg_armour;
    public Transform leftLowerLeg_armour;
    public Transform rightUpperLeg_armour;
    public Transform rightLowerLeg_armour;
    public Transform leftUpperArm_armour;
    public Transform leftForearm_armour;
    public Transform rightUpperArm_armour;
    public Transform rightForearm_armour;
    public Transform head_armour;

    void Start ()
    {
        torso_armour.parent = torso;
        leftUpperLeg_armour.parent = leftUpperLeg;
        leftLowerLeg_armour.parent = leftLowerLeg;
        rightUpperLeg_armour.parent = rightUpperLeg;
        rightLowerLeg_armour.parent = rightLowerLeg;
        leftUpperArm_armour.parent = leftUpperArm;
        leftForearm_armour.parent = leftForearm;
        rightUpperArm_armour.parent = rightUpperArm;
        rightForearm_armour.parent = rightForearm;
        head_armour.parent = head;
	}
	
	void Update ()
    {
		
	}
}
