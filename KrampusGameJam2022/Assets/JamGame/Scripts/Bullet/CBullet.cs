using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBullet : CGenericBullet
{
    [SerializeField]private int Damage;
    // Start is called before the first frame update
   

    // Update is called once per frame
   

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
           collision.gameObject.GetComponent<CPlayerLife>().TakeDamage(Damage);

        }
    }

}
