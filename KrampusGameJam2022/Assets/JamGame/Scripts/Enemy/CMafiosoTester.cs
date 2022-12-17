using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class CMafiosoTester : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    public float Hearth;
    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;


    public Material green, red, yellow;

    [SerializeField]private Transform Weapon;

    public GameObject Projectile;

    [SerializeField] private GameObject Gun;

    public bool iSDead;

    public enum states
    {
        STATE_STAND = 0,
        STATE_PATRULLA = 1,
        STATE_FOLLOW = 2,
        STATE_SHOOT_PLAYER = 3,
        STATE_DEAD = 4,
        STATE_SCARED = 5,
        STATE_CUBRIRSE = 6,
        STATE_RODEAR_PLAYER = 7

    }

    [SerializeField] private int state = (int)states.STATE_STAND;

    //private int state = (int)states.STATE_STAND;
    //private Ray= new Ray;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }


    private void Patroling()
    {
        if (iSDead) return;
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;

        //GetComponent<MeshRenderer>().material = green;
    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2, whatIsGround))
            walkPointSet = true;
    }
    private void ChasePlayer()
    {
        if (iSDead) return;

        agent.SetDestination(player.position);

        //GetComponent<MeshRenderer>().material = yellow;
        //base.ChasePlayer();
        // Quaternion lookRotation = Quaternion.LookRotation(player.position - transform.position);
    }
    private void AttackPlayer()
    {
        if (iSDead) return;

        //Make sure enemu doesn't move
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            ///Attack code here


            Rigidbody rb = Instantiate(Projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            rb.AddForce(transform.up * 8, ForceMode.Impulse);
            ///

            alreadyAttacked = true;
            Invoke("ResetAttack", timeBetweenAttacks);
        }

        //GetComponent<MeshRenderer>().material = red;
    }
    private void ResetAttack()
    {
        if (iSDead) return;

        alreadyAttacked = false;

    }

    public void TakeDamage(int damage)
    {
        Hearth -= damage;

        if (Hearth <= 0)
        {
            iSDead = true;
            //Invoke(nameof(DestroyEnemy), 0.5f);
            SetState((int)states.STATE_DEAD);
        }
    }
    public void DestroyEnemy()
    {
        Destroy(gameObject);
    }
    //private void Start()
    //{
    // //Detection = ISee.GetComponent<Collision>();
    //}

    public void Update()
    {

        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInAttackRange && playerInSightRange) AttackPlayer();



        //DebugHealth();
        switch (state)
        {
            case (int)states.STATE_STAND:
                //Debug.Log("Estado Stand");
                break;
            case (int)states.STATE_FOLLOW:
                Debug.Log("Estado Follow");
                break;
            case (int)states.STATE_PATRULLA:
                Debug.Log("Estado Patrulla");
                break;
            case (int)states.STATE_SHOOT_PLAYER:
                Debug.Log("Estado Shoot Player");
                break;
            case (int)states.STATE_DEAD:
                //CControllerWave.Inst.KilledEnemy();
                Invoke(nameof(DestroyEnemy), 0.1f);
                break;
            case (int)states.STATE_SCARED:
                Debug.Log("Estado Scared");
                break;
        }
    }


    //Esto es para mucho mas adelante y pulir el sistema a la hora de entrar en los estados
    //Dejar preparado para despues
    public int EnterState()
    {
        return 0;
    }
    public int ExitState()
    {
        return 0;
    }
    public int SetState(int astate)
    {
        state = astate;
        return state;
    }
    //private void _collision(Collision body)
    //{
    //    body = Detection; 
    //    if(Detection.gameObject.tag == "Player")
    //    {
    //        SetState(SetState((int)states.STATE_STAND));
    //    }
    //}

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
        Vector3 vector3 = -transform.up;
        Debug.DrawRay(walkPoint, vector3, Color.red);
    }

    public void dead()
    {
        SetState((int)states.STATE_DEAD);
    }

    //public void DebugHealth()
    //{
    //    Debug.Log(Hearth);
    //}

    public float GetHearth()
    {
        return Hearth;
    }
}
