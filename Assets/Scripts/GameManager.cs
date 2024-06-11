using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public List<GameObject> zombies;
    public WinMenu winMenu;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        winMenu = FindObjectOfType<WinMenu>();
    }

    public void RegisterZombie(GameObject zombie)
    {
        zombies.Add(zombie);
    }

    public void UnregisterZombie(GameObject zombie)
    {
        zombies.Remove(zombie);
        CheckWinCondition();
    }

    private void CheckWinCondition()
    {
        if (zombies.Count == 0)
        {
            winMenu.ShowWinMenu();
        }
    }
}