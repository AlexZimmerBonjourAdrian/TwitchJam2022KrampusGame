using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CPlayerLife : MonoBehaviour
{
    public int playerHealth = 100;

    [SerializeField] private Slider BarGrphic;
    [SerializeField] private Text LifePlayer;
   
    #region PlaholderValues 
    private bool IsInMortal = false;
    #endregion
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            IsInMortal = !IsInMortal;
        }
        if(IsInMortal == true)
        {
            playerHealth = 100;
        }    
        UpdateHealth();
        //colission = GetComponentInChildren<CapsuleCollider>();
    }
    // Start is called before the first frame update
    public void UpdateHealth()
    {
        LifePlayer.text = playerHealth.ToString();
    }
    public void TakeDamage(int Damage)
    {
       
        if(playerHealth >= Damage)
        { 
            playerHealth = playerHealth - Damage;
        }
        else
        {
            playerHealth = 0;
        }
        if (playerHealth <=0)
        {
            Invoke(nameof(LoseCondition), .05f);
        }
    }
    public  void LoseCondition()
    {
        //Death.GetComponent<CRespawn>().Dead();
        CRespawn.PlayerIsDead = true;
    }

   
   




}
