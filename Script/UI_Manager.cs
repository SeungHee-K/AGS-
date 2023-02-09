using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// UI ���� ��ũ��Ʈ

// ���� ���� �� ī��Ʈ �ٿ�
// �÷��̾� HP / EXP / COIN ǥ��
// �������� ��ų ī��Ʈ
// ������ / �������� �ؽ�Ʈ


public class UI_Manager : MonoBehaviour
{
    public GameObject CountDown; // ���� ���� ī��Ʈ�ٿ�
    public float Count = 6;

    public Slider HP;
    public Slider EXP;
    public Text Coin;
    public Text[] ScoreTime; // �÷���Ÿ�� ��:��

    public int MaxHP = 0;
    public int GoldUp = 0;
    public int ExpUp = 0;


    public Slider Skil_1; // ��������1 ��Ÿ��
    public float Skil_1_time;
    public Slider Skil_2; // ��������2 ��Ÿ��
    public float Skil_2_time;

    public GameObject[] Panel; // ������, ��������Ŭ����, �������� �г�
    public GameObject GameOver;// ���ӿ��� �г�
    public GameObject Setting_Panel;

    public MosterSpawner MosterSpawner;
    public Text Enemy_Count;
    public Text NowStage_text;
    public float Dead_count; // óġ ��
    public Text Dead_count_text;
    public Text Score;

    public float time;
    public Player player;

    public AudioSource audioSource;
    public Slider Sound; // ��������


    // ��ũ��Ʈ
    public GameManager gameManager;
    public Datamanager data;

    private void Awake()
    {
        CountDown.SetActive(true);
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<Player>();
        data = GameObject.FindObjectOfType<Datamanager>();

        audioSource = FindObjectOfType<AudioSource>();
        gameManager = FindObjectOfType<GameManager>();

        Panel[2].SetActive(false);
        Panel[0].SetActive(false);


        Skil_1_time = 0;
        Skil_2_time = 0;

        Invoke("Upgrade", 1f);
    }

    void Update()
    {
        HP.value = player.HP;
        EXP.value = player.EXP;
        Coin.text = player.Coin.ToString();
        Dead_count = gameManager.Dead_monster;
        Score.text = gameManager.BestScore.ToString();
        Dead_count_text.text = Dead_count.ToString();

        // ī��Ʈ�ٿ�

        if (CountDown.activeSelf) // ���� ���� �� 5�� ī��Ʈ
        {
            Count -= Time.deltaTime;
            CountDown.gameObject.GetComponent<Text>().text = ((int)Count).ToString();
        }       
        
        if (Count <= 0)
        {
            CountDown.SetActive(false);
            Time_on(); // ���� �÷���Ÿ�� ����

            //Debug.Log("Time : " + time.ToString());
        }
        


        // �÷��̾� ����

        if (player.HP <= 0) // HP = 0 ���ӿ��� + ���� �Ͻ�����
        {
            GameOver.SetActive(true);
            data.nowPlayer.coin += player.Coin;
            data.SaveData();
            Time.timeScale = 0; 
        }

        if (player.EXP >= data.nowPlayer.MaxExp[player.Level]) // EXP = 100 ������, EXP �ʱ�ȭ
        {
            Panel[0].SetActive(true);
            player.Level++;
            EXP.maxValue = data.nowPlayer.MaxExp[player.Level];
            player.EXP = 0;
            player.HP = HP.maxValue;

                      

            Invoke("Panel_x", 2f);
        }


        if (!GameManager.GameStart)
        {
            return;
        }

        if (Setting_Panel.activeSelf || GameOver.activeSelf) // ����â or ���ӿ��� �� �Ͻ�����
        {
            Time.timeScale = 0;

            if (Dead_count > gameManager.BestScore) // óġ �� > ��� = Best Score�� ���
            {
                gameManager.BestScore = Dead_count;
            }            
        }

        else
        {
            Time.timeScale = 1;
        }

    }
    public void StageClear()
    {
        if (gameManager.EnemyCount <= 0)
        {
            Panel[1].SetActive(true);

            Invoke("Panel_x", 2f);
        }
    }

    public void GameEnd()
    {
        Panel[1].SetActive(true);

    }

    public void Sound_Volume(string volume)
    {
        // ���� - �Ҹ�ũ�� ���� +- (��ư ����)

        if (volume == "up")
        {
            Sound.value ++;
            audioSource.volume += 0.1f;
        }

        if (volume == "down")
        {
            Sound.value --;
            audioSource.volume -= 0.1f;
        }
    }

    private void Time_on() // ���� �÷���Ÿ��
    {
        GameManager.GameStart = true;
        time += Time.deltaTime;
        ScoreTime[0].text = (((int)time / 60 % 60)).ToString();
        ScoreTime[1].text = ((int)time % 60).ToString();
    }

    private void Panel_x() // ��� Panel �ݱ�
    {
        for (int i = 0; i < Panel.Length; i++)
        {
            Panel[i].SetActive(false);
        }
    }

    public void SubSkil(int num)
    {
        // �������� ��Ÿ��

        if (num == 1 && Skil_1.value >= 10)
        {


            Skil_1_time = 0;
        }
        
        if (num == 2 && Skil_2.value >= 10)
        {


            Skil_2_time = 0; // ��ų ��� �� ��Ÿ�� �ʱ�ȭ
        }
        
    }

    private void Upgrade()
    {
        HP.maxValue = player.HP;
    }

}
