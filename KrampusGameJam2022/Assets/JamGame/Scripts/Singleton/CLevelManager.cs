using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CLevelManager : MonoBehaviour
{
    private GameObject DeathScreen;

    private static CLevelManager _inst;

    [SerializeField] private bool EnemyDetection;
    [SerializeField] private bool levelIsInterior;
    public static CLevelManager Inst
    {
        get
        {
            if (_inst == null)
            {
                GameObject obj = new GameObject("LevelManager");
                return obj.AddComponent<CLevelManager>();
            }
            return _inst;
        }
    }

    private void Awake()
    {
        //if (_inst != null && _inst != this)
        //{
        //    Destroy(gameObject);
        //    return;
        //}
        //DontDestroyOnLoad(this.gameObject);
        _inst = this;
        //_bulletAsset = Resources.Load<GameObject>("GenericBullet");
        // _bulletList = new List<CGenericBullet>();
    }
    // Start is called before the first frame update
    private void Start()
    {

        DeathScreen = GameObject.Find("DeathScreen");
    }

    //La solucion mas dificil pero puede que mas rapida en performance
    //Respaldo 
    //public void ResetButDontLoadLevel()
    //{

    //}
    //La forma no se si mas barata pero si comun
    public void update()
    {
         
      

    }

    
    public void ResetLoadLevel()
    {
        Scene scene = SceneManager.GetActiveScene();
        CGameManager.Inst.LoadScene(scene.name);
        //CControllerWave.Inst.StartWave();
    }

    public bool GetLevelIsInterior()
    {
        return levelIsInterior;
    }
    //public void Respawn()

    public void SetEnemyDetection(bool EnemyDetection )
    {
        this.EnemyDetection = EnemyDetection;
        CGameEvents.current.StartWave();
        Debug.Log( "Enemy Detection" + EnemyDetection);
    }

    public bool GetDetection()
    {
        if (EnemyDetection == true)
        {
            return true;
        }

        else
        {
            return false;
        }
    }
}

