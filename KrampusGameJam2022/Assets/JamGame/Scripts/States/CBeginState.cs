using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBeginState : CState
{

    public CBeginState(CEnemySystem system) : base(system)
    {
    }

    public override IEnumerator Start()
    {
        yield return new WaitForSeconds(2f);

        //_System.SetState(new CIdleState(_System));
        _System.SetState(new CIdleState(_System));
    }
}
