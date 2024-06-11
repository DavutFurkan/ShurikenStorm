using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartMenu : MonoBehaviour
{
    public GameObject restartMenu;
    //bool isRestart;

    private void Awake()
    {
        //restartMenu.SetActive(false);
        //isRestart = false;
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadScene(int sceneIndex)
    {
        Time.timeScale = 1; 
        SceneManager.LoadScene(1); 
    }

    public void Quit()
    {
        SceneManager.LoadScene(0);  
    }
}