using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CAudio : MonoBehaviour
{
    //public AudioSource musicOrSound;
    public AudioSource musicOrSound_1;
    public AudioSource musicOrSound_2;
    public AudioSource musicOrSound_3;
    public bool IsLoop = false;
    // Start is called before the first frame update
    public void Start()
    {
        musicOrSound_3.Play();
    }
    public void ReproductionMusic(AudioSource musicOrSound)
    {
        musicOrSound.Play();
    }
    public void ReproductionSound(AudioSource musicOrSound)
    {
        musicOrSound.Play();
    }

    public void ChangeLoop(AudioSource musicOrSound)
    {
        musicOrSound.loop = !musicOrSound.loop;
    }

    public void StopMusic(AudioSource musicOrSound)
    {
        musicOrSound.Stop();
    }
  
}
