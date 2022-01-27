using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UndoUI : MonoBehaviour
{
    private BoxManager boxManager;
    void Start()
    {
        boxManager = FindObjectOfType<BoxManager>();
    }
    public void UndoAction()
    {
        if (boxManager != null)
        {
            boxManager.UndoAction();
        }
    }

    public void Restart()
    {
        Debug.Log("Lumière ????");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
