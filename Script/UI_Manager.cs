using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// UI 관련 스크립트

// 게임 시작 전 카운트 다운
// 플레이어 HP / EXP / COIN 표기
// 보조무기 스킬 쿨타임 Slider
// 레벨업 / 보스등장 텍스트

public class UI_Manager : MonoBehaviour
{
    public GameObject CountDown; // 게임 시작 카운트다운
    public float Count = 6; // 5초부터 표기

    public Slider HP;
    public Slider EXP;
    public Text Coin;
    public Text[] ScoreTime; // 플레이타임 분:초

    public int MaxHP = 0;
    public int GoldUp = 0;
    public int ExpUp = 0;

    public Slider Skil_1; // 보조무기1 쿨타임
    public float Skil_1_time;
    public Slider Skil_2; // 보조무기2 쿨타임
    public float Skil_2_time;

    public GameObject[] Panel; // 레벨업, 스테이지클리어, 보스등장 패널
    public GameObject GameOver;// 게임오버 패널
    public GameObject Setting_Panel; // 설정창
    
    public Text Enemy_Count; // 현재 몬스터 수
    public Text NowStage_text;
    public float Dead_count; // 처치 수
    public Text Dead_count_text;
    public Text Score;

    public float time;
    public Player player;

    public AudioSource audioSource;
    public Slider Sound; // 볼륨조절

    // 스크립트
    public GameManager gameManager;
    public Datamanager data;
    public MosterSpawner MosterSpawner;
    

    private void Awake() // 게임 시작 시 카운트다운패널 ON
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

        // 카운트다운

        if (CountDown.activeSelf) // 게임 시작 전 5초 카운트
        {
            Count -= Time.deltaTime;
            CountDown.gameObject.GetComponent<Text>().text = ((int)Count).ToString();
        }       
        
        if (Count <= 0)
        {
            CountDown.SetActive(false);
            Time_on(); // 게임 플레이타임 시작

            //Debug.Log("Time : " + time.ToString());
        }
        

        // 플레이어 스탯

        if (player.HP <= 0) // HP = 0 게임오버 + 게임 일시정지
        {
            GameOver.SetActive(true);
            data.nowPlayer.coin += player.Coin;
            data.SaveData();
            Time.timeScale = 0; 
        }

        if (player.EXP >= data.nowPlayer.MaxExp[player.Level]) // EXP = 100 레벨업, EXP 초기화
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

        if (Setting_Panel.activeSelf || GameOver.activeSelf) // 설정창 or 게임오버 시 일시정지
        {
            Time.timeScale = 0;

            if (Dead_count > gameManager.BestScore) // 처치 수 > 기록 = Best Score로 등록
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

    public void GameEnd() // 게임
    {
        Panel[1].SetActive(true);
    }

    public void Sound_Volume(string volume)
    {
        // 설정 - 소리크기 조절 +- (버튼 동작)

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

    private void Time_on() // 게임 플레이타임
    {
        GameManager.GameStart = true;
        time += Time.deltaTime;
        ScoreTime[0].text = (((int)time / 60 % 60)).ToString();
        ScoreTime[1].text = ((int)time % 60).ToString();
    }

    private void Panel_x() // 모든 Panel 닫기
    {
        for (int i = 0; i < Panel.Length; i++)
        {
            Panel[i].SetActive(false);
        }
    }

    public void SubSkil(int num)
    {
        // 보조무기 쿨타임

        if (num == 1 && Skil_1.value >= 10)
        {
            Skil_1_time = 0;
        }
        
        if (num == 2 && Skil_2.value >= 10)
        {
            Skil_2_time = 0; // 스킬 사용 후 쿨타임 초기화
        }        
    }

    private void Upgrade()
    {
        HP.maxValue = player.HP;
    }
}
