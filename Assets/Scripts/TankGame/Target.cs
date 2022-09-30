using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{

    // Start is called before the first frame update
    public GameObject orgEff = null;
    public float totalDist;
    Vector3 dir = Vector3.zero;
    public bool Life = false;
    void Start()
    {
        dir = this.transform.right;
        totalDist = 8.0f;
        //totalDist = Target_move._instance.Max - Target_move._instance.random_x;
       
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Life)
        {
            float delta = 10.0f * Time.deltaTime;

            if (totalDist < delta)
            {
                delta = totalDist;
            }

            totalDist -= delta;

            transform.Translate(dir * delta, Space.World);
            if (Mathf.Approximately(totalDist, 0.0f))
            {
                dir = -dir;
                totalDist = 16.0f;
            }
        }
        

    }
    public void MovingTarget()
    {
        Life = true;
        transform.SetParent(null);

    }
    
    public void OnDelete()
    {

        Instantiate(orgEff, transform.position, Quaternion.identity);
        Destroy(gameObject);
       
    }
    private void OnDestroy()
    {
    }

}
