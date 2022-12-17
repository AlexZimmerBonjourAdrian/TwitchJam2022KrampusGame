using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CControllerWave : MonoBehaviour
{
    public enum SpawnState
    {
        SPAWNING, WAITING, COUNTING, NOTSTART,COMPLETE
    };


    [System.Serializable]
    public class wave
    {
        public string name;
        public Transform enemy;
        public int count;
        public float rate;
    }
    public bool IsStartWave = false;
    public wave[] enemis;
    public wave[] waves;
    public List<wave[]> WavesLists;
    private int nextWave = 0;
    [SerializeField] public Transform[] spawnPoints;
    [SerializeField] public float timeBetweenWaves = 5f;
    [SerializeField] public float WaveCountDown;

    private SpawnState state = SpawnState.NOTSTART;
    private int enemySpawning;
    private int WaveNumber;
   
    [Header("Enemy Code my")]
    public int enemiesKilled;
    private int enemySpanwAmout = 2;
    private float searchCountdown = 1f;
    [SerializeField] private GameObject WaveUi;
    [SerializeField] private List<Transform> _List_Transform = new List<Transform>();
    [SerializeField] private Text TextoWave;
    [SerializeField] public bool CompleteWave;
    [SerializeField] public bool IsStart;

    private Animator anim_State;
    void Start()
    {
        CompleteWave = false;
        if (spawnPoints.Length == 0)
        {
            Debug.LogError("No spawn points referenced.");
        }
       
        anim_State = GetComponent<Animator>();
        //StartWave();
        CGameEvents.current.onStartWave += StartWave;
        //WaveCountDown = timeBetweenWaves;
        //enemySpanwAmout = 2;
    }
    private void Awake()
    {
        if (_inst != null && _inst != this)
        {
            Destroy(gameObject);
            return;
        }
        //DontDestroyOnLoad(this.gameObject);
        _inst = this;
    }

    void Update()
    {
        if (state != SpawnState.NOTSTART && state != SpawnState.COMPLETE)
        {

            if (state == SpawnState.WAITING)
            {
                if (!EnemyIsAlive())
                {
                    WaveCompleted();


                }
                else
                {
                    return;
                }
            }
            if (CompleteWave != true)
            {

                if (WaveCountDown <= 0)
                {
                    if (state != SpawnState.SPAWNING)
                    {
                        StartCoroutine(SpawnWave(waves[nextWave]));
                    }
                }
                else
                {
                    WaveCountDown -= Time.deltaTime;
                }

            }
            //if(WaveCountDown <= 0)
            //{
            //    if (state != SpawnState.SPAWNING)
            //    {
            //       StartCoroutine(SpawnWave(waves[nextWake]));
            //    }
            //}
            //else
            //{
            //    WaveCountDown -= Time.deltaTime;
            //}

            //if(WaveCountDown <= 0)
            //{

            //    Debug.Log("TODO: Continuar la Wave");
            //    Debug.Log("https://www.youtube.com/watch?v=q0SBfDFn2Bs&ab_channel=Brackeys");

            //}
            //Debug.Log("numero de ronda");
            //if (Input.GetKeyDown(KeyCode.Q)) 
            //{
            //    TestEnemyWaveSpawn();
            //}
           
            if (nextWave + 1 > waves.Length - 1)
            {
                //nextWave = 0;
                TextoWave.text = "All Clear";
                state = SpawnState.COMPLETE;
            }
        }
         TextoWave.text = "Spawning Wave: " + state;
    }


    void WaveCompleted()
    {
        Debug.Log("Wave Completed!");
        TextoWave.text = "Wave Completed!";

        state = SpawnState.COUNTING;
        WaveCountDown = timeBetweenWaves;

        CompleteWave = true;

        Debug.Log("aLL Waves complete? Lopping...");

        //Looping
        if (nextWave + 1 > waves.Length - 1)
        {
            //nextWave = 0;
            Debug.Log("aLL Waves complete? Lopping...");
            TextoWave.text = "aLL Waves complete? Lopping...";
            TextoWave.text = "Complete All Waves";
        }
        else
        {
            nextWave++;
        }

    }
    bool EnemyIsAlive()
    {
        //Debug.Log("Entra en EnemyIsAlive");
        searchCountdown -= Time.deltaTime;
        if (searchCountdown <= 0f)
        {
            //Debug.Log("Entra en searchCountdown");
            searchCountdown = .4f;
            if (GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                //Debug.Log("No Existen mas enemigos");
                return false;
            }
        }
        //Debug.Log("Hay enemigos vivos");
        return true;
    }
    IEnumerator SpawnWave(wave _wave)
    {
        //Debug.Log("Spawning Wave: " + _wave.name);

        //TextoWave.text = "Spawning Wave: " + _wave.name;

        state = SpawnState.SPAWNING;

        for (int i = 0; i < _wave.count; i++)
        {
            SpawnEnemy(_wave.enemy);
            yield return new WaitForSeconds(.5f/_wave.rate);
        }
        //
        state = SpawnState.WAITING;

        yield break;
    }


    void SpawnEnemy(Transform _enemy)
    {
        Debug.Log("Spawning Enemy: " + _enemy.name);
        TextoWave.text = ("Spawning Wave: " + _enemy.name).ToString();

        if (spawnPoints.Length == 0)
        {
            Debug.LogError("No spawn points referenced.");
        }
        Transform _sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
        //Instantiate(_enemy, transform.position, transform.rotation);
        SpawnEnemySpawn();
    }


    public static CControllerWave Inst
    {
        get
        {
            if (_inst == null)
            {
                GameObject obj = new GameObject("ControllerWave");
                return obj.AddComponent<CControllerWave>();
            }
            return _inst; 
        }
    }
    private static CControllerWave _inst;

    private void SpawnEnemySpawn()
    {
        int SpawnId = Random.Range(0, _List_Transform.Count);
        CManagerEnemy.Inst.Spawn(_List_Transform[SpawnId].transform.position);
    }

    public void StartWave()
    {
        state = SpawnState.COUNTING;
    }

    //public void startwave()
    //{
    //    wavenumber = 1;
    //    enemyspanwamout = 2;
    //    enemieskilled = 0;
    //    enemyspawning = enemyspanwamout;
    //    for (int i = 0; i < enemyspanwamout; i++)
    //    {
    //        spawnenemy();
    //    }

    //}



    //private void NextWave()
    //{
    //    WaveNumber++;
    //    enemySpanwAmout += 2;
    //    enemiesKilled = 0;
    //    for (int i = 0; i <= enemySpanwAmout; i++)
    //    {
    //        SpawnEnemy();
    //    }
    //}


    //IEnumerator TimeActiveUI()
    //{
    //    WaveUi.

    //}

    //public void KilledEnemy()
    //{


    //    if(enemiesKilled >= enemySpanwAmout)
    //    {
    //        NextWave();
    //    }
    //    else
    //    {
    //        enemiesKilled+= 1;
    //    }
    //}
   

}


//private void TestEnemyWaveSpawn()
// {
//     CManagerEnemy.Inst.Spawn(tesSpawnTransform.position);
// }

// //Todo Probar
// private void Waves(int NumberWabe, int MaxNumEnemy,List<Transform> positions)

// {
//     for(int i  = 0; i <= NumberWabe; i++)
//     {
//         foreach (Transform p in positions)
//         {
//             for (int j = 0; j <= (MaxNumEnemy/NumberWabe); j++)
//          {
//                 CManagerEnemy.Inst.Spawn(p.position);  
//          }
//         }
//     }
// }


