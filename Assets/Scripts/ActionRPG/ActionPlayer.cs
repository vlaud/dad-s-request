using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ActionPlayer : CharacterProperty, IBattle
{
    public LayerMask myEnemyMask;
    public Transform myAttackPoint;
    public float smoothMoveSpeed = 10.0f;
    Vector2 targetDir = Vector2.zero;
    bool IsCombable = false;
    int clickCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        targetDir.x = Input.GetAxis("Horizontal");
        targetDir.y = Input.GetAxis("Vertical");

        float x = Mathf.Lerp(myAnim.GetFloat("x"), targetDir.x, Time.deltaTime * smoothMoveSpeed);
        float y = Mathf.Lerp(myAnim.GetFloat("y"), targetDir.y, Time.deltaTime * smoothMoveSpeed);
        myAnim.SetFloat("x", x);
        myAnim.SetFloat("y", y);
        if (Input.GetKeyDown(KeyCode.F1))
        {
            myAnim.SetTrigger("JumpAttack");
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            myAnim.SetTrigger("Roll");
        }
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if(!myAnim.GetBool("Sneak"))
                myAnim.SetBool("Sneak", true);
            else
                myAnim.SetBool("Sneak", false);
        }
        if(Input.GetMouseButtonDown(0) && !myAnim.GetBool("IsComboAttacking"))
        {
            myAnim.SetTrigger("ComboAttack");
        }
        if(IsCombable)
        {
            if(Input.GetMouseButtonDown(0))
            {
                ++clickCount;
            }
        }
    }

    public void OnDamage(float dmg)
    {

    }
    public bool IsLive()
    {
        return true;
    }
    public void AddAttacker(IBattle ib)
    {

    }
    public void RemoveAttacker(IBattle ib)
    {

    }
    public void DeadMessage(Transform tr)
    {

    }

    public void JumpAttack()
    {
        AttackTarget();
    }

    public void AttackTarget()
    {
        Collider[] list = Physics.OverlapSphere(myAttackPoint.position, 2.0f, myEnemyMask);

        foreach (Collider col in list)
        {
            col.GetComponent<IBattle>()?.OnDamage(30.0f);
        }
    }
    public void ComboCheck(bool v)
    {
        if(v)
        {
            //Start Combo Check
            IsCombable = true;
            clickCount = 0;
        }
        else
        {
            //End Combo Check
            IsCombable = false;
            if (clickCount == 0)
            {
                myAnim.SetTrigger("ComboFail");
            }
        }
    }
}
