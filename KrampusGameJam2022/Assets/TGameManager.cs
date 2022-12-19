using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TGameManager : MonoBehaviour
{
    [SerializeField] public int ObjectBurning = 0;
    [SerializeField] public Text BurningObjectUi;
    // Start is called before the first frame update
    public void Awake()
    {
        ObjectBurning = 0;
    }

    public void burningChrismas()
    {
        ObjectBurning ++;
        BurningObjectUi.text = ObjectBurning.ToString();
    }
    private void Update()
    {
        WinTheGame();
    }
    public void WinTheGame()
    {
        if(ObjectBurning == 11)
        {
            BurningObjectUi.text = "Complete Game! Congratulatios :v";
        }
    }
}
