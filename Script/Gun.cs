using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
   

    
    
    public string gunName; //���̸�
    public float range; //�����Ÿ�
    
    public float fireRate;//����ӵ�
    public float reloadTime;//������ �ӵ�

    public int damage;//���� ������

    public int reloadBulletCount; // �Ѿ� ������ ����
    public int crrentBulletCount;//���� ź������ �����ִ� �Ѿ��� ����
    public int maxBulletCount;//�ִ� ���� ���� �Ѿ� ����
    public int carryBulletCount;//���� �����ϰ� �ִ� �Ѿ� ����

    
    
   
    public ParticleSystem muzzleFlash;
    public GameObject hitEffect;
    public AudioClip fireSound;//�ѼҸ�
    public AudioClip reloadSound;//�������Ҹ�

    public float hitTime;//�ѿ��°� ������ ����Ʈ
    public float hitEffectDuration;//������Ʈ ���ӽð� 

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
