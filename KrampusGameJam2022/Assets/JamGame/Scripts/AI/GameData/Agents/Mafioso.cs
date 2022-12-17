using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mafioso : Enemy
{

    private void Start()
    {
        health = 100;
        speed = 20;
        regenRate = 50f;
        initialSpeed = (speed / 10) / 2;
        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        player = GameObject.Find("Player");
        ShootRangeFindOFWiew = GameObject.Find("Character1_Head").GetComponent<FindOfView>();
        PositionEnemy = GameObject.Find("Player").transform;


    }
    public override HashSet<KeyValuePair<string, object>> createGoalState()
    {
        HashSet<KeyValuePair<string, object>> goal = new HashSet<KeyValuePair<string, object>>();
        goal.Add(new KeyValuePair<string, object>("damagePlayer", true));
        goal.Add(new KeyValuePair<string, object>("hitMe", true));
        goal.Add(new KeyValuePair<string, object>("deathMe",true));
        return goal;
    }

    public override void TakeDamage(int damage)
    {
        Debug.Log("Vida" + health);
        animator.Play("hit");
        health = health - damage;
        if(health <= 0)
        {
           MafiosoValues.SetEnable(true);
        }
    }
   

    public override void deathMe()
    {
        isDeath = true;
    }

    public override void hitDamage()
    {
       
    }
}
