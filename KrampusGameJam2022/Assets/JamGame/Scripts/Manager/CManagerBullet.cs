using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CManagerBullet : MonoBehaviour
{
    private List<CBullet> _bulletList = new List<CBullet>();
    [SerializeField] private GameObject _bulletAsset;

    public static CManagerBullet Inst
    {
        get
        {
            if(_inst == null)
            {
                GameObject obj = new GameObject("ManagerBullet");
                return obj.AddComponent<CManagerBullet>();
            }
            return _inst;
        }
    }
    private static CManagerBullet _inst;
    private void Awake()
    {
        if(_inst != null && _inst != this)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(this.gameObject);
        _inst = this;
    }

    public void Update()
    {
        for(int i = _bulletList.Count -1; i>= 0; i--)
        {
            if (_bulletList[i] == null)
                _bulletList.RemoveAt(i);
        }
    }

    public void Spawn(Vector3 post, Vector3 vel, float Rot)
    {
        GameObject obj = (GameObject)Instantiate(_bulletAsset, post, Quaternion.identity);
        // Vector3 localScale = obj.transform.localScale;
        //localScale.x * =Rot
        CBullet newBullet = obj.GetComponent<CBullet>();
        //newBullet.addVel(vel);
        _bulletList.Add(newBullet);
    }

    //public List<CGenericBullet> GetList()
    //{
    //    return _bulletList;
    //}
}
