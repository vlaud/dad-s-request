using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.AI;

public class StudyAgent : CharacterProperty
{
    public Transform myTargetArrow;
    public NavMeshAgent myAgent;
    public Animator myDoor;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 1000.0f, 1 << LayerMask.NameToLayer("Ground")))
            {
                //myAgent.SetDestination(hit.point);
                //myAgent.Warp(hit.point); 원하는 위치로 바로 순간이동
                //myAnim.SetBool("IsMoving", true);
                StopAllCoroutines();
                StartCoroutine(MovingByNav(myAgent, hit.point));
            }
        }
        if(Input.GetKeyDown(KeyCode.F1))
        {
            if(!myDoor.GetBool("IsDoor"))
                myDoor.SetBool("IsDoor", true);
            else
                myDoor.SetBool("IsDoor", false);
        }
       
    }
    IEnumerator MovingByNav(NavMeshAgent agent, Vector3 pos)
    {
        myTargetArrow.gameObject.SetActive(true);
        myTargetArrow.position = pos;
        agent.SetDestination(pos);
        myAnim.SetBool("IsMoving", true);
        while(agent.pathPending)
        {
            yield return null;
        }
        switch(agent.pathStatus)
        {
            case NavMeshPathStatus.PathComplete:
                // 갈수 있음
                break;
            case NavMeshPathStatus.PathPartial:
                // 가다 막힘
                break;
            case NavMeshPathStatus.PathInvalid:
                // 못감
                break;
        }
        while(agent.remainingDistance > agent.stoppingDistance)
        {
            if(agent.isOnOffMeshLink) // isOnOffMeshLink값이 트루면 오프메시링크에 진입했다는 뜻
            {
                agent.isStopped = true;
                
                myAnim.SetTrigger("Jump");
                Vector3 endPos = agent.currentOffMeshLinkData.endPos;
                endPos.y = transform.position.y;
                Vector3 dir = endPos - transform.position;
                float dist = dir.magnitude + 1.0f;
                float totalDist = dist;
                dir.Normalize();
                Vector3 curPos = transform.position;
                while(dist > 0.0f)
                {
                    float delta = agent.speed * Time.deltaTime;
                    if (delta > dist)
                    {
                        delta = dist;
                    }
                    dist -= delta;
                    curPos += dir * delta;

                    float x = (dist / totalDist) * Mathf.PI;
                    float y = Mathf.Sin(x) * 1.5f;
                    transform.position = curPos + Vector3.up * y;
                    yield return null;
                }
                
                agent.CompleteOffMeshLink();
                agent.Warp(transform.position);
                agent.isStopped = false;
                agent.SetDestination(pos);
                while(agent.pathPending)
                {
                    yield return null;
                }

            }
            yield return null;
        }
        myAnim.SetBool("IsMoving", false);
        myTargetArrow.gameObject.SetActive(false);
    }
}
