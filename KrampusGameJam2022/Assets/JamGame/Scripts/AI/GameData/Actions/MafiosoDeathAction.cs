using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MafiosoDeathAction : GoapAction
{
    private bool death = false;

    public MafiosoDeathAction()
    {
        //addEffect()
        addPrecondition("Health", 0);
        addEffect("deathMe", true);
        cost = 1f;
    }

    public override bool checkProceduralPrecondition(GameObject agent)
    {
        target = null;
        return target == null;
    }

    public override bool isDone()
    {
        return death;
    }

    public override bool perform(GameObject agent)
    {
    
        Mafioso currMafioso = agent.GetComponent<Mafioso>();
        if (currMafioso.health <= 0)
        {
            //currMafioso.animator.Play("Death");
            currMafioso.deathMe();
            return true;
        }
        else
        {
            return false;
        }



    }


    public override bool requiresInRange()
    {
        return true;
    }

    public override void reset()
    {
        death = false;
        target = null;
    }

    public override int TakeDamage(int _damage)
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
