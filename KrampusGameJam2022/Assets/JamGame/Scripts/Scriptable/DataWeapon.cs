using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Weapon", menuName = "weapon")]
public class DataWeapon : ScriptableObject
{
    // Start is called before the first frame update
    public string weapon_name = "Name";
    public string weapon_description;
    public float equip_speed = 1.0f;
    public float unequip_speed = 1.0f;
    public float reload_speed = 1.0f;
    public float accuarcy = 1.0f;
    public string type_Weapon = "Weapon";
   
    public float recoilX= 1.0f;
    public float recoilY = 1.0f;
    public float recoilZ = 1.0f;
    public float returnSpeed = 1.0f;
    public float Snappiness = 1.0f;
    public float offset = 1.0f;
    public bool multidisparo = false;
    public float exparsion = 1.0f;
    public int ammo_in_mag = 15;
    public int extra_ammo = 30;
    public int mag_size = 0;
    public int damage = 10;
    public float fire_rate = 1.0f;
    public float distance = 100.0f;

    public void UpdateMagSize()
    {
        mag_size = ammo_in_mag;
    }
}

