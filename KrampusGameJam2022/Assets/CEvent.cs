using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEvent : MonoBehaviour
{

    // Start is called before the first frame update
   public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Consumible")
        {
            collision.gameObject.GetComponent<CObjective>().ChangeStateObjective();
            Destroy(collision.gameObject);
        }
    }
}
