using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MafiosoAttackAction : GoapAction
{
    private bool attacked = false;
    private MafiosoValueComponent Val;

    public void Start()
    {
        Val = GetComponent<MafiosoValueComponent>();
    }
    public MafiosoAttackAction()
    {
        Debug.Log("Entra en el efecto del mafioso");
        addEffect("damagePlayer", true);
        cost = -1f;
    }

   
    public override void reset()
    {
        attacked = false;
        target = null;
    }

    public override bool isDone()
    {
        return attacked;
    }

    //Esta parte seria apropiada revisar tambien el Codigo de Fear SDK 1.8 para cambiarlo
    // Tendria que asignar el target mediante el agent y no de esta manera
    public override bool checkProceduralPrecondition(GameObject agent)
    {
        target = GameObject.Find("Player");
        return target != null;
    }

    public override bool perform(GameObject agent)
    {
        
        Mafioso currMafioso = agent.GetComponent<Mafioso>();

       
        Debug.Log("Entra en Perform en la accion");
        if(currMafioso.ShootRangeFindOFWiew.canSeePlayer)
        {
            Debug.Log("Pasa Por El if ");
            //int damage = currMafioso.damage;
            Val.playAnimationsController("FiringRiffle");
            //currMafioso.player.GetComponent<CPlayerLife>().TakeDamage(damage);
            Rigidbody rb = Instantiate(currMafioso.Projectile, currMafioso.ShootTransform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(target.transform.position * 32f, ForceMode.Impulse);

           
            attacked = true;
            return true;
        }
        else{
            Val.playAnimationsController("RifleRun");
            return false;
        }
    }

    public override bool requiresInRange()
    {
        return true;
    }

    public override int TakeDamage(int _damage)
    {
        throw new System.NotImplementedException();
    }
     
}
