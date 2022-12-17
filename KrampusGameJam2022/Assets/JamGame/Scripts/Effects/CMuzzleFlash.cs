using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMuzzleFlash : MonoBehaviour
{

    public GameObject flashHolder;
    //public Sprite[] flashSprite;
    //public SpriteRenderer[] spriteRenderers;
    [SerializeField]public ParticleSystem[] Muzzles;

    public float flashTime;


    // Start is called before the first frame update
    void Start()
    {
        Deactivate();
    }

    public void Activate()
    {
        flashHolder.SetActive(true);

        int flashSpriteIndex = Random.Range(0, Muzzles.Length);
        for (int i = 0; i < Muzzles.Length; i++)
        {
            Muzzles[i]= Muzzles[flashSpriteIndex];
        }

        Invoke("Deactivate", flashTime);
    }

    public void Deactivate()
    {
        flashHolder.SetActive(false);
    }

}
