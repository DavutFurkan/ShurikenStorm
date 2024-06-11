using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinMenu : MonoBehaviour
{
    public GameObject winMenu;

    private void Awake()
    {
        winMenu.SetActive(false); 
    }

    public void ShowWinMenu()
    {
        Time.timeScale = 0;
        winMenu.SetActive(true);  
    }

    public void Restart()
    {
        Time.timeScale = 1; 
        SceneManager.LoadScene(1); 
    }

    public void Quit()
    {
        SceneManager.LoadScene(0); 
    }
}