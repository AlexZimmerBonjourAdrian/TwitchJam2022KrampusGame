using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class MafiosoValueComponent : MonoBehaviour
{
    public Rigidbody[] rigibodies;
    public bool isKinematic = false;
    public GameObject Weapon;
    public bool isDetection;
    public bool isVisible;
    public GameObject PositionPlayer;
    public float Visibliti;
    public float findOfView;
    public float maxRangeWalk, maxRangeShoot;
    public Animator anim;
    public CapsuleCollider capsule;
    public NavMeshAgent navMeshEnemy;
    public void Start()
    {
        anim = GetComponent<Animator>();
        rigibodies = transform.GetComponentsInChildren<Rigidbody>();
        SetEnable(false);
        capsule.GetComponent<CapsuleCollider>();
        navMeshEnemy = GetComponent<NavMeshAgent>();
    }
    public void playAnimationsController(string AnimName)
    {
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("hit"))
        {
            anim.Play(AnimName);
        }
       else if(anim.GetCurrentAnimatorStateInfo(0).IsName("hit") && anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
       {
                anim.Play(AnimName);
            
       }
                //else if (anim.name == "hit" && anim.GetCurrentAnimatorStateInfo(0).normalizedTime <= 0f)
        //{
        //    anim.Play(AnimName);
        //}

    }
    public void SetEnable(bool enabled)
    {

         isKinematic = !enabled;
        foreach (Rigidbody rigidbody in rigibodies)
        {
            rigidbody.isKinematic = isKinematic;
               
        }
        anim.enabled = !enabled;
        capsule.enabled = !enabled;
        navMeshEnemy.enabled = !enabled;
        
    }

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.R))
    //    {
    //        SetEnable(true);
    //    }
    //    if (Input.GetKeyDown(KeyCode.T))
    //    {
    //        SetEnable(false);
    //    }
    //}
}
