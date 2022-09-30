using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class StudyTransform : MonoBehaviour
{
   
    
    Coroutine CoroutineTotal;
    Coroutine CoroutineAngle;
    
    Ray ray;
    
    public LayerMask pickMask;
    
    private void Awake()
    {


    }
    private void OnEnable()
    {

    }

    private void Reset()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        /*
        Vector3 target = new Vector3(2, 2, 2);//위치
        Vector3 pos = this.transform.position;//위치
        dir = target - pos;//벡터
        totalDist = dir.magnitude; //스칼라값(길이)
        //this.transform.position = pos + dir;
        dir.Normalize();
        
        //
        dir = new Vector3(0, 1, 0);
        totalDist = 3.0f;
        */

    }

    // Update is called once per frame
    void Update()
    {
        //transform.Rotate(Vector3.forward * 360.0f * Time.deltaTime);

        /*
        if (Input.GetMouseButtonDown(0))
        {
            
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, 1000.0f, pickMask))
            {
                dir = hit.point - this.transform.position;
                totalDist = dir.magnitude;
                dir.Normalize();
                //float angle = Vector3.Angle(transform.forward, dir);
                float d = Vector3.Dot(transform.forward, dir);
                float r = Mathf.Acos(d);
                angle = r * Mathf.Rad2Deg;
                //float angle = (r / Mathf.PI) * 180.0f;
                rotDir = 1.0f;
                if (Vector3.Dot(transform.right, dir) < 0.0f)
                {
                    rotDir = -1.0f;
                }
            }
           
        }*/


        if (Input.GetMouseButtonDown(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            LimitRotate();


        }



        /*
        if (totalDist > 0.0f)
        {
            float delta = 2.0f * Time.deltaTime;
            if (totalDist < delta)
            {
                delta = totalDist;
            }
            totalDist -= delta;
            //this.transform.position += (dir / dir.magnitude) * delta;
            this.transform.Translate(dir * delta,Space.World);
        }
        if (angle > 0.0f)
        {

            float timedeg = 180.0f * Time.deltaTime;
            if (angle < timedeg)
            {
                timedeg = angle;
            }



            angle -= timedeg;
            transform.Rotate(Vector3.up * rotDir * timedeg);
        }*/

        //초당 2미터 속도로 클릭 지점까지 이동

        /*
        //초당 2미터 속도로 위로 3미터 아래로 3미터 무한반복
        float delta = 2.0f * Time.deltaTime;


        if (totalDist < delta)
        {
            delta = totalDist;
        }

        totalDist -= delta;

        this.transform.position += dir * delta;

        if (Mathf.Approximately(totalDist, 0.0f))
        {
            dir = -dir;
            totalDist = 3.0f;
        }


        //초당 0.5미터 속도로 이동하게 만들자
        if (totalDist > 0.0f)
        {
            float delta = 0.5f * Time.deltaTime;

            if (totalDist < delta)
            {
                delta = totalDist;
            }
            totalDist -= delta;
            this.transform.position += (dir / dir.magnitude) * delta;
        }
        */

    }

    void LimitRotate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out RaycastHit hit, 1000.0f, pickMask))
        {
            
            Vector3 dir = hit.point - this.transform.position;
            float totalDist = dir.magnitude;
            dir.Normalize();

            float d = Vector3.Dot(transform.forward, dir);
            float r = Mathf.Acos(d);
            float angle = r * Mathf.Rad2Deg;

            float rotDir = 1.0f;
            if (Vector3.Dot(transform.right, dir) < 0.0f)
            {
                rotDir = -1.0f;
            }
            if (CoroutineTotal != null)
            {
                StopCoroutine(CoroutineTotal);
                CoroutineTotal = null;
            }
            CoroutineTotal = StartCoroutine(LerpMoving(dir, totalDist));
            //StopAllCoroutines();
            if (CoroutineAngle != null)
            {
                StopCoroutine(CoroutineAngle);
                CoroutineAngle = null;
            }
            CoroutineAngle = StartCoroutine(SLerpRotating(angle, rotDir));
        }
    }
    IEnumerator AngleCheck(float angle, float rotDir)
    {
        while (angle > 0.0f)
        {
            
            float timedeg = 180.0f * Time.deltaTime;
            if (angle < timedeg)
            {
                timedeg = angle;
            }

            angle -= timedeg;
            transform.Rotate(Vector3.up * rotDir * timedeg);
            yield return null;
        }
    }
    IEnumerator SLerpRotating(float Angle, float rotDir)
    {
        Vector3 targetRot = transform.rotation.eulerAngles;

        while(true)
        {
            float delta = 180.0f * Time.deltaTime;
            if (Angle < delta)
            {
                delta = Angle;
            }

            Angle -= delta; 
            targetRot.y += delta * rotDir;

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(targetRot), Time.deltaTime * 5.0f);
            yield return null;
        }
    }
    IEnumerator Moving(Vector3 dir, float totalDist)
    {
        
        while (totalDist > 0.0f)
        {
            float delta = 2.0f * Time.deltaTime;
            //Vector3 destination = Vector3.MoveTowards(transform.position, target, delta);
   
            if (totalDist < delta)
            {
                delta = totalDist;
            }
            totalDist -= delta;
            this.transform.Translate(dir * delta, Space.World);
            if(Input.GetKeyDown(KeyCode.Space))
            {
                yield break;
            }
            yield return null;
        }
    }
    IEnumerator LerpMoving(Vector3 dir, float totalDist)
    {
        Vector3 tarPos = transform.position;
        while (totalDist > 0)
        {
            float delta = 10.0f * Time.deltaTime;
            if (totalDist < delta)
            {
                delta = totalDist;
            }
            totalDist -= delta;
            tarPos += dir * delta;
            transform.position = Vector3.Lerp(transform.position, tarPos, Time.deltaTime * 3.0f);
            yield return null;
        }
        while(Vector3.Distance(transform.position, tarPos) > 0.01f)
        {
            transform.position = Vector3.Lerp(transform.position, tarPos, Time.deltaTime * 3.0f);
            yield return null;
        }
        transform.position = tarPos;
    }
}
