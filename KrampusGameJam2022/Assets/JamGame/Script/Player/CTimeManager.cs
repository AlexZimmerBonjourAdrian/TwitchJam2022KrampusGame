using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CTimeManager : MonoBehaviour
{
    [SerializeField] private float slowdownFactor = 0.05f;
    [SerializeField]private float slowdownLength = 2f;
    [SerializeField] private KeyCode KeyTime;
     void Update()
    {
        DoSlowmotion();
    }
   public void DoSlowmotion()
    {
        if (Input.GetKey(KeyTime))
        {
            Time.timeScale = slowdownFactor;
            Time.fixedDeltaTime = Time.timeScale * .02f;
        }
        else
        {
            if(CPauseMenu.GameIsPaused == false)
            { 
            Time.timeScale += (1f / slowdownLength) * Time.unscaledDeltaTime;
            Time.timeScale = Mathf.Clamp(Time.timeScale, 0, 1f);
            }
        }
    }

}
