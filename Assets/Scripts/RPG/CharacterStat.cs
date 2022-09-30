using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct CharacterStat
{
    [SerializeField] float hp;
    [SerializeField] float maxHp;
    [SerializeField] float moveSpeed;
    [SerializeField] float rotSpeed;
    [SerializeField] float ap;
    [SerializeField] float attackRange;
    [SerializeField] float attackDelay;

    public float HP
    {
        get => hp;
        set
        {
            hp = Mathf.Clamp(value, 0.0f, maxHp);
        }
    }
    
    public float MoveSpeed
    {
        get => moveSpeed;

    }
    public float RotSpeed
    {
        get => rotSpeed;

    }
    public float AP
    {
        get => ap; 
        
    }                   
    public float AttackRange
    {
        get => attackRange;

    }
    public float AttackDelay
    {
        get => attackDelay;

    }
}
