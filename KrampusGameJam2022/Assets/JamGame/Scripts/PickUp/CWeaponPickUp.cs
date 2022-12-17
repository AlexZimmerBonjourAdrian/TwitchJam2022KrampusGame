using DL;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CWeaponPickUp : MonoBehaviour/* IInteract<GameObject>*/
{
    // Start is called before the first frame update

    public CManagerWeapon ManagerWeapon;
    //public Rigidbody rb;
    //public BoxCollider coll;
    //public Transform player, gunContainer, fpsCam;
   [SerializeField] private GameObject _weapon;

    //public BoxCollider box;

    //public float pickUpRange;
    //public float dropForwardForce, dropUpForce;

    //public bool equipped;
    //public static bool slotFull;

    public void Start()
    {

        ManagerWeapon = FindObjectOfType<CManagerWeapon>();
        //box = GetComponent<BoxCollider>();
        Debug.Log(ManagerWeapon);
    }
    private void Update()
    {
        ////Check if player is in range and "E" is pressed
        //Vector3 distanceToPlayer = player.position - transform.position;
        //if (!equipped && distanceToPlayer.magnitude <= pickUpRange && Input.GetKeyDown(KeyCode.E) && !slotFull) PickUp();

        ////Drop if equiped and "Q" is pressed
        //if (equipped && Input.GetKeyDown(KeyCode.Q)) Drop();
    }


    //public void addWeapon()
    //{

    //}

    private void PickUp()
    {
        //equipped = true;
        //slotFull = true;

        ////Make Rigidbody kinematic and Box Collider a trigger
        //rb.isKinematic = true;
        //coll.isTrigger = true;

        ////Enable script
        //ManagerWeapon.AddWeapon(_weapon);
       
    }

    private void Drop()
    {

    }
    //public void OnCollisionEnter(Collision Player)
    //{

    //    if(Player.gameObject.tag == "Player")
    //    {

    //        //ManagerWeapon.AddWeapon(_weapon);
    //        Destroy(this.gameObject);
    //    }
    //}

    void OnCollisionEnter(Collision collision)
    {
       if(collision.gameObject.tag == "Player")
        {
            Debug.Log("Entra en la funcion de collider");
            //ManagerWeapon.AddWeapon(_weapon);
            Destroy(this.gameObject);
        }
    }
    //public void OnInteract(GameObject Weapon)
    //{

    //}
}
