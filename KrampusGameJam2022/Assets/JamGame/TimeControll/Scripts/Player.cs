using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.ImageEffects;
public class Player : MonoBehaviour
{
    private TimeManager timemanager;
    private CAudio Audio;
    [SerializeField]private AudioSource StopTimeSFX;
    [SerializeField] private AudioSource ContinueTimeSFX;
    [SerializeField] private AudioSource MusicNotStopTimeSFX;
    [SerializeField] private AudioSource MusicStopTimeSFX;
    //public GrayscaleLayers Grayscale;
    // Start is called before the first frame update
    void Start()
    {
        timemanager = GameObject.FindGameObjectWithTag("TimeManager").GetComponent<TimeManager>();
        Audio = GetComponent<CAudio>();
        Audio.ChangeLoop(MusicNotStopTimeSFX);
        Audio.ChangeLoop(MusicStopTimeSFX);
    }

    // Update is called once per frameSOP
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.T)) //Stop Time when Q is pressed
        {
            timemanager.StopTime();
            Audio.ReproductionSound(StopTimeSFX);
            Audio.StopMusic(MusicStopTimeSFX);
            Audio.ReproductionMusic(MusicNotStopTimeSFX);
              
            //Grayscale.enabled = true;
        }
        if(Input.GetKeyDown(KeyCode.R) && timemanager.TimeIsStopped)  //Continue Time when E is pressed
        {
            timemanager.ContinueTime();
            Audio.ReproductionSound(ContinueTimeSFX);
            Audio.StopMusic(MusicNotStopTimeSFX);
            Audio.ChangeLoop(MusicStopTimeSFX);
       
            //Grayscale.enabled = false;

        }
    }


  
}
