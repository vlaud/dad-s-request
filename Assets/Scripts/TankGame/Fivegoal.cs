using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fivegoal : MonoBehaviour
{
    public Transform[] Goal;
    public Transform myBox;
    public Transform myTank;

    Vector3[] checkDir = null;
    // Start is called before the first frame update
    void Start()
    {
        checkDir = new Vector3[Goal.Length];
        for (int i = 0; i < Goal.Length; ++i)
        {
            checkDir[i] = GetCrossV(Goal[(i + 1)%Goal.Length], Goal[i]);
           
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        int count = 0;
        for (int i = 0; i < Goal.Length; ++i)
        {
            Vector3 tank = (myTank.position - Goal[i].position).normalized;
            if (Vector3.Dot(checkDir[i], tank) > 0.0f)
            {


                ++count;
            }
            else
            {

                break;
            }
        }
        
        if(count == checkDir.Length)
        {
            myBox.Rotate(Vector3.up * 360.0f * Time.deltaTime);
        }
        

    }
    Vector3 GetCrossV(Transform point, Transform start)
    {
        Vector3 hori = point.position - start.position;
        Vector3 res = Vector3.Cross(Vector3.up, hori);
        res.Normalize();

        return res;
    }
   
}
