using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CManagerPlayer : MonoBehaviour
{
    public static CManagerPlayer Inst
    {
        get
        {
            if (_inst == null)
            {
                GameObject obj = new GameObject("PlayertManager");
                return obj.AddComponent<CManagerPlayer>();
            }
            return _inst;
        }
    }
    private static CManagerPlayer _inst;
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
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
