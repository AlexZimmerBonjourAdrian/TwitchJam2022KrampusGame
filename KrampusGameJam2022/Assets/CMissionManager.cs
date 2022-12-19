using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CMissionManager : MonoBehaviour
{

    [System.Serializable]
    public struct Mission
    {
        public int idMision;
        public List<CObjective> listObjectives;
        public List<SphereCollider> listTriggerTask;
    };
    
    public List<Mission> listTask;
    public AudioSource CompleteTask;
    public CAudio Audio;
    //public List<GameObject> listObjectives;
    //public List<SphereCollider> listTriggerTask;
    private void Start()
    {
        Audio = GetComponent<CAudio>();
        ControllerTrigger();
        
    }

    public void Update()
    {
        for (int i = listTask.Count - 1; i >= 0; i--)
        {
            if (listTask[i].listObjectives == null)
            { 
            listTask.RemoveAt(i);
            }
        }
        

    }

   public void ControllerTrigger()
    {
        for (int i = 0; i > listTask.Count - 1; i++)
        {
            for (int j = listTask[i].listObjectives.Count - 1; i > 0; j--)
            {
                var  value = listTask[i].listObjectives[j];
               if(value.wasCompleteMision())
                {
                    //Audio.ReproductionSound(CompleteTask);
                    continue;
                }
               else
                {
                    ControllerTrigger();
                }
             
        }
            ControllerTrigger();
        }
    }


}
