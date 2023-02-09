using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{       
    
    public string gunName; //총이름
    public float range; //사정거리
    
    public float fireRate;//연사속도
    public float reloadTime;//재장전 속도

    public int damage;//총의 데미지

    public int reloadBulletCount; // 총알 재장전 개수
    public int crrentBulletCount;//현재 탄알집에 남아있는 총알의 개수
    public int maxBulletCount;//최대 소유 가능 총알 개수
    public int carryBulletCount;//현재 소유하고 있는 총알 개수
        
   
    public ParticleSystem muzzleFlash;
    public GameObject hitEffect;
    public AudioClip fireSound;//총소리
    public AudioClip reloadSound;//재장전소리

    public float hitTime;//총에맞고 몇초후 이펙트
    public float hitEffectDuration;//힛이펙트 지속시간 

    private void Awake()
    {
        maxBulletCount += (int)(Datamanager.instance.nowPlayer.PlusAmmo * maxBulletCount * 0.1f);
        carryBulletCount = maxBulletCount;
        damage += (int)(Datamanager.instance.nowPlayer.ATTUp * damage * 0.1f);
    }
    private void Start()
    {
        muzzleFlash.Stop();


    }
}
