using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MosterSpawner : MonoBehaviour
{
    public GameObject[] MonsterObject;
    public GameObject Player;

    public GameObject[] Monsters;
    private int GameLevel = 0;

    public GameObject Boss;
    private GameObject _Boss;

    private bool isBoss = true;

    public GameManager gameManager;
    public GameObject BossPanel;

    public UI_Manager uiManager;

    public int EnemyCount=0;
    private int barrecount=0;

    public int[] StageMonsterCount = new int[9];

    public GameObject Barrets;
    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        EnemyCount = 0;

    }
    private void CreateMonster(Transform player, int Level)
    {
        Monsters = new GameObject[GameLevel+1];
        for (int i = 0; i < GameLevel+1; i++)
        {
            float RandomX = Random.Range(-15f, 15f);
            float RandomZ = Random.Range(-15f, 15f);

            if (RandomX < 10 && RandomX > -10f && RandomZ < 10f && RandomZ > -10f)
            {
                if (RandomX > 0)
                {
                    RandomX = Random.Range(10f, 15f);
                }
                else
                {
                    RandomX = Random.Range(-15f ,- 10f);
                }
                if (RandomZ > 0)
                {
                    RandomZ = Random.Range(10f, 15f);
                }
                else
                {
                    RandomZ = Random.Range( - 15f, -10f);
                }
            }

            Monsters[i] = Instantiate(MonsterObject[Level], new Vector3(player.position.x + RandomX, 0, player.position.z + RandomZ), Quaternion.identity);
            Monsters[i].transform.LookAt(player.position);
            EnemyCount++;
        }


    }
    private void Barretspawn(Transform player)
    {
        float RandomX = Random.Range(-12f, 12f);
        float RandomZ = Random.Range(-12f, 12f);

        if (RandomX < 8 && RandomX > -8f && RandomZ < 8f && RandomZ > -8f)
        {
            if (RandomX > 0)
            {
                RandomX = Random.Range(8f, 12f);
            }
            else
            {
                RandomX = Random.Range(-12f, -8f);
            }
            if (RandomZ > 0)
            {
                RandomZ = Random.Range(8f, 12f);
            }
            else
            {
                RandomZ = Random.Range(-12f, -8f);
            }
        }

        Instantiate(Barrets, new Vector3(player.position.x + RandomX, 0, player.position.z + RandomZ), Quaternion.identity);



    }
    private void forWardMonster()
    {
        for (int i = 0; i < Monsters.Length; i++)
        {
            if (Monsters[i] != null && Monsters[i].GetComponent<Monster>().isMonsterDie == false)
            {
                Monsters[i].transform.LookAt(Player.transform.position);

                Monsters[i].transform.position += Monsters[i].transform.forward * 0.3f * Monsters[i].GetComponent<Monster>().monsterSpeed * Time.deltaTime;
            }
        }
    }

    void Start()
    {
        // CreateMonster(Player.transform, GunGameMananger.Stage);
    }

    private void BossCrate(float time)
    {
        
       // gameManager.BossAudio.clip = gameManager.BossAudioClip[0];
       // gameManager.BossAudio.Play();
        StartCoroutine(BossInstace(time));

    }

    private IEnumerator BossInstace(float time)
    {
        float RandomX = Random.Range(-15f, 15f);
        float RandomZ = Random.Range(-15f, 15f);

        if (RandomX < 12f && RandomX > -15f && RandomZ < 12f && RandomZ > -15f)
        {
            if (RandomX > 0)
            {
                RandomX = Random.Range(12f, 15f);
            }
            else
            {
                RandomX = Random.Range(-15f ,- 12f);
            }
            if (RandomZ > 0)
            {
                RandomZ = Random.Range(12f, 15f);
            }
            else
            {
                RandomZ = Random.Range(-15f ,- 12f);
            }

            //보스 페널 서서히 나타나고 서서히 사라지게 고쳐야함
            for (int i = 0; i < 5; i++)
            {
                yield return new WaitForSeconds(0.3f);
                BossPanel.SetActive(true);
                yield return new WaitForSeconds(0.3f);
                BossPanel.SetActive(false);
            }

            yield return new WaitForSeconds(time);
            _Boss = Instantiate(Boss, new Vector3(Player.transform.position.x + RandomX, 0, Player.transform.position.z + RandomZ), Quaternion.identity);
            _Boss.transform.LookAt(Player.transform.position);
            EnemyCount++;
        }
    }
    void Update()
    {
        if (!isBoss) { return; }
        uiManager.Enemy_Count.text = EnemyCount.ToString();

        if (GameManager.GameOver == false&&GameManager.GameStart==true)
        {
            Debug.Log((int)(gameManager.ui_manager.time / 60 % 60));
            if ((int)(gameManager.ui_manager.time / 60 % 60) >= gameManager.Stage+1)
            {
                Barretspawn(Player.transform);
                gameManager.Stage++;
                uiManager.NowStage_text.text = " " + gameManager.Stage;
                barrecount = 0;
                GameLevel = 0;
            }
            else if ((int)(gameManager.ui_manager.time % 60) / 10 >= GameLevel)
            {
                
                if (gameManager.Stage < MonsterObject.Length)
                {
                    CreateMonster(Player.transform, gameManager.Stage);
                }
                else
                {
                    if (isBoss == true)
                    {
                        BossCrate(2f);
                        isBoss = false;
                    }
                }
                GameLevel++;
                
            }
            if ((int)(gameManager.ui_manager.time % 60) / 30 >= 1 && barrecount ==0)
            {
                barrecount++;
                Barretspawn(Player.transform);
            }
        }
    }
}
