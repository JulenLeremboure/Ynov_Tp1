using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenuUI : MonoBehaviour
{
    public GameObject MainMenuPanel;

    private void Start()
    {
        MainMenuPanel.SetActive(true);
    }

    public void PlayGame()
    {
        PlayButtonSound();
        SceneManager.LoadScene("OuhYeah");
    }

    public void QuitGame()
    {
        PlayButtonSound();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void BackToMenu()
    {
        PlayButtonSound();
        MainMenuPanel.SetActive(true);
    }

    private void PlayButtonSound()
    {
        //AudioManager.instance.PlayOneShot("UI_button");
    }
}
