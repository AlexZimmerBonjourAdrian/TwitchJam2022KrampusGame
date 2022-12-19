using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CObjective : MonoBehaviour
{
    public CAudio audioComplete;
    public bool isComplete = false;
    public AudioSource CompleteTask;
    public TGameManager Manager;
    public void Start()
    {
        audioComplete =  GetComponent<CAudio>();
        CompleteTask = GameObject.Find("ObjectiveCompleteSFX").GetComponent<AudioSource>();
        Manager = GameObject.Find("GameManager").GetComponent<TGameManager>();
    }
    public void ChangeStateObjective()
    {
        Manager.burningChrismas();
        audioComplete.ReproductionSound(CompleteTask);
        isComplete = !isComplete;
    }
    public bool wasCompleteMision()
    {
        
        return isComplete;
    }

}
