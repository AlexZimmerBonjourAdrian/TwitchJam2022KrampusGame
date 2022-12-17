using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DL;

public class CManagerRayCast : MonoBehaviour
{
    //private CManagerWeapon _ManagerWeapon;



    //private Keyboard kb = Keyboard.current;
    //private Mouse ms = Mouse.current;
    //[SerializeField] private LayerMask collision; 
    //public void Start()
    //{
    //    _ManagerWeapon = GetComponent<CManagerWeapon>();   
    //}
    //public void Update()
    //{
    //    //ShootController();
    //}

    //private void ShootController()
    //{
    //    Debug.Log("ShootController");
    //    if (_ManagerWeapon.GetCurrentWeapon() != null)
    //    { 
    //      var Weapon = _ManagerWeapon.GetCurrentWeapon().GetComponent<CArmed>();

    //         if (ms.leftButton.wasPressedThisFrame)
    //         {
    //            if(Weapon.GetAmmo_in_Mag() > 0)
    //            {
    //                TipeWeapon();
    //            }
    //         }   
    //   }
    //}

    //public void TipeWeapon()
    //{
    //    Debug.Log("ENTRA EN EL TIPO DE ARMA");
    //    var tipeWeapon = _ManagerWeapon.GetCurrentWeapon().GetComponent<CArmed>().GetWeaponType();
    //    var damageWeapon = _ManagerWeapon.GetCurrentWeapon().GetComponent<CArmed>().GetWeaponDamage();
    //    //Debug.Log("Nombre del arma: " + _ManagerWeapon.GetCurrentWeapon().GetComponent<CArmed>().GetWeaponName());
    //    RaycastHit hit;
    //    var ray = Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, collision);
    //    Debug.DrawRay(transform.position, -transform.TransformDirection(Vector3.forward) * hit.distance, Color.red);

    //    switch (tipeWeapon)
    //    {
    //        case "Pistol":
    //            ray = Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, collision);
    //            Debug.Log("Pistol");
    //            if (hit.collider.tag == "Enemy")
    //            {
    //                //hit.collider.GetComponent<CEnemy>().TakeDamage(damageWeapon);
    //            }
    //            break;
    //        //case "Shootgun":
    //        //    Debug.Log("Shootgun");
    //        //    ray = Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, collision);
    //        //    var ray_bullet = Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, collision);
    //        //    var ray_bullet_2 = Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, collision);
    //        //    var ray_bullet_3 = Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, collision);
    //        //    var ray_bullet_4 = Physics.Raycast(transform.position, transform.TransformDirection(new Vector3(5f,10f,35f)), out hit, Mathf.Infinity, collision);
    //        //    if (hit.collider.tag == "Enemy")
    //        //    {
    //        //        hit.collider.GetComponent<CEnemy>().TakeDamage(damageWeapon);
    //        //    }
    //        //    break;
    //        //case "Subfusil":
    //        //    Debug.Log("Subfusil");
    //        //    ray = Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, collision);
    //        //    if(hit.collider.tag == "Enemy")
    //        //    {
    //        //        hit.collider.GetComponent<CEnemy>().TakeDamage(damageWeapon);
    //        //    }
    //        //    break;
    //        //case "Carabina":
    //        //    Debug.Log("Carabina");
    //        //    for (int i = 3; i >= 0; i-- )
    //        //        ray = Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, collision);
    //        //    if (hit.collider.tag == "Enemy")
    //        //    {

    //        //        hit.collider.GetComponent<CEnemy>().TakeDamage(damageWeapon);
    //        //    }
    //        //    break;
    //        //case "RifleDeAssalto":
    //        //    Debug.Log("RifledeAssalto");
    //        //    if (ms.leftButton.isPressed)
    //        //     ray = Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, collision);
    //        //    if (hit.collider.tag == "Enemy")
    //        //    {
    //        //        hit.collider.GetComponent<CEnemy>().TakeDamage(damageWeapon);
    //        //    }
    //        //    break;
    //        //case "Rifle":
    //        //    Debug.Log("Rifle");
    //        //    ray = Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, collision);
    //        //    if (hit.collider.tag == "Enemy")
    //        //    {
    //        //        hit.collider.GetComponent<CEnemy>().TakeDamage(damageWeapon);
    //        //    }
    //        //    break;
    //        //case "Especial":
    //        //    Debug.Log("Especial");
    //        //    break;
    //        //case "Microfusil":
    //        //    Debug.Log("Microfusil");
    //        //    if (ms.leftButton.isPressed)
    //        //        ray = Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, collision);
    //        //    if (hit.collider.tag == "Enemy")
    //        //    {
    //        //        hit.collider.GetComponent<CEnemy>().TakeDamage(damageWeapon);
    //        //    }
    //        //    break;
    //        default:
    //            Debug.LogError("No existe esta categoria de armas, revisar si esta mal configurada los datos");
    //            break;

    //    }
}
