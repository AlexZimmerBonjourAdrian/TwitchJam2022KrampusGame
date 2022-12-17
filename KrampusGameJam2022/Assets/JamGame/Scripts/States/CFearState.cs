using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CFearState : CState
{
    public CFearState(CEnemySystem system) : base(system)
    {
    }


    public override IEnumerator Start()
    {
        Debug.Log("Entra en Start");
        yield break;
    }

    public override IEnumerator Death()
    {
        yield break;
    }
}
