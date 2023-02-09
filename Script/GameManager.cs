using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public List<GameObject> Enemys = new List<GameObject>();
    public int EnemyCount;
    public bool isEnemy;

    public int Stage =0;
    public static bool GameOver = false;
    public static bool GameStart = false;

    public float Dead_monster;
    public float BestScore;
    

    // 스크립트
    public UI_Manager ui_manager;


    void Start()
    {
        GameOver = false;
        ui_manager = FindObjectOfType<UI_Manager>();
        isEnemy = false;
    }    

    void Update()
    {
        if (!GameStart) { return; }

        if (Datamanager.instance.nowPlayer.Gameclear == true)
        {
            ui_manager.GameEnd();
        }


        Debug.Log("적탐색");
        //EnemyCount = Enemys.Count;

        //if (EnemyCount <= 0)
        //{
        //    isEnemy = true;
            
        //    if (isEnemy)
        //    {
        //        foreach (GameObject Enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        //        {
        //            Enemys.Add(Enemy);
        //        }

        //        isEnemy = false;
        //    }
        //}
            




    }
    


    private void EnemySense()
    {
        foreach (GameObject Enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            Enemys.Add(Enemy);
        }

        isEnemy = false;
    }

    
}
