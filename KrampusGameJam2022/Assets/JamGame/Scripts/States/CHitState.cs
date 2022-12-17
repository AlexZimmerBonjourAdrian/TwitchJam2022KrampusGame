using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CHitState : CState
{
    public CHitState(CEnemySystem system) : base(system)
    {
    }

    public override IEnumerator Start()
    {
        Debug.Log("Entra en Start");
        yield break;
    }

    public override IEnumerator Attack_Player()
    {
        yield break;
    }


    public override IEnumerator Move_Player()
    {
        yield break;
    }

    public override IEnumerator Death()
    {
        yield break;
    }

    public override IEnumerator Fear()
    {
        yield break;
    }



}
