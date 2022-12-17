using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CManagerPickUp : MonoBehaviour
{

    public List<Transform> transforms;
   [SerializeField] private List<GameObject> _WeaponAsset;

     private List<CWeaponPickUp> _PickUpList;
    public static CManagerPickUp Inst
    {
        get
        {
            if (_inst == null)
            {
                GameObject obj = new GameObject("PickUpManager");
                return obj.AddComponent<CManagerPickUp>();
            }
            return _inst;
        }
    }
    private static CManagerPickUp _inst;
    private void Awake()
    {
        if (_inst != null && _inst != this)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(this.gameObject);
        _inst = this;
        //_bulletAsset = Resources.Load<GameObject>("GenericBullet");
        // _bulletList = new List<CGenericBullet>();
    }

    // Start is called before the first frame update
  
    // Update is called once per frame
    void Update()
    {
        //for (int i = _PickUpList.Count - 1; i >= 0; i--)
        //{
        //    if (_PickUpList[i] == null)
        //        _PickUpList.RemoveAt(i);
        //}

    }

    public void SpawnWeapon(Vector3 post, GameObject _AssetPickUp)
    {

        GameObject obj = (GameObject)Instantiate(_AssetPickUp, post, Quaternion.identity);

        CWeaponPickUp newWeapon = obj.GetComponent<CWeaponPickUp>();
        
        _PickUpList.Add(newWeapon);

    }
   
}
