using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MafiosoHitAction : GoapAction
{
    private int damage;
    private int health;
    private bool hit = false;
    private Mafioso mafioso;
    public MafiosoHitAction()
    {
        addEffect("hitMe", true);
        addEffect("Health", health);
        cost = 1f;
    }
    private void Start()
    {
        health = gameObject.GetComponent<Mafioso>().health;
        mafioso = gameObject.GetComponent<Mafioso>();
    }
    public override bool checkProceduralPrecondition(GameObject agent)
    {
        target = GameObject.Find("Player");
        return target != null;
    }

    public override bool isDone()
    {
        return hit;
    }


    public override bool perform(GameObject agent)
    {
        Enemy e = agent.GetComponent<Enemy>();
        return true;



    }

    public override bool requiresInRange()
    {
        return true;
    }

    public override void reset()
    {
        hit = false;
        target = null;
    }

    public override int TakeDamage(int _damage)
    {
        // Debug.Log(health);
        // health = health - _damage;
        //if(health < 0)
        // {
        //     addEffect("Health", health);
        // }
        return 0;

    }
}
