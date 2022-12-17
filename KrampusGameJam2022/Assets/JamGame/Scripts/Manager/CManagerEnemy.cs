using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CManagerEnemy : MonoBehaviour
{

    // Start is called before the first frame update
    public static CManagerEnemy Inst
    {
        get
        {
            if(_inst == null)
            {
                GameObject obj = new GameObject("GameManager");
                return obj.AddComponent<CManagerEnemy>();
            }
            return _inst;
        }
    }
    private static CManagerEnemy _inst;

    private ArrayList _EnemyList = new ArrayList();
    [SerializeField] private GameObject _EnemyAsset;
    //[SerializeField]private List<Transform> transforms = new List<Transform>(); 
    public void Awake()
    {
        if(_inst != null && _inst != this)
        {
            Destroy(gameObject);
            return;
        }

        //DontDestroyOnLoad(this.gameObject);
        _inst = this;
    }

    public void FixedUpdate()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        for(int i = _EnemyList.Count -1; i >= 0; i--)
        {
            if (_EnemyList[i] == null)
                _EnemyList.RemoveAt(i);
        }
    }

    

    public void Spawn(Vector3 pos)
    {
        GameObject obj = (GameObject)Instantiate(_EnemyAsset, pos, Quaternion.identity);
        CMafioso newEnemy = obj.GetComponent<CMafioso>();
        _EnemyList.Add(newEnemy);
    }
}
