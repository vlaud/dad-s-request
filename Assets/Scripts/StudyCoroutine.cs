using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StudyCoroutine : MonoBehaviour
{
    //IEnumerator moving = null;
    // Start is called before the first frame update
    void Start()
    {
        //moving = MovingUP(3.0f);
        StartCoroutine(MovingUP(3.0f)); 
    }

    // Update is called once per frame
    void Update()
    {
       if(Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(Rotating(360.0f));
        }
        //moving.MoveNext();
    }
    // 초당 한바퀴
    IEnumerator Rotating(float angle)
    {
        
        
        while (angle > 0.0f)
        {
            float delta = 360.0f * Time.deltaTime;
            if (angle < delta)
            {
                delta = angle;
            }
            angle -= delta;
            transform.Rotate(Vector3.up * delta);

            
            yield return null;
        }
    }
    // 초당 2미터의 속도로 위로 3미터 아래로 3미터 왔다갔다 하게 만드시오
    IEnumerator MovingUP(float d)
    {
        float totalDist = d;
        Vector3 dir = Vector3.up;
        while (true)
        {
            float delta = 2.0f * Time.deltaTime;
            if (totalDist < delta)
            {
                delta = totalDist;
            }

            totalDist -= delta;
            transform.Translate(dir * delta);

            if (Mathf.Approximately(totalDist, 0.0f))
            {
                dir = -dir;
                totalDist = d;
                yield return StartCoroutine(Rotating(180.0f));
            }
            //여기 코드가 매프레임마다 반복
            //transform.Translate(Vector3.up * h * t);
            yield return null;
        }
        
    }

    
}
