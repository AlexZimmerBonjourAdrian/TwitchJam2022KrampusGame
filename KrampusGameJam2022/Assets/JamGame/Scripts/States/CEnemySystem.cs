using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnemySystem : MonoBehaviour
{

    private CState _currentState;
    // Start is called before the first frame update
    
    public void SetState(CState state)
    {
        _currentState = state;
        StartCoroutine(_currentState.Start());
        //_sys
        
    }

    public void SetupEnemy()
    {
        SetState(new CBeginState(this));
    }
}
