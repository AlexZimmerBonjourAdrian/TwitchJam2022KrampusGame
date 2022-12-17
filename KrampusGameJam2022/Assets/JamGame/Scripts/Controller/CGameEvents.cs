using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CGameEvents : MonoBehaviour
{
    public static CGameEvents current;
    // Start is called before the first frame update

    private void Awake()
    {
        current = this;
    }

    public event Action OnEndAnimation;
    public event Action OnDeathEnemy;
    public event Action OnDoorwayTriggerEnter;
    public event Action onStartWave;
    public void DoorwayTriggerEnter()
    {
        if(OnDoorwayTriggerEnter != null)
        {
            OnDoorwayTriggerEnter();
        }
    }


    public void EndAnimation()
    {
        if(OnEndAnimation != null)
        {
            OnEndAnimation();
        }
    }

    public void StartWave()
    {
        if(onStartWave != null)
        {
            onStartWave();
        }
    }

    public void DeathEnemy()
    {
        if(OnDeathEnemy != null)
        {
            OnDeathEnemy();
        }
    }
}
