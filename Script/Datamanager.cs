using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PlayerData
{
    public int level = 1;
    public int EXP = 0;
    public int[] MaxExp = { 100, 200, 300, 500, 800, 1300, 2100, 3400 };
    public int coin = 0;

    public bool[] Weapons = new bool[4];
    public bool missile=false;

    public int RegenHP = 0;
    public int MaxHP = 0;
    public int ATTUp = 0;
    public int GoldUp = 0; //골드 희득량 증가/
    public int ExpUp = 0;  //경험치 희득량 증가/
    public int PlusAmmo =0;

    public bool Gameclear = false;
}

public class Datamanager : MonoBehaviour
{
    public static Datamanager instance;

    public PlayerData nowPlayer = new PlayerData();

    public string path;
    public int nowSlot;

    public bool deleteReady;
    public int deletenum;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(instance.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);

        path = Application.persistentDataPath + "/save"; //저장 경로
        print(path);
    }

    public void SaveData()
    {
        string Playerdata = JsonUtility.ToJson(nowPlayer);
        File.WriteAllText(path + nowSlot.ToString(), Playerdata);

    }
    public void LoadData()
    {
        string Playerdata = File.ReadAllText(path + nowSlot.ToString());
        nowPlayer = JsonUtility.FromJson<PlayerData>(Playerdata);

    }

    public void DeleteData()
    {
        File.Delete(path + deletenum.ToString());
    }

    public void DataClear()
    {
        nowSlot = -1;
        nowPlayer = new PlayerData();
    }
}
