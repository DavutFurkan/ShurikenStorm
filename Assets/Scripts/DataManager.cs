using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataManager : MonoBehaviour
{
    
    public static DataManager Instance;

    private int shotBullet;
    public int totalShotBullet;
    private int enemyKilled;
    public int totalEnemyKilled;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Bu çaðrý yalnýzca ilk oluþturulan instance için yapýlýr.
        }
        else
        {
            Destroy(gameObject); // Bu çaðrý, fazladan oluþturulan instance'larý yok eder.
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int ShotBullet
    {
        get 
        { 
            return shotBullet; 
        }
        set
        { 
            shotBullet = value;
            GameObject.Find("ShotBulletText").GetComponent<Text>().text = "SHOT BULLET : " + shotBullet.ToString();
        }
    }

    public int EnemyKilled 
    {
        get 
        {
            return enemyKilled;
        }

        set
        {
            enemyKilled = value;
            GameObject.Find("EnemyKilledText").GetComponent<Text>().text = "ENEMY KILLED : " + enemyKilled.ToString();
        }
    }

}
