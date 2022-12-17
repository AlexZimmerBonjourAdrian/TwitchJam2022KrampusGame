using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CArmed : MonoBehaviour
{
    //private weapon weapon_controller;
    public DataWeapon data;
    [Header("Name")]
    [SerializeField] public string weapon_name = "Name";
    [Header("Description")]
    [SerializeField] public string weapon_description;
    [Header("Equip SettingsSpeed")]
    [SerializeField] public float equip_speed = 1.0f;
    [SerializeField] public float unequip_speed = 1.0f;
    [Header("SpeedReload/workProgress")]
    [SerializeField] public float reload_speed = 1.0f;
    [Header("accurcy/Work")]
    [SerializeField] public float accuarcy = 1.0f;
    [Header("Type Weapon")]
    [SerializeField] public string type_Weapon = "Weapon";
    [Header("Recoil Setting")] 
    [SerializeField] public float recoil = 1.0f;
    //hipFire Recoil
    [SerializeField] protected float recoilX;
    [SerializeField] protected float recoilY;
    [SerializeField] protected float recoilZ;
    //Settings
    [SerializeField] protected float snappiness;
    [SerializeField] protected float returnSpeed;

    [SerializeField] protected CRecoil recoil_Script;

    [Header("Bloom")]
    [SerializeField] public float offset = 1.0f;
    [SerializeField] protected float Bloom;
    [Header("Execute MultiShoot/WIP")]
    [SerializeField] public bool multidisparo = false;
    [Header("Exparsion")]
    //Esto seria un valor que se multiplica en las armas es un Bloom mas o menos
    [SerializeField] public float exparsion = 1.0f;
    [Header("Ammo Settings")]
    [SerializeField] public int ammo_in_mag = 15;
    [SerializeField] public int extra_ammo = 30;
    [SerializeField] public int mag_size = 0;

    [Header("Damage")]
    [SerializeField] public int damage = 10;
    [Header("fire Rate")]

    [SerializeField] public float fire_rate = 1.0f;
    // Start is called before the first frame update
    [Header("Controll Values")]
    //Las variables de control estan en el Carmed como para Heredar pero nada mas
    [SerializeField] protected bool isShooting = false;
    [SerializeField] protected bool isReload = false;
    [SerializeField] protected bool isCrossing = false;
    [Header("Extra Values")]
    //Estp es para hacer una collision en el mundopara que el arma se viera o no es opcional pero esta preparada en caso de que se nesecite
    [SerializeField] protected LayerMask collision;
    [SerializeField] protected float msBetweenShots = 100;
    [SerializeField] protected float nextShotTime;
    //[SerializeField] protected Transform BulletSpawnPoint;
    [SerializeField] protected float ShootDelay = 0.5f;
    [SerializeField] protected float BulletSpeed = 100f;
    [SerializeField] protected float KickBack;
    [SerializeField] protected float LastShootTime;
    [SerializeField]
    private bool AddBulletSpread = true;
    [SerializeField] protected Vector3 BulletSpreadVariance = new Vector3(0.1f, 0.1f, 0.1f);
    [Header("Distance Max weapon")]
    [SerializeField] protected float distance = 100f;

    [Header("Effects")]
    [SerializeField] protected Transform muzzle;
    [SerializeField] protected float muzzleVelocity = 35;
    [SerializeField] protected GameObject shell;
    [SerializeField] protected Transform shellEjection;
    [SerializeField] protected CMuzzleFlash muzzleFlash;
    [SerializeField] protected ParticleSystem ImpactParticleSystem;
    [SerializeField] protected ParticleSystem ShootingSystemParticle;
    [SerializeField] protected TrailRenderer BulletTrail;
    [SerializeField] protected GameObject _bulletHolePreb;
    protected LayerMask Mask;
    [Header("Interface")]
    [SerializeField] public GameObject marketUI;


    [Header("Shoot")]
    [SerializeField] protected Transform ShootPosition;
    [SerializeField] public LayerMask maskEnemy;
    [SerializeField]
    protected Transform _WeaponTransform;

    protected Transform playerCamera;
    protected CCharacterBehaviour characterBehaviour;



    public void Awake()
    {
       
    }


    public virtual void Start()
    {
        muzzleFlash = GetComponent<CMuzzleFlash>();

    }


    public virtual string GetWeaponName()
    {
        return weapon_name;
    }

    public virtual string GetWeaponType()
    {
        return type_Weapon;
    }

    public virtual int GetWeaponDamage()
    {
        return damage;
    }

    public virtual int GetAmmo_in_Mag()
    {
        return ammo_in_mag;
    }

    public virtual bool GetIsShooting()
    {
        return isShooting;
    }

    public virtual bool GetIsReload()
    {
        return isReload;
    }

    public virtual bool GetIsCrossing()
    {
        return isCrossing;
    }
    public virtual void Add_ammo(DataPickUp PickUp)
    {
        extra_ammo += PickUp.Ammo;
    }
    public virtual void Trail(Transform BulletSpawnPoint)
    {
        if (LastShootTime - ShootDelay < Time.time)
        {

            //Use an object pool instead for these! To keep this tutorial focused, we'll skip impementing one
            //far more details you can see: https://youtu.be/fsDE_mO4RZM  and if using unity 2021+: https://youtu.be/zyzqA_CPz2E 
            Transform t_spawn = GameObject.Find("PlayerCam").transform;



            Vector3 t_bloom = t_spawn.position + t_spawn.forward * 1000f;
            t_bloom += Random.Range(-Bloom, Bloom) * t_spawn.up;
            t_bloom += Random.Range(-Bloom, Bloom) * t_spawn.right;
            t_bloom -= t_spawn.position;
            t_bloom.Normalize();
            RaycastHit t_hit = new RaycastHit();
            //ShootingSystem.Play();
            Vector3 direction = GetDirection();

            if (Physics.Raycast(BulletSpawnPoint.position, t_bloom, out t_hit, float.MaxValue, Mask))
            {
                TrailRenderer trail = Instantiate(BulletTrail, BulletSpawnPoint.position, Quaternion.identity);

                StartCoroutine(SpawnTrail(trail, t_hit.point, t_hit.normal, true));

                LastShootTime = Time.time;

            }

            else
            {
                TrailRenderer trail = Instantiate(BulletTrail, BulletSpawnPoint.position, Quaternion.identity);

                StartCoroutine(SpawnTrail(trail, transform.forward * 100, Vector3.zero, false));

                LastShootTime = Time.time;
            }



        }

        Instantiate(shell, shellEjection.position, shellEjection.rotation);
        muzzleFlash.Activate();

    }

    public virtual void Shoot()
    {


        #region Code To Verification
        //if(type_Weapon == "Pistol" || type_Weapon == "Shootgun"|| type_Weapon == "Rifle" )
        // {
        //     if (Input.GetKeyDown(KeyCode.Mouse0))
        //     {
        //         if (ammo_in_mag >= 0)
        //         {
        //             DebugFunction();
        //             DebugLog();
        //             isShooting = true;
        //             ammo_in_mag--;
        //             //data.ammo_in_mag --;
        //             extra_ammo--;
        //             //data.extra_ammo --;
        //         }
        //         isShooting = false;
        //     }
        // }
        // else if( type_Weapon == "Carabine")
        // {
        //    if(Input.GetKeyDown(KeyCode.Mouse0))
        //     { 
        //         if (ammo_in_mag >= 0)
        //         {
        //             isShooting = true;
        //             for (int i = 3; i >= 0; i--)
        //             {
        //             ammo_in_mag -= 1;
        //             //data.ammo_in_mag -= 1;
        //             extra_ammo -= 1;
        //             //data.extra_ammo -= 1;
        //             }

        //         }
        //         else if(ammo_in_mag < 0)
        //         {
        //             ammo_in_mag = 0;
        //             ammo_in_mag = 0;
        //         }
        //         isShooting = false;
        //     }
        // }
        //else
        // {
        //     if (Input.GetKeyDown(KeyCode.Mouse0))
        //     {
        //         if(ammo_in_mag >= 0)
        //         {
        //             ammo_in_mag -= 1;
        //             //data.ammo_in_mag -= 1;
        //             extra_ammo -= 1;
        //             //data.extra_ammo -= 1;
        //         }
        //     }
        //     isShooting = false;
        // }
        #endregion
    }

    private Vector3 GetDirection()
    {
        Vector3 direction = transform.forward;
        if (AddBulletSpread)
        {
            direction += new Vector3(
                Random.Range(-BulletSpreadVariance.x, BulletSpreadVariance.x),
                Random.Range(-BulletSpreadVariance.y, BulletSpreadVariance.y),
                Random.Range(-BulletSpreadVariance.z, BulletSpreadVariance.z)

                );
        }
        return direction;
    }

    private IEnumerator SpawnTrail(TrailRenderer Trail, Vector3 HitPoint, Vector3 HitNormal, bool MadeImpact)
    {
        // This has been updated from the video implementation to fix a commonly raised issue about the bullet trails
        // moving slowly when hitting something close, and not
        Vector3 startPosition = Trail.transform.position;
        float distance = Vector3.Distance(Trail.transform.position, HitPoint);
        float remainingDistance = distance;

        while (remainingDistance > 0)
        {
            Trail.transform.position = Vector3.Lerp(startPosition, HitPoint, 1 - (remainingDistance / distance));

            remainingDistance -= BulletSpeed * Time.deltaTime;

            yield return null;
        }
        
        Trail.transform.position = HitPoint;
        if (MadeImpact)
        {
            Instantiate(ImpactParticleSystem, HitPoint, Quaternion.LookRotation(HitNormal));
        }

        Destroy(Trail.gameObject, Trail.time);
    }
    public virtual void Reload()
    {
        #region ReloadCode
        //if(Input.GetKeyDown(KeyCode.R))
        // {
        //     if(ammo_in_mag >= 0)
        //     {
        //         ammo_in_mag = mag_size;
        //     }
        // }
        #endregion
    }


   

    public virtual void Equip()
    {
        this.gameObject.SetActive(true);
    }
    public virtual void Desequip()
    {
        this.gameObject.SetActive(false);
    }


    public void DebugLog()
    {
        Debug.Log(weapon_name);
        //Debug.Log(type_Weapon);
        Debug.Log(damage);
        Debug.Log("Mag Size: " + ammo_in_mag);
    }

    public void OnInteract()
    {
        DebugLog();
    }

    public virtual void LoadInfo()
    {
        weapon_name = data.weapon_name;
        weapon_description = data.weapon_description;
        equip_speed = data.equip_speed;
        unequip_speed = data.unequip_speed;
        reload_speed = data.reload_speed;
        accuarcy = data.accuarcy;
        type_Weapon = data.type_Weapon;
        recoilX = data.recoilX;
        recoilY = data.recoilY;
        recoilZ = data.recoilZ;
        returnSpeed = data.returnSpeed;
        snappiness = data.Snappiness;
        multidisparo = data.multidisparo;
        exparsion = data.exparsion;
        ammo_in_mag = data.ammo_in_mag;
        extra_ammo = data.extra_ammo;
        mag_size = data.mag_size;
        damage = data.damage;
        fire_rate = data.fire_rate;
        distance = data.distance;

    }

    public float GetRecoilX()
    {
        return recoilX;

    }

    public float GetRecoilY()
    {
        return recoilY;

    }

    public float GetRecoilZ()
    {
        return recoilZ;

    }

    public float GetSnappiness()
    {
        return snappiness;
    }

    public float GetReturnSpeed()
    {
        return returnSpeed;
    }

    public virtual void DebugFunction()
    {
        Debug.Log("IsShoting: " + isShooting);

        //Debug.Log("IsReload: " + isReload);

        //Debug.Log("IsCrossing: " + isCrossing);

    }

    public void BulletHole()
    {
        Ray rayOriginal = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        if (Physics.Raycast(rayOriginal, out hitInfo))
        {
            if (hitInfo.collider.tag == "wall")
            {
                //Inantiate bullet hole
                Instantiate(_bulletHolePreb, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));

                Vector3 direction = hitInfo.point - _WeaponTransform.position;
                _WeaponTransform.rotation = Quaternion.LookRotation(direction);
            }

        }

    }




}
