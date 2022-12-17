using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CCharacterKinematics))]
public sealed class CCharacter : CCharacterBehaviour
{
    [Tooltip("Normal Camera.")]
    [SerializeField]
    private Camera cameraWorld;

    public override Camera GetCameraWorld() => cameraWorld;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
