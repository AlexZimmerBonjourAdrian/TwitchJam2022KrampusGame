using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSwitch : MonoBehaviour
{
   

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            CGameManager.Inst.LoadSceneAsync("PlayGround");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            CGameManager.Inst.LoadSceneAsync("demo_day");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            CGameManager.Inst.LoadSceneAsyncAdditive("MenuTest");
        }
    }

    public void SwitchScene()
    {
        CGameManager.Inst.LoadSceneAsync(name);
    }

    public void SwitchSceneIndexName()
    {
        CGameManager.Inst.LoadScene(name);
    }
 

    public void Test(Vector3 vector)
    {
        Debug.Log(vector);
    }

}
