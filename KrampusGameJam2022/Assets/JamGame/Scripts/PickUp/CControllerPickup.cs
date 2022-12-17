using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.InputSystem;
public class CControllerPickup : MonoBehaviour
{

    //private Keyboard kb = Keyboard.current;
    // Start is called before the first frame update
    [SerializeField] private List<Transform> _SpawnTransform;
    [SerializeField] private List<GameObject> _WeaponAsset;
    private void Start()
    {

        //_AssetPPk = Resources.Load("/Prefabs/PickUps/PPK-PickUp.prefab") as GameObject;
        //_AssetMP5K = Resources.Load("/Prefabs/PickUps/mp5K-PickUp.prefab") as GameObject; 
    }
    public void Update()
    {
        TestController();
    }
    private void TestController()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            int WeaponId = Random.Range(0, _WeaponAsset.Count);
            int Positiion = Random.Range(0, _SpawnTransform.Count);
            CManagerPickUp.Inst.SpawnWeapon(_SpawnTransform[Positiion].position, _WeaponAsset[WeaponId]);
        }
        //if(kb.eKey.wasPressedThisFrame)
        //{
        //    CManagerPickUp.Inst.SpawnWeapon(transform.position, _AssetMP5K);
        //}
    }
}
