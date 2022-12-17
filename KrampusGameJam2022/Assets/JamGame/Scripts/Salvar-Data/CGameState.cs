using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization;

public class CGameState : ISerializable
{
    public Vector3 playerPosition;
    public float time;


    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        // Aquí debes serializar la información del estado del juego y guardarla en el objeto
        // info. Por ejemplo:
        info.AddValue("playerPosition", playerPosition);
        info.AddValue("time", time);
        //info.AddValue("playerInventory", playerInventory);
        //info.AddValue("playerGoals", playerGoals);
    }

    public CGameState(SerializationInfo info, StreamingContext context)
    {
        // Aquí debes recuperar la información del estado del juego desde el objeto info.
        // Por ejemplo:
        playerPosition = (Vector3)info.GetValue("playerPosition", typeof(Vector3));
        time = (float)info.GetValue("time", typeof(float));
        //playerInventory = (List<Item>)info.GetValue("playerInventory", typeof(List<Item>));
        //playerGoals = (List<Goal>)info.GetValue("playerGoals", typeof(List<Goal>));
    }
 }


