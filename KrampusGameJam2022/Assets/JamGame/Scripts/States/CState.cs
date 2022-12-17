using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CState
{

    protected CEnemySystem _System;

    public CState(CEnemySystem system)
    {
        _System = system;
    }
    public virtual IEnumerator Start()
    {
        yield break;
    }
    public virtual IEnumerator Idle()
    {
        yield break;
    }
    public virtual IEnumerator Move_Player()
    {
        yield break;
    }
    public virtual IEnumerator Attack_Player()
    {
        yield break;
    }
    public virtual IEnumerator Fear()
    {
        yield break;
    }
    public virtual IEnumerator Patrol()
    {
        yield break;
    }
    public virtual IEnumerator Death()
    {
        yield break;
    }
    public virtual IEnumerator Hit()
    {
        yield break;
    }
}
