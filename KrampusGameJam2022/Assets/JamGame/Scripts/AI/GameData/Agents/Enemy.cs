using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;


public abstract class Enemy : MonoBehaviour, IGoap
{
    //Esto controla al Enemigo si hace falta modificar algo con respecto a la posicion y las demas variables es aqui.
    public Animator animator;
    public Rigidbody rigidbody;
    public BoxCollider boxCollider;
    public int health;
    public int speed;
    public int mag;
    public int damage;
    //Ejemplo para crear regen rate para poder crear una funsion
    public float regenRate;
    protected float terminalSpeed;
    //Velocidad inicial
    protected float initialSpeed;
    protected float acceleration;
    //Minima distanca operativa
    protected float minDist = 6f;
    protected float aggroDist = 60f;
    protected bool loop = true;
    public GameObject player;
    //FindOfView para chequear si el objetivo esta en la bista o no
    //to-do: crear una funsion que tome a el rango y ejecute el disparo
    public FindOfView ShootRangeFindOFWiew;
    //Projectile a usar
    public GameObject Projectile;
    //Posicion de disparo // no se si se usara
    public Transform ShootTransform;
    
    private NavMeshAgent navMeshEnemy;
    public MafiosoValueComponent MafiosoValues;
    public bool isDetected = false;

    public float moveSpeed = 1;
    public Transform PositionEnemy;
    public int damageRecived;
    public bool isDeath = false;
    public bool isHit = false;
    public void setDamageRecived(int _damageRecived)
    {
        damageRecived = _damageRecived;
    }
    void Start()
    {
        navMeshEnemy = GetComponent<NavMeshAgent>();
    }

    public virtual void Update()
    {
        Invoke("Detection", 1.0f);
    }

    
    public HashSet<KeyValuePair<string,object>> getWorldState()
    {
        HashSet<KeyValuePair<string, object>> worldData = new HashSet<KeyValuePair<string, object>>();
        worldData.Add(new KeyValuePair<string, object>("damagePlayer",true)); //to-do: change player's state for world data here
        worldData.Add(new KeyValuePair<string, object>("hitMe", true)); //to-do: change player's state for world data here
        worldData.Add(new KeyValuePair<string, object>("deathMe", true)); //to-do: change player's state for world data here
        return worldData;
    }

    


    public void actionsFinished()
    {
        Debug.Log("<color=blue>Actions completed</color>");
    }

    public abstract HashSet<KeyValuePair<string, object>> createGoalState();
    

    public bool moveAgent(GoapAction nextAction)
    {
        //Esto funciona debido a que no hay elementos en el campino que molesten, hay que hacer una prueba en un entorno mas cerrado
        float step = moveSpeed * Time.deltaTime;
        float dist = Vector3.Distance(transform.position, nextAction.target.transform.position);
        bool isKinematic = GetComponent<MafiosoValueComponent>().isKinematic;
        MafiosoValueComponent mafioso = GetComponent<MafiosoValueComponent>();
        if(isKinematic )
        {
            if (dist < aggroDist)
            {
                //Vector3 moveDirection = player.transform.position - transform.position;
                //navMeshEnemy.SetDestination(nextAction.target.transform.position);
                nextAction.agent.SetDestination(nextAction.target.transform.position);
                //mafioso.playAnimationsController("RifleRun");
                //transform.LookAt(player.transform.position);
                setSpeed(speed);
                nextAction.agent.speed = 4f;
            

                //Vector3 newPosition = moveDirection * initialSpeed * Time.deltaTime;
                //transform.position += newPosition;
            }
            if (dist <= minDist)
            {

                nextAction.setInRange(true);
                nextAction.agent.speed = 0;
               
                return true;
            }
            else
            {
                mafioso.playAnimationsController("RifleRun");
                return false;
            }
        }
        else
        {
            return false;
        }
        //float step = moveSpeed * Time.deltaTime;
        //gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, nextAction.target.transform.position, step);

        //if (gameObject.transform.position.Equals(nextAction.target.transform.position))
        //{
        //    // we are at the target location, we are done
        //    nextAction.setInRange(true);
        //    return true;
        //}
        //else
        //    return false;

    }

    public void planAborted(GoapAction aborter)
    {
        Debug.Log("<color=red>Plan Aborted</color> " + GoapAgent.prettyPrint(aborter));
    }

    public void planFailed(HashSet<KeyValuePair<string, object>> failedGoal)
    {
        // Not handling this here since we are making sure our goals will always succeed.
        // But normally you want to make sure the world state has changed before running
        // the same goal again, or else it will just fail.
        Debug.Log("Plan Failed ");
    }

    public void planFound(HashSet<KeyValuePair<string, object>> goal, Queue<GoapAction> actions)
    {
        Debug.Log("<color=green>Plan found</color> " +GoapAgent.prettyPrint(actions));
    }



    //void Start()
    //{
    //    if (MafiosoValues == null)
    //    {
    //        MafiosoValues = gameObject.AddComponent<MafiosoValueComponent>() as MafiosoValueComponent;
         
    //    }
    //    if(PositionEnemy == null)
    //    {
    //        PositionEnemy = GameObject.Find("Player").transform;
    //     }
    //}

    //void Update()
    //{

    //}
    public void setSpeed(float val)
    {
        initialSpeed = (val / 10) / 2;
        return;
    }

    public abstract void hitDamage();

    public abstract void deathMe();

    public abstract void TakeDamage(int damage);
    
     
}
