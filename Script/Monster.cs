using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    private GameObject Target;

    private Animator M_Ani;

    public M_Data Monsterdata;
    private Animator MonsterAnimator;
    private AudioSource MonsterAudio;

    public float monsterHP;
    private float monsterDamage;
    public float monsterSpeed;
    private float monsterExp;
    private float monstercoin;


    public bool isMonsterDie = false;
    public ParticleSystem hitEffect;

    public float AttackTime = 1.2f;
    public float LastAttackTime;

    public UI_Manager UImanager;
    public GameManager gamemanager;
    private Player player;

    private bool Idlebool = false;
    public bool Boss = false;
    void Start()
    {
        MonsterAnimator = GetComponent<Animator>();
        MonsterAudio = GetComponent<AudioSource>();
        UImanager = GameObject.Find("UI").GetComponent<UI_Manager>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        Target = GameObject.FindGameObjectWithTag("Player");
        gamemanager = GameObject.FindObjectOfType<GameManager>();
        MonsterSetUp(Monsterdata);
    }

    private void MonsterSetUp(M_Data monsterdata)
    {
        monsterHP = monsterdata.M_HP;
        monsterDamage = monsterdata.M_DMG;
        monsterSpeed = monsterdata.M_Speed;
        monsterExp = monsterdata.M_Exp;
        monstercoin = monsterdata.M_Coin;
        
    }


    public void Damage(float GunDMG, Transform hitPoint)
    {
        if (!isMonsterDie)
        {

            MonsterAnimator.SetTrigger("hit");
            //MonsterAudio.clip = Monsterdata.M_Hit_audio;
            //MonsterAudio.Play();

            monsterHP -= GunDMG;

            if (monsterHP <= 0)
            {
                player.EXP += (int)monsterExp;
                player.Coin += (int)monstercoin;
                isMonsterDie = true;
               // MonsterAudio.clip = Monsterdata.M_Die_audio;
               // MonsterAudio.Play();
                MonsterAnimator.SetTrigger("Die");
                BoxCollider monstercolider = this.GetComponent<BoxCollider>();
                monstercolider.enabled = false;
                UImanager.MosterSpawner.EnemyCount--;
                gamemanager.Dead_monster++; // ¸ó½ºÅÍ Ã³Ä¡ ¼ö

                Invoke("monsterDestroy", 2f);
                if (Boss)
                {
                    Datamanager.instance.nowPlayer.Gameclear = true;
                }
            }

        }
    }
    public void RangeDamage(float DMG)
    {
        if (!isMonsterDie)
        {
            MonsterAnimator.SetTrigger("hit");
            monsterHP -= DMG;
            if (monsterHP <= 0)
            {
                player.EXP += (int)monsterExp;
                player.Coin += (int)monstercoin;
                isMonsterDie = true;
                // MonsterAudio.clip = Monsterdata.M_Die_audio;
                // MonsterAudio.Play();
                MonsterAnimator.SetTrigger("Die");
                BoxCollider monstercolider = this.GetComponent<BoxCollider>();
                monstercolider.enabled = false;
                UImanager.MosterSpawner.EnemyCount--;
                
                Invoke("monsterDestroy", 2f);
            }

        }
    }
    private void monsterDestroy()
    {
        Destroy(gameObject);
    }


    private void OnCollisionStay(Collision collision)
    {
        if (isMonsterDie == false && Time.time >= LastAttackTime + AttackTime)
        {
            if (collision.gameObject.tag == "Player")
            {
                if (!Idlebool)
                {
                    Idlebool = false;
                    MonsterAnimator.SetBool("Idle", true);
                }


                LastAttackTime = Time.time;
                player = collision.gameObject.GetComponent<Player>();

                if (player != null)
                {
                    player.Damage(monsterDamage);
                    MonsterAnimator.SetTrigger("Attack");
                }
            }
        }
    }
    private void forWardMonster()
    {

        if (isMonsterDie == false)
        {
            this.transform.LookAt(Target.transform.position);

            this.transform.position += this.transform.forward * 0.3f * this.GetComponent<Monster>().monsterSpeed * Time.deltaTime;
        }

    }
    void Update()
    {
        if (GameManager.GameOver == false && GameManager.GameStart == true)
        {
            forWardMonster();
        }
    }
}
