using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using ExtraLibrary;
public class CMafioso : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    public float Hearth;
    //Patroling
    //public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;
    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;


    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    public Transform Weapon;

    //public Material green, red, yellow;
    public int Bullet_shoot;

    public GameObject Projectile;

    public bool isHit = false;

    private int Ammo = 0;
    public int magSize = 0;
    [SerializeField] private GameObject Gun;

    public Transform[] points;

    public Transform[] waypoints;

    int waypointIndex;
    Vector3 target;

    public bool iSDead;
    public Animator state_anim;
    public enum states
    {
        STATE_STAND = 0,
        STATE_PATRULLA = 1,
        STATE_MOVE_PLAYER = 2,
        STATE_SHOOT_PLAYER = 3,
        STATE_HIT = 4,
        STATE_FEAR = 5,
        STATE_DEAD = 6,

    }

    private void Start()
    {
        Ammo = state_anim.GetInteger("AmmoinMagEnemy");
        magSize = state_anim.GetInteger("AmmoinMagEnemy");

        UpdateDestination();
        waypoints = points;
        //EnterState((int)states.STATE_PATRULLA);

    }
    //public void NeverRotation()
    //{
    //    var RotatioFreeze = Quaternion.Euler(Vector3.zero.x, Vector3.zero.y, transform.rotation.z);
    //    transform.rotation = RotatioFreeze;
    //}

    [SerializeField] private int state = (int)states.STATE_STAND;

    //private int state = (int)states.STATE_STAND;
    //private Ray= new Ray;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player").transform;
        state_anim = GetComponent<Animator>();
        //current = 0;
        waypoints = points;
    }

    private void Patroling()
    {
        if (iSDead) return;
        if (!walkPointSet) SearchPattern();

        if (walkPointSet)
            agent.SetDestination(target);

        //Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;

        //GetComponent<MeshRenderer>().material = green;
    }

    //private void SearchWalkPoint()
    //{
    //    float randomZ = Random.Range(-walkPointRange, walkPointRange);
    //    float randomX = Random.Range(-walkPointRange, walkPointRange);

    //    walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

    //    if (Physics.Raycast(walkPoint, -transform.up, 2, whatIsGround))
    //        walkPointSet = true;
    //}

    private void SearchPattern()
    {
        //    if (agent.transform.position != points[current].position)
        //    {
        //        agent.SetDestination(points[current].position);
        //    }
        //    else
        //        current = current + 1;
        if (Vector3.Distance(transform.position, target) < 1)
        {
            UpdateDestination();
            IterateWayPointIndex();
        }
    }

    //void UpdateDestination()
    //{
    //    target = points[current].position;
    //    agent.SetDestination(target);
    //}

    //void IterateCurrent()
    //{
    //    current++;
    //    if(current == points.Length)
    //    {
    //        current = 0;
    //    }
    //}

    //void UpdateDestination()
    //{
    //    IterateWayPointinIndex();
    //    UpdateDestination();
    //}

    void UpdateDestination()
    {
    
        if(ExtrLibrary.isArrayemptyTransform(waypoints))
        { 
        target = waypoints[waypointIndex].position;
        agent.SetDestination(target);
        }
    }

    void IterateWayPointIndex()
    {
        if (ExtrLibrary.isArrayemptyTransform(waypoints))
        {
            waypointIndex++;
            if(waypointIndex == waypoints.Length)
            {
                waypointIndex = 0;
            }
        }
    }

    private void ChasePlayer()
    {
        if (iSDead) return;

        Debug.Log("Jugador en rango" + playerInSightRange);
        agent.SetDestination(player.position);

        Debug.Log("Posicion de jugador" + player.position);

        //GetComponent<MeshRenderer>().material = yellow;
        //base.ChasePlayer();
        Quaternion lookRotation = Quaternion.LookRotation(player.position);

    }
    private void AttackPlayer()
    {
        if (iSDead) return;
        //Make sure enemu doesn't move
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        
        //transform.localEulerAngles = new Vector3(0, transform.rotation.y, transform.rotation.z);
        //Quaternion lookRotation = Quaternion.LookRotation(player.position);
        //transform.rotation = lookRotation;

        if (!alreadyAttacked)
        {
            ///Attack code here

            if (Ammo >= 0)
            {
                Rigidbody rb = Instantiate(Projectile, Weapon.position, Quaternion.identity).GetComponent<Rigidbody>();
                rb.AddForce(Weapon.forward * 32f, ForceMode.Impulse);
                //rb.AddForce(Weapon.up * 8, ForceMode.Impulse);
                Debug.Log(Ammo);
                Ammo = Ammo - 1;
                state_anim.SetInteger("AmmoinMagEnemy", Ammo);            
                alreadyAttacked = true;
                Invoke("ResetAttack", timeBetweenAttacks);
            }

            else
            {
                state_anim.SetInteger("AmmoinMagEnemy", Ammo);
                float animationLength = state_anim.GetCurrentAnimatorStateInfo(0).length;
                if (state_anim.GetCurrentAnimatorStateInfo(0).IsName("ReloadRifle") && state_anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
                {
                    Ammo = magSize;
                    state_anim.SetInteger("AmmoinMagEnemy", Ammo);
                }
            }
        }
    }

    private void ResetAttack()
    {
        if (iSDead) return;

        alreadyAttacked = false;
    }

    IEnumerable ChangePatroling()
    {
        state_anim.SetBool("isStartPatrulla", true);
       yield return new WaitForSeconds(.1f);
        state_anim.SetBool("isStartPatrulla", false);
        //if(playerInSightRange)
        //{
        //    yield break;
        //}
    }

    public void TakeDamage(int damage)
    {
        //Hearth -= damage;

        //if (Hearth <= 0)
        //{
        //    iSDead = true;
        //    //Invoke(nameof(DestroyEnemy), 0.5f);
        //    SetState((int)states.STATE_DEAD);
        //}

        Hearth -= damage;

        isHit = true;
        state_anim.SetInteger("RandomDeath", RandomDeath());
        state_anim.Play("Rifle hit Idle");
        SetState((int)states.STATE_HIT);

        if (Hearth <= 0)
        {
            iSDead = true;
            state_anim.SetFloat("Health", Hearth);
            state_anim.Play("DeathRigth");
            SetState((int)states.STATE_DEAD);
        }
    }
    private int RandomDeath()
    {
        var randomD = Random.Range(0, 10);
        //state_anim.SetInteger("RandomDeath", randomD);
        return randomD;
    }
    public void DestroyEnemy()
    {
        Destroy(gameObject);
    }
    //private void Start()
    //{
    // //Detection = ISee.GetComponent<Collision>();
    //}

    //public void Fear()
    //{ 
    //  if(Hearth <= 40)
    //    {
    //        var randomF = Random.Range(0, 10);
    //        if(randomF >= 8)
    //        {
    //            SetState((int)states.STATE_FEAR);
    //        }
    //    }
    //}

    public void Update()
    {

        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange && !isHit && !iSDead && CLevelManager.Inst.GetDetection()== false) SetState((int)states.STATE_PATRULLA);
        if (playerInSightRange && !playerInAttackRange && !isHit && !iSDead) SetState((int)states.STATE_MOVE_PLAYER);
        if (playerInAttackRange && playerInSightRange && !isHit && !iSDead) SetState((int)states.STATE_SHOOT_PLAYER);
        if (playerInAttackRange || playerInSightRange)
        {
            if(CLevelManager.Inst.GetDetection() == false)
            {
                CLevelManager.Inst.SetEnemyDetection(true);
                SetState((int)states.STATE_MOVE_PLAYER);
                CControllerWave.Inst.StartWave();

            }
        }


        //DebugHealth();
        switch (state)
        {
            case (int)states.STATE_STAND:
                Debug.Log("Estado Stand");
                //var randomI = Random.Range(0, 3);
                //state_anim.SetFloat("RandomAnimationIdle",randomI);

                break;
            case (int)states.STATE_MOVE_PLAYER:
                ChasePlayer();
                EnterState((int)states.STATE_MOVE_PLAYER);
                state_anim.SetBool("IsEnemyInRange", false);
                state_anim.SetBool("IsDetectionEnemy", true);
                Debug.Log("Estado Shoot Player");
                break;
            case (int)states.STATE_PATRULLA:
                Patroling();
                break;
            case (int)states.STATE_SHOOT_PLAYER:
                state_anim.SetBool("IsEnemyInRange", true);
                AttackPlayer();
                break;
            case (int)states.STATE_HIT:
                Debug.Log("Estado Shoot Player");
                state_anim.SetFloat("Health", Hearth);
                if(state_anim.GetCurrentAnimatorStateInfo(0).IsName("Rifle hit Idle") && state_anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
                {
                    isHit = false;
                    state_anim.SetBool("IsHit", isHit);

                    if (!playerInAttackRange)
                    {
                        SetState((int)states.STATE_MOVE_PLAYER);
                    }

                    else if (playerInAttackRange)
                    {
                        SetState((int)states.STATE_SHOOT_PLAYER);
                    }
                }
                break;

            //case (int)states.STATE_FEAR:
            //    Debug.Log("Estado Scared");

            //break;
            case (int)states.STATE_DEAD:
                //CControllerWave.Inst.KilledEnemy();
                if (state_anim.GetCurrentAnimatorStateInfo(0).IsName("DeathRigth") && state_anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
                    Invoke(nameof(DestroyEnemy), 2f);

                break;
        }
    }


    //Esto es para mucho mas adelante y pulir el sistema a la hora de entrar en los estados
    //Dejar preparado para despues
    public int EnterState(int EnterState)
    {
        if (EnterState == (int)states.STATE_MOVE_PLAYER)
        {
            state_anim.Play("RifleRun");

        }
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
        //Debug.DrawRay(walkPoint, vector3, Color.red);
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
