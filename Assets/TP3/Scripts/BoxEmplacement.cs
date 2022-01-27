using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxEmplacement : MonoBehaviour
{
    private GameManager gameManager;
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Sokobox"))
        {
            gameManager.AddBox();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Sokobox"))
        {
            gameManager.DeleteBox();
        }
    }
}
