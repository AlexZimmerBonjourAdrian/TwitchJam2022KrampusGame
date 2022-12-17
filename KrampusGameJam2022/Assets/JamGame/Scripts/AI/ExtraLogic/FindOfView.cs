using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindOfView : MonoBehaviour
{
    public float radius;
    [Range(0,360)]

    public float angle;

    public GameObject playerRef;

    public LayerMask targetMask;
    public LayerMask obstructionMask;

    public bool canSeePlayer;

    private void Start()
    {
        playerRef = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(FOVRoutine());
    }

    public IEnumerator FOVRoutine()
    {
        
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while(true)
        {
            yield return wait;
            FindOfViewCheck();
        }

    }

    private void FindOfViewCheck()
    {
        Collider[] rangeCheck = Physics.OverlapSphere(transform.position, radius, targetMask);

        if (rangeCheck.Length != 0)
        {
            Transform target = rangeCheck[0].transform;
            Vector3 directionToTargert = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTargert) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTargert, distanceToTarget, obstructionMask))
                {
                    canSeePlayer = true;

                }
                else
                {
                    canSeePlayer = false;
                }
            }
            else
                canSeePlayer = false;
        }
        else if (canSeePlayer)
            canSeePlayer = true;
    }
}
