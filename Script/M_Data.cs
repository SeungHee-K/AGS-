using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Scriptable/MonsterData", fileName = "MonsterData")]

public class M_Data : ScriptableObject
{
    public float M_HP =100;
    public float M_DMG =1;
    public float M_Speed = 1;
    public float M_Exp=5;
    public float M_Coin=5;

    public AudioClip M_Hit_audio;
    public AudioClip M_Die_audio;




    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
