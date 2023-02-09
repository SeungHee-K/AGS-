using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 플레이어 정보

public class Player : MonoBehaviour
{
    public int Level = 0;
    public float HP = 100;
    public float EXP = 0;
    public float ATK = 10;
    public float DEF = 5;
    public int Coin = 0;

    public Text GunName;
    public Text Ammo;
    public Text ATK_text;
    public Text DEF_text;

    public Gun gun;

    public string[] Weapons = new string[4];
    public int RegenHP = 0;
    public int MaxHP = 0;
    public int ATTUp = 0;

    public int GoldUp = 0; //골드 희득량 증가/
    public int ExpUp = 0;  //경험치 희득량 증가/


    void Start()
    {
        MaxHP = Datamanager.instance.nowPlayer.MaxHP;
        ExpUp = Datamanager.instance.nowPlayer.EXP;
        ATTUp = Datamanager.instance.nowPlayer.ATTUp;
        GoldUp = Datamanager.instance.nowPlayer.GoldUp;
        RegenHP = Datamanager.instance.nowPlayer.RegenHP;

        HP += MaxHP * 10;
        ATK += ATK * ATTUp *0.1f;
        EXP = 0;
        Coin = 0;
    }

    public void Damage(float Dmg)
    {
        HP-= Dmg;
    }

    void Update()
    {
        gun = GameObject.FindObjectOfType<Gun>();

        if (gun !=null)
        {
            ATK = gun.damage;
            GunName.text = "GUN " + gun.gunName;
            Ammo.text = "Ammo " + gun.crrentBulletCount + "/" + gun.carryBulletCount;
            ATK_text.text = "ATK " + ATK.ToString();
            DEF_text.text = "DEF " + DEF.ToString();

        }                  
       
    }
}
