using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;
    public GameObject optionMenuUi;

    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
    if(Input.GetKeyDown(KeyCode.Escape))
        {

            if(GameIsPaused)
            {
                Resume();
            }else
            {
                Paused();
            }
        }
    }
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

     public void Paused()
    {

        pauseMenuUI.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadMenu()
    {
        CGameManager.Inst.LoadScene("MenuTest");
    }

    public void Option()
    {
        pauseMenuUI.SetActive(false);
        optionMenuUi.SetActive(true);
       
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
