using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPatrollingState : CState
{
    public CPatrollingState(CEnemySystem system) : base(system)
    {

    }

    public override IEnumerator Start()
    {
        yield break;
    }

    public override IEnumerator Move_Player()
    {
        yield break;
    }

    public override IEnumerator Hit()
    {
        yield break;
    }

    public override IEnumerator Attack_Player()
    {
        yield break;
    }


}
