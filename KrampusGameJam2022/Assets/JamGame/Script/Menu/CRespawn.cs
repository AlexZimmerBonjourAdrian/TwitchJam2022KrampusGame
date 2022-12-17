using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CRespawn : MonoBehaviour
{
    // Start is called before the first frame update

    public static bool PlayerIsDead = false;
    public GameObject DeathMenuUi;

    private void Update()
    {
        if(PlayerIsDead == true)
        {
            Dead();
           if(Input.GetKeyDown(KeyCode.R))
           {
                Respawn();
           }
        }
    }
    public void Dead()
    {
        DeathMenuUi.SetActive(true);
        Time.timeScale = 0;
        //PlayerIsDead = true;
    }
    public void Respawn()
    {
        //DeathMenuUi.SetActive(false);
        Time.timeScale = 1;
        PlayerIsDead = false;
        CLevelManager.Inst.ResetLoadLevel();

    }

}
