using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CIdleState : CState
{
    public CIdleState(CEnemySystem system) : base(system)
    {
    }

    public override IEnumerator Start()
    {
        Debug.Log("Inicio la maquina de estado finito");
        yield break;
    }

    public override IEnumerator Patrol()
    {
        return base.Patrol();
    }

    public override IEnumerator Move_Player()
    {
        return base.Move_Player();
    }

    public override IEnumerator Hit()
    {
        return base.Hit();
    }
}
