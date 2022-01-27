using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameObject[] boxEmplacements;
    private int numBoxesInEmplacement = 0;

    void Start()
    {
        boxEmplacements = GameObject.FindGameObjectsWithTag("BoxEmplacement");
    }

    public void AddBox()
    {
        numBoxesInEmplacement++;

        if(numBoxesInEmplacement >= boxEmplacements.Length)
        {
            Debug.Log("Gg j'ai toujours cru en toi");
        }
    }

    public void DeleteBox()
    {
        numBoxesInEmplacement--;
    }
}
