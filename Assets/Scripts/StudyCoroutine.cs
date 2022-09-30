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
    // �ʴ� �ѹ���
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
    // �ʴ� 2������ �ӵ��� ���� 3���� �Ʒ��� 3���� �Դٰ��� �ϰ� ����ÿ�
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
            //���� �ڵ尡 �������Ӹ��� �ݺ�
            //transform.Translate(Vector3.up * h * t);
            yield return null;
        }
        
    }

    
}
