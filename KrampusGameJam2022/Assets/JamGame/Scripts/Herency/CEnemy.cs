using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class CEnemy : MonoBehaviour
{
   // [SerializeField]
   // protected Transform Eyes;

   // [SerializeField] protected Transform player;

   // [SerializeField] protected NavMeshAgent agent;

   // [SerializeField] protected LayerMask whatIsGround, WhatIsPlayer;

   // //Patroling
   // [SerializeField] protected Vector3 walkPoint;
   // [SerializeField] protected bool walkPointSet;
   // [SerializeField] protected float walkPointRange;

   // //Attacking
   // [SerializeField] protected float timeBetweenAttacks;
   // [SerializeField] protected bool alreadyAttaacked;

   // //States
   // [SerializeField] protected float sightRange, attackRange;
   // [SerializeField] protected bool playerInSightRange, playerInAttackRange;
   // [SerializeField] protected GameObject projectile;

   // [SerializeField]protected float Health;

   // protected Collision Detection;
   // [SerializeField]
   // protected GameObject ISee;
   // // Start is called before the first frame update

   // protected enum states
   // {
   //     STATE_STAND = 0,
   //     STATE_PATRULLA = 1,
   //     STATE_FOLLOW = 2,
   //     STATE_SHOOT_PLAYER = 3,
   //     STATE_DEAD = 4,
   //     STATE_SCARED = 5,
   //     STATE_CUBRIRSE = 6,
   //     STATE_RODEAR_PLAYER = 7

   // }

   // protected int state = (int)states.STATE_STAND;

   // private void Awake()
   // {
   //     player = GameObject.Find("CameraHolder").transform;
   //     agent = GetComponent<NavMeshAgent>();
   // }

   // protected virtual void Patroling()
   // {

   //     if (walkPointSet) SearchWalkPoint();
   //     if (walkPointSet)
   //         agent.SetDestination(walkPoint);

   //     Vector3 distanceToWalkPoint = transform.position - walkPoint;

   //     if (distanceToWalkPoint.magnitude < 1f)
   //     {
   //         walkPointSet = false;
   //     }
   // }

   // protected virtual void SearchWalkPoint()
   // {
   //     float randomZ = Random.Range(-walkPointRange, walkPointRange);
   //     float randomX = Random.Range(-walkPointRange, walkPointRange);

   //     walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

   //     if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
   //     {
   //         walkPointSet = true;
   //     }
   // }
   // protected virtual void ChasePlayer()
   // {
   //     agent.SetDestination(player.position);
   // }
   // protected virtual void AttackPlayer()
   // {
   //     //Make sure enemu doesn't move
   //     agent.SetDestination(transform.position);

   //     transform.LookAt(player);

   //     if (!alreadyAttaacked)
   //     {
   //         ///Attack code here
   //         Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
   //         rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
   //         rb.AddForce(transform.up * 8f, ForceMode.Impulse);


   //         alreadyAttaacked = true;
   //         Invoke(nameof(ResetAttack), timeBetweenAttacks);
   //     }
   // }
   // protected virtual void ResetAttack()
   // {
   //     alreadyAttaacked = false;
   // }

   // public virtual void TakeDamage(int damage)
   // {
   //     Health -= damage;

   //     if (Health <= 0) Invoke(nameof(DestroyEnemy), .5f);

   // }
   // protected virtual void DestroyEnemy()
   // {
   //     Destroy(gameObject);
   // }

   //public virtual int SetState(int astate)
   // {
   //     state = astate;
   //     return state;
   // }

   // public virtual void Dead()
   // {
   //     SetState((int)states.STATE_DEAD);
   // }
}
