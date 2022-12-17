using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CShootPlayerState : CState
{
    // Start is called before the first frame update

    public CShootPlayerState(CEnemySystem system) : base(system)
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

}
