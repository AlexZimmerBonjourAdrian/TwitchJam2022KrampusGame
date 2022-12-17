using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CM4Shootgun : CArmed
{

    [Header("Gun Setting")]

    [SerializeField] bool _canShoot;


    public Vector3 normalLocapPoition;
    public Vector3 aimIngLocalPosition;

    public float aimSmoothing = 10f;

    [Header("Mouse setting")]
    public float mouseSensitivity = 10;
    Vector2 _currentRotation;
    public float weaponSwayAmount = 10f;
    [SerializeField] int _ammoInReserve;
    //Weapon Recoil
    public bool randomizeRecoil;
    public Vector2 randomRecoilConstraints;
    //you only need to assign this if randomizeRecoil if off
    public Vector2[] recoilPattern;
    [SerializeField] protected LayerMask LayerEnemy;
    [SerializeField] private Transform _Shootposition;
    [SerializeField] private float range = 30;
    //[SerializeField] private CRecoil recoil_Script;
    //Weapon Recoil



    // Start is called before the first frame update
    void Start()
    {
        _canShoot = true;
        //_Shootposition = GetComponentInChildren<Transform>();
        recoil_Script = GameObject.Find("CameraRecoil").GetComponent<CRecoil>();
        playerCamera = GameObject.Find("PlayerCam").transform;
        marketUI = GameObject.Find("Crosshair");
        maskEnemy = LayerMask.NameToLayer("enemy");
        LoadInfo();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (Input.GetKeyDown(KeyCode.Mouse0) && _canShoot && ammo_in_mag > 0)
        {
            _canShoot = false;

            ammo_in_mag--;
            StartCoroutine(ShootGun());
        }

        else if (Input.GetKeyDown(KeyCode.R) && ammo_in_mag < mag_size && extra_ammo > 0)
        {
            int amoutNeeded = mag_size - ammo_in_mag;
            if (amoutNeeded >= extra_ammo)
            {
                ammo_in_mag += extra_ammo;
                extra_ammo -= amoutNeeded;
            }
            else
            {
                ammo_in_mag = mag_size;
                extra_ammo -= amoutNeeded;
            }
        }
    }

    public override void Shoot()
    {
        base.Shoot();
    }

    public override void Add_ammo(DataPickUp PickUp)
    {
        base.Add_ammo(PickUp);
    }

    public override void Reload()
    {
        base.Reload();
    }

    public override void LoadInfo()
    {
        base.LoadInfo();
    }

    public override string GetWeaponName()
    {
        return base.GetWeaponName();
    }

    public override string GetWeaponType()
    {
        return GetWeaponType();
    }

    public override int GetWeaponDamage()
    {
        return GetWeaponDamage();
    }

    public override int GetAmmo_in_Mag()
    {
        return GetAmmo_in_Mag();
    }

    public override bool GetIsShooting()
    {
        return GetIsShooting();
    }

    public override bool GetIsReload()
    {
        return GetIsReload();
    }

    public override bool GetIsCrossing()
    {
        return GetIsCrossing();
    }

    public override void Equip()
    {
        base.Equip();
    }
    public override void Desequip()
    {
        base.Desequip();
    }
    IEnumerator ShootGun()
    {
        //Shoot();
        RayCastForEne();
        //recoil_Script.RecoilFire();
        yield return new WaitForSeconds(fire_rate);
        _canShoot = true;

    }
    public void RayCastForEne()
    {
        RaycastHit hit;
        RaycastHit hit_1;
        RaycastHit hit_2;
        RaycastHit hit_3;

        //GameObject muzzleInstance = Instantiate(muzzle, spawnPoint.position, spawnPoint.localRotation);
        //muzzleInstance.transform.parent = spawnPoint;

        //Bulle that goes forward;
        //if(Physics.Raycast(_Shootposition.position, _Shootposition.forward, out hit,distance),)
        if (Physics.Raycast(playerCamera.position, playerCamera.forward, out hit, Mathf.Infinity, maskEnemy))
        {
            //Instantiate(impact, hit.point, Quaternion.LookRotation(hit.normal));

            //Apply damage if you have a method that does it;
         
                Debug.Log("Hit an Enemy");
                //marketUI.GetComponent<CHitmarket>().Hit();
            //Rigidbody rb = hit.transform.GetComponent<Rigidbody>();
            //rb.constraints = RigidbodyConstraints.None;
            //rb.AddForce(transform.parent.transform.forward * 500);
            hit.collider.GetComponent<MafiosoHitAction>().TakeDamage(damage);
            //Debug.Log(hit.collider.gameObject.GetComponent<CMafioso>().Hearth);
            //Debug.DrawRay(transform.position, transform.forward, Color.red);

        }
        if (Physics.Raycast(playerCamera.position, playerCamera.forward + new Vector3(-.2f, 0f,0f), out hit_1, Mathf.Infinity, maskEnemy))
        {
            //Instantiate(impact, hit.point, Quaternion.LookRotation(hit_1.normal));

          
                Debug.Log("Hit an Enemy");
                //marketUI.GetComponent<CHitmarket>().Hit();
            //Rigidbody rb = hit.transform.GetComponent<Rigidbody>();
            //rb.constraints = RigidbodyConstraints.None;
            //rb.AddForce(transform.parent.transform.forward * 500);
            hit_1.collider.GetComponent<MafiosoHitAction>().TakeDamage(damage);
            //Debug.Log(hit.collider.gameObject.GetComponent<CMafioso>().Hearth);
            //Debug.DrawRay(transform.position, transform.forward, Color.red);


            //Apply damage if you have a method that does it;

        }
        if (Physics.Raycast(playerCamera.position, playerCamera.forward + new Vector3(.0f, .1f, 0f), out hit_2, Mathf.Infinity, maskEnemy))
        {
            //Instantiate(impact, hit.point, Quaternion.LookRotation(hit_2.normal));

            //Apply damage if you have a method that does it;
            
                Debug.Log("Hit an Enemy");
                //marketUI.GetComponent<CHitmarket>().Hit();
            //Rigidbody rb = hit.transform.GetComponent<Rigidbody>();
            //rb.constraints = RigidbodyConstraints.None;
            //rb.AddForce(transform.parent.transform.forward * 500);
            hit_2.collider.GetComponent<MafiosoHitAction>().TakeDamage(damage);
            //Debug.Log(hit.collider.gameObject.GetComponent<CMafioso>().Hearth);
            //Debug.DrawRay(transform.position, transform.forward, Color.red);



        }
        if (Physics.Raycast(playerCamera.position, playerCamera.forward + new Vector3(0f, -1f, 0f), out hit_3, Mathf.Infinity, maskEnemy))
        {
            //Instantiate(impact, hit.point, Quaternion.LookRotation(hit_3.normal));

            //Apply damage if you have a method that does it;
           
                Debug.Log("Hit an Enemy");
                //marketUI.GetComponent<CHitmarket>().Hit();
            //Rigidbody rb = hit.transform.GetComponent<Rigidbody>();
            //rb.constraints = RigidbodyConstraints.None;
            //rb.AddForce(transform.parent.transform.forward * 500);
            hit_3.collider.GetComponent<MafiosoHitAction>().TakeDamage(damage);
            //Debug.Log(hit.collider.gameObject.GetComponent<CMafioso>().Hearth);
            //Debug.DrawRay(transform.position, transform.forward, Color.red);


        }
    }
}
