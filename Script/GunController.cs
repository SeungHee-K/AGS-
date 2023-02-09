using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunController : MonoBehaviour
{
    [SerializeField]
    private Gun gun;

    private float currentFireRate;

    private AudioSource audio;

    private bool isReload;

    private RaycastHit hitInfo;

    public Camera cam;

    public GameObject[] Guns;
    public bool[] GunsBool;
    private bool isGunChage;
    private int ChageGun = 0;

    private bool Attacking;

    void Start()
    {
        audio = GetComponent<AudioSource>();
        cam = Camera.main;
        ChageGun = 0;
        GunsBool = Datamanager.instance.nowPlayer.Weapons;
        GunsBool[0] = true;
    }


    void Update()
    {
        gun = GameObject.FindObjectOfType<Gun>();


        GunFireRateCalc();
        //TryFire();

       //GunChange();

    }
    private void GunFireRateCalc()
    {
        if (currentFireRate > 0)
        {
            currentFireRate -= Time.deltaTime;
        }
    }

    public void AttackONDown()
    {
        if (currentFireRate <= 0 && !isReload)
        {
            Attacking = true;
            StartCoroutine(AttackON());
        }

    }
    public IEnumerator AttackON()
    {
        var rate = new WaitForSeconds(gun.fireRate);
        while (Attacking)
        {
            Fire();
            yield return rate;
        }
    }
    public void AttackONUP()
    {
        Attacking = false;
    }


    private void Fire()
    {
        if (!isReload)
        {
            if (gun.crrentBulletCount > 0)
            {
                Shoot();
            }
            else
            {
                StartCoroutine(Reload());
            }
        }
    }
    private void Shoot()
    {
        gun.crrentBulletCount--;
        currentFireRate = gun.fireRate;
        gun.muzzleFlash.Play();
        Hit();
        PlaySound(gun.fireSound);
    }
    private void PlaySound(AudioClip _clip)
    {
        audio.clip = _clip;
        audio.Play();
    }
    IEnumerator Reload()
    {
        if (gun.carryBulletCount > 0)
        {
            isReload = true;
            audio.clip = gun.reloadSound;
            audio.Play();
            gun.carryBulletCount += gun.crrentBulletCount;
            gun.crrentBulletCount = 0;
            yield return new WaitForSeconds(gun.reloadTime);

            if (gun.carryBulletCount >= gun.reloadBulletCount)
            {
                gun.crrentBulletCount = gun.reloadBulletCount;
                gun.carryBulletCount -= gun.reloadBulletCount;
            }
            else
            {
                gun.crrentBulletCount = gun.carryBulletCount;
                gun.carryBulletCount = 0;
            }
            isReload = false;
        }
    }
    private void Hit()
    {
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hitInfo, gun.range))
        {
            if (hitInfo.transform.tag == "Enemy")
            {
                Invoke("HitEffect", gun.hitTime);
                //¸ó½ºÅÍ ´ë¹ÌÁö
                Monster monster = hitInfo.transform.gameObject.GetComponent<Monster>();
                monster.Damage(gun.damage, hitInfo.transform);
            }
            if (hitInfo.transform.tag == "Item")
            {
                Invoke("HitEffect", gun.hitTime);
                hitInfo.transform.gameObject.GetComponent<Barret>().Death();


                gun.carryBulletCount += 30;
                if (gun.carryBulletCount>gun.maxBulletCount)
                {
                    gun.carryBulletCount = gun.maxBulletCount;
                }

            }
        }
    }
    private void HitEffect()
    {
        GameObject clone = Instantiate(gun.hitEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
        Destroy(clone, gun.hitEffectDuration);

    }

    public void GunChange_btn()
    {
        isGunChage = true;
        GunChange();
    }

    private void GunChange()
    {
        if (isGunChage && GunsBool[ChageGun])
        {
            GunSetactive();
            Guns[ChageGun].SetActive(true);

            ChageGun++;

            isGunChage = false;
            if (ChageGun > 3)
            {
                ChageGun = 0;
            }
        }
        else
        {
            ChageGun++;
            if (ChageGun > 3)
            {
                ChageGun = 0;
            }
            GunChange();
        }

    }
    public void GunSetactive()
    {
        for (int i = 0; i < Guns.Length; i++)
        {
            Guns[i].SetActive(false);
        }
    }
}
