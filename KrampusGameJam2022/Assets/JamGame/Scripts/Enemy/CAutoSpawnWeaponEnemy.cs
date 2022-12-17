using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CAutoSpawnWeaponEnemy : MonoBehaviour
{

    
    private enum tipe_weapon
    {
        Berreta =0,
        Calico = 1,
        M4A1 = 2,
        AK74M = 3,
        MP5k=4,
        MCkmilliam = 5,
        Judge = 6,
        
    }

   [SerializeField]  private List<GameObject> guns = new List<GameObject>();

    private GameObject gun;

    // Start is called before the first frame update
    void Start()
    {
        autoSpawnWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void autoSpawnWeapon()
    {
        var IdWeapon = Random.Range(0, guns.Count);
        gun = (GameObject)Instantiate(guns[IdWeapon], transform.position, transform.rotation);
        gun.transform.parent = gameObject.transform;
        gun.transform.localEulerAngles = Vector3.zero;
        gun.transform.localPosition = new Vector3(-0.039f, -0.36f, 0.424f);
        gun.transform.localRotation = Quaternion.Euler(0f, 9.535f, 0);
    }
}
