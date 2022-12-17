using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MafiosoValue : MonoBehaviour
{

    public GameObject Weapon;
    public bool isDetection;
    public bool isVisible;
    public GameObject PositionPlayer;
    public float Visibliti;
    public float findOfView;
    public float maxRangeWalk, maxRangeShoot;
    public Animator anim;

    public void Start()
    {
        anim = GetComponent<Animator>();
    }

    


}
