using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootMotionDirect : MonoBehaviour
{
    public bool DontMove = false;
    Vector3 deltaPosition = Vector3.zero;
    Quaternion deltaRotation = Quaternion.identity;


    private void FixedUpdate()
    {
        if (DontMove) return;
        transform.Translate(deltaPosition, Space.World);
        deltaPosition = Vector3.zero;
        transform.rotation *= deltaRotation;
        deltaRotation = Quaternion.identity;
    }
    private void OnAnimatorMove()
    {
        deltaPosition += GetComponent<Animator>().deltaPosition;
        deltaRotation *= GetComponent<Animator>().deltaRotation;
    }
}
