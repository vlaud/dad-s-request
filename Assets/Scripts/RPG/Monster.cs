using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : BattleSystem
{
    Color orgColor = Color.white;
    Vector3 startPos = Vector3.zero;
    public enum STATE
    {
        Create, Idle, Roaming, Battle, Dead
    }
    public STATE myState = STATE.Create;

    void ChangeState(STATE s)
    {
        if (myState == s) return;
        myState = s;

        switch(myState)
        {
            case STATE.Create:
                break;
            case STATE.Idle:
                StartCoroutine(DelayRoaming(2.0f));
                break;
            case STATE.Roaming:
                Vector3 pos = Vector3.zero;
                pos.x = Random.Range(-6.0f, 6.0f);
                pos.z = Random.Range(-6.0f, 6.0f);
                pos = startPos + pos;
                MoveToPosition(pos, ()=>ChangeState(STATE.Idle));
                break;
            case STATE.Battle:
                AttackTarget(myTarget);
                break;
            case STATE.Dead:
                StopAllCoroutines();
                myAnim.SetTrigger("Dead");
                foreach(IBattle ib in myAttackers)
                {
                    ib.DeadMessage(transform);
                }
                StartCoroutine(Disapearing(2.0f , 4.0f));
                break;

        }
    }

    void StateProcess()
    {
        switch (myState)
        {
            case STATE.Create:
                break;
            case STATE.Idle:
                break;
            case STATE.Roaming:
                break;
            case STATE.Battle:
                break;
            case STATE.Dead:
                break;

        }
    }
    
    IEnumerator Disapearing(float d, float t)
    {
        yield return new WaitForSeconds(t);
        
        float dist = d;
        while (dist > 0.0f)
        {
            float delta = 0.5f * Time.deltaTime;
            if (delta > dist)
            {
                delta = dist;
            }
            dist -= delta;
            transform.Translate(Vector3.down * delta, Space.World);
            yield return null;
        }
        Destroy(gameObject);
    }
    IEnumerator DelayRoaming(float t)
    {
        yield return new WaitForSeconds(t);
        ChangeState(STATE.Roaming);
    }
    // Start is called before the first frame update
    void Start()
    {
        orgColor = GetComponentInChildren<Renderer>().material.color;
        startPos = transform.position;
        ChangeState(STATE.Idle);
    }

    // Update is called once per frame
    void Update()
    {
        StateProcess();

    }

    public void FindTarget(Transform target)
    {
        if (myState == STATE.Dead) return;
        myTarget = target;
        StopAllCoroutines();
        ChangeState(STATE.Battle);
    }

    public void LostTarget()
    {
        if (myState == STATE.Dead) return;
        myTarget = null;
        StopAllCoroutines();
        myAnim.SetBool("IsMoving", false);
        ChangeState(STATE.Idle);
    }

    
    public override void OnDamage(float dmg)
    {
        myStat.HP -= dmg;
        if(Mathf.Approximately(myStat.HP, 0.0f))
        {
            ChangeState(STATE.Dead);
        }
        else
        {
            myAnim.SetTrigger("Damage");
            StartCoroutine(DamagingColor(Color.red, 0.5f));
        }
        
    }

    IEnumerator DamagingColor(Color color, float t)
    {

        GetComponentInChildren<Renderer>().material.color = color;
        yield return new WaitForSeconds(t);
        GetComponentInChildren<Renderer>().material.color = orgColor;
    }
    public override bool IsLive()
    {
        return myState != STATE.Dead;
    }

    
    public override void DeadMessage(Transform tr)
    {
        if(tr == myTarget)
        {
            LostTarget();
        }
    }
    
}
