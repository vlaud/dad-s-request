using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalCheck : MonoBehaviour
{
    public Transform Left;
    public Transform Right;
    public Transform myBox;
    public Transform myTank;
    bool bPass = false;
    Vector3 checkDir = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 hori = Right.position - Left.position;
        checkDir = Vector3.Cross(Vector3.up, hori);
        checkDir.Normalize();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 tank = (myTank.position - Left.position).normalized;

        if(Vector3.Dot(checkDir, tank) < 0.0f)
        {
            bPass = true;
            //Åë°ú
        }
        else
        {
            bPass = false;
        }

        if(bPass)
        {
            myBox.Rotate(Vector3.up * 180.0f * Time.deltaTime);
        }
    }
}
