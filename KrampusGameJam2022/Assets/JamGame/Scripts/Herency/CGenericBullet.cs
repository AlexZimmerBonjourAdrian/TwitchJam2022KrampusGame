using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CGenericBullet : MonoBehaviour
{
    protected Rigidbody _rigidbody;
    // Start is called before the first frame update
    
    protected virtual void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public virtual void AddVel(Vector3 vel)
    {
        _rigidbody.AddForce(vel, ForceMode.Impulse);
    }
}
