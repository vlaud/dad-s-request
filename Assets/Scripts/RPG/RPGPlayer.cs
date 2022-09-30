using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RPGPlayer : BattleSystem
{
    public enum STATE
    {
        Create, Play, Death
    }
    public STATE myState = STATE.Create;

    public LayerMask picMask = default;
    public LayerMask enemyMask = default;
    public Button mySkill;
    public GameObject myInventory;
    bool IsInventory = false;

    void ChangeState(STATE s)
    {
        if (myState == s) return;

        myState = s;

        switch(myState)
        {
            case STATE.Create:
                break;
            case STATE.Play:
                break;
            case STATE.Death:
                StopAllCoroutines();
                myAnim.SetTrigger("Dead");
                foreach (IBattle ib in myAttackers)
                {
                    ib.DeadMessage(transform);
                }
                break;
        }
    }
    void StateProcess()
    {
        switch (myState)
        {
            case STATE.Create:
                break;
            case STATE.Play:
                if (Input.GetMouseButtonDown(0))
                {
                    //마우스 위치에서 내부의 가상공간으로 뻗어 나가는 레이를 만든다
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;
                    //레이어마스크에 해당하는 오브젝트가 선택되어 있는지 확인
                    if (Physics.Raycast(ray, out hit, 1000.0f, enemyMask))
                    {
                        myTarget = hit.transform;
                        AttackTarget(myTarget);
                    }
                    else if (Physics.Raycast(ray, out hit, 1000.0f, picMask))
                    {
                        MoveToPosition(hit.point);

                    }
                }
                /*
                if (Input.GetMouseButtonDown(1) && !myAnim.GetBool("IsAttacking"))
                {
                    myAnim.SetTrigger("Attack"); 
                }*/
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    myRigid.AddForce(Vector3.up * 300.0f);
                }
                break;
            case STATE.Death:
                break;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        ChangeState(STATE.Play);
    }

    // Update is called once per frame
    void Update()
    {
        StateProcess();

        if (Input.GetKeyDown(KeyCode.F1))
        {
            myAnim.SetTrigger("JumpAttack");
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (IsInventory)
                IsInventory = false;
            else
                IsInventory = true;
        }
        myInventory.SetActive(IsInventory);
    }
    public void JumpAttack()
    {
        myAnim.SetTrigger("JumpAttack");
    }
    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
            myAnim.SetBool("IsAir", true);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
            myAnim.SetBool("IsAir", false);
    }
    public override void OnDamage(float dmg)
    {
        myStat.HP -= dmg;
        if(Mathf.Approximately(myStat.HP, 0.0f))
        {
            ChangeState(STATE.Death);
        }
        myAnim.SetTrigger("Damage");
    }

    
    public override bool IsLive()
    {
        return !Mathf.Approximately(myStat.HP, 0.0f);
    }
    
   
    public override void DeadMessage(Transform tr)
    {
        if(tr == myTarget)
        {
            StopAllCoroutines();
        }
    }
    
}
