using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CCharacterBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    protected virtual void Awake() { }

    /// <summary>
    /// Start.
    /// </summary>
    protected virtual void Start() { }

    /// <summary>
    /// Update.
    /// </summary>
    protected virtual void Update() { }

    /// <summary>
    /// Late Update.
    /// </summary>
    protected virtual void LateUpdate() { }
    public abstract Camera GetCameraWorld();
}
