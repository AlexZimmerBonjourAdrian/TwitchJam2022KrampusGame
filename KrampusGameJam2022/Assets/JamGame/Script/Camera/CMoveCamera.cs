using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMoveCamera : MonoBehaviour
{
    public Transform cameraPosition;

    private void Update()
    {
        transform.position = cameraPosition.position;
    }
}
