using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CGameManager : MonoBehaviour
{


    public static CGameManager Inst
    {
        get
        {
            if (_inst == null)
            {
                GameObject obj = new GameObject("GameManager");
                return obj.AddComponent<CGameManager>();
            }
            return _inst;
        }
    }
    private static CGameManager _inst;
    private AsyncOperation _currentLoadingScene;

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
    public void LateUpdate()
    {
        if(_currentLoadingScene != null)
        {
            _currentLoadingScene = null;
        }
    }

    public bool IsLoadingScene()
    {
        return _currentLoadingScene != null && !_currentLoadingScene.isDone;
    }

    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void LoadSceneAsync(string name)
    {
        _currentLoadingScene = SceneManager.LoadSceneAsync(name);
    }

    public void LoadSceneAsyncAdditive(string name)
    {
        _currentLoadingScene = SceneManager.LoadSceneAsync(name, LoadSceneMode.Additive);
    }

    public void Salir()
    {
        Application.Quit();
    }

}
