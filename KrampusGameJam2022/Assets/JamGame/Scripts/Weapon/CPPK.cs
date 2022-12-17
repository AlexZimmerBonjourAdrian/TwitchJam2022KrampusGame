using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.InputSystem;

public class CPPK : CArmed
{

    [Header("Gun Setting")]
    public float fireRate = 0.1f;
    // Start is called before the first frame update

    [SerializeField] bool _canShoot;
    

    //public Image muzzleFlashImage;

    public Sprite[] flashes;

    public Vector3 normalLocalPosition;
    public Vector3 aimingLocalPosition;
    [SerializeField] int _ammoInReserve;
    public float aimSmoothing = 10f;

    [Header("Mouse setting")]
    public float mouseSensitivity = 10;
    Vector2 _currentRotation;
    public float weaponSwayAmount = 10f;
    //Weapon Recoil
    public bool randomizeRecoil;
    public Vector2 randomRecoilConstraints;
    //you only need to assign this if randomizeRecoil if off
    public Vector2[] recoilPattern;
    public bool _CanReload;
    [SerializeField] protected LayerMask LayerEnemy;

    //private weapon_Input _input;
    private Animator _anim;

    public float range = 100f;


    public override void Start()
        
    {
        base.Start();

        transform.localPosition = new Vector3(-0.177f, -0.267f, -0.114f);
        marketUI = GameObject.Find("Crosshair");
        //recoil_Script = GameObject.Find("CameraRecoil").GetComponent<CRecoil>();
        recoil_Script = GameObject.Find("CameraRecoil").GetComponent<CRecoil>();
        //_input = GetComponent<weapon_Input>();
        _anim = GetComponent<Animator>();
        playerCamera = GameObject.Find("PlayerCam").transform;
        _anim.SetInteger("Ammo", ammo_in_mag);
        _anim.Play("PPK_Summon");
        LoadInfo();
        _canShoot = true;
        _CanReload = false;
        maskEnemy = LayerMask.NameToLayer("enemy");
    }
    public void FixedUpdate()
    {
        //// Reload();
        // Controller();
        // if (Input.GetKeyDown(KeyCode.Mouse0) && _canShoot && ammo_in_mag > 0)
        // {
        //     _canShoot = false;
        //     ammo_in_mag--;
        //     StartCoroutine(Shootgun());
        //     //Debug.Log("Entro Aqui");
        //     //isShooting = true;
        //     //_anim.SetBool("IsShooting", isShooting);

        // }

        // else if (Input.GetKeyUp(KeyCode.R) && ammo_in_mag < mag_size && extra_ammo > 0)
        // {
        //     //isReload = true;
        //     ////_anim.SetBool("CanReload", isReload);
        //     int amountNeeded = mag_size - ammo_in_mag;
        //     if (amountNeeded >= _ammoInReserve)
        //     {
        //         ammo_in_mag += extra_ammo;
        //         extra_ammo -= amountNeeded;
        //     }
        //     else
        //     {
        //         ammo_in_mag = mag_size;
        //         extra_ammo -= amountNeeded;
        //     }
        // }
        ControllerAnimation();

        
    

        if (Input.GetKeyDown(KeyCode.Mouse0) && _canShoot && ammo_in_mag > 2 )
        {
            StartCoroutine(Shootgun());
            ammo_in_mag--;
            //recoil_Script.RecoilFire();
            //_canShoot = false;
        }
        else if (Input.GetKeyDown(KeyCode.Mouse0) && _canShoot && ammo_in_mag <= 1)
        {
            StartCoroutine(ShootgunNotAmmo());
            ammo_in_mag--;
            //recoil_Script.RecoilFire();
            //_canShoot = false;
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

        //if (FinishAnimation(_anim, _anim.GetCurrentAnimatorStateInfo(0), "PPK_Reload"))
        //{
        //    Debug.Log("entro en el finish animator");
        //    _canShoot = true;
        //}
        //Shoot();
    }
    public bool FinishAnimation(Animator animator, AnimatorStateInfo stateInfo, string NameAnimation)
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName(NameAnimation) && stateInfo.normalizedTime >= 0.9f)
        {
            Debug.Log("Entro en el finish y me salta que termino");
            return true;
        }
        Debug.Log("Entro en el finish y me salta que no termino");
        return false;

        
    }
    public void ControllerAnimation()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0) && _canShoot && ammo_in_mag > 1 /*&& isCrossing == false*/)
        {

            _anim.SetInteger("Ammo", ammo_in_mag);
            _anim.SetBool("IsShooting", _canShoot);
            
            
        }
        if (Input.GetKeyUp(KeyCode.Mouse0) )
        {
            _anim.SetInteger("Ammo", ammo_in_mag);
            _anim.SetBool("IsShooting", false);

        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && _canShoot && ammo_in_mag <= 0 /*&& isCrossing == false*/)
        {
            _anim.SetInteger("Ammo", ammo_in_mag);
            _anim.SetBool("IsShooting", _canShoot);
        }

        if (Input.GetKeyDown(KeyCode.R) && ammo_in_mag < mag_size && extra_ammo > 0 && ammo_in_mag <= 1)
        {
            //_anim.SetBool("IsCrossing", false);
            _canShoot = false;
            _anim.SetBool("CanReload", true);
            _anim.SetInteger("Ammo", ammo_in_mag);
            
            
        }
        else if (FinishAnimation(_anim, _anim.GetCurrentAnimatorStateInfo(0), "PPK_Reload") )
        {
            _canShoot = true;
            _anim.SetBool("CanReload", false);
            _anim.SetInteger("AmmoinMagEnemy", ammo_in_mag);
        }


       if (Input.GetKeyDown(KeyCode.R) && ammo_in_mag < mag_size && extra_ammo > 0 && ammo_in_mag >= 0)
        {
            _canShoot = false;
            _anim.SetBool("CanReload", true);
            _anim.SetInteger("Ammo", ammo_in_mag);
            
        }

        else if (FinishAnimation(_anim, _anim.GetCurrentAnimatorStateInfo(0), "PPK_Reload_Not_Ammo"))
        {
            _canShoot = true;
            _anim.SetBool("CanReload", false);
            _anim.SetInteger("AmmoinMagEnemy", ammo_in_mag);
        }
        //    if (Input.GetKeyUp(KeyCode.R) )
        //{
            
           
            
          
           
        //}
      
        //if (Input.GetMouseButton(1) )
        //{
        //    //isCrossing = true;
        //    //_anim.SetBool("IsCrossing", isCrossing);
        //    if (Input.GetKeyDown(KeyCode.Mouse0) && _canShoot && ammo_in_mag > 0)
        //    {
        //        _anim.SetBool("IsShooting", _canShoot);
        //    }
        //    else if (_canShoot == false)
        //    {
        //        _anim.SetBool("IsShooting", false);
        //    }
        //    //_anim.SetBool("IsShooting", _canShoot);
        //}
        //else if (Input.GetMouseButtonUp(1))
        //{
        //    isCrossing = false;
        //    _anim.SetBool("IsCrossing", isCrossing);

        //}
        //    //if(Input.GetMouseButtonDown(0))
        //    //{
        //    //    _anim.Play("ShootCrosshair");
        //    //}       
        //}
        //else if(Input.GetMouseButtonUp(1))
        //{
        //    isCrossing = false;
        //    _anim.SetBool("IsCrossing", isCrossing);       
        //}

        //Debug.DrawRay(transform.position, -transform.TransformDirection(Vector3.forward) * hit.distance, Color.red);

        //else if(this._anim.GetCurrentAnimatorStateInfo(0).IsName("Shoot"))
        //{
        //    _anim.SetBool("IsShooting", isShooting);
        //}

        //if(Input.GetKey(KeyCode.Mouse1))
        //{   
        //    Debug.Log("Entra Aqui");
        //    isCrossing = true;
        //    _anim.SetBool("IsCrossing", true);

        //    if (Input.GetKeyDown(KeyCode.Mouse0) )
        //    {
        //        RayCastForEne();
        //        isShooting = true;
        //        _anim.SetBool("IsShooting", isShooting);
        //    }
        //}

        //else if(Input.GetKeyUp(KeyCode.Mouse1))
        //{
        //    //_anim.playbackTime("Crosshair");
        //    _anim.StartPlayback();
        //    isCrossing = false;
        //    _anim.SetBool("IsCrossing", isCrossing);  
        //}




    }

    //public override void Shoot()
    //{
    //    base.Shoot();
    //}

    public void ShootWeapon()
    {

    }

    public override void Add_ammo(DataPickUp PickUp)
    {
        base.Add_ammo(PickUp);
    }

    //public override void Reload()
    //{
    //    base.Reload();
    //}

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
    //IEnumerable ShootCrosshair()
    //{
    //    _anim.Play("ShootCrosshair");
    //    yield return new WaitForSeconds(fire_rate);
    //    _anim.Play("")

    //}

    //void DetermineAim()
    //{
    //    Vector3 target = normalLocalPosition;
    //    if (Input.GetMouseButton(1)) 

    //    Vector3 desiredPosition = Vector3.Lerp(transform.localPosition, target, Time.deltaTime * aimSmoothing);

    //    transform.localPosition = desiredPosition;
    //}
    IEnumerator Shootgun()
    {

       
        yield return new WaitForSeconds(fire_rate);
        RayCastForEne();
        //Shoot();
        _anim.Play("PPK_Shoot");
        _canShoot = true;

    }
    IEnumerator ShootgunNotAmmo()
    {


        yield return new WaitForSeconds(fire_rate);
        RayCastForEne();
        //Shoot();
        _anim.Play("PPK_Shoot_Not_Ammo");
        _canShoot = true;

    }

    void RayCastForEne()
    {
        RaycastHit hit;
        if (Physics.Raycast(playerCamera.position, playerCamera.forward, out hit,Mathf.Infinity,maskEnemy))
        {
           
                Debug.Log("Hit an Enemy");
                //marketUI.GetComponent<CHitmarket>().Hit();
            //Rigidbody rb = hit.transform.GetComponent<Rigidbody>();
            //rb.constraints = RigidbodyConstraints.None;w
            //rb.AddForce(transform.parent.transform.forward * 500);
            hit.collider.GetComponent<MafiosoHitAction>().TakeDamage(damage);
                //hit.collider.GetComponent<CMafioso>().TakeDamage(damage);
                //Debug.Log(hit.collider.gameObject.GetComponent<CMafioso>().Hearth);
                //Debug.DrawRay(transform.position, transform.forward, Color.red);
           


        }
    }
}

